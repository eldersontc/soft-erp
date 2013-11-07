using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class ItemDocumento : Parent 
    {
        public ItemDocumento() { }

        private Decimal mPrecio;
        public virtual Decimal Precio { get { return mPrecio; } set { mPrecio = value; } }

        private Decimal mCantidad;
        public virtual Decimal Cantidad { get { return mCantidad; } set { mCantidad = value; } }

        private Decimal mTotal;
        public virtual Decimal Total { get { return Precio * Cantidad; } set { mTotal = value; } }
    }
}
