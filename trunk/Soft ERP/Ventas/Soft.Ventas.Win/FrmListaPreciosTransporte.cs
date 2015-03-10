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
using Soft.Entities;
using Soft.Inventario.Entidades;
using Infragistics.Win;

namespace Soft.Ventas.Win
{
    public partial class FrmListaPreciosTransporte : FrmParent 
    {
        public FrmListaPreciosTransporte()
        {
            InitializeComponent();
        }

        //Constantes
        const String colOrigen = "Origen";
        const String colDestino = "Destino";
        const String colTipoVehiculo = "Tipo Vehículo";
        const String colDesde = "Desde";
        const String colHasta = "Hasta";
        const String colDescripcion = "Descripción";
        const String colPrecio = "Precio";

        public ListaPreciosTransporte ListaPreciosTransporte { get { return (ListaPreciosTransporte)base.m_ObjectFlow; } }
        public ItemListaPreciosTransporte ItemListaPreciosTransporte;

        #region "Métodos"

        public override void Init()
        {
            InitGrid();
            Mostrar();
        }

        public void InitGrid()
        {
            // Distritos
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colOrigen);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colDestino);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colTipoVehiculo);
            column.DataType = typeof(String);

            ugDistritos.DataSource = columns;
            ugDistritos.DisplayLayout.Bands[0].Columns[colOrigen].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            ugDistritos.DisplayLayout.Bands[0].Columns[colDestino].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.EditButton;
            
            ValueList List = new ValueList();
            List.ValueListItems.Add("AUTOMÓVIL", "AUTOMÓVIL");
            List.ValueListItems.Add("MOTOCICLETA", "MOTOCICLETA");
            List.ValueListItems.Add("CAMIÓN PEQUEÑO", "CAMIÓN PEQUEÑO");
            List.ValueListItems.Add("CAMIÓN MEDIANO", "CAMIÓN MEDIANO");
            List.ValueListItems.Add("CAMIÓN GRANDE", "CAMIÓN GRANDE");

            ugDistritos.DisplayLayout.Bands[0].Columns[colTipoVehiculo].ValueList = List;
            ugDistritos.DisplayLayout.Bands[0].Columns[colTipoVehiculo].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            MapKeys(ref ugDistritos);

            //Escalas
            columns = new DataTable();

            column = columns.Columns.Add(colDesde);
            column.DataType = typeof(Int32);

            column = columns.Columns.Add(colHasta);
            column.DataType = typeof(Int32);

            column = columns.Columns.Add(colDescripcion);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colPrecio);
            column.DataType = typeof(Decimal);

            ugEscalas.DataSource = columns;
            ugEscalas.DisplayLayout.Bands[0].Columns[colDesde].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colHasta].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colPrecio].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugEscalas.DisplayLayout.Bands[0].Columns[colPrecio].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colPrecio].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            MapKeys(ref ugEscalas);

        }

        public void Mostrar()
        {
            txtCodigo.Text = ListaPreciosTransporte.Codigo;
            txtNombre.Text = ListaPreciosTransporte.Nombre;
            uceActivo.Checked = ListaPreciosTransporte.Activo;
            MostrarItems();
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugDistritos);
            foreach (ItemListaPreciosTransporte Item in ListaPreciosTransporte.Items)
            {
                UltraGridRow Row = ugDistritos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemListaPreciosTransporte Item = (ItemListaPreciosTransporte)Row.Tag;
            Row.Cells[colOrigen].Value = (Item.Origen != null) ? Item.Origen.Nombre : "";
            Row.Cells[colDestino].Value = (Item.Destino != null) ? Item.Destino.Nombre : "";
            Row.Cells[colTipoVehiculo].Value = Item.TipoVehiculo;
            MostrarEscalas(Item);
        }

        public void MostrarEscalas(ItemListaPreciosTransporte ItemListaPreciosTransporte)
        {
            base.ClearAllRows(ref ugEscalas);
            foreach (EscalaListaPreciosTransporte Item in ItemListaPreciosTransporte.Escalas)
            {
                UltraGridRow Row = ugEscalas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarEscala(Row);
            }
        }

        public void MostrarEscala(UltraGridRow Row)
        {
            EscalaListaPreciosTransporte Item = (EscalaListaPreciosTransporte)Row.Tag;
            Row.Cells[colDesde].Value = Item.Desde;
            Row.Cells[colHasta].Value = Item.Hasta;
            Row.Cells[colDescripcion].Value = Item.Descripcion;
            Row.Cells[colPrecio].Value = Item.Precio;
        }

        #endregion

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ListaPreciosTransporte.Codigo = txtCodigo.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            ListaPreciosTransporte.Activo = uceActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ListaPreciosTransporte.Nombre = txtNombre.Text;
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugDistritos.DisplayLayout.Bands[0].AddNew();
            Row.Tag = ListaPreciosTransporte.AddItem();
            ItemListaPreciosTransporte = (ItemListaPreciosTransporte)Row.Tag;
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugDistritos.ActiveRow == null) { return; }
            ListaPreciosTransporte.Items.Remove((ItemListaPreciosTransporte)ugDistritos.ActiveRow.Tag);
            ugDistritos.ActiveRow.Delete(false);
        }

        private void ubNuevaEscala_Click(object sender, EventArgs e)
        {
            if (ItemListaPreciosTransporte == null) { return; }
            UltraGridRow Row = ugEscalas.DisplayLayout.Bands[0].AddNew();
            Row.Tag = ItemListaPreciosTransporte.AddEscala();
        }

        private void ubElminarEscala_Click(object sender, EventArgs e)
        {
            if (ugEscalas.ActiveRow == null) { return; }
            ItemListaPreciosTransporte.Escalas.Remove((EscalaListaPreciosTransporte)ugEscalas.ActiveRow.Tag);
            ugEscalas.ActiveRow.Delete(false);
        }

        private void ugEscalas_CellChange(object sender, CellEventArgs e)
        {
            EscalaListaPreciosTransporte Escala = (EscalaListaPreciosTransporte)e.Cell.Row.Tag;
            ugEscalas.UpdateData();
            switch (e.Cell.Column.Key)
            {
                case colDesde:
                    Escala.Desde = Convert.ToInt32((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colHasta:
                    Escala.Hasta = Convert.ToInt32((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colDescripcion:
                    Escala.Descripcion = Convert.ToString(e.Cell.Text);
                    break;
                case colPrecio:
                    Escala.Precio= Convert.ToDecimal((e.Cell.Value == DBNull.Value)?0:e.Cell.Value);
                    break;
                default:
                    break;
            }
            MostrarEscala(e.Cell.Row);
        }

        private void ugDistritos_ClickCellButton(object sender, CellEventArgs e)
        {
            ItemListaPreciosTransporte Item = (ItemListaPreciosTransporte)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colOrigen:
                    FrmSelectedEntity FrmSeleccionarOrigen = new FrmSelectedEntity();
                    Item.Origen = (Distrito)FrmSeleccionarOrigen.GetSelectedEntity(typeof(Distrito), "Distrito");
                    break;
                case colDestino:
                    FrmSelectedEntity FrmSeleccionarDestino = new FrmSelectedEntity();
                    Item.Destino = (Distrito)FrmSeleccionarDestino.GetSelectedEntity(typeof(Distrito), "Distrito");
                    break;
            }
            MostrarItem(e.Cell.Row);
        }

        private void ugDistritos_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (ugDistritos.ActiveRow == null) { return; }
            ItemListaPreciosTransporte = (ItemListaPreciosTransporte)ugDistritos.ActiveRow.Tag;
            MostrarEscalas(ItemListaPreciosTransporte);
        }

        private void ugDistritos_ClickCell(object sender, ClickCellEventArgs e)
        {
           MostrarItem(e.Cell.Row);
        }

        private void ugDistritos_CellChange(object sender, CellEventArgs e)
        {
            ItemListaPreciosTransporte Item = (ItemListaPreciosTransporte)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colTipoVehiculo:
                    Item.TipoVehiculo = e.Cell.Text;
                    MostrarItem(e.Cell.Row);
                    break;
                default:
                    break;
            }
            
        }

    }
}
