﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Ventas.Entidades;
using Soft.Entities;

namespace Soft.Ventas.Win
{
    public class GenerarSolicitudDesdePlantilla : ControllerApp 
    {
        public override void  Start()
        {
            try 
	        {
                Plantilla Plantilla = (Plantilla)base.m_ObjectFlow;
                SolicitudCotizacion Solicitud = new SolicitudCotizacion();
                Solicitud.Cantidad = 1;
                Solicitud.Descripcion = Plantilla.Nombre;
                Solicitud.TipoDocumento = (TipoSolicitudCotizacion)HelperNHibernate.GetEntityByID("TipoSolicitudCotizacion", "B8AA5B25-9180-44A6-B750-F96D1EA17147");
                Solicitud.Observacion = "";
                Solicitud.LineaProduccion = Plantilla.LineaProduccion;
                foreach (ItemPlantilla Item in Plantilla.Items)
                {
                    ItemSolicitudCotizacion ItemSolicitud = Solicitud.AddItem();
                    ItemSolicitud.Nombre = Item.Nombre;

                    if (Item.TipoUnidad != null) {
                        ItemSolicitud.TipoUnidad = Item.TipoUnidad.Nombre;
                    }
                    ItemSolicitud.Operacion = Item.Operacion;
                    ItemSolicitud.Material = Item.Material;
                    ItemSolicitud.m_FiltroMaquina = Item.ObtenerFiltroMaquinas();
                    ItemSolicitud.TieneMedidaAbierta = Item.TieneMedidaAbierta;
                    ItemSolicitud.TieneMedidaCerrada = Item.TieneMedidaCerrada;
                    ItemSolicitud.TieneTiraRetira = Item.TieneTiraRetira;
                    ItemSolicitud.TieneGraficos = Item.TieneGraficos;
                    ItemSolicitud.TieneMaquina = Item.TieneMaquina;
                    ItemSolicitud.TieneMaterial = Item.TieneMaterial;
                    ItemSolicitud.TieneTipoUnidad = Item.TieneTipoUnidad;
                    ItemSolicitud.UnidadMedidaAbierta = Item.UnidadMedidaAbierta;
                    ItemSolicitud.TieneFondo = Item.TieneFondo;

                    foreach (ItemPlantillaServicio ItemServicio in Item.Servicios)
                    {
                        ItemSolicitudCotizacionServicio ItemSolicitudSerivcio = ItemSolicitud.AddServicio();
                        ItemSolicitudSerivcio.Servicio = ItemServicio.Servicio;
                        ItemSolicitudSerivcio.Unidad = ItemServicio.Unidad;
                        ItemSolicitudSerivcio.CantidadInicial = ItemServicio.Cantidad;
                        ItemSolicitudSerivcio.CantidadFinal = ItemServicio.Cantidad;
                    }
                }
                base.m_ObjectFlow = Solicitud;
                base.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", "1DEDB5BA-376B-41CE-9923-29B6CF61B9E6");
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
