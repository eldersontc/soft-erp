using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Entities;
using Soft.Ventas.Entidades;
using Soft.Exceptions;

namespace Soft.Ventas.Win
{
    public class GenerarRendicionCotizacionDesdeCotizacion : ControllerApp
    {
        public override void Start()
        {
            try
            {
                Cotizacion Cotizacion = (Cotizacion)base.m_ObjectFlow;

                if (!Cotizacion.EstadoAprobacion.Equals("APROBADO"))
                {
                    throw new Exception(String.Format("La Cotización Nº : {0} aún no ha sido APROBADA.", Cotizacion.Numeracion));
                }

                if (Cotizacion.EstadoRendicion.Equals("TOTAL"))
                {
                    throw new Exception(String.Format("La Cotización Nº : {0} ya ha sido RENDIDA.", Cotizacion.Numeracion));
                }

                RendicionCotizacion Rendicion = new RendicionCotizacion();
                Rendicion.IDCotizacion = Cotizacion.ID;
                Rendicion.NumeroCotizacion = Cotizacion.Numeracion;
                foreach (ItemCotizacion ItemCotizacion in Cotizacion.Items)
                {
                    if (ItemCotizacion.Maquina != null) 
                    {
                        ItemRendicionCotizacion ItemRendicion = new ItemRendicionCotizacion();
                        ItemRendicion.Codigo = ItemCotizacion.Maquina.Codigo;
                        ItemRendicion.Descripcion = ItemCotizacion.Maquina.Descripcion;
                        ItemRendicion.CantidadCotizacion = 1;
                        ItemRendicion.PrecioCotizacion = ItemCotizacion.CostoMaquina;
                        ItemRendicion.TotalCotizacion = ItemCotizacion.CostoMaquina / 1;
                        ItemRendicion.CantidadReal = 1;
                        ItemRendicion.PrecioReal = ItemCotizacion.CostoMaquina;
                        ItemRendicion.TotalReal = ItemCotizacion.CostoMaquina / 1;
                        Rendicion.Items.Add(ItemRendicion);
                    }
                    foreach (ItemCotizacionServicio ItemServicio in ItemCotizacion.Servicios)
                    {
                        if (ItemServicio.Servicio != null && ItemServicio.UnidadServicio != null) 
                        {
                            ItemRendicionCotizacion ItemRendicion = new ItemRendicionCotizacion();
                            ItemRendicion.Codigo = ItemServicio.Servicio.Codigo;
                            ItemRendicion.Descripcion = ItemServicio.Servicio.Descripcion;
                            ItemRendicion.Unidad = ItemServicio.UnidadServicio.Unidad.Codigo;
                            ItemRendicion.CantidadCotizacion = ItemServicio.CantidadServicio;
                            ItemRendicion.PrecioCotizacion = ItemServicio.CostoServicio / ItemServicio.CantidadServicio;
                            ItemRendicion.TotalCotizacion = ItemServicio.CostoServicio;
                            ItemRendicion.CantidadReal = ItemServicio.CantidadServicio;
                            ItemRendicion.PrecioReal = ItemServicio.CostoServicio / ItemServicio.CantidadServicio;
                            ItemRendicion.TotalReal = ItemServicio.CostoServicio;
                            Rendicion.Items.Add(ItemRendicion);
                        }
                        if (ItemServicio.Maquina != null && ItemServicio.UnidadMaquina != null) 
                        {
                            ItemRendicionCotizacion ItemRendicion = new ItemRendicionCotizacion();
                            ItemRendicion.Codigo = ItemServicio.Maquina.Codigo;
                            ItemRendicion.Descripcion = ItemServicio.Maquina.Descripcion;
                            ItemRendicion.Unidad = ItemServicio.UnidadMaquina.Codigo;
                            ItemRendicion.CantidadCotizacion = ItemServicio.CantidadMaquina;
                            ItemRendicion.PrecioCotizacion = ItemServicio.CostoMaquina / ItemServicio.CantidadMaquina;
                            ItemRendicion.TotalCotizacion = ItemServicio.CostoMaquina;
                            ItemRendicion.CantidadReal = ItemServicio.CantidadMaquina;
                            ItemRendicion.PrecioReal = ItemServicio.CostoMaquina / ItemServicio.CantidadMaquina;
                            ItemRendicion.TotalReal = ItemServicio.CostoMaquina;
                            Rendicion.Items.Add(ItemRendicion);
                        }
                    }
                }
                base.m_ObjectFlow = Rendicion;
                base.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByField("EntidadSF","NombreClase", "RendicionCotizacion");
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
