using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class NotaDebito : DocumentoGenerico
    {
        public NotaDebito() {
            FechaCreacion = DateTime.Now;
            Items = new List<ItemNotaDebito>(); 
        }

        //public virtual TipoNotaDebito TipoNotaDebito { get; set; }
        //public virtual string Numeracion { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Moneda Moneda { get; set; }
        //public virtual DateTime FechaCreacion { get; set; }
        //public virtual string Observacion { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Impuesto { get; set; }
        public virtual decimal Total { get; set; }
        public virtual string NroFactura { get; set; }
        public virtual string IDFactura { get; set; }
        public virtual IList<ItemNotaDebito> Items { get; set; }

        public virtual TipoNotaDebito TipoNotaDebito { get { return (TipoNotaDebito)TipoDocumento; } }

        public virtual ItemNotaDebito AddItem()
        {
            ItemNotaDebito Item = new ItemNotaDebito();
            Items.Add(Item);
            return Item;
        }

        public virtual void CalcularTotales()
        {
            decimal Subtotal = 0;
            foreach (ItemNotaDebito Item in Items)
            {
                Subtotal += Item.Total;
            }
            this.SubTotal = Subtotal;
            this.Impuesto = (TipoNotaDebito != null && TipoNotaDebito.PorcentajeImpuesto > 0) ? (SubTotal * (TipoNotaDebito.PorcentajeImpuesto / 100)) : 0;
            this.Total = SubTotal + Impuesto;
        }
    }
}
