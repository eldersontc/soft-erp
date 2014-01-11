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
        public virtual Int32 Cantidad { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Maquina Maquina { get; set; }
        public virtual Existencia Material { get; set; }
        public virtual String TipoUnidad { get; set; }
        public virtual Decimal MedidaAbiertaLargo { get; set; }
        public virtual Decimal MedidaAbiertaAlto { get; set; }
        public virtual Decimal MedidaCerradaLargo { get; set; }
        public virtual Decimal MedidaCerradaAlto { get; set; }
        public virtual Decimal Costo { get; set; }
        public virtual Decimal Precio { get; set; }
        public virtual Decimal CostoMaquina { get; set; }
        public virtual Decimal CostoMaterial { get; set; }
        public virtual Decimal CostoTransporte { get; set; }
        public virtual Boolean TieneMedidaAbierta { get; set; }
        public virtual Boolean TieneMedidaCerrada { get; set; }
        public virtual Boolean TieneTiraRetira { get; set; }
        
        public virtual ItemCotizacionServicio AddServicio()
        {
            ItemCotizacionServicio Item = new ItemCotizacionServicio();
            Servicios.Add(Item);
            return Item;
        }



    }
}
