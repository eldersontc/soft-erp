using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Ventas.Entidades;
using Infragistics.Win.UltraWinGrid;
using Soft.Inventario.Entidades;
using Soft.DataAccess;
using Infragistics.Win;

namespace Soft.Ventas.Win
{
    public partial class FrmPlantilla : FrmParent 
    {
        public FrmPlantilla()
        {
            InitializeComponent();
        }

        //Constantes
        const String colExitencia = "Existencia";
        const String colUnidad = "Unidad";
        const String colCantidad = "Cantidad";

        public Plantilla Plantilla { get { return (Plantilla)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrid();
            Mostrar();
        }

        public void InitGrid()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();
            
            column = columns.Columns.Add(colExitencia);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCantidad);
            column.DataType = typeof(int);

            ugExistencias.DataSource = columns;
        }

        public void Mostrar()
        {
            txtCodigo.Text = Plantilla.Codigo;
            txtNombre.Text = Plantilla.Nombre;
            uceActivo.Checked = Plantilla.Activo;
            MostrarItems();
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugExistencias);
            foreach (ItemPlantilla Item in Plantilla.Items)
            {
                UltraGridRow Row = ugExistencias.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemPlantilla Item = (ItemPlantilla)Row.Tag;
            if (Item.Existencia != null) {
                AgregarUnidades(Row);
                Row.Cells[colExitencia].Value = Item.Existencia.Nombre;
                Row.Cells[colUnidad].Value = Item.Unidad.Nombre;    
            }
            Row.Cells[colCantidad].Value = Item.Cantidad;
        }

        public void AgregarUnidades(UltraGridRow Row)
        {
            ItemPlantilla Item = (ItemPlantilla)Row.Tag;
            ValueList List = new ValueList();
            foreach (ExistenciaUnidad Unidad in Item.Existencia.Unidades)
            {
                if (Item.Unidad == null & Unidad.EsUnidadBase) { Item.Unidad = Unidad.Unidad; }
                List.ValueListItems.Add(Unidad.Unidad, Unidad.Unidad.Nombre);
            }
            Row.Cells[colUnidad].ValueList = List;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Plantilla.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Plantilla.Nombre = txtNombre.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Plantilla.Activo = uceActivo.Checked;
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String Filtro = "ID NOT IN (";
            String IDs = "";
            foreach (ItemPlantilla Item in Plantilla.Items)
            {
                IDs = IDs + "'" + Item.Existencia.ID + "',";
            }
            Filtro = (IDs.Length > 0) ? Filtro + IDs.Substring(0, IDs.Length - 1) + ")" : "";
            Existencia Existencia = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", Filtro);
            if (Existencia != null)
            {
                Existencia ExistenciaCompleta = (Existencia)HelperNHibernate.GetEntityByID("Existencia",Existencia.ID);
                UltraGridRow Row = ugExistencias.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Plantilla.CrearItem(ExistenciaCompleta);
                MostrarItem(Row);
            }
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugExistencias.ActiveRow == null) { return; }
            Plantilla.Items.Remove((ItemPlantilla)ugExistencias.ActiveRow.Tag);
            ugExistencias.ActiveRow.Delete(false);
        }

        private void ugExistencias_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            ItemPlantilla Item = (ItemPlantilla)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colUnidad:
                    Item.Unidad = (Unidad)e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex);
                    break;
                case colCantidad:
                    Item.Cantidad = Convert.ToDecimal(e.Cell.Text);
                    break;
                default:
                    break;
            }
            MostrarItem(e.Cell.Row);
        }

    }
}
