using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemCotizacionServicio : Parent 
    {
        public ItemCotizacionServicio() { }
        public virtual Existencia Servicio { get; set; }
        public virtual Existencia Material { get; set; }
        public virtual Maquina Maquina { get; set; }

        public virtual Unidad UnidadServicio { get; set; }
        public virtual Decimal CantidadServicio { get; set; }
        public virtual Decimal CostoServicio { get; set; }
        public virtual Decimal CantidadMaquina { get; set; }
        public virtual Decimal CostoMaquina { get; set; }

        public virtual Unidad UnidadMaterial { get; set; }
        public virtual Decimal CantidadMaterial { get; set; }
        public virtual Decimal CostoMaterial { get; set; }
        public virtual Decimal CostoTotalServicio { get; set; }
    }
}
