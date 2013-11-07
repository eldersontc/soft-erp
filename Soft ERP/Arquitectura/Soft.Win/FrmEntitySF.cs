using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;
using Soft.Configuracion.Entidades;
using System.Xml;
using Soft.DataAccess;
using Infragistics.Win.UltraWinGrid;

namespace Soft.Win
{
    public partial class FrmEntitySF : FrmParent
    {
        public FrmEntitySF()
        {
            InitializeComponent();
        }

        const String colNombre = "Nombre";
        const String colCampo = "Campo";
        const String colPropiedad = "Propiedad";
        const String colObligatorio = "Obligatorio";

        public EntidadSF EntidadSF { get { return (EntidadSF)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCampo);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colPropiedad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colObligatorio);
            column.DataType = typeof(Boolean);

            ugAtributos.DataSource = columns;
            MapKeys(ref ugAtributos);
        }

        public void Mostrar()
        {
            if (EntidadSF.EnsambladoClase != null) { ssEnsambladoClase.Text = EntidadSF.EnsambladoClase.Nombre; }
            if (EntidadSF.EnsambladoFormulario != null) { ssEnsambladoFormulario.Text = EntidadSF.EnsambladoFormulario.Nombre; }
            txtNombreClase.Text = EntidadSF.NombreClase;
            txtNombreFormulario.Text = EntidadSF.NombreFormulario;
            ssTabla.Text = EntidadSF.Tabla;
            MostrarAtributos();
        }

        public void MostrarAtributos()
        {
            ugAtributos.Selected.Rows.AddRange((UltraGridRow[])ugAtributos.Rows.All);
            ugAtributos.DeleteSelectedRows(false);
            foreach (AtributoSF Item in EntidadSF.Atributos)
            {
                UltraGridRow Row = ugAtributos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarAtributo(Row);
            }
        }

        public void ContruirAtributos(String Tabla) {
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_ColumnasxTabla", String.Format(" TABLE_NAME = '{0}'", Tabla));
            if (XML.HasChildNodes)
            {
                EntidadSF.Tabla = Tabla;
                EntidadSF.Atributos.Clear();
                ugAtributos.Selected.Rows.AddRange((UltraGridRow[])ugAtributos.Rows.All);
                ugAtributos.DeleteSelectedRows(false);
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    AtributoSF atributo = new AtributoSF();
                    atributo.Nombre = NodoItem.SelectSingleNode("@COLUMN_NAME").Value;
                    atributo.Campo = NodoItem.SelectSingleNode("@COLUMN_NAME").Value;
                    atributo.Propiedad = NodoItem.SelectSingleNode("@COLUMN_NAME").Value;
                    UltraGridRow Row = ugAtributos.DisplayLayout.Bands[0].AddNew();
                    Row.Tag = atributo;
                    EntidadSF.Atributos.Add(atributo);
                    MostrarAtributo(Row);
                }
            }
        }

        public void MostrarAtributo(UltraGridRow Row)
        {
            AtributoSF atributo = (AtributoSF)Row.Tag;
            Row.Cells[colNombre].Value = atributo.Nombre;
            Row.Cells[colCampo].Value = atributo.Campo;
            Row.Cells[colPropiedad].Value = atributo.Propiedad;
            Row.Cells[colObligatorio].Value = atributo.Obligatorio;
        }

        private void ssEnsambladoClase_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            EntidadSF.EnsambladoClase = (Ensamblado)FrmSeleccionar.GetSelectedEntity(typeof(Ensamblado), "Ensamblado");
            this.Mostrar();
        }

        private void txtNombreClase_TextChanged(object sender, EventArgs e)
        {
            EntidadSF.NombreClase = txtNombreClase.Text;
        }

        private void ssEnsambladoFormulario_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            EntidadSF.EnsambladoFormulario = (Ensamblado)FrmSeleccionar.GetSelectedEntity(typeof(Ensamblado), "Ensamblado");
            this.Mostrar();
        }

        private void txtNombreFormulario_TextChanged(object sender, EventArgs e)
        {
            EntidadSF.NombreFormulario = txtNombreFormulario.Text;
        }

        private void ssTabla_Search(object sender, EventArgs e)
        {
            ContruirAtributos(ssTabla.Text);
        }

        private void ugAtributos_CellChange(object sender, CellEventArgs e)
        {
            AtributoSF atributo = (AtributoSF)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colNombre:
                    atributo.Nombre = Convert.ToString(e.Cell.Text);
                    break;
                case colPropiedad:
                    atributo.Propiedad = Convert.ToString(e.Cell.Text);
                    break;
                case colObligatorio:
                    atributo.Obligatorio = Convert.ToBoolean(e.Cell.Text);
                    break;
                default:
                    break;
            }
        }

    }
}
