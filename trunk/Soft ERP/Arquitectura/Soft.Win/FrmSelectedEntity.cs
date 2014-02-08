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
using Soft.Configuracion.Entidades;

namespace Soft.Win
{
    public partial class FrmSelectedEntity : FrmParent 
    {

        private XmlDocument XMLCofiguration;
        private Boolean SWAcept = false;

        public FrmSelectedEntity()
        {
            InitializeComponent();
        }

        public Parent GetSelectedEntity(Type Type,String NamePanel, String Filter = "")
        {
            try
            {
                Parent SelectedEntity = null;
                ConfigureColumns(NamePanel, Filter);
                if (ugEntity.Rows.Count == 1)
                {
                    SelectedEntity = AsignValuesToParent(Type, ugEntity.Rows[0]);
                    return SelectedEntity;
                }
                else {
                    ShowDialog();
                }
                if (SWAcept & ugEntity.ActiveRow != null && !ugEntity.ActiveRow.IsFilterRow) { 
                    SelectedEntity = AsignValuesToParent(Type, ugEntity.ActiveRow); 
                }
                return SelectedEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Collection GetSelectedsEntities(Type Type, String NamePanel, String Filter = "")
        {
            try
            {
                Collection ColReturn = new Collection();
                ConfigureColumns(NamePanel, Filter);
                ActivarMultiSeleccion();
                if (ugEntity.Rows.Count == 1)
                {
                    Parent Entity = AsignValuesToParent(Type, ugEntity.Rows[0]);
                    ColReturn.Add(Entity);
                    return ColReturn;
                }
                else {
                    ShowDialog();
                }
                if (SWAcept) {
                    foreach (UltraGridRow Row in ugEntity.Rows)
                    {
                        if (Convert.ToBoolean(Row.Cells["Select"].Value)) {
                            Parent Entity = AsignValuesToParent(Type, Row);
                            ColReturn.Add(Entity);
                        }
                    }
                }
                return ColReturn;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Parent AsignValuesToParent(Type Type,UltraGridRow Row)
        {
            Parent Entity = Cast(Activator.CreateInstance(Type), Type); 
            foreach (XmlNode NodoItem in XMLCofiguration.DocumentElement.ChildNodes)
            {
                if (NodoItem.SelectSingleNode("@Establecer").Value == "1")
                {
                    PropertyInfo pInfo = Type.GetProperty(NodoItem.SelectSingleNode("@Propiedad").Value);
                    if (pInfo != null)
                    {
                        pInfo.SetValue(Entity, Row.Cells[NodoItem.SelectSingleNode("@CampoSQL").Value].Value, null);
                    }
                    else
                    {
                        throw new Exception(String.Format("Propiedad no encontrada : {0}", NodoItem.SelectSingleNode("@Propiedad").Value));
                    }
                }
            }
            return Entity;
        }

        public static dynamic Cast(dynamic obj, Type castTo)
        {
            return Convert.ChangeType(obj, castTo);
        }

        public void ConfigureColumns(String NombrePanel, String Filtro)
        {
            String ConsultaSQL = String.Empty;
            String Ordenamiento = String.Empty;
            UltraGridBand Band = ugEntity.DisplayLayout.Bands[0];
            Soft.Configuracion.Entidades.Panel Panel = (Soft.Configuracion.Entidades.Panel)HelperNHibernate.GetEntityByField("Panel", "Nombre", NombrePanel);
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
            SWAcept = true;
            Close();
        }

        private void ugEntity_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            SWAcept = true;
            Close();
        }

        private void ugEntity_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    base.m_ResultProcess = EnumResult.SUCESS;
                    SWAcept = true;
                    Close();
                    break;
                case Keys.Escape :
                    base.m_ResultProcess = EnumResult.SUCESS;
                    SWAcept = false;
                    Close();
                    break;
                //case Keys.Space :
                //    ChangeStateCheck();
                //    break;
            }
        }

        public void ChangeStateCheck()
        {
            if (ugEntity.ActiveRow != null && ugEntity.ActiveRow.Cells.Exists("Select")) {
                ugEntity.ActiveRow.Cells["Select"].Value = !Convert.ToBoolean(ugEntity.ActiveRow.Cells["Select"].Value);
            }
        }

    }
}
