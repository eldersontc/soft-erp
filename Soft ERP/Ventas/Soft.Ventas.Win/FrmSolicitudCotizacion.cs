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
using Soft.DataAccess;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Win
{
    public partial class FrmSolicitudCotizacion : FrmParent 
    {
        public FrmSolicitudCotizacion()
        {
            InitializeComponent();
        }

        public SolicitudCotizacion SolicitudCotizacion { get { return (SolicitudCotizacion)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const String colCodigo = "Código";
        const String colNombre = "Nombre";
        const String colObservacion = "Observación";
        const String colUnidad = "Unidad";

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

            ugProductos.DataSource = columns;
            MapKeys(ref ugProductos);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            ssTipoDocumento.Text = (SolicitudCotizacion.TipoDocumento != null) ? SolicitudCotizacion.TipoDocumento.Descripcion : "";
            ssCliente.Text = (SolicitudCotizacion.Cliente != null) ? SolicitudCotizacion.Cliente.Nombre : "";
            ssResponsable.Text = (SolicitudCotizacion.Responsable != null) ? SolicitudCotizacion.Responsable.Nombre : "";
            txtNumeracion.Text = SolicitudCotizacion.Numeracion;
            udtFechaCreacion.Value = SolicitudCotizacion.FechaCreacion;
            txtObservacion.Text = SolicitudCotizacion.Observacion;
            MostrarItems();
            ActualizandoIU = false;
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugProductos);
            foreach (ItemSolicitudCotizacion Item in SolicitudCotizacion.Items)
            {
                UltraGridRow Row = ugProductos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemSolicitudCotizacion Item = (ItemSolicitudCotizacion)Row.Tag;
            if (Item.Existencia != null)
            {
                Row.Cells[colCodigo].Activation = Activation.NoEdit;
                Row.Cells[colNombre].Activation = Activation.NoEdit;
                Row.Cells[colUnidad].Activation = Activation.NoEdit;
                Row.Cells[colCodigo].Value = Item.Existencia.Codigo;
                Row.Cells[colNombre].Value = Item.Existencia.Nombre;
                Row.Cells[colUnidad].Value = Item.Unidad.Nombre;
            }
            Row.Cells[colObservacion].Value = Item.Observacion;
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
            SolicitudCotizacion.TipoDocumento = (TipoSolicitudCotizacion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoSolicitudCotizacion), "Tipo Solicitud Cotización");
            ssTipoDocumento.Text = (SolicitudCotizacion.TipoDocumento != null) ? SolicitudCotizacion.TipoDocumento.Descripcion : "";
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.Numeracion = txtNumeracion.Text;
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
            SolicitudCotizacion.Cliente = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Cliente = 1");
            ssCliente.Text = (SolicitudCotizacion.Cliente != null) ? SolicitudCotizacion.Cliente.Nombre : "";
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.Descripcion = txtDescripcion.Text;
        }

        private void uneCantidad_ValueChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.Cantidad = Convert.ToInt32(uneCantidad.Value);
        }

        private void ssLineaTrabajo_Search(object sender, EventArgs e)
        {

        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.Observacion = txtObservacion.Text;
        }

        private void ubNuevaExistencia_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = ugProductos.DisplayLayout.Bands[0].AddNew();
            Row.Tag = SolicitudCotizacion.AddItem();
            Row.Cells[colCodigo].Activate();
            ugProductos.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
        }

        private void ubEliminarExistencia_Click(object sender, EventArgs e)
        {
            if (ugProductos.ActiveRow == null) { return; }
            SolicitudCotizacion.Items.Remove((ItemSolicitudCotizacion)ugProductos.ActiveRow.Tag);
            ugProductos.ActiveRow.Delete(false);
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            SolicitudCotizacion.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssResponsable.Text = (SolicitudCotizacion.Responsable != null) ? SolicitudCotizacion.Responsable.Nombre : "";
        }

        public void AgregarProductosServicios(String Codigo, String Descripcion, UltraGridRow Row)
        {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemSolicitudCotizacion Item = (ItemSolicitudCotizacion)Row.Tag;
            Productos = FrmSeleccionar.GetSelectedsEntities(typeof(Existencia), "Seleción de Existencia", String.Format(" Codigo LIKE '{0}%' AND Nombre LIKE '{1}%'", Codigo, Descripcion));
            if (Productos.Count == 1)
            {
                Existencia Producto = (Existencia)Productos[1];
                Item.Existencia = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                Item.Cantidad = 1;
                MostrarItem(Row);
            }
            else if (Productos.Count > 1)
            {
                Existencia Producto = (Existencia)Productos[1];
                Item.Existencia = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                Item.Cantidad = 1;
                MostrarItem(Row);
                for (int i = 2; i <= Productos.Count; i++)
                {
                    UltraGridRow RowNuevo = ugProductos.DisplayLayout.Bands[0].AddNew();
                    ItemSolicitudCotizacion ItemNuevo = SolicitudCotizacion.AddItem();
                    Existencia ProductoNuevo = (Existencia)Productos[i];
                    ItemNuevo.Existencia = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ProductoNuevo.ID);
                    ItemNuevo.Cantidad = 1;
                    RowNuevo.Tag = ItemNuevo;
                    MostrarItem(RowNuevo);
                }
            }
        }

        private void ugProductos_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                ItemSolicitudCotizacion Item = (ItemSolicitudCotizacion)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    //case colUnidad:
                    //    Item.Unidad = (Unidad)e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex);
                    //    break;
                    case colObservacion:
                        Item.Observacion = e.Cell.Text;
                        break;
                    default:
                        break;
                }
                MostrarItem(e.Cell.Row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ugProductos_CellKeyEnter(UltraGridCell Cell)
        {
            try
            {
                if (Cell == null) { return; }
                ItemSolicitudCotizacion Item = (ItemSolicitudCotizacion)Cell.Row.Tag;
                switch (Cell.Column.Key)
                {
                    case colCodigo:
                        if (Cell.Text.Equals("")) { break; }
                        AgregarProductosServicios(Cell.Text, "%", Cell.Row);
                        break;
                    case colNombre:
                        if (Cell.Text.Equals("")) { break; }
                        AgregarProductosServicios("%", Cell.Text, Cell.Row);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

    }
}
