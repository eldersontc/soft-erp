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
        public override Decimal Total { get; set; }

        private Decimal mTotalFinal;
        public virtual Decimal TotalFinal {
            get { return mTotalFinal; }
            set {
                mTotalFinal = value;
                Recargo = TotalFinal - Total;
                } 
        }

        public virtual Decimal Recargo { get; set; }
    }
}
