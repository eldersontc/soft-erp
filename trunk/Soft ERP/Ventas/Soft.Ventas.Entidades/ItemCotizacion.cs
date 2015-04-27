using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemCotizacion : ItemDocumento 
    {
        public ItemCotizacion() {
            Servicios = new List<ItemCotizacionServicio>();

            RequerimientosMaterial = new List<RequerimientoMaterialItemCotizacion>();
            RequerimientosServicio = new List<RequerimientoServicioItemCotizacion>();

            IncluirEnPrespuesto = true;
            MuestraPrecioEnPresupuesto = true;
        }

        public virtual bool IncluirEnPrespuesto { get; set; }

        public virtual IList<ItemCotizacionServicio> Servicios { get; set; }
        public virtual IList<RequerimientoMaterialItemCotizacion> RequerimientosMaterial { get; set; }
        public virtual IList<RequerimientoServicioItemCotizacion> RequerimientosServicio { get; set; }


        public virtual Int32 ImpresoTiraColor { get; set; }
        public virtual Int32 ImpresoRetiraColor { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Existencia Operacion { get; set; }

        public virtual String LabelMaterial { get; set; }
        public virtual String LabelMaterialAlmancen { get; set; }
        public virtual String LabelProduccion { get; set; }

        public virtual Boolean MuestraPrecioEnPresupuesto { get; set; }


        public virtual Boolean TieneFondo { get; set; }
        


        public virtual decimal MedidaAnchoCaja { get; set; }

        private Maquina mMaquina;
        public virtual Maquina Maquina
        {
            get
            {
                return mMaquina;
            }
            set
            {
                mMaquina = value;
            }
        }

        public virtual Existencia Material { get; set; }

        public virtual String TipoUnidad { get; set; }

        private Decimal mMedidaAbiertaLargo;
        public virtual Decimal MedidaAbiertaLargo
        {
            get
            {
                return mMedidaAbiertaLargo;
            }
            set
            {
                mMedidaAbiertaLargo = value;
            }
        }

        private Decimal mMedidaAbiertaAlto;
        public virtual Decimal MedidaAbiertaAlto
        {
            get
            {
                return mMedidaAbiertaAlto;
            }
            set
            {
                mMedidaAbiertaAlto = value;
            }
        }

        public virtual Decimal MedidaCerradaLargo { get; set; }
        public virtual Decimal MedidaCerradaAlto { get; set; }
        public virtual Decimal Costo { get; set; }
        public virtual Decimal CostoMaquina { get; set; }
        public virtual Decimal CostoMaterial { get; set; }
        public virtual Decimal CostoTransporte { get; set; }
        public virtual bool TieneMedidaAbierta { get; set; }
        public virtual bool TieneMedidaCerrada { get; set; }
        public virtual bool TieneTiraRetira { get; set; }
        public virtual Int32 SeparacionX { get; set; }
        public virtual Int32 SeparacionY { get; set; }
        
        //Nuevos
        public virtual Decimal FormatoImpresionLargo { get; set; }
        public virtual Decimal FormatoImpresionAlto { get; set; }
        public virtual bool GraficoPrecorteGirado { get; set; }
        public virtual bool GraficoImpresionGirado { get; set; }
        public virtual Int32 NroPiezasPrecorte { get; set; }
        public virtual Int32 NroPiezasImpresion { get; set; }
        public virtual String MetodoImpresion { get; set; }
        public virtual bool TieneGraficos { get; set; }
        public virtual bool TieneMaterial { get; set; }
        public virtual bool TieneMaquina { get; set; }

        public virtual Int32 NumerodePases { get; set; }
        public virtual Decimal CantidadMaterial { get; set; }
        public virtual Decimal CantidadProduccion { get; set; }
        public virtual Decimal CantidadDemasia { get; set; }

        public virtual Decimal CantidadElemento { get; set; }

        public virtual Decimal CostoServicio { get; set; }

        public virtual Decimal CantidadUnidad { get; set; }

        public virtual Decimal CantidadDemasiaMaterial { get; set; }

        public virtual Int32 NumeroPliegos { get; set; }

        public virtual Boolean GraficoImpresionManual { get; set; }

        public virtual Boolean TieneTipoUnidad { get; set; }
        public virtual String UnidadMedidaAbierta { get; set; }


        public virtual Int32 PaginasSobrantes { get; set; }

        public virtual Int32 NumeroCambios { get; set; }


        public virtual MetodoImpresion MetodoImpresionOffset { get; set; }
        

        public virtual ItemCotizacionServicio AddServicio()
        {
            ItemCotizacionServicio Item = new ItemCotizacionServicio();
            Servicios.Add(Item);
            return Item;
        }



        public virtual ItemCotizacion Copiar()
        {
            ItemCotizacion ItemCopia = new ItemCotizacion();
            ItemCopia.ImpresoTiraColor = ImpresoTiraColor;
            ItemCopia.ImpresoRetiraColor = ImpresoRetiraColor;
            ItemCopia.ImpresoRetiraColor = ImpresoRetiraColor;
            ItemCopia.Cantidad = Cantidad;
            ItemCopia.Observacion = Observacion;
            ItemCopia.TipoUnidad = TipoUnidad;
            ItemCopia.Nombre = Nombre;

            ItemCopia.MedidaAbiertaLargo = MedidaAbiertaLargo;
            ItemCopia.MedidaAbiertaAlto = MedidaAbiertaAlto;
            ItemCopia.MedidaCerradaLargo = MedidaCerradaLargo;
            ItemCopia.MedidaCerradaAlto = MedidaCerradaAlto;
            ItemCopia.Precio = Precio;
            ItemCopia.Costo = Costo;
            ItemCopia.CostoMaquina = CostoMaquina;


            ItemCopia.CostoMaterial = CostoMaterial;
            ItemCopia.CostoTransporte = CostoTransporte;
            ItemCopia.TieneMedidaAbierta = TieneMedidaAbierta;
            ItemCopia.TieneMedidaCerrada = TieneMedidaCerrada;




            ItemCopia.TieneTiraRetira = TieneTiraRetira;
            ItemCopia.SeparacionX = SeparacionX;
            ItemCopia.SeparacionY = SeparacionY;
            ItemCopia.FormatoImpresionLargo = FormatoImpresionLargo;
            ItemCopia.FormatoImpresionAlto = FormatoImpresionAlto;
            ItemCopia.GraficoPrecorteGirado = GraficoPrecorteGirado;
            ItemCopia.GraficoImpresionGirado = GraficoImpresionGirado;
            ItemCopia.NroPiezasPrecorte = NroPiezasPrecorte;


            ItemCopia.NroPiezasImpresion = NroPiezasImpresion;

            ItemCopia.MetodoImpresion = MetodoImpresion;
            ItemCopia.TieneGraficos = TieneGraficos;
            ItemCopia.TieneMaterial = TieneMaterial;
            ItemCopia.TieneMaquina = TieneMaquina;
            ItemCopia.LabelMaterial = LabelMaterial;





            ItemCopia.LabelMaterialAlmancen = LabelMaterialAlmancen;
            ItemCopia.LabelProduccion = LabelProduccion;
            ItemCopia.TieneTipoUnidad = TieneTipoUnidad;
            ItemCopia.UnidadMedidaAbierta = UnidadMedidaAbierta;
            ItemCopia.NumerodePases = NumerodePases;
            ItemCopia.CantidadMaterial = CantidadMaterial;

            ItemCopia.CantidadProduccion = CantidadProduccion;
            ItemCopia.CantidadDemasia = CantidadDemasia;
            ItemCopia.CantidadElemento = CantidadElemento;
            ItemCopia.CostoServicio = CostoServicio;
            ItemCopia.CantidadUnidad = CantidadUnidad;


            ItemCopia.CantidadDemasiaMaterial = CantidadDemasiaMaterial;
            ItemCopia.NumeroPliegos = NumeroPliegos;
            ItemCopia.GraficoImpresionManual = GraficoImpresionManual;
            ItemCopia.MuestraPrecioEnPresupuesto = MuestraPrecioEnPresupuesto;


            ItemCopia.NumeroCambios = NumeroCambios;
            ItemCopia.MetodoImpresionOffset = MetodoImpresionOffset;
            ItemCopia.Operacion = Operacion;
            ItemCopia.Maquina = Maquina;


            ItemCopia.Material = Material;
            



            foreach (ItemCotizacionServicio ItemServicio in Servicios)
            {
                ItemCotizacionServicio ItemServicioCopia = new ItemCotizacionServicio();

                ItemServicioCopia.Servicio = ItemServicio.Servicio;

                ItemServicioCopia.UnidadServicio = ItemServicio.UnidadServicio;
                ItemServicioCopia.CantidadServicio = ItemServicio.CantidadServicio;
                ItemServicioCopia.CostoServicio = ItemServicio.CostoServicio;
                ItemServicioCopia.Maquina = ItemServicio.Maquina;
                ItemServicioCopia.UnidadMaquina = ItemServicio.UnidadMaquina;
                ItemServicioCopia.CantidadMaquina = ItemServicio.CantidadMaquina;
                ItemServicioCopia.CostoMaquina = ItemServicio.CostoMaquina;
                ItemServicioCopia.Material = ItemServicio.Material;
                ItemServicioCopia.UnidadMaterial = ItemServicio.UnidadMaterial;


                ItemServicioCopia.CantidadMaterial = ItemServicio.CantidadMaterial;

                ItemServicioCopia.CostoMaterial = ItemServicio.CostoMaterial;
                ItemServicioCopia.CostoTotalServicio = ItemServicio.CostoTotalServicio;
                ItemServicioCopia.EsAutogenerado = ItemServicio.EsAutogenerado;
                ItemCopia.Servicios.Add(ItemServicioCopia);

            }

            return ItemCopia;
        }



    }
}
