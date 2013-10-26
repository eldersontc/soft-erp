using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class ItemEntradaInventario : Parent
    {
        public ItemEntradaInventario() { }
        public virtual Existencia Producto { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual Decimal Precio { get; set; }
        public virtual Decimal Cantidad { get; set; }
        public virtual Decimal Total { get; set; }

    }
}
