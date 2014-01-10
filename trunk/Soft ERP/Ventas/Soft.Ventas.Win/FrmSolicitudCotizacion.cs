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
using Infragistics.Win.UltraWinTree;
using Soft.Seguridad.Entidades;

namespace Soft.Ventas.Win
{
    public partial class FrmSolicitudCotizacion : FrmParent
    {
        public FrmSolicitudCotizacion()
        {
            InitializeComponent();
        }

        public SolicitudCotizacion SolicitudCotizacion { get { return (SolicitudCotizacion)base.m_ObjectFlow; } }
        private ItemSolicitudCotizacion ItemSolicitudCotizacion = null;

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const String colNombre = "Nombre";

        private Boolean ActualizandoIU = false;

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colNombre].Width = 250;
            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            ssTipoDocumento.Text = (SolicitudCotizacion.TipoDocumento != null) ? SolicitudCotizacion.TipoDocumento.Descripcion : "";
            ssCliente.Text = (SolicitudCotizacion.Cliente != null) ? SolicitudCotizacion.Cliente.Nombre : "";
            ssResponsable.Text = (SolicitudCotizacion.Responsable != null) ? SolicitudCotizacion.Responsable.Nombre : "";
            ssFormaPago.Text = (SolicitudCotizacion.ModalidadCredito != null) ? SolicitudCotizacion.ModalidadCredito.Descripcion : "";
            ssMoneda.Text = (SolicitudCotizacion.Moneda != null) ? SolicitudCotizacion.Moneda.Simbolo : "";
            ssContacto.Text = (SolicitudCotizacion.Contacto != null) ? SolicitudCotizacion.Contacto.Nombre : "";
            ssDireccionEntrega.Text = SolicitudCotizacion.DireccionEntrega;
            ssDireccionFactura.Text = SolicitudCotizacion.DireccionFacturacion;
            txtNumeracion.Text = SolicitudCotizacion.Numeracion;
            udtFechaCreacion.Value = SolicitudCotizacion.FechaCreacion;
            txtObservacion.Text = SolicitudCotizacion.Observacion;
            txtDescripcion.Text = SolicitudCotizacion.Descripcion;
            uneCantidad.Value = SolicitudCotizacion.Cantidad;
            txtNumeracion.Text = SolicitudCotizacion.Numeracion;

