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
        public virtual String TipoUnidad { get; set; }
        public virtual Decimal MedidaAbiertaLargo { get; set; }
        public virtual Decimal MedidaAbiertaAlto { get; set; }
        public virtual Decimal MedidaCerradaLargo { get; set; }
        public virtual Decimal MedidaCerradaAlto { get; set; }
        public virtual String m_FiltroMaquina { get; set; }
        public virtual String m_FiltroServicios { get; set; }

        public virtual ItemSolicitudCotizacionServicio AddServicio()
        {
            ItemSolicitudCotizacionServicio Item = new ItemSolicitudCotizacionServicio();
            Servicios.Add(Item);
            return Item;
        }

    }
}
