using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Configuracion.Entidades;
using Soft.DataAccess;
using System.Xml;
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Soft.Entities;
using Infragistics.Win;

namespace Soft.Configuracion.Win
{
    public partial class FrmPanel : FrmParent 
    {
        public FrmPanel()
        {
            InitializeComponent();
        }

        private Boolean UpdateUI = false;

        const String colNombre = "Nombre";
        const String colCampo = "Campo";
        const String colAncho = "Ancho";
        const String colVisible = "Visible";
        const String colEstilo = "Estilo";
        const String colPropiedad = "Propiedad";
        const String colEstablecer = "Establecer";
        const String colDescripcion = "Descripción";
        const String colOrden = "#";

        public Soft.Configuracion.Entidades.Panel Panel { get { return (Soft.Configuracion.Entidades.Panel)base.m_ObjectFlow; } }

        #region Metodos

        public override void Init() {
            InitGrids();
            Mostrar();
        }

        public void InitGrids() {

            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colOrden);
            column.DataType = typeof(String);
            
            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCampo);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colPropiedad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colEstablecer);
            column.DataType = typeof(Boolean);

            column = columns.Columns.Add(colVisible);
            column.DataType = typeof(Boolean);

            column = columns.Columns.Add(colAncho);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colEstilo);
            column.DataType = typeof(String);

            ugColumnas.DataSource = columns;
            ugColumnas.DisplayLayout.Bands[0].Columns[colOrden].Width = 50;
            ugColumnas.DisplayLayout.Bands[0].Columns[colAncho].Width = 50;
            ugColumnas.DisplayLayout.Bands[0].Columns[colEstilo].Width = 80;

            ValueList List = new ValueList();
            List.ValueListItems.Add("Default");
            List.ValueListItems.Add("String");
            List.ValueListItems.Add("Int");
            List.ValueListItems.Add("Decimal");
            List.ValueListItems.Add("Boolean");

            ugColumnas.DisplayLayout.Bands[0].Columns[colEstilo].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            ugColumnas.DisplayLayout.Bands[0].Columns[colEstilo].ValueList = List;
        }

        public void Mostrar() {
            this.UpdateUI = true;
            txtNombre.Text = Panel.Nombre;
            ssEntidadSF.Text = (Panel.EntidadSF != null) ? Panel.EntidadSF.NombreClase : "";
            ssVistaSQL.Text = this.Panel.NombreVista;
            this.MostrarColumnas();
            this.UpdateUI = false;
        }

        public void MostrarColumnas() {
            ugColumnas.Selected.Rows.AddRange((UltraGridRow[])ugColumnas.Rows.All);
            ugColumnas.DeleteSelectedRows(false);
            foreach (ColumnaPanel Item in Panel.Columnas)
            {
                UltraGridRow Row = ugColumnas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarColumna(Row);
            }
        }

        public void MostrarColumna(UltraGridRow Row) {
            ColumnaPanel columna = (ColumnaPanel)Row.Tag;
            columna.Orden = Row.Index + 1;
            Row.Cells[colNombre].Value = columna.Nombre;
            Row.Cells[colCampo].Value = columna.CampoSQL;
            Row.Cells[colAncho].Value = columna.Ancho;
            Row.Cells[colVisible].Value = columna.Visible;
            Row.Cells[colEstilo].Value = columna.Estilo;
            Row.Cells[colPropiedad].Value = columna.Propiedad;
            Row.Cells[colEstablecer].Value = columna.Establecer;
            Row.Cells[colOrden].Value = columna.Orden;
        }

        public void ConstruirColumnas(XmlDocument XML) {
            this.Panel.Columnas.Clear();
            if (XML.HasChildNodes) {
                ugColumnas.Selected.Rows.AddRange((UltraGridRow[])ugColumnas.Rows.All);
                ugColumnas.DeleteSelectedRows(false);
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    ColumnaPanel columna = new ColumnaPanel();
                    UltraGridRow Row = ugColumnas.DisplayLayout.Bands[0].AddNew();
                    columna.Nombre = NodoItem.SelectSingleNode("@COLUMN_NAME").Value;
                    columna.CampoSQL = NodoItem.SelectSingleNode("@COLUMN_NAME").Value;
                    columna.Estilo = "Default";
                    columna.Visible = true;
                    columna.Ancho = 0;
                    columna.Orden = Row.Index; 
                    Row.Tag = columna;
                    Panel.Columnas.Add(columna);
                    MostrarColumna(Row);
                }   
            }
        }

        #endregion

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Panel.Nombre = txtNombre.Text;
        }

        private void ugColumnas_CellChange(object sender, CellEventArgs e)
        {
            ColumnaPanel columna = (ColumnaPanel)e.Cell.Row.Tag;
            switch (e.Cell .Column.Key)
            {
                case colNombre :
                    columna.Nombre = Convert.ToString(e.Cell.Text);
                    break;
                case colVisible:
                    columna.Visible = Convert.ToBoolean(e.Cell.Text);
                    break;
                case colPropiedad:
                    columna.Propiedad = Convert.ToString(e.Cell.Text);
                    break;
                case colEstablecer:
                    columna.Establecer = Convert.ToBoolean(e.Cell.Text);
                    break;
                case colAncho:
                    columna.Ancho = Convert.ToInt32(e.Cell.Text);
                    break;
                case colEstilo:
                    columna.Estilo = e.Cell.Column.ValueList.GetValue(e.Cell.Column.ValueList.SelectedItemIndex).ToString();
                    break;
                case colOrden:
                    columna.Orden = Convert.ToInt32(e.Cell.Text);
                    break;
                default:
                    break;
            }
        }

        private void ssVistaSQL_Search(object sender, EventArgs e)
        {
            Panel.NombreVista = ssVistaSQL.Text;
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_ColumnasxVista", String.Format(" TABLE_NAME = '{0}'", ssVistaSQL.Text));
            ConstruirColumnas(XML);
        }

        private void ssEntidadSF_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Panel.EntidadSF = (EntidadSF)FrmSeleccionar.GetSelectedEntity(typeof(EntidadSF), "EntidadSF");
            Mostrar();
        }

        private void ugSubir_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugColumnas.ActiveRow;
            if (Row != null && Row.Index > 0)
            {
                ColumnaPanel Columna = (ColumnaPanel)Row.Tag;
                Int32 Indice = Row.Index - 1;
                Panel.Columnas.RemoveAt(Indice + 1);
                Panel.Columnas.Insert(Indice, Columna);
                MostrarColumnas();
                ugColumnas.Rows[Columna.Orden - 1].Activated = true;
            }
        }

        private void ubBajar_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugColumnas.ActiveRow;
            if (Row != null && Row.Index < ugColumnas.Rows.Count - 1)
            {
                ColumnaPanel Columna = (ColumnaPanel)Row.Tag;
                Int32 Indice = Row.Index + 1;
                Panel.Columnas.RemoveAt(Indice - 1);
                Panel.Columnas.Insert(Indice,Columna);
                MostrarColumnas();
                ugColumnas.Rows[Columna.Orden -1].Activated = true;
            }
        }

    }
}
