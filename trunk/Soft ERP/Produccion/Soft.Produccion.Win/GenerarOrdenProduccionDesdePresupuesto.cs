using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Ventas.Entidades;
using Soft.Entities;
using Soft.Produccion.Entidades;

namespace Soft.Produccion.Win
{
    public class GenerarOrdenProduccionDesdePresupuesto : ControllerApp
    {

        public override void Start()
        {
            try
            {
                InfoAsistenteGeneracionOPdesdePresupuesto InfoAsisten = (InfoAsistenteGeneracionOPdesdePresupuesto)base.m_ObjectFlow;
                ItemPresupuesto ItemPresupuesto = (ItemPresupuesto)HelperNHibernate.GetEntityByID("ItemPresupuesto", InfoAsisten.ItemPresupuesto.ID);
                Cotizacion cotizacion = (Cotizacion)HelperNHibernate.GetEntityByID("Cotizacion", ItemPresupuesto.IDCotizacion);
                OrdenProduccion Op = new OrdenProduccion();

                Op.IDItemPresupuesto = ItemPresupuesto.ID;
                Op.Cantidad = cotizacion.Cantidad;
                Op.Descripcion = cotizacion.Descripcion;
                Op.TipoDocumento = (TipoOrdenProduccion)HelperNHibernate.GetEntityByID("TipoOrdenProduccion", InfoAsisten.TipoDocumento.ID);
                Op.Cliente = cotizacion.Cliente;
                Op.Responsable = InfoAsisten.Responsable;
                Op.Cotizador = cotizacion.Responsable;
                Op.Vendedor = cotizacion.Vendedor;
                Op.Observacion = cotizacion.Observacion;

                Op.ListaCostosMaquina = cotizacion.ListaCostosMaquina;
                Op.ListaPreciosExistencia = cotizacion.ListaPreciosExistencia;
                Op.ListaPreciosTransporte = cotizacion.ListaPreciosTransporte;

                Op.Contacto = cotizacion.Contacto;
                Op.DireccionEntrega = cotizacion.DireccionEntrega;
                Op.DireccionFacturacion = cotizacion.DireccionFacturacion;


                foreach (ItemCotizacion Item in cotizacion.Items)
                {
                    ItemOrdenProduccion ItemOrdenProduccion = Op.AddItem();

                    ItemOrdenProduccion.Nombre = Item.Nombre;
                    ItemOrdenProduccion.Cantidad = Item.Cantidad;
                    ItemOrdenProduccion.CantidadUnidad = Item.CantidadUnidad;
                    ItemOrdenProduccion.CantidadElemento = Item.CantidadElemento;
                    ItemOrdenProduccion.Operacion = Item.Operacion;
                    ItemOrdenProduccion.Maquina = Item.Maquina;

                    if (ItemOrdenProduccion.Maquina != null)
                    {
                        ItemOrdenProduccion.FormatoImpresionAlto = ItemOrdenProduccion.Maquina.PliegoAltoMaximo;
                        ItemOrdenProduccion.FormatoImpresionLargo = ItemOrdenProduccion.Maquina.PliegoAnchoMaximo;
                    }

                    ItemOrdenProduccion.TipoUnidad = Item.TipoUnidad;
                    ItemOrdenProduccion.Material = Item.Material;
                    ItemOrdenProduccion.ImpresoRetiraColor = Item.ImpresoRetiraColor;
                    ItemOrdenProduccion.ImpresoTiraColor = Item.ImpresoTiraColor;
                    ItemOrdenProduccion.MedidaAbiertaAlto = Item.MedidaAbiertaAlto;
                    ItemOrdenProduccion.MedidaAbiertaLargo = Item.MedidaAbiertaLargo;
                    ItemOrdenProduccion.MedidaCerradaAlto = Item.MedidaCerradaAlto;
                    ItemOrdenProduccion.MedidaCerradaLargo = Item.MedidaCerradaLargo;
                    ItemOrdenProduccion.TieneMedidaAbierta = Item.TieneMedidaAbierta;
                    ItemOrdenProduccion.TieneMedidaCerrada = Item.TieneMedidaCerrada;
                    ItemOrdenProduccion.TieneTiraRetira = Item.TieneTiraRetira;
                    ItemOrdenProduccion.TieneGraficos = Item.TieneGraficos;
                    ItemOrdenProduccion.TieneMaquina = Item.TieneMaquina;
                    ItemOrdenProduccion.TieneMaterial = Item.TieneMaterial;

                    ItemOrdenProduccion.TieneTipoUnidad = Item.TieneTipoUnidad;
                    ItemOrdenProduccion.UnidadMedidaAbierta = Item.UnidadMedidaAbierta;
                    ItemOrdenProduccion.CantidadUnidad = Item.CantidadUnidad;

                    ItemOrdenProduccion.FormatoImpresionAlto = Item.FormatoImpresionAlto;
                    ItemOrdenProduccion.FormatoImpresionLargo = Item.FormatoImpresionLargo;
                    ItemOrdenProduccion.SeparacionX = Item.SeparacionX;
                    ItemOrdenProduccion.SeparacionY = Item.SeparacionY;
                    ItemOrdenProduccion.GraficoImpresionManual = Item.GraficoImpresionManual;
                    ItemOrdenProduccion.MetodoImpresion = Item.MetodoImpresion;




                    if (ItemOrdenProduccion.MetodoImpresion != null) { 
                        if (ItemOrdenProduccion.MetodoImpresion.Equals("TIRA / RETIRA"))
                        {
                            ItemOrdenProduccion.NumerodePases = 2;
                        }
                        else if (ItemOrdenProduccion.MetodoImpresion.Length > 0)
                        {
                            ItemOrdenProduccion.NumerodePases = 1;
                        }
                    }
                    ItemOrdenProduccion.NumeroPliegos = Item.NumeroPliegos;
                    ItemOrdenProduccion.CantidadDemasia = Item.NumeroPliegos;
                    ItemOrdenProduccion.CantidadDemasiaMaterial = Item.CantidadDemasiaMaterial;
                    ItemOrdenProduccion.NroPiezasImpresion = Item.NroPiezasImpresion;
                    ItemOrdenProduccion.NroPiezasPrecorte = Item.NroPiezasPrecorte;
                    ItemOrdenProduccion.CantidadElemento = Item.CantidadElemento;
                    ItemOrdenProduccion.CantidadProduccion = Item.CantidadProduccion;

                    ItemOrdenProduccion.CalcularProduccionItem();

                    foreach (ItemCotizacionServicio ItemServicio in Item.Servicios)
                    {
                        ItemOrdenProduccionServicio ItemOPServicio = ItemOrdenProduccion.AddServicio();
                        ItemOPServicio.Servicio = ItemServicio.Servicio;
                    }

                }

                base.m_ObjectFlow = Op;
                base.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", "42E79D78-CC98-4024-89CF-56E160AF52D4");
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
