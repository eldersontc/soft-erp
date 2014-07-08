using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemSolicitudCotizacion  : ItemDocumento  
    {
        public ItemSolicitudCotizacion() {
            Servicios = new List<ItemSolicitudCotizacionServicio>();
        }

        public virtual IList<ItemSolicitudCotizacionServicio> Servicios { get; set; }
        public virtual Decimal MedidaAbierta { get; set; }
        public virtual Decimal MedidaCerrada { get; set; }
        public virtual Int32 ImpresoTiraColor { get; set; }
        public virtual Int32 ImpresoRetiraColor { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Maquina Maquina { get; set; }
        public virtual Existencia Material { get; set; }

        public virtual Existencia Operacion { get; set; }

        public virtual String TipoUnidad { get; set; }
        public virtual Decimal MedidaAbiertaLargo { get; set; }
        public virtual Decimal MedidaAbiertaAlto { get; set; }
        public virtual Decimal MedidaCerradaLargo { get; set; }
        public virtual Decimal MedidaCerradaAlto { get; set; }
        public virtual String m_FiltroMaquina { get; set; }
        public virtual Boolean TieneMedidaAbierta { get; set; }
        public virtual Boolean TieneMedidaCerrada { get; set; }
        public virtual Boolean TieneTiraRetira { get; set; }
        public virtual Boolean TieneGraficos { get; set; }
        public virtual Boolean TieneMaterial { get; set; }
        public virtual Boolean TieneMaquina { get; set; }

        public virtual Decimal CantidadItem { get; set; }


        public virtual Boolean TieneTipoUnidad { get; set; }


        public virtual Boolean TieneFondo { get; set; }
        
        public virtual String UnidadMedidaAbierta { get; set; }


        public virtual Int32 CantidadUnidad { get; set; }

        public virtual decimal MedidaAnchoCaja { get; set; }


        public virtual ItemSolicitudCotizacionServicio AddServicio()
        {
            ItemSolicitudCotizacionServicio Item = new ItemSolicitudCotizacionServicio();
            Servicios.Add(Item);
            return Item;
        }

        public virtual ItemSolicitudCotizacion Copiar()
        {
            ItemSolicitudCotizacion ItemCopia = new ItemSolicitudCotizacion();
            ItemCopia.Nombre = Nombre;
            ItemCopia.ImpresoTiraColor = ImpresoTiraColor;
            ItemCopia.ImpresoRetiraColor = ImpresoRetiraColor;
            ItemCopia.Observacion = Observacion;
            ItemCopia.Maquina = Maquina;
            ItemCopia.Material = Material;
            ItemCopia.TipoUnidad = TipoUnidad;
            ItemCopia.MedidaAbiertaLargo = MedidaAbiertaLargo;
            ItemCopia.MedidaAbiertaAlto = MedidaAbiertaAlto;
            ItemCopia.MedidaCerradaLargo = MedidaCerradaLargo;
            ItemCopia.MedidaCerradaAlto = MedidaCerradaAlto;
            ItemCopia.TieneMedidaAbierta = TieneMedidaAbierta;
            ItemCopia.TieneMedidaCerrada = TieneMedidaCerrada;
            ItemCopia.TieneTiraRetira = TieneTiraRetira;
            ItemCopia.TieneGraficos = TieneGraficos;
            ItemCopia.TieneMaquina = TieneMaquina;
            ItemCopia.TieneMaterial = TieneMaterial;
            ItemCopia.CantidadItem = CantidadItem;
            ItemCopia.Operacion = Operacion;
            foreach (ItemSolicitudCotizacionServicio ItemServicio in Servicios)
            {
                ItemSolicitudCotizacionServicio ItemServicioCopia = new ItemSolicitudCotizacionServicio();
                ItemServicioCopia.Servicio = ItemServicio.Servicio;
                ItemServicioCopia.Unidad = ItemServicio.Unidad;
                ItemServicioCopia.CantidadInicial = ItemServicio.CantidadInicial;
                ItemServicioCopia.CantidadFinal = ItemServicio.CantidadFinal;
                ItemCopia.Servicios.Add(ItemServicioCopia);
            }
            return ItemCopia;
        }

    }
}
