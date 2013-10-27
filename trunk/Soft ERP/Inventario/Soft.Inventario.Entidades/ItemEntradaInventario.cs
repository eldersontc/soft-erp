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

        private Decimal mPrecio;
        public virtual Decimal Precio { get { return mPrecio; } set { mPrecio = value; } }

        private Decimal mCantidad;
        public virtual Decimal Cantidad { get { return mCantidad; } set { mCantidad = value; } }

        private Decimal mTotal;
        public virtual Decimal Total { get { return Precio * Cantidad; } set { mTotal = value; } }

    }
}
