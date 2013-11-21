using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemPlantilla
    {
        public ItemPlantilla() { }
        public virtual Existencia Existencia { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual Decimal Cantidad { get; set; }
    }
}
