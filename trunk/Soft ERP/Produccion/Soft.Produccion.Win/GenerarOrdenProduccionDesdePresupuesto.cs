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

    

                foreach (ItemCotizacion ItemCoti in cotizacion.Items)
                {
                    ItemOrdenProduccion ItemOrdenProduccion = Op.AddItem();

                    ItemCotizacion Item = (ItemCotizacion)HelperNHibernate.GetEntityByID("ItemCotizacion", ItemCoti.ID);

                    ItemOrdenProduccion.Nombre = Item.Nombre;
                    ItemOrdenProduccion.Cantidad = Item.Cantidad;

                    ItemOrdenProduccion.MedidaAnchoCaja = Item.MedidaAnchoCaja;
                    ItemOrdenProduccion.TieneMedidaAbierta = Item.TieneMedidaAbierta;
                    ItemOrdenProduccion.TieneMedidaCerrada = Item.TieneMedidaCerrada;
                    ItemOrdenProduccion.TieneTiraRetira = Item.TieneTiraRetira;
                    ItemOrdenProduccion.TieneGraficos = Item.TieneGraficos;
                    ItemOrdenProduccion.TieneMaterial = Item.TieneMaterial;
                    ItemOrdenProduccion.TieneMaquina = Item.TieneMaquina;
                    ItemOrdenProduccion.MedidaAbiertaLargo = Item.MedidaAbiertaLargo;
                    ItemOrdenProduccion.MedidaAbiertaAlto = Item.MedidaAbiertaAlto;
                    ItemOrdenProduccion.MedidaCerradaLargo = Item.MedidaCerradaLargo;
                    ItemOrdenProduccion.MedidaCerradaAlto = Item.MedidaCerradaAlto;
                    ItemOrdenProduccion.ImpresoRetiraColor = Item.ImpresoRetiraColor;
                    ItemOrdenProduccion.ImpresoTiraColor = Item.ImpresoTiraColor;
                    ItemOrdenProduccion.TipoUnidad = Item.TipoUnidad;
                    ItemOrdenProduccion.Observacion = Item.Observacion;
                    ItemOrdenProduccion.Material = Item.Material;
                    ItemOrdenProduccion.Maquina = Item.Maquina;
                    ItemOrdenProduccion.SeparacionX = Item.SeparacionX;
                    ItemOrdenProduccion.SeparacionY = Item.SeparacionY;
                    ItemOrdenProduccion.FormatoImpresionLargo = Item.FormatoImpresionLargo;
                    ItemOrdenProduccion.FormatoImpresionAlto = Item.FormatoImpresionAlto;
                    ItemOrdenProduccion.GraficoPrecorteGirado = Item.GraficoPrecorteGirado;
                    ItemOrdenProduccion.GraficoImpresionGirado = Item.GraficoImpresionGirado;
                    ItemOrdenProduccion.NroPiezasPrecorte = Item.NroPiezasPrecorte;
                    ItemOrdenProduccion.NroPiezasImpresion = Item.NroPiezasImpresion;
                    ItemOrdenProduccion.MetodoImpresion = Item.MetodoImpresion;
                    ItemOrdenProduccion.NumerodePases = Item.NumerodePases;
                    ItemOrdenProduccion.CantidadMaterial = Item.CantidadMaterial;
                    ItemOrdenProduccion.CantidadProduccion = Item.CantidadProduccion;
                    ItemOrdenProduccion.CantidadDemasia = Item.CantidadDemasia;
                    ItemOrdenProduccion.CantidadElemento = Item.CantidadElemento;
                    ItemOrdenProduccion.Operacion = Item.Operacion;
                    ItemOrdenProduccion.CantidadUnidad = Item.CantidadUnidad;
                    ItemOrdenProduccion.CantidadDemasiaMaterial = Item.CantidadDemasiaMaterial;
                    ItemOrdenProduccion.NumeroPliegos = Item.NumeroPliegos;
                    ItemOrdenProduccion.GraficoImpresionManual = Item.GraficoImpresionManual;
                    ItemOrdenProduccion.TieneTipoUnidad = Item.TieneTipoUnidad;
                    ItemOrdenProduccion.UnidadMedidaAbierta = Item.UnidadMedidaAbierta;
                    ItemOrdenProduccion.LabelMaterial = Item.LabelMaterial;
                    ItemOrdenProduccion.LabelMaterialAlmancen = Item.LabelMaterialAlmancen;
                    ItemOrdenProduccion.LabelProduccion = Item.LabelProduccion;

              

                    foreach (ItemCotizacionServicio ItemServicio in Item.Servicios)
                    {
                        ItemOrdenProduccionServicio ItemOPServicio = ItemOrdenProduccion.AddServicio();
                        ItemOPServicio.Servicio = ItemServicio.Servicio;
                        ItemOPServicio.UnidadServicio = ItemServicio.UnidadServicio;
                        ItemOPServicio.CantidadServicio = ItemServicio.CantidadServicio;
                        ItemOPServicio.Material = ItemServicio.Material;
                        ItemOPServicio.UnidadMaterial = ItemServicio.UnidadMaterial;
                        ItemOPServicio.CantidadMaterial = ItemServicio.CantidadMaterial;
                        
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
