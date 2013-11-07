using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class TipoCambio : Parent
    {
        public TipoCambio() { Fecha = DateTime.Now; }
        
        public virtual Decimal TipoCambioCompra { get; set; }
        public virtual Decimal TipoCambioVenta { get; set; }
        public virtual DateTime Fecha { get; set; }
        public virtual Moneda Moneda { get; set; }
    }
}
