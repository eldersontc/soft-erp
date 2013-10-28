﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Inventario.Entidades;
using Infragistics.Win.UltraWinGrid;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.DataAccess;
using Infragistics.Win;

namespace Soft.Inventario.Win
{
    public partial class FrmEntradaInventario : FrmParent 
    {
        public FrmEntradaInventario()
        {
            InitializeComponent();
        }

        public EntradaInventario EntradaInventario { get { return (EntradaInventario)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const String colCodigo = "Código";
        const String colDescripcion = "Descripción";
        const String colObservacion = "Observación";
        const String colUnidad = "Unidad";
        const String colPrecio = "Precio";
        const String colCantidad = "Cantidad";
        const String colTotal = "Total";
        
        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colCodigo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colDescripcion);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colObservacion);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colPrecio);
            column.DataType = typeof(Decimal);

            column = columns.Columns.Add(colCantidad);
            column.DataType = typeof(Decimal);

            column = columns.Columns.Add(colTotal);
            column.DataType = typeof(Decimal);

            ugProductos.DataSource = columns;
            ugProductos.DisplayLayout.Bands[0].Columns[colUnidad].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            ugProductos.DisplayLayout.Bands[0].Columns[colCantidad].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugProductos.DisplayLayout.Bands[0].Columns[colPrecio].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugProductos.DisplayLayout.Bands[0].Columns[colTotal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
        }

        public void Mostrar()
        {
            ssTipoDocumento.Text = (EntradaInventario.TipoDocumento != null)?EntradaInventario.TipoDocumento.Descripcion:"";
            ssProveedor.Text = (EntradaInventario.Proveedor != null) ? EntradaInventario.Proveedor.Nombre : "";
            ssAlmacen.Text = (EntradaInventario.Almacen != null) ? EntradaInventario.Almacen.Descripcion : "";
            ssResponsable.Text = (EntradaInventario.Responsable != null) ? EntradaInventario.Responsable.Nombre : "";
            txtNumeracion.Text = EntradaInventario.Numeracion;
            txtOrdenCompra.Text = EntradaInventario.OrdenCompra;
            txtFactura.Text = EntradaInventario.Factura;
            udtFechaCreacion.Value = EntradaInventario.FechaCreacion;
            txtObservacion.Text = EntradaInventario.Observacion;
            MostrarItems();
            MostrarCostos();
        }

        public void MostrarCostos() {
            uneSubTotal.Value = EntradaInventario.SubTotal;
            uneImpuesto.Value = EntradaInventario.Impuesto;
            uneTotal.Value = EntradaInventario.Total;
        }

