using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Soft.Entities;
using System.Xml;
using Soft.Win;
using System.Reflection;
using Microsoft.VisualBasic;
using Soft.DataAccess;
using Soft.Exceptions;
using Soft.Configuracion.Entidades;
using Soft.Exceptions;

namespace Soft.Win
{
    public partial class FrmSelectedEntity : FrmParent 
    {

        private EntidadSF mEntidadSF = null;
        private Boolean mAceptar = false;

        public FrmSelectedEntity()
        {
            InitializeComponent();
        }

        public Parent GetSelectedEntity(Type Tipo,String NombrePanel, String Filtro = "")
        {
            try
            {
                Parent EntidadSeleccionada = null;
                ConfigurarColumnas(NombrePanel, Filtro);
                if (ugEntity.Rows.Count == 1)
                {
                    String ID = ugEntity.Rows[0].Cells["ID"].Value.ToString();
                    EntidadSeleccionada = HelperNHibernate.GetEntityByID(mEntidadSF.NombreClase, ID);
                    return EntidadSeleccionada;
                }
                else {
                    ShowDialog();
                }
                if (mAceptar & ugEntity.ActiveRow != null && !ugEntity.ActiveRow.IsFilterRow) {
                    String ID = ugEntity.ActiveRow.Cells["ID"].Value.ToString();
                    EntidadSeleccionada = HelperNHibernate.GetEntityByID(mEntidadSF.NombreClase, ID);
                }
                return EntidadSeleccionada;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
                return null;
            }
        }

        public Collection GetSelectedsEntities(Type Tipo, String NombrePanel, String Filtro = "")
        {
            try
            {
                Collection ColRetorno = new Collection();
                ConfigurarColumnas(NombrePanel, Filtro);
                ActivarMultiSeleccion();
                if (ugEntity.Rows.Count == 1)
                {
                    String ID = ugEntity.Rows[0].Cells["ID"].Value.ToString();
                    Parent Entidad = HelperNHibernate.GetEntityByID(mEntidadSF.NombreClase, ID);
                    ColRetorno.Add(Entidad);
                    return ColRetorno;
                }
                else {
                    ShowDialog();
                }
                if (mAceptar) {
                    foreach (UltraGridRow Row in ugEntity.Rows)
                    {
                        if (Convert.ToBoolean(Row.Cells["Select"].Value)) {
                            String ID = Row.Cells["ID"].Value.ToString();
                            Parent Entidad = HelperNHibernate.GetEntityByID(mEntidadSF.NombreClase, ID);
                            ColRetorno.Add(Entidad);
                        }
                    }
                }
                return ColRetorno;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
                return null;
            }
        }

        public void ConfigurarColumnas(String NombrePanel, String Filtro)
        {
            String ConsultaSQL = String.Empty;
            String Ordenamiento = String.Empty;
            UltraGridBand Band = ugEntity.DisplayLayout.Bands[0];
            Soft.Configuracion.Entidades.Panel Panel = (Soft.Configuracion.Entidades.Panel)HelperNHibernate.GetEntityByField("Panel", "Nombre", NombrePanel);
            mEntidadSF = Panel.EntidadSF;
            foreach (ColumnaPanel Columna in Panel.Columnas)
            {
                UltraGridColumn Column = Band.Columns.Add(Columna.CampoSQL);
                Column.CellActivation = Activation.NoEdit;
                Column.Header.Caption = Columna.Nombre;
                Column.Width = Columna.Ancho;
                Column.Hidden = !Columna.Visible;
                if (Columna.Indice) { Ordenamiento = String.Format("ORDER BY {0}", Columna.CampoSQL); }
            }
            if (Filtro.Length > 0) { Filtro = String.Format(" WHERE {0} ", Filtro); }
            ConsultaSQL = String.Format("SELECT * FROM {0} {1} {2}", Panel.NombreVista, Filtro, Ordenamiento);
            ugEntity.DataSource = HelperNHibernate.GetDataSet(ConsultaSQL);
        }

        public void ActivarMultiSeleccion() {
            UltraGridColumn column = ugEntity.DisplayLayout.Bands[0].Columns.Add("Select", "");
            column.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            column.CellActivation = Activation.AllowEdit;
            column.DataType = System.Type.GetType("System.Boolean");
            column.Header.VisiblePosition = 0;
            column.Width = 50;
            uceTodos.Visible = true;
        }

        private void uceTodos_CheckedChanged(object sender, EventArgs e)
        {
            foreach (UltraGridRow Row in ugEntity.Rows)
            {
                Row.Cells["Select"].Value = !Convert.ToBoolean(Row.Cells["Select"].Value);
            }
        }

        private void ugEntity_ClickCell(object sender, ClickCellEventArgs e)
        {
            switch (e.Cell.Column.Key)
            {
                case "Select":
                    e.Cell.Value = !Convert.ToBoolean(e.Cell.Value);
                    break;
                default: break;
            }
        }

        public override void Cancelar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

        private void ubSelecionar_Click(object sender, EventArgs e)
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            mAceptar = true;
            Close();
        }

        private void ugEntity_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            mAceptar = true;
            Close();
        }

        private void ugEntity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        base.m_ResultProcess = EnumResult.SUCESS;
                        mAceptar = true;
                        Close();
                        break;
                    case Keys.Escape:
                        base.m_ResultProcess = EnumResult.SUCESS;
                        mAceptar = false;
                        Close();
                        break;
                    //case Keys.Space:
                    //    ChangeStateCheck();
                    //    break;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        public void CambiarEstadoCheck()
        {
            if (ugEntity.ActiveRow != null && ugEntity.ActiveRow.Cells.Exists("Select")) {
                ugEntity.ActiveRow.Cells["Select"].Value = !Convert.ToBoolean(ugEntity.ActiveRow.Cells["Select"].Value);
            }
        }

    }
}
