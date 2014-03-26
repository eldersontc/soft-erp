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
    public partial class FrmSalidaInventario : FrmParent 
    {
        public FrmSalidaInventario()
        {
            InitializeComponent();
        }

        public SalidaInventario SalidaInventario { get { return (SalidaInventario)base.m_ObjectFlow; } }

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
            ugProductos.DisplayLayout.Bands[0].Columns[colNombre].Width = 250;
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
            if (SalidaInventario.TipoDocumento != null)
            {
                ssTipoDocumento.Text = SalidaInventario.TipoDocumento.Descripcion;
                LabelSocioNegocio.Text = SalidaInventario.TipoDocumento.TipoSocioDeNegocio;
                ssProveedor.Enabled = (SalidaInventario.TipoDocumento.TipoSocioDeNegocio.Equals("Ninguno")) ? false : true;
                txtNumeracion.Enabled = (SalidaInventario.TipoDocumento.NumeracionAutomatica) ? false : true;
            }
            ssProveedor.Text = (SalidaInventario.Proveedor != null) ? SalidaInventario.Proveedor.Nombre : "";
            ssAlmacen.Text = (SalidaInventario.Almacen != null) ? SalidaInventario.Almacen.Descripcion : "";
            ssResponsable.Text = (SalidaInventario.Responsable != null) ? SalidaInventario.Responsable.Nombre : "";
            ssMoneda.Text = (SalidaInventario.Moneda != null) ? SalidaInventario.Moneda.Simbolo : "";
            txtNumeracion.Text = SalidaInventario.Numeracion;
            udtFechaCreacion.Value = SalidaInventario.FechaCreacion;
            txtObservacion.Text = SalidaInventario.Observacion;
            txtOrdenProduccion.Text = SalidaInventario.OrdenProduccion;

            MostrarItems();
            MostrarCostos();
            ActualizandoIU = false;
        }

        public void MostrarCostos() {
            if (SalidaInventario.Moneda != null)
            {
                LabelSubTotal.Text = "Sub Total " + SalidaInventario.Moneda.Simbolo;
                LabelImpuesto.Text = "Impuesto " + Convert.ToInt16(SalidaInventario.TipoDocumento.PorcentajeImpuesto) + "%";
                LabelTotal.Text = "Total " + SalidaInventario.Moneda.Simbolo;
            }

            uneSubTotal.Value = SalidaInventario.SubTotal;
            uneImpuesto.Value = SalidaInventario.Impuesto;
            uneTotal.Value = SalidaInventario.Total;
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugProductos);
            foreach (ItemSalidaInventario Item in SalidaInventario.Items)
            {
                UltraGridRow Row = ugProductos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemSalidaInventario Item = (ItemSalidaInventario)Row.Tag;
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
            Productos = FrmSeleccionarProducto.GetSelectedsEntities(typeof(Existencia), "Seleción de Existencia", String.Format(" IDAlmacen = '{0}'", SalidaInventario.Almacen.ID));
            foreach (Existencia Producto in Productos)
            {
                UltraGridRow RowNuevo = ugProductos.DisplayLayout.Bands[0].AddNew();
                ItemSalidaInventario ItemNuevo = SalidaInventario.AddItem();
                ItemNuevo.Producto = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                ItemNuevo.Cantidad = 1;
                ItemNuevo.Precio = ItemNuevo.Producto.CostoPromedio;
                RowNuevo.Tag = ItemNuevo;
                AgregarUnidades(RowNuevo);
                MostrarItem(RowNuevo);
            }
            MostrarCostos();
        }

        public void AgregarUnidades(UltraGridRow Row)
        {
            ItemSalidaInventario Item = (ItemSalidaInventario)Row.Tag;
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
                SalidaInventario.TipoDocumento = (TipoDocumentoInventario)FrmSeleccionar.GetSelectedEntity(typeof(TipoDocumentoInventario), "Tipo de Inventario", " Operacion = 'Salida'", true);
                if (SalidaInventario.TipoDocumento != null)
                {
                    SalidaInventario.GenerarNumeracion();
                    SalidaInventario.Responsable = FrmMain.ObtenerResponsable();
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
                SalidaInventario.Proveedor = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", Filtro);
                ssProveedor.Text = (SalidaInventario.Proveedor != null) ? SalidaInventario.Proveedor.Nombre : "";
            }
        }

        private void ssAlmacen_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarAlmancen = new FrmSelectedEntity();
            SalidaInventario.Almacen = (Almacen)FrmSeleccionarAlmancen.GetSelectedEntity(typeof(Almacen), "Almacen");
            ssAlmacen.Text = (SalidaInventario.Almacen != null) ? SalidaInventario.Almacen.Descripcion : "";
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            SalidaInventario.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssResponsable.Text = (SalidaInventario.Responsable != null) ? SalidaInventario.Responsable.Nombre : "";
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            SalidaInventario.Numeracion = txtNumeracion.Text;
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            SalidaInventario.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void ubNuevoProducto_Click(object sender, EventArgs e)
        {
            AgregarProductos();
        }

        private void ubEliminarProducto_Click(object sender, EventArgs e)
        {
            if (ugProductos.ActiveRow == null) { return; }
            SalidaInventario.Items.Remove((ItemSalidaInventario)ugProductos.ActiveRow.Tag);
            ugProductos.ActiveRow.Delete(false);
            MostrarCostos(); 
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            SalidaInventario.Observacion = txtObservacion.Text;
        }

        private void ugProductos_CellChange(object sender, CellEventArgs e)
        {
            try 
	        {
                ItemSalidaInventario Item = (ItemSalidaInventario)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colUnidad:
                        ExistenciaUnidad Unidad = (ExistenciaUnidad)e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex);
                        Item.Unidad = Unidad.Unidad;
                        Item.Factor = Unidad.FactorConversion;
                        Item.Precio = Item.Precio * Item.Factor;
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

        private void txtOrdenProduccion_TextChanged(object sender, EventArgs e)
        {
            SalidaInventario.OrdenProduccion = txtOrdenProduccion.Text;
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
                SalidaInventario.Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");
                String filtro = "";
                if (SalidaInventario.Moneda != null)
                {
                    if (SalidaInventario.Moneda.Simbolo.Equals("US $"))
                    {
                        filtro = "IDMoneda='" + SalidaInventario.Moneda.ID + "' and Fecha='" + SalidaInventario.FechaCreacion.Date + "'";
                        FrmSelectedEntity FrmSelectedMoneda = new FrmSelectedEntity();
                        TipoCambio tc = (TipoCambio)FrmSelectedMoneda.GetSelectedEntity(typeof(TipoCambio), "Tipo de Cambio", filtro);
                        SalidaInventario.TipoCambioFecha = tc.TipoCambioVenta;
                    }
                    else
                    {
                        SalidaInventario.TipoCambioFecha = 1;
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
