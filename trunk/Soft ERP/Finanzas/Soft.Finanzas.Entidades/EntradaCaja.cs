using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Finanzas.Entidades
{
    public class EntradaCaja : DocumentoGenerico
    {
        public EntradaCaja()
        {
            FechaCreacion = DateTime.Now;
            Items = new List<ItemEntradaCaja>();
        }

        public virtual Caja Caja { get; set; }
        public virtual SocioNegocio SocioNegocio { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual IList<ItemEntradaCaja> Items { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Impuesto { get; set; }
        public virtual decimal Total { get; set; }

        public virtual TipoCaja TipoCaja { get { return (TipoCaja)TipoDocumento;} }

        public virtual ItemEntradaCaja AddItem()
        {
            ItemEntradaCaja Item = new ItemEntradaCaja();
            Items.Add(Item);
            return Item;
        }

        public virtual void CalcularTotales()
        {
            decimal Subtotal = 0;
            foreach (ItemEntradaCaja Item in Items)
            {
                Subtotal += Item.Total;
            }
            this.SubTotal = Subtotal;
            this.Impuesto = (TipoDocumento != null && TipoDocumento.PorcentajeImpuesto > 0) ? (SubTotal * (TipoDocumento.PorcentajeImpuesto / 100)) : 0;
            this.Total = SubTotal + Impuesto;
        }
    }
}
