using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class ItemPresupuesto : ItemDocumento
    {
        public ItemPresupuesto() { }
        public virtual String IDCotizacion { get; set; }
    }
}
