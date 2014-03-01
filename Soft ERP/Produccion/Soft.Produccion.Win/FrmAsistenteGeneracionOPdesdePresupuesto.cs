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
using Soft.Exceptions;
using Soft.Entities;
using Soft.DataAccess;
using NHibernate;
using System.Data.SqlClient;
using System.Xml;
using Soft.Produccion.Transaccional;
using Soft.Produccion.Entidades;


namespace Soft.Produccion.Win
{
    public partial class FrmAsistenteGeneracionOPdesdePresupuesto : FrmParent
    {
        public FrmAsistenteGeneracionOPdesdePresupuesto()
        {
            InitializeComponent();
            Iniciar();
        }

        private InfoAsistenteGeneracionOPdesdePresupuesto InfoAsistente = null;

        public Presupuesto Presupuesto { get { return (Presupuesto)base.m_ObjectFlow; } }


        public void Iniciar()
        {
            InfoAsistente = new InfoAsistenteGeneracionOPdesdePresupuesto();
            Mostrar();
        }


        private Boolean ActualizandoIU = false;
        public void Mostrar() {
            ActualizandoIU = true;
            ssTipoDocumento.Text = (InfoAsistente.TipoDocumento != null) ? InfoAsistente.TipoDocumento.Nombre : "";
            ssCliente.Text = (InfoAsistente.Cliente != null) ? InfoAsistente.Cliente.Nombre : "";
            ssReponsable.Text = (InfoAsistente.Responsable != null) ? InfoAsistente.Responsable.Nombre : "";

            if (InfoAsistente.ItemPresupuesto != null)
            {
                Cotizacion coti = (Cotizacion)HelperNHibernate.GetEntityByID("Cotizacion", InfoAsistente.ItemPresupuesto.IDCotizacion);
                ssCotizacion.Text = (coti != null) ? coti.Descripcion : "";
            }
            else {
                ssCotizacion.Text = "";
            
            }
            
            ActualizandoIU = false;
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }


                InfoAsistente.Presupuesto = (Presupuesto)HelperNHibernate.GetEntityByID("Presupuesto", Presupuesto.ID);

                InfoAsistente.Cliente = InfoAsistente.Presupuesto.Cliente;
                InfoAsistente.FechaCreacion = InfoAsistente.Presupuesto.FechaCreacion;
                
                udtFechaCreacion.Value = InfoAsistente.Presupuesto.FechaCreacion;
                
                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                TipoOrdenProduccion TipoDocumento = (TipoOrdenProduccion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoOrdenProduccion), "Tipo Orden de Producción");
                if (TipoDocumento != null)
                {
                    InfoAsistente.TipoDocumento = TipoDocumento;
                    ssTipoDocumento.Text = InfoAsistente.TipoDocumento.Nombre;
                    InfoAsistente.Responsable = ObtenerResponsable();
                    ssReponsable.Text = (InfoAsistente.Responsable != null) ? InfoAsistente.Responsable.Nombre : "";
                }
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }


        public SocioNegocio ObtenerResponsable()
        {
           
            XmlDocument XML = HelperNHibernate.ExecuteSQL("SELECT TOP (1) IDSocioNegocio FROM SocioNegocioEmpleado ", String.Format(" IDUsuario = '{0}'", FrmMain.Usuario.ID));
            SocioNegocio Responsable = null;
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    Responsable = (SocioNegocio)HelperNHibernate.GetEntityByID("SocioNegocio", NodoItem.SelectSingleNode("@IDSocioNegocio").Value);
                }
            }
            return Responsable;
        }

        private void ssTipoDocumento_Clear(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                InfoAsistente.Responsable = null;
                ssReponsable.Text = (InfoAsistente.Responsable != null) ? InfoAsistente.Responsable.Nombre : "";
                
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssReponsable_Search(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
                InfoAsistente.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                ssReponsable.Text = (InfoAsistente.Responsable != null) ? InfoAsistente.Responsable.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssReponsable_Clear(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                InfoAsistente.Responsable = null;
                ssReponsable.Text = (InfoAsistente.Responsable != null) ? InfoAsistente.Responsable.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCotizacion_Search(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                String Filtro = " IDPresupuesto='" + InfoAsistente.Presupuesto.ID + "'";
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                ItemPresupuesto itempresupuesto = (ItemPresupuesto)FrmSeleccionar.GetSelectedEntity(typeof(ItemPresupuesto), "Cotizaciones de Presupuesto", Filtro);

                if (itempresupuesto != null) {
                    itempresupuesto = (ItemPresupuesto)HelperNHibernate.GetEntityByID("ItemPresupuesto", itempresupuesto.ID);
                    InfoAsistente.ItemPresupuesto = itempresupuesto;
                    Cotizacion coti = (Cotizacion)HelperNHibernate.GetEntityByID("Cotizacion", InfoAsistente.ItemPresupuesto.IDCotizacion);
                    ssCotizacion.Text = (coti != null) ? coti.Descripcion : "";
                }
                
                
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCotizacion_Clear(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                InfoAsistente.ItemPresupuesto = null;
                ssCotizacion.Text = "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubGenerarOrdenProducccion_Click(object sender, EventArgs e)
        {
            try
            {
                
                GenerarOrdenProduccionDesdePresupuesto Generacion = new GenerarOrdenProduccionDesdePresupuesto();
                Generacion.m_ObjectFlow = InfoAsistente;
                Generacion.Start();
                OrdenProduccion OrdenProduccion = (OrdenProduccion)Generacion.m_ObjectFlow;
                IniciarOrdenProduccion(OrdenProduccion);
                ssCotizacion.Text = "";

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }



        public virtual void IniciarOrdenProduccion(OrdenProduccion OrdenProduccion)
        {
            FrmOrdenProduccion FrmSolicitud = new FrmOrdenProduccion();
            FrmSolicitud.m_ObjectFlow = OrdenProduccion;
            FrmSolicitud.m_Modal = true;
            FrmSolicitud.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", "42E79D78-CC98-4024-89CF-56E160AF52D4");
            
            
            
            FrmSolicitud.Start();
            if (FrmSolicitud.m_ResultProcess == EnumResult.SUCESS)
            {
                /*CreateEntity Crear = new CreateEntity();
                Crear.m_ObjectFlow = FrmSolicitud.m_ObjectFlow;
                Crear.Start();*/
                CrearOrdenProduccion validar = new CrearOrdenProduccion();
                validar.m_ObjectFlow = FrmSolicitud.m_ObjectFlow;
                validar.Start();

                CrearOrdenProduccioNumeracion numeracion = new CrearOrdenProduccioNumeracion();
                numeracion.m_ObjectFlow = FrmSolicitud.m_ObjectFlow;
                numeracion.Start();


                FrmMain.RefreshView();
            }
        }
       


    }
}
