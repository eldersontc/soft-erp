using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemSolicitudCotizacionServicio : Parent 
    {
        public ItemSolicitudCotizacionServicio() { }
        public virtual Existencia Servicio { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual Decimal CantidadInicial { get; set; }
        public virtual Decimal CantidadFinal { get; set; }
    }
}
