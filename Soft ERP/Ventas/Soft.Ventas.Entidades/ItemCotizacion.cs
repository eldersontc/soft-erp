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
        }

        public virtual IList<ItemCotizacionServicio> Servicios { get; set; }
        public virtual Int32 ImpresoTiraColor { get; set; }
        public virtual Int32 ImpresoRetiraColor { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Existencia Operacion { get; set; }


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
        public virtual Boolean TieneMedidaAbierta { get; set; }
        public virtual Boolean TieneMedidaCerrada { get; set; }
        public virtual Boolean TieneTiraRetira { get; set; }
        public virtual Int32 SeparacionX { get; set; }
        public virtual Int32 SeparacionY { get; set; }
        
        //Nuevos
        public virtual Decimal FormatoImpresionLargo { get; set; }
        public virtual Decimal FormatoImpresionAlto { get; set; }
        public virtual Boolean GraficoPrecorteGirado { get; set; }
        public virtual Boolean GraficoImpresionGirado { get; set; }
        public virtual Int32 NroPiezasPrecorte { get; set; }
        public virtual Int32 NroPiezasImpresion { get; set; }
        public virtual String MetodoImpresion { get; set; }
        public virtual Boolean TieneGraficos { get; set; }
        public virtual Boolean TieneMaterial { get; set; }
        public virtual Boolean TieneMaquina { get; set; }

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

        public virtual ItemCotizacionServicio AddServicio()
        {
            ItemCotizacionServicio Item = new ItemCotizacionServicio();
            Servicios.Add(Item);
            return Item;
        }

    }
}
