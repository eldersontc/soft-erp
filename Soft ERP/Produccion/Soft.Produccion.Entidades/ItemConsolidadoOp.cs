using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Produccion.Entidades
{
    public class ItemConsolidadoOp : Parent
    {
        public ItemConsolidadoOp() { }
        public virtual String IDOrdenProduccion { get; set; }
        public virtual String Orden { get; set; }
 
    }
}
