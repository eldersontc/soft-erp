using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Ventas.Entidades;
using Soft.Entities;
using Soft.Win;
using Soft.Exceptions;

namespace Soft.Ventas.Win
{
    public class GenerarCotizacionDesdeSolicitud : ControllerApp 
    {
        public override void  Start()
        {
            try 
	        {
                SolicitudCotizacion SolicitudCotizacion = (SolicitudCotizacion)base.m_ObjectFlow;
                if (!SolicitudCotizacion.EstadoAprobacion.Equals("APROBADO"))
                {
                    throw new Exception(String.Format("La Solicitud de Cotización Nº : {0} no está APROBADO",SolicitudCotizacion.Numeracion));
                }
                if (!SolicitudCotizacion.EstadoCotizacion.Equals("PENDIENTE"))
                {
                    throw new Exception(String.Format("La Solicitud de Cotización Nº : {0} ya tiene una Cotizacion", SolicitudCotizacion.Numeracion));
                }

                Cotizacion Cotizacion = new Cotizacion();
                Cotizacion.Cantidad = 1;
                Cotizacion.Descripcion = SolicitudCotizacion.Descripcion;
                Cotizacion.Cantidad = SolicitudCotizacion.Cantidad;
                Cotizacion.Cliente = SolicitudCotizacion.Cliente;
                Cotizacion.ModalidadCredito = SolicitudCotizacion.ModalidadCredito;
                Cotizacion.TipoDocumento = (TipoCotizacion)HelperNHibernate.GetEntityByID("TipoCotizacion", "B8AA5B25-9180-44A6-B750-F96D1EA17147");
                Cotizacion.Contacto = SolicitudCotizacion.Contacto;
                Cotizacion.Vendedor = SolicitudCotizacion.Responsable;
                Cotizacion.Observacion = SolicitudCotizacion.Observacion;
                Cotizacion.Moneda = SolicitudCotizacion.Moneda;
                Cotizacion.IDSolicitudCotizacion = SolicitudCotizacion.ID;

                Cotizacion.LineaProduccion = SolicitudCotizacion.LineaProduccion;
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
                    ItemCotizacion.MedidaAnchoCaja = Item.MedidaAnchoCaja;
                    ItemCotizacion.Cantidad = 1;
                    ItemCotizacion.CantidadUnidad = Item.Cantidad;
                    ItemCotizacion.CantidadElemento = Item.CantidadItem;
                    ItemCotizacion.Operacion = Item.Operacion;
                    ItemCotizacion.Maquina = Item.Maquina;

                    if (ItemCotizacion.Maquina != null) {
                        ItemCotizacion.FormatoImpresionAlto = ItemCotizacion.Maquina.PliegoAltoMaximo;
                        ItemCotizacion.FormatoImpresionLargo=ItemCotizacion.Maquina.PliegoAnchoMaximo;
                    }

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

                    ItemCotizacion.TieneTipoUnidad = Item.TieneTipoUnidad;
                    ItemCotizacion.UnidadMedidaAbierta = Item.UnidadMedidaAbierta;
                    ItemCotizacion.CantidadUnidad = Item.CantidadUnidad;
                    foreach (ItemSolicitudCotizacionServicio ItemServicio in Item.Servicios)
                    {
                        ItemCotizacionServicio ItemCotizacionServicio = ItemCotizacion.AddServicio();
                        ItemCotizacionServicio.Servicio = ItemServicio.Servicio;
                    }
                    ItemCotizacion.Observacion = Item.Observacion;
                }
                base.m_ObjectFlow = Cotizacion;
                base.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", "11D3E3C0-1639-49FF-8596-149E9D24F60A");
                base.m_ResultProcess = EnumResult.SUCESS;
	        }
	        catch (Exception ex)
	        {
                base.m_ResultProcess = EnumResult.ERROR;
                SoftException.ShowException(ex);
	        }
 	        base.Start();
        }
    }
}
