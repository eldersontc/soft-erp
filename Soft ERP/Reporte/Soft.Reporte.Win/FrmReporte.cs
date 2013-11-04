using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Reporte.Entidades;
using Infragistics.Win.UltraWinGrid;
using Soft.Entities;
using Infragistics.Win;

namespace Soft.Reporte.Win
{
    public partial class FrmReporte : FrmParent
    {
        public FrmReporte()
        {
            InitializeComponent();
        }

        public Soft.Reporte.Entidades.Reporte Reporte { get { return (Soft.Reporte.Entidades.Reporte)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }


        const String colTipo = "Tipo";
        const String colNombre = "Nombre";
        const String colPropiedad = "Propiedad";
        const String colValor = "Valor";

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colTipo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colPropiedad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colValor);
            column.DataType = typeof(String);

            ugParametros.DataSource = columns;

            ValueList List = new ValueList();
            List.ValueListItems.Add("Propiedad");
            List.ValueListItems.Add("String");
            List.ValueListItems.Add("Int");
            List.ValueListItems.Add("Decimal");
            List.ValueListItems.Add("Boolean");
            List.ValueListItems.Add("DateTime");

            ugParametros.DisplayLayout.Bands[0].Columns[colTipo].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            ugParametros.DisplayLayout.Bands[0].Columns[colTipo].ValueList = List;
            ugParametros.DisplayLayout.Bands[0].Columns[colPropiedad].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            ugParametros.DisplayLayout.Bands[0].Columns[colPropiedad].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            MapKeys(ref ugParametros);
        }

        public void Mostrar()
        {
            txtCodigo.Text = Reporte.Codigo;
            txtNombre.Text = Reporte.Nombre;
            txtSQL.Text = Reporte.SQL;
            uceActivo.Checked = Reporte.Activo;
            ssUbicacion.Text = Reporte.Ubicacion;
            uceEspecifico.Checked = Reporte.Especifico;
            ssEntidadSF.Text = (Reporte.EntidadSF != null) ? Reporte.EntidadSF.NombreClase : "";
            MostrarParametros();
        }


        public void MostrarParametros()
        {
            base.ClearAllRows(ref ugParametros);
            foreach (ParametroReporte Item in Reporte.Parametros)
            {
                UltraGridRow Row = ugParametros.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarParametro(Row);
            }
        }

        public void MostrarParametro(UltraGridRow Row)
        {
            ParametroReporte Item = (ParametroReporte)Row.Tag;
            Row.Cells[colPropiedad].Activation = (Item.Tipo.Equals("Propiedad")) ? Activation.AllowEdit: Activation.NoEdit;
            Row.Cells[colNombre].Value = Item.Nombre;
            Row.Cells[colTipo].Value = Item.Tipo;
            Row.Cells[colPropiedad].Value = Item.Propiedad;
            Row.Cells[colValor].Value = Item.Valor;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Reporte.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Reporte.Nombre = txtNombre.Text;
        }

        private void ssUbicacion_Search(object sender, EventArgs e)
        {
            OpenFileDialog fop = new OpenFileDialog();
            fop.InitialDirectory = @"C:\";
            fop.Filter = "[Rpt]|*.rpt";
            if (fop.ShowDialog() == DialogResult.OK)
            {
                Reporte.Ubicacion = fop.FileName;
                ssUbicacion.Text = Reporte.Ubicacion;
            }
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Reporte.Activo = uceActivo.Checked;
        }

        private void uceEspecifico_CheckedChanged(object sender, EventArgs e)
        {
            Reporte.Especifico = uceEspecifico.Checked;
            if (Reporte.Especifico)
            {
                ssEntidadSF.Enabled = true;
            }
            else {
                Reporte.EntidadSF = null;
                ssEntidadSF.Enabled = false;
                ssEntidadSF.Text = "";
            }
        }

        private void ssEntidadSF_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarEntidadSF = new FrmSelectedEntity();
            Reporte.EntidadSF = (EntidadSF)FrmSeleccionarEntidadSF.GetSelectedEntity(typeof(EntidadSF), "EntidadSF");
            ssEntidadSF.Text = (Reporte.EntidadSF != null) ? Reporte.EntidadSF.NombreClase : "";
        }

        private void txtSQL_TextChanged(object sender, EventArgs e)
        {
            Reporte.SQL = txtSQL.Text;
        }

        private void ubNuevoParametro_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugParametros.DisplayLayout.Bands[0].AddNew();
            Row.Tag = Reporte.AddParametro();
        }

        private void ubEliminarParametro_Click(object sender, EventArgs e)
        {
            if (ugParametros.ActiveRow == null) { return; }
            Reporte.Parametros.Remove((ParametroReporte)ugParametros.ActiveRow.Tag);
            ugParametros.ActiveRow.Delete(false);
        }

        private void ugParametros_CellChange(object sender, CellEventArgs e)
        {
            ParametroReporte Parametro = (ParametroReporte)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colNombre:
                    Parametro.Nombre = Convert.ToString(e.Cell.Text);
                    break;
                case colTipo:
                    Parametro.Tipo = e.Cell.Column.ValueList.GetValue(e.Cell.Column.ValueList.SelectedItemIndex).ToString();
                    break;
                case colValor:
                    Parametro.Valor = Convert.ToString(e.Cell.Text);
                    break;
                default:
                    break;
            }
            MostrarParametro(e.Cell.Row);
        }

        private void ugParametros_ClickCellButton(object sender, CellEventArgs e)
        {
            try
            {
                if (Reporte.EntidadSF == null) { throw new Exception("Seleccionar la Entidad SF ..."); }
                ParametroReporte Parametro = (ParametroReporte)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colPropiedad:
                        FrmSelectedProperty SelecionarPropiedad = new FrmSelectedProperty();
                        Parametro.Propiedad = SelecionarPropiedad.GetSeletedProperty(Reporte.EntidadSF.ID);
                        break;
                    default:
                        break;
                }
                MostrarParametro(e.Cell.Row);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