            MostrarItems();
            ActualizandoIU = false;
        }

        public void MostrarItems()
        {
            utSolicitudCotizacion.Nodes.Clear();
            foreach (ItemSolicitudCotizacion Item in SolicitudCotizacion.Items)
            {
                UltraTreeNode Node = new UltraTreeNode();
                Node.Tag = Item;
                Node.Text = Item.Nombre;
                utSolicitudCotizacion.Nodes.Add(Node);
                MostrarItem(Node);
            }
            if (utSolicitudCotizacion.Nodes.Count > 0)
            {
                utSolicitudCotizacion.ActiveNode = utSolicitudCotizacion.Nodes[0];
                utSolicitudCotizacion.Nodes[0].Selected = true;
            }
            utSolicitudCotizacion.ExpandAll();
        }

        public void MostrarItem(UltraTreeNode Node)
        {
            ItemSolicitudCotizacion Item = (ItemSolicitudCotizacion)Node.Tag;
            ssMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
            lblTipoUnidad.Text = Item.TipoUnidad;
            txtObservacionItem.Text = Item.Observacion;
            txtCantidadItem.Value = Item.Cantidad;
            txtMedidaAbiertoLargo.Value = Item.MedidaAbiertaLargo;
            txtMedidaAbiertoAlto.Value = Item.MedidaAbiertaAlto;
            txtMedidaCerradaLargo.Value = Item.MedidaCerradaLargo;
            txtMedidaCerradaAlto.Value = Item.MedidaCerradaAlto;
            txtImpresoTiraColor.Value = Item.ImpresoTiraColor;
            txtImpresoRetiraColor.Value = Item.ImpresoRetiraColor;
            MostrarServicios(Item);
        }

        public void MostrarServicios(ItemSolicitudCotizacion ItemSolicitud)
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemSolicitudCotizacionServicio Item in ItemSolicitud.Servicios)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarServicio(Row);
            }
        }

        public void MostrarServicio(UltraGridRow Row)
        {
            ItemSolicitudCotizacionServicio Item = (ItemSolicitudCotizacionServicio)Row.Tag;
            if (Item.Servicio != null)
            {
                Row.Cells[colNombre].Activation = Activation.NoEdit;
                Row.Cells[colNombre].Value = Item.Servicio.Nombre;
            }
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {

            try
            {
                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                TipoSolicitudCotizacion TipoDocumento = (TipoSolicitudCotizacion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoSolicitudCotizacion), "Tipo Solicitud de Cotización");
                if ((SolicitudCotizacion.TipoDocumento == null) || (SolicitudCotizacion.TipoDocumento.Codigo != TipoDocumento.Codigo))
                {
                    SolicitudCotizacion.TipoDocumento = (TipoSolicitudCotizacion)HelperNHibernate.GetEntityByID("TipoSolicitudCotizacion", TipoDocumento.ID);
                    SolicitudCotizacion.GenerarNumCp();
                    try
                    {
                        FrmSelectedEntity FrmSeleccionarEmpleado = new FrmSelectedEntity();
                        String filtro = "IDUsuario='" + FrmMain.Usuario.ID + "'";
                        SocioNegocio sn = (SocioNegocio)FrmSeleccionarEmpleado.GetSelectedEntity(typeof(SocioNegocio), "Empleado", filtro);

                        SolicitudCotizacion.Responsable = (SocioNegocio)HelperNHibernate.GetEntityByID("SocioNegocio", sn.ID);
                    }
                    catch (Exception)
                    {
                    }
                }
                Mostrar();

            }
            catch (Exception ex)
            {

                Soft.Exceptions.SoftException.ShowException(ex);
            }


        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.Numeracion = txtNumeracion.Text;
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            String filtro = "Activo=1 ";

            if (FrmMain.Usuario.SuperAdministrador==false)
            {
                filtro = "UserID='" + FrmMain.Usuario.UserID + "'";
            }
            

            if (ssCliente.Text.Length > 0)
            {
                filtro = filtro + " and Nombre like '" + ssCliente.Text + "%'";
            }

            FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
            SolicitudCotizacion.Cliente = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Cliente", filtro);
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

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            SolicitudCotizacion.Observacion = txtObservacion.Text;
        }

        private void ubNuevaExistencia_Click(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
            Row.Tag = ItemSolicitudCotizacion.AddServicio();
            Row.Cells[colNombre].Activate();
            ugServicios.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
        }

        private void ubEliminarExistencia_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null) { return; }
            ItemSolicitudCotizacion.Servicios.Remove((ItemSolicitudCotizacionServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            SolicitudCotizacion.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssResponsable.Text = (SolicitudCotizacion.Responsable != null) ? SolicitudCotizacion.Responsable.Nombre : "";
        }

        public void AgregarServicios(String Codigo, String Descripcion, UltraGridRow Row)
        {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemSolicitudCotizacionServicio Item = (ItemSolicitudCotizacionServicio)Row.Tag;
            String Filtro = String.Format(" Codigo LIKE '{0}%' AND Nombre LIKE '{1}%'", Codigo, Descripcion);
            if (ItemSolicitudCotizacion.m_FiltroServicios.Length > 0) { Filtro += String.Format(" AND {0}", ItemSolicitudCotizacion.m_FiltroServicios); }
            Productos = FrmSeleccionar.GetSelectedsEntities(typeof(Existencia), "Selección de Servicios", Filtro);
            if (Productos.Count == 1)
            {
                Existencia Servicio = (Existencia)Productos[1];
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Servicio.ID);
                Item.CantidadFinal = 1;
                MostrarServicio(Row);
            }
            else if (Productos.Count > 1)
            {
                Existencia Producto = (Existencia)Productos[1];
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                Item.CantidadFinal = 1;
                MostrarServicio(Row);
                for (int i = 2; i <= Productos.Count; i++)
                {
                    UltraGridRow RowNuevo = ugServicios.DisplayLayout.Bands[0].AddNew();
                    ItemSolicitudCotizacionServicio ItemNuevo = ItemSolicitudCotizacion.AddServicio();
                    Existencia ProductoNuevo = (Existencia)Productos[i];
                    ItemNuevo.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ProductoNuevo.ID);
                    ItemNuevo.CantidadFinal = 1;
                    RowNuevo.Tag = ItemNuevo;
                    MostrarServicio(RowNuevo);
                }
            }
        }

        public void ugServicios_CellKeyEnter(UltraGridCell Cell)
        {
            try
            {
                if (Cell == null || ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacionServicio Item = (ItemSolicitudCotizacionServicio)Cell.Row.Tag;
                switch (Cell.Column.Key)
                {
                    case colNombre:
                        if (Cell.Text.Equals("")) { break; }
                        AgregarServicios("%", Cell.Text, Cell.Row);
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


        private void ubRecalcular_Click(object sender, EventArgs e)
        {
            //foreach (ItemSolicitudCotizacion Item in SolicitudCotizacion.Items)
            //{
            //    Item.CantidadFinal = Item.CantidadInicial * SolicitudCotizacion.Cantidad;
            //}
            //MostrarItems();
        }

        private void ssFormaPago_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SolicitudCotizacion.ModalidadCredito = (ModalidadCredito)FrmSeleccionar.GetSelectedEntity(typeof(ModalidadCredito), "Modalidad de Crédito");
            ssFormaPago.Text = (SolicitudCotizacion.ModalidadCredito != null) ? SolicitudCotizacion.ModalidadCredito.Descripcion : "";
        }

        private void utSolicitudCotizacion_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {
            UltraTreeNode Node = utSolicitudCotizacion.ActiveNode;
            if (Node != null)
            {
                ItemSolicitudCotizacion = (ItemSolicitudCotizacion)Node.Tag;
                utcItemSolicitid.Tabs[0].Text = Node.Text;
                MostrarItem(Node);
            }
        }

        public void DeshabilitarControles()
        {
            txtMedidaAbiertoLargo.Value = 0;
            txtMedidaAbiertoAlto.Value = 0;

            txtMedidaCerradaLargo.Value = 0;
            txtMedidaCerradaAlto.Value = 0;


            txtImpresoTiraColor.Value = 0;
            txtImpresoRetiraColor.Value = 0;
            txtCantidadItem.Value = 0;
            ssMaquina.Text = "";
            ssMaterial.Text = "";
            txtObservacionItem.Text = "";
            ClearAllRows(ref ugServicios);
            utcItemSolicitid.Enabled = false;
        }



        private void txtImpresoTiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.ImpresoTiraColor = Convert.ToInt32(txtImpresoTiraColor.Value);
        }

        private void txtImpresoRetiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.ImpresoRetiraColor = Convert.ToInt32(txtImpresoRetiraColor.Value);
        }

        private void ssMaquina_Search(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemSolicitudCotizacion.Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Máquina", ItemSolicitudCotizacion.m_FiltroMaquina);
            ssMaquina.Text = (ItemSolicitudCotizacion.Maquina != null) ? ItemSolicitudCotizacion.Maquina.Nombre : "";
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemSolicitudCotizacion.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", " EsInventariable = 1");
            ssMaterial.Text = (ItemSolicitudCotizacion.Material != null) ? ItemSolicitudCotizacion.Material.Nombre : "";
        }

        private void txtCantidadItem_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.Cantidad = Convert.ToInt32(txtCantidadItem.Value);
        }

        private void txtObservacionItem_TextChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.Observacion = txtObservacionItem.Text;
        }


        private void txtMedidaAbiertoLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.MedidaAbiertaLargo = Convert.ToInt32(txtMedidaAbiertoLargo.Value);
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.MedidaAbiertaAlto = Convert.ToInt32(txtMedidaAbiertoAlto.Value);
        }

        private void txtMedidaCerradaLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.MedidaCerradaLargo = Convert.ToInt32(txtMedidaCerradaLargo.Value);
        }

        private void txtMedidaCerradaAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemSolicitudCotizacion == null) { return; }
            ItemSolicitudCotizacion.MedidaCerradaAlto = Convert.ToInt32(txtMedidaCerradaAlto.Value);
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
            SolicitudCotizacion.Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");
            ssMoneda.Text = (SolicitudCotizacion.Moneda != null) ? SolicitudCotizacion.Moneda.Simbolo : "";
        }

        private void ssContacto_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarContacto = new FrmSelectedEntity();
            SolicitudCotizacion.Contacto = (ItemSocioNegocioContacto)FrmSeleccionarContacto.GetSelectedEntity(typeof(ItemSocioNegocioContacto), "Contacto", String.Format("IDSocioNegocio = '{0}'", SolicitudCotizacion.Cliente.ID));
            ssContacto.Text = (SolicitudCotizacion.Contacto != null) ? SolicitudCotizacion.Contacto.Nombre : "";
        }

        private void ssDireccionEntrega_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
            ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionEntrega = 1", SolicitudCotizacion.Cliente.ID));
            if (Direccion != null)
            {
                SolicitudCotizacion.DireccionEntrega = Direccion.Direccion;
                ssDireccionEntrega.Text = Direccion.Direccion;
            }
        }

        private void ssDireccionFactura_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
            ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionFacturacion = 1", SolicitudCotizacion.Cliente.ID));
            if (Direccion != null)
            {
                SolicitudCotizacion.DireccionFacturacion = Direccion.Direccion;
                ssDireccionFactura.Text = Direccion.Direccion;
            }
        }


    }
}
