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
using Infragistics.Win.UltraWinGrid;
using Soft.DataAccess;
using Infragistics.Win;

namespace Soft.Configuracion.Win
{
    public partial class FrmAccion : FrmParent 
    {
        public FrmAccion()
        {
            InitializeComponent();
        }

        const String colNombre = "Nombre";
        const String colEnsamblado = "Ensamblado";
        const String colClase = "Clase";
        const String colParametro = "Parámetro(s)";
        const String colExito = "Éxito";
        const String colError = "Error";

        public Accion Accion { get { return (Accion)base.m_ObjectFlow; } }

        #region Metodos

        public override void Init()
        {
            this.InitGrids();
            this.Mostrar();
            this.ActualizarCombos();
        }

        public void InitGrids()
        {

            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colEnsamblado);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colClase);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colParametro);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colExito);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colError);
            column.DataType = typeof(String);

            ugItemAccion.DataSource = columns;
            ugItemAccion.DisplayLayout.Bands[0].Columns[colEnsamblado].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            ugItemAccion.DisplayLayout.Bands[0].Columns[colExito].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            ugItemAccion.DisplayLayout.Bands[0].Columns[colError].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

        }

        public void ActualizarCombos() {
            foreach (UltraGridRow  Row in ugItemAccion.Rows)
            {
                ValueList List = new ValueList();
                foreach (ItemAccion Item in this.Accion.Items) {
                    if(!Item.ID.Equals(((ItemAccion)Row.Tag).ID)){
                        List.ValueListItems.Add(Item.Nombre, Item.Nombre);
                    } 
                }
                List.ValueListItems.Add("Salir", "Salir del Flujo");
                Row.Cells[colExito].ValueList = List;
                Row.Cells[colError].ValueList = List;
            }
        }

        public void Mostrar()
        {
            txtNombre.Text = this.Accion.Nombre;
            txtDescripcion.Text = this.Accion.Descripcion;
            ucTeclas.Text = this.Accion.Teclas;
            uceActivo.Checked = this.Accion.Activo;
            uceFilaSeleccionada.Checked = this.Accion.FilaSeleccionada;
            upbImagen.Image = base.GetImage(this.Accion.Imagen);
            this.MostrarItems();
        }

        public void MostrarItems()
        {
            foreach (ItemAccion Item in this.Accion .Items)
            {
                UltraGridRow Row = ugItemAccion.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemAccion Item = (ItemAccion)Row.Tag;
            if (Item.Ensamblado != null) { Row.Cells[colEnsamblado].Value = Item.Ensamblado.Nombre; }
            Row.Cells[colNombre].Value = Item.Nombre;
            Row.Cells[colClase].Value = Item.Clase;
            Row.Cells[colParametro].Value = Item.Parametro;
            Row.Cells[colExito].Value = Item.Exito;
            Row.Cells[colError].Value = Item.Error;
        }

        #endregion

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            this.Accion.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            this.Accion.Descripcion = txtDescripcion.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            this.Accion.Activo = uceActivo.Checked;
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugItemAccion.DisplayLayout.Bands[0].AddNew();
            Row.Tag = this.Accion.AddItem();
            this.ActualizarCombos();
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugItemAccion.ActiveRow == null) { return; }
            this.Accion.Items.Remove((ItemAccion)this.ugItemAccion.ActiveRow.Tag);
            this.ugItemAccion.ActiveRow.Delete(false);
        }

        private void uceFilaSeleccionada_CheckedChanged(object sender, EventArgs e)
        {
            this.Accion.FilaSeleccionada = uceFilaSeleccionada.Checked;
        }

        private void ubBuscarImage_Click(object sender, EventArgs e)
        {
            FrmSelectedImage FrmImage = new FrmSelectedImage();
            this.Accion.Imagen = FrmImage.GetSelectedImage();
            upbImagen.Image = base.GetImage(this.Accion.Imagen);
        }

        private void ucTeclas_ValueChanged(object sender, EventArgs e)
        {
            this.Accion.Teclas = ucTeclas.Text;
        }

        private void ugItemAccion_ClickCellButton(object sender, CellEventArgs e)
        {
            ItemAccion Item = (ItemAccion)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colEnsamblado:
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    Item.Ensamblado = (Ensamblado)FrmSeleccionar.GetSelectedEntity(typeof(Ensamblado), "Ensamblado");
                    break;
                default:
                    break;
            }
            this.MostrarItem(e.Cell.Row);
        }

        private void ugItemAccion_CellChange(object sender, CellEventArgs e)
        {
            ItemAccion Item = (ItemAccion)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colNombre:
                    Item.Nombre = Convert.ToString(e.Cell.Text);
                    this.ActualizarCombos();
                    break;
                case colClase:
                    Item.Clase = Convert.ToString(e.Cell.Text);
                    break;
                case colParametro:
                    Item.Parametro = Convert.ToString(e.Cell.Text);
                    break;
                case colExito:
                    Item.Exito = e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex).ToString();
                    break;
                case colError:
                    Item.Error = e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex).ToString();
                    break;
                default:
                    break;
            }
            this.MostrarItem(e.Cell.Row);
        }

    }
}
