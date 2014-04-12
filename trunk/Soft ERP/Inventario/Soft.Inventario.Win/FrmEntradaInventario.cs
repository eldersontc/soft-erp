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
using Microsoft.VisualBasic;
using Soft.DataAccess;
using Infragistics.Win;
using Soft.Seguridad.Entidades;

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
        const String colNombre = "Nombre";
        const String colObservacion = "Observación";
        const String colUnidad = "Unidad";
        const String colPrecio = "Precio";
        const String colCantidad = "Cantidad";
        const String colTotal = "Total";

        private Boolean ActualizandoIU = false;

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colCodigo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colNombre);
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
            ugProductos.DisplayLayout.Bands[0].Columns[colCantidad].CellAppearance.TextHAlign = HAlign.Right;
            ugProductos.DisplayLayout.Bands[0].Columns[colPrecio].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugProductos.DisplayLayout.Bands[0].Columns[colPrecio].CellAppearance.TextHAlign = HAlign.Right;
            ugProductos.DisplayLayout.Bands[0].Columns[colTotal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugProductos.DisplayLayout.Bands[0].Columns[colTotal].CellAppearance.TextHAlign = HAlign.Right;
            ugProductos.DisplayLayout.Bands[0].Columns[colTotal].CellActivation = Activation.NoEdit;
            MapKeys(ref ugProductos);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            if (EntradaInventario.TipoDocumento != null) {
                ssTipoDocumento.Text = EntradaInventario.TipoDocumento.Descripcion;
                LabelSocioNegocio.Text = EntradaInventario.TipoDocumento.TipoSocioDeNegocio;
                ssProveedor.Enabled = (EntradaInventario.TipoDocumento.TipoSocioDeNegocio.Equals("Ninguno")) ? false : true;
                txtNumeracion.Enabled = (EntradaInventario.TipoDocumento.NumeracionAutomatica) ? false : true;
            }
            ssProveedor.Text = (EntradaInventario.Proveedor != null) ? EntradaInventario.Proveedor.Nombre : "";
            ssAlmacen.Text = (EntradaInventario.Almacen != null) ? EntradaInventario.Almacen.Descripcion : "";
            ssResponsable.Text = (EntradaInventario.Responsable != null) ? EntradaInventario.Responsable.Nombre : "";
            ssMoneda.Text = (EntradaInventario.Moneda != null) ? EntradaInventario.Moneda.Simbolo : "";
            txtNumeracion.Text = EntradaInventario.Numeracion;
            txtOrdenCompra.Text = EntradaInventario.OrdenCompra;
            txtFactura.Text = EntradaInventario.Factura;
            udtFechaCreacion.Value = EntradaInventario.FechaCreacion;
            txtObservacion.Text = EntradaInventario.Observacion;

            MostrarItems();
            MostrarCostos();
            ActualizandoIU = false;
        }

        public void MostrarCostos() {
            if (EntradaInventario.Moneda != null) {
                LabelSubtotal.Text = "Sub Total " + EntradaInventario.Moneda.Simbolo;
                LabelImpuesto.Text = "Impuesto " + Convert.ToInt16(EntradaInventario.TipoDocumento.PorcentajeImpuesto)+"%";
                LabelTotal.Text = "Total " + EntradaInventario.Moneda.Simbolo;
            }
            uneSubTotal.Value = EntradaInventario.SubTotal;
            uneImpuesto.Value = EntradaInventario.Impuesto;
            uneTotal.Value = EntradaInventario.Total;
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugProductos);
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
                Row.Cells[colCodigo].Activation = Activation.NoEdit;
                Row.Cells[colNombre].Activation = Activation.NoEdit;
                Row.Cells[colCodigo].Value =Item.Producto.Codigo;
                Row.Cells[colNombre].Value = Item.Producto.Nombre;
                Row.Cells[colUnidad].Value = Item.Unidad.Nombre;
            }
            Row.Cells[colObservacion].Value = Item.Observacion;
            Row.Cells[colUnidad].Value = (Item.Unidad != null) ? Item.Unidad.Nombre : "";
            Row.Cells[colPrecio].Value = Item.Precio;
            Row.Cells[colCantidad].Value = Item.Cantidad;
            Row.Cells[colTotal].Value = Item.Total;
        }

        public void AgregarProductos()
        {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionarProducto = new FrmSelectedEntity();
            Productos = FrmSeleccionarProducto.GetSelectedsEntities(typeof(Existencia), "Seleción de Existencia", String.Format(" IDAlmacen = '{0}'", EntradaInventario.Almacen.ID));
            foreach (Existencia Producto in Productos)
            {
                UltraGridRow RowNuevo = ugProductos.DisplayLayout.Bands[0].AddNew();
                ItemEntradaInventario ItemNuevo = EntradaInventario.AddItem();
                ItemNuevo.Producto = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                ItemNuevo.Cantidad = 1;
                RowNuevo.Tag = ItemNuevo;
                AgregarUnidades(RowNuevo);
                MostrarItem(RowNuevo);
            }
        }

        public void AgregarUnidades(UltraGridRow Row)
        {
            ItemEntradaInventario Item = (ItemEntradaInventario)Row.Tag;
            ValueList List = new ValueList();
            foreach (ExistenciaUnidad Unidad in Item.Producto.Unidades)
            {
                if (Unidad.EsUnidadBase)
                {
                    Item.Unidad = Unidad.Unidad;
                    Item.Factor = Unidad.FactorConversion;
                }
                List.ValueListItems.Add(Unidad, Unidad.Unidad.Nombre);
            }
            Row.Cells[colUnidad].ValueList = List;
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                EntradaInventario.TipoDocumento = (TipoDocumentoInventario)FrmSeleccionar.GetSelectedEntity(typeof(TipoDocumentoInventario), "Tipo de Inventario", " Operacion = 'Entrada'", true);
                if (EntradaInventario.TipoDocumento != null) {
                    EntradaInventario.GenerarNumeracion();
                    EntradaInventario.Responsable = FrmMain.ObtenerResponsable();
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void ssProveedor_Search(object sender, EventArgs e)
        {
            if (!LabelSocioNegocio.Text.Equals("Ninguno"))
            {
                String Filtro = string.Format(" {0} = 1", LabelSocioNegocio.Text);
                FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
                EntradaInventario.Proveedor = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", Filtro);
                ssProveedor.Text = (EntradaInventario.Proveedor != null) ? EntradaInventario.Proveedor.Nombre : "";
            }
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
            AgregarProductos();
        }

        private void ubEliminarProducto_Click(object sender, EventArgs e)
        {
            if (ugProductos.ActiveRow == null) { return; }
            EntradaInventario.Items.Remove((ItemEntradaInventario)ugProductos.ActiveRow.Tag);
            ugProductos.ActiveRow.Delete(false);
            MostrarCostos(); 
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            EntradaInventario.Observacion = txtObservacion.Text;
        }

        private void ugProductos_CellChange(object sender, CellEventArgs e)
        {
            try 
	        {	        
		        ItemEntradaInventario Item = (ItemEntradaInventario)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colUnidad:
                        ExistenciaUnidad Unidad = (ExistenciaUnidad)e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex);
                        Item.Unidad = Unidad.Unidad;
                        Item.Factor = Unidad.FactorConversion;
                        break;
                    case colPrecio:
                        Item.Precio = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        MostrarCostos();
                        break;
                    case colCantidad:
                        Item.Cantidad = Convert.ToDecimal(e.Cell.Text.Replace('_',' '));
                        MostrarCostos();
                        break;
                    case colObservacion:
                        Item.Observacion = e.Cell.Text;
                        break;
                    default:
                        break;
                }
                MostrarItem(e.Cell.Row);
                MostrarCostos();
	        }   
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message);
	        }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
                EntradaInventario.Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");
                String filtro = "";
                if (EntradaInventario.Moneda != null)
                {
                    if (EntradaInventario.Moneda.Simbolo.Equals("US $"))
                    {
                        filtro = "IDMoneda='" + EntradaInventario.Moneda.ID + "' and Fecha='" + EntradaInventario.FechaCreacion.Date + "'";
                        FrmSelectedEntity FrmSelectedMoneda = new FrmSelectedEntity();
                        TipoCambio tc = (TipoCambio)FrmSelectedMoneda.GetSelectedEntity(typeof(TipoCambio), "Tipo de Cambio", filtro);
                        EntradaInventario.TipoCambioFecha = tc.TipoCambioVenta;
                    }
                    else
                    {
                        EntradaInventario.TipoCambioFecha = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
            Mostrar();
        }

    }
}
