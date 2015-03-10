using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class ItemRendicionCotizacion: Parent
    {
        public virtual string Codigo { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string Unidad { get; set; }
        public virtual decimal CantidadCotizacion { get; set; }
        public virtual decimal PrecioCotizacion { get; set; }
        public virtual decimal TotalCotizacion { get; set; }
        public virtual decimal CantidadReal { get; set; }
        public virtual decimal PrecioReal { get; set; }
        public virtual decimal TotalReal { get; set; }
    }
}
