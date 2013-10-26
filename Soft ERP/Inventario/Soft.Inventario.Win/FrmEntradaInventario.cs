using System;
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
            uneSubTotal.Value = EntradaInventario.SubTotal;
            uneImpuesto.Value = EntradaInventario.Impuesto;
            uneTotal.Value = EntradaInventario.Total;
            MostrarItems();
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
            Row.Cells[colCodigo].Value = Item.Producto.Codigo;
            Row.Cells[colDescripcion].Value = Item.Producto.Descripcion;
            Row.Cells[colObservacion].Value = Item.Observacion;
            Row.Cells[colUnidad].Value = Item.Unidad.Nombre;
            Row.Cells[colPrecio].Value = Item.Precio;
            Row.Cells[colCantidad].Value = Item.Cantidad;
            Row.Cells[colTotal].Value = Item.Total;
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
            EntradaInventario.TipoDocumento = (TipoDocumentoInventario)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoDocumentoInventario), "Tipo de Inventario");
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

        }

        private void ubEliminarProducto_Click(object sender, EventArgs e)
        {

        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            EntradaInventario.Observacion = txtObservacion.Text;
        }

    }
}
