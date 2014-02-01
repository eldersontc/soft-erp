using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Ventas.Entidades;
using Soft.Entities;
using Soft.Win;

namespace Soft.Ventas.Win
{
    public class GenerarCotizacionDesdeSolicitud : ControllerApp 
    {
        public override void  Start()
        {
            try 
	        {
                SolicitudCotizacion SolicitudCotizacion = (SolicitudCotizacion)base.m_ObjectFlow;
                Cotizacion Cotizacion = new Cotizacion();
                Cotizacion.Cantidad = 1;
                Cotizacion.Descripcion = SolicitudCotizacion.Descripcion;
                Cotizacion.Cantidad = SolicitudCotizacion.Cantidad;
                Cotizacion.Cliente = SolicitudCotizacion.Cliente;
                Cotizacion.ModalidadCredito = SolicitudCotizacion.ModalidadCredito;
                Cotizacion.TipoDocumento = (TipoCotizacion)HelperNHibernate.GetEntityByID("TipoCotizacion", "B8AA5B25-9180-44A6-B750-F96D1EA17147");
                Cotizacion.Contacto = SolicitudCotizacion.Contacto;
                Cotizacion.Vendedor = SolicitudCotizacion.Responsable;
                Cotizacion.Observacion = String.Format("Generado desde la Solicitud - {0}", SolicitudCotizacion.Numeracion);
                Cotizacion.Moneda = SolicitudCotizacion.Moneda;

                String filtro = "";
                if (Cotizacion.Moneda != null)
                {
                    if (Cotizacion.Moneda.Simbolo.Equals("US $"))
                    {
                        filtro = "IDMoneda='" + Cotizacion.Moneda.ID + "' and Fecha='" + Cotizacion.FechaCreacion.Date + "'";
                        FrmSelectedEntity FrmSelectedMoneda = new FrmSelectedEntity();
                        TipoCambio tc = (TipoCambio)FrmSelectedMoneda.GetSelectedEntity(typeof(TipoCambio), "Tipo de Cambio", filtro);
                        Cotizacion.TipoCambioFecha = tc.TipoCambioVenta;
                    }
                    else
                    {
                        Cotizacion.TipoCambioFecha = 1;
                    }
                }


                foreach (ItemSolicitudCotizacion Item in SolicitudCotizacion.Items)
                {
                    ItemCotizacion ItemCotizacion = Cotizacion.AddItem();
                    ItemCotizacion.Nombre = Item.Nombre;
                    ItemCotizacion.Cantidad = 1;
                    ItemCotizacion.CantidadElemento = Item.Cantidad;
                    ItemCotizacion.Maquina = Item.Maquina;
                    ItemCotizacion.TipoUnidad = Item.TipoUnidad;
                    ItemCotizacion.Material = Item.Material;
                    ItemCotizacion.ImpresoRetiraColor = Item.ImpresoRetiraColor;
                    ItemCotizacion.ImpresoTiraColor = Item.ImpresoTiraColor;
                    ItemCotizacion.MedidaAbiertaAlto = Item.MedidaAbiertaAlto;
                    ItemCotizacion.MedidaAbiertaLargo = Item.MedidaAbiertaLargo;
                    ItemCotizacion.MedidaCerradaAlto = Item.MedidaCerradaAlto;
                    ItemCotizacion.MedidaCerradaLargo = Item.MedidaCerradaLargo;
                    ItemCotizacion.TieneMedidaAbierta = Item.TieneMedidaAbierta;
                    ItemCotizacion.TieneMedidaCerrada = Item.TieneMedidaCerrada;
                    ItemCotizacion.TieneTiraRetira = Item.TieneTiraRetira;
                    ItemCotizacion.TieneGraficos = Item.TieneGraficos;
                    ItemCotizacion.TieneMaquina = Item.TieneMaquina;
                    ItemCotizacion.TieneMaterial = Item.TieneMaterial;

                    foreach (ItemSolicitudCotizacionServicio ItemServicio in Item.Servicios)
                    {
                        ItemCotizacionServicio ItemCotizacionServicio = ItemCotizacion.AddServicio();
                        ItemCotizacionServicio.Servicio = ItemServicio.Servicio;
                    }
                }
                base.m_ObjectFlow = Cotizacion;
                base.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", "11D3E3C0-1639-49FF-8596-149E9D24F60A");
                base.m_ResultProcess = EnumResult.SUCESS;
	        }
	        catch (Exception)
	        {
                base.m_ResultProcess = EnumResult.ERROR;
	        }
 	        base.Start();
        }
    }
}