        public void MostrarItems()
        {
            foreach (ItemEntradaInventario Item in EntradaInventario.Items)
            {
                UltraGridRow Row = ugProductos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemEntradaInventario Item = (ItemEntradaInventario)Row.Tag;
            if (Item.Producto != null) {
                Row.Cells[colCodigo].Value =Item.Producto.Codigo;
                Row.Cells[colDescripcion].Value = Item.Producto.Descripcion;
                AgregarUnidades(Row);
            }
            Row.Cells[colObservacion].Value = Item.Observacion;
            Row.Cells[colUnidad].Value = (Item.Unidad != null) ? Item.Unidad.Nombre : "";
            Row.Cells[colPrecio].Value = Item.Precio;
            Row.Cells[colCantidad].Value = Item.Cantidad;
            Row.Cells[colTotal].Value = Item.Total;
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
            TipoDocumentoInventario TipoDocumento = (TipoDocumentoInventario)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoDocumentoInventario), "Tipo de Inventario");
            EntradaInventario.TipoDocumento = (TipoDocumentoInventario)HelperNHibernate.GetEntityByID("TipoDocumentoInventario", TipoDocumento.ID);
            ssTipoDocumento.Text = (EntradaInventario.TipoDocumento != null) ? EntradaInventario.TipoDocumento.Descripcion : "";
        }

        private void ssProveedor_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
            EntradaInventario.Proveedor = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio"," Proveedor = 1");
            ssProveedor.Text = (EntradaInventario.Proveedor != null) ? EntradaInventario.Proveedor.Nombre : "";
        }

        private void ssAlmacen_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarAlmancen = new FrmSelectedEntity();
            EntradaInventario.Almacen = (Almacen)FrmSeleccionarAlmancen.GetSelectedEntity(typeof(Almacen), "Almacen");
            ssAlmacen.Text = (EntradaInventario.Almacen != null) ? EntradaInventario.Almacen.Descripcion : "";
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            EntradaInventario.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssResponsable.Text = (EntradaInventario.Responsable != null) ? EntradaInventario.Responsable.Nombre : "";
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            EntradaInventario.Numeracion = txtNumeracion.Text;
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            EntradaInventario.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void ubNuevoProducto_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugProductos.DisplayLayout.Bands[0].AddNew();
            Row.Tag = EntradaInventario.AddItem();
        }

        private void ubEliminarProducto_Click(object sender, EventArgs e)
        {
            if (ugProductos.ActiveRow == null) { return; }
            EntradaInventario.Items.Remove((ItemEntradaInventario)ugProductos.ActiveRow.Tag);
            ugProductos.ActiveRow.Delete(false);
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            EntradaInventario.Observacion = txtObservacion.Text;
        }

        public void AgregarProductos(String Codigo ,String Descripcion ,UltraGridRow Row) {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionarProducto = new FrmSelectedEntity();
            ItemEntradaInventario Item = (ItemEntradaInventario)Row.Tag;
            Productos = FrmSeleccionarProducto.GetSelectedsEntities(typeof(Existencia), "Existencia");
            if (Productos.Count == 1) {
                Existencia Producto = (Existencia)Productos[1];
                Item.Producto = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                //Item.Producto = (Existencia)Productos[1];
                //AgregarUnidades(Row);
                Item.Cantidad = 1;
            }
            else if (Productos.Count > 1) {
                Item.Producto = (Existencia)Productos[1];
                Item.Cantidad = 1;
                for (int i = 2; i <= Productos.Count; i++)
                {
                    UltraGridRow RowNuevo = ugProductos.DisplayLayout.Bands[0].AddNew();
                    ItemEntradaInventario ItemNuevo = EntradaInventario.AddItem();
                    Existencia Producto = (Existencia)Productos[1];
                    Item.Producto = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                    //AgregarUnidades(RowNuevo);
                    //Item.Producto = (Existencia)Productos[1];
                    ItemNuevo.Cantidad = 1;
                    RowNuevo.Tag = ItemNuevo;
                    MostrarItem(RowNuevo);
                }
            }
        }

        public void AgregarUnidades(UltraGridRow Row)
        {
            ItemEntradaInventario Item = (ItemEntradaInventario)Row.Tag;
            //Item.Producto = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Item.Producto.ID);
            ValueList List = new ValueList();
            foreach (ExistenciaUnidad Unidad in Item.Producto.Unidades)
            {
                if (Unidad.EsUnidadBase) { Item.Unidad = Unidad.Unidad; }
                List.ValueListItems.Add(Unidad.Unidad, Unidad.Unidad.Nombre);
            }
            Row.Cells[colUnidad].ValueList = List;
        }

        private void ugProductos_CellChange(object sender, CellEventArgs e)
        {
            try 
	        {	        
		        ItemEntradaInventario Item = (ItemEntradaInventario)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colCodigo:
                        if (e.Cell.Text.Equals("")) { break; }
                        AgregarProductos(e.Cell.Text,"",e.Cell.Row);
                        break;
                    case colDescripcion:
                        if (e.Cell.Text.Equals("")) { break; }
                        AgregarProductos("", e.Cell.Text, e.Cell.Row);
                        break;
                    case colUnidad:
                        Item.Unidad = (Unidad)e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex);
                        break;
                    case colPrecio:
                        Item.Precio = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        MostrarCostos();
                        break;
                    case colCantidad:
                        Item.Cantidad = Convert.ToDecimal(e.Cell.Text.Replace('_',' '));
                        MostrarCostos();
                        break;
                    default:
                        break;
                }
                MostrarItem(e.Cell.Row);
	        }   
	        catch (Exception)
	        {
                throw;
	        }
        }

    }
}