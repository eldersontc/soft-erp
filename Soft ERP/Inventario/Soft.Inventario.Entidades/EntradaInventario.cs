using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class EntradaInventario : Parent
    {
        public EntradaInventario() { 
            Items = new List<ItemEntradaInventario>();
            FechaCreacion = DateTime.Now;
        }

        public virtual String Numeracion { get; set; }
        public virtual String OrdenCompra { get; set; }
        public virtual String Factura { get; set; }
        public virtual String Observacion { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual TipoDocumentoInventario TipoDocumento { get; set; }
        public virtual SocioNegocio Proveedor { get; set; }
        public virtual Almacen Almacen { get; set; }
        public virtual SocioNegocio Responsable { get; set; }

        private Decimal mSubTotal;
        public virtual Decimal SubTotal {
            get {
                Decimal SubTotal = 0;
                foreach (ItemEntradaInventario Item in Items)
                {
                    SubTotal += Item.Total;
                }
                //mSubTotal = SubTotal;
                return SubTotal; 
                }
            set { mSubTotal = value; }
        }

        private Decimal mImpuesto;
        public virtual Decimal Impuesto
        { 
            get {
                Decimal Impuesto = 0;
                if (SubTotal > 0 & TipoDocumento != null && TipoDocumento.TieneImpuesto) { 
                    Impuesto = SubTotal * (TipoDocumento.PorcentajeImpuesto / 100); 
                }
                return Impuesto; 
                }
            set { mImpuesto = value; }
        }

        private Decimal mTotal;
        public virtual Decimal Total
        { 
            get {
                Decimal Total = 0;
                if (SubTotal > 0) { Total = SubTotal + Impuesto; }
                return Total; 
                }
            set { mTotal = value; }
        }

        public virtual IList<ItemEntradaInventario> Items { get; set; }

        public virtual ItemEntradaInventario AddItem()
        {
            ItemEntradaInventario Item = new ItemEntradaInventario();
            Items.Add(Item);
            return Item;
        }

    }
}
