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
using Soft.Exceptions;

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
            ssTipoDocumento.Text = (SolicitudCotizacion.TipoDocumento != null) ? SolicitudCotizacion.TipoDocumento.Nombre : "";
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
            txtCodigoGrupo.Value = SolicitudCotizacion.CodigoGrupo;
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
                MostrarItem(Item);
            }
            if (utSolicitudCotizacion.Nodes.Count > 0)
            {
                utSolicitudCotizacion.ActiveNode = utSolicitudCotizacion.Nodes[0];
                utSolicitudCotizacion.Nodes[0].Selected = true;
            }
            utSolicitudCotizacion.ExpandAll();
        }

        public void MostrarItem(ItemSolicitudCotizacion Item)
        {
            //ItemSolicitudCotizacion Item = (ItemSolicitudCotizacion)Node.Tag;
            GrupoMedidaAbierta.Visible = Item.TieneMedidaAbierta;
            GrupoMedidaCerrada.Visible = Item.TieneMedidaCerrada;
            GruposTiras.Visible = Item.TieneTiraRetira;
            ssMaquina.Visible = Item.TieneMaquina;
            lblMaquina.Visible = Item.TieneMaquina;
            ssMaterial.Visible = Item.TieneMaterial;
            lblMaterial.Visible = Item.TieneMaterial;
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
            //txtNombre.Text = Item.Nombre;
            chkTieneMedidaAbierta.Checked = Item.TieneMedidaAbierta;
            chkTieneMedidadCerrada.Checked = Item.TieneMedidaCerrada;
            chkTieneTiraRetira.Checked = Item.TieneTiraRetira;
            chkTieneGraficos.Checked = Item.TieneGraficos;
            chkTieneMaquina.Checked = Item.TieneMaquina;
            chkTieneMaterial.Checked = Item.TieneMaterial;
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
                //Mostrar();
                ssTipoDocumento.Text = (SolicitudCotizacion.TipoDocumento != null) ? SolicitudCotizacion.TipoDocumento.Nombre : "";
                ssResponsable.Text = (SolicitudCotizacion.Responsable != null) ? SolicitudCotizacion.Responsable.Nombre : "";

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SolicitudCotizacion.Numeracion = txtNumeracion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            try
            {
                String filtro = "Activo=1 ";
                if (FrmMain.Usuario.SuperAdministrador == false)
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
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SolicitudCotizacion.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SolicitudCotizacion.Descripcion = txtDescripcion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void uneCantidad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SolicitudCotizacion.Cantidad = Convert.ToInt32(uneCantidad.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SolicitudCotizacion.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubNuevaExistencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ItemSolicitudCotizacion.AddServicio();
                AgregarServicios("%", "%", Row);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubEliminarExistencia_Click(object sender, EventArgs e)
        {
            try
            {
                if (ugServicios.ActiveRow == null) { return; }
                ItemSolicitudCotizacion.Servicios.Remove((ItemSolicitudCotizacionServicio)ugServicios.ActiveRow.Tag);
                ugServicios.ActiveRow.Delete(false);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
                SolicitudCotizacion.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                ssResponsable.Text = (SolicitudCotizacion.Responsable != null) ? SolicitudCotizacion.Responsable.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        public void AgregarServicios(String Codigo, String Descripcion, UltraGridRow Row)
        {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemSolicitudCotizacionServicio Item = (ItemSolicitudCotizacionServicio)Row.Tag;
            String Filtro = String.Format(" Codigo LIKE '{0}%' AND Nombre LIKE '{1}%'", Codigo, Descripcion);
            //if (ItemSolicitudCotizacion.m_FiltroServicios.Length > 0) { Filtro += String.Format(" AND {0}", ItemSolicitudCotizacion.m_FiltroServicios); }
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

        private void ssFormaPago_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                SolicitudCotizacion.ModalidadCredito = (ModalidadCredito)FrmSeleccionar.GetSelectedEntity(typeof(ModalidadCredito), "Modalidad de Crédito");
                ssFormaPago.Text = (SolicitudCotizacion.ModalidadCredito != null) ? SolicitudCotizacion.ModalidadCredito.Descripcion : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void utSolicitudCotizacion_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {
            try
            {
                UltraTreeNode Node = utSolicitudCotizacion.ActiveNode;
                if (Node != null)
                {
                    ItemSolicitudCotizacion = (ItemSolicitudCotizacion)Node.Tag;
                    utcItemSolicitid.Tabs[0].Text = Node.Text;
                    txtNombre.Text = Node.Text;
                    MostrarItem(ItemSolicitudCotizacion);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
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
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.ImpresoTiraColor = Convert.ToInt32(txtImpresoTiraColor.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtImpresoRetiraColor_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.ImpresoRetiraColor = Convert.ToInt32(txtImpresoRetiraColor.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMaquina_Search(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                ItemSolicitudCotizacion.Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Máquina", ItemSolicitudCotizacion.m_FiltroMaquina);
                ssMaquina.Text = (ItemSolicitudCotizacion.Maquina != null) ? ItemSolicitudCotizacion.Maquina.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                ItemSolicitudCotizacion.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", " EsInventariable = 1");
                ssMaterial.Text = (ItemSolicitudCotizacion.Material != null) ? ItemSolicitudCotizacion.Material.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtCantidadItem_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.Cantidad = Convert.ToInt32(txtCantidadItem.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtObservacionItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.Observacion = txtObservacionItem.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }


        private void txtMedidaAbiertoLargo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.MedidaAbiertaLargo = Convert.ToDecimal(txtMedidaAbiertoLargo.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.MedidaAbiertaAlto = Convert.ToDecimal(txtMedidaAbiertoAlto.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtMedidaCerradaLargo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.MedidaCerradaLargo = Convert.ToDecimal(txtMedidaCerradaLargo.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtMedidaCerradaAlto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.MedidaCerradaAlto = Convert.ToDecimal(txtMedidaCerradaAlto.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
                SolicitudCotizacion.Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");
                ssMoneda.Text = (SolicitudCotizacion.Moneda != null) ? SolicitudCotizacion.Moneda.Simbolo : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssContacto_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarContacto = new FrmSelectedEntity();
                SolicitudCotizacion.Contacto = (ItemSocioNegocioContacto)FrmSeleccionarContacto.GetSelectedEntity(typeof(ItemSocioNegocioContacto), "Contacto", String.Format("IDSocioNegocio = '{0}'", SolicitudCotizacion.Cliente.ID));
                ssContacto.Text = (SolicitudCotizacion.Contacto != null) ? SolicitudCotizacion.Contacto.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssDireccionEntrega_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
                ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionEntrega = 1", SolicitudCotizacion.Cliente.ID));
                if (Direccion != null)
                {
                    SolicitudCotizacion.DireccionEntrega = Direccion.Direccion;
                    ssDireccionEntrega.Text = Direccion.Direccion;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssDireccionFactura_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
                ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionFacturacion = 1", SolicitudCotizacion.Cliente.ID));
                if (Direccion != null)
                {
                    SolicitudCotizacion.DireccionFacturacion = Direccion.Direccion;
                    ssDireccionFactura.Text = Direccion.Direccion;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void btnEliminarElemento_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                SolicitudCotizacion.Items.Remove(ItemSolicitudCotizacion);
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void btnCopiarElemento_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion ItemCopia = ItemSolicitudCotizacion.Copiar();
                SolicitudCotizacion.Items.Add(ItemCopia);
                MostrarItems();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void chkTieneMedidaAbierta_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.TieneMedidaAbierta = chkTieneMedidaAbierta.Checked;
                MostrarItem(ItemSolicitudCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void chkTieneMedidadCerrada_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.TieneMedidaCerrada = chkTieneMedidadCerrada.Checked;
                MostrarItem(ItemSolicitudCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void chkTieneTiraRetira_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.TieneTiraRetira = chkTieneTiraRetira.Checked;
                MostrarItem(ItemSolicitudCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void chkTieneGraficos_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.TieneGraficos = chkTieneGraficos.Checked;
                MostrarItem(ItemSolicitudCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void chkTieneMaterial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.TieneMaterial = chkTieneMaterial.Checked;
                MostrarItem(ItemSolicitudCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void chkTieneMaquina_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.TieneMaquina = chkTieneMaquina.Checked;
                MostrarItem(ItemSolicitudCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemSolicitudCotizacion == null) { return; }
                ItemSolicitudCotizacion.Nombre = txtNombre.Text;
                utSolicitudCotizacion.ActiveNode.Text = txtNombre.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }


    }
}
