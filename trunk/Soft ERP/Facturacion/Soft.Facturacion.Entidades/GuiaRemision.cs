using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class GuiaRemision : Parent
    {
        public GuiaRemision()
        {
            FechaCreacion = DateTime.Now;
            Items = new List<ItemGuiaRemision>(); 
        }

        public virtual TipoEntrega TipoEntrega { get; set; }
        public virtual string Numeracion { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual string Observacion { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Impuesto { get; set; }
        public virtual decimal Total { get; set; }
        public virtual IList<ItemGuiaRemision> Items { get; set; }

        public virtual void AddItem(string IDOP) {
            ItemGuiaRemision Item = new ItemGuiaRemision();
            Item.IDOrdenProduccion = IDOP;
            Items.Add(Item);
        }

        public virtual ItemGuiaRemision ObtenerItem(String IDItem)
        {
            return (ItemGuiaRemision)Items.First(Item => ((ItemGuiaRemision)Item).IDOrdenProduccion.Equals(IDItem));
        }

        public virtual string ObtenerFiltroOps()
        {
            string Filtro = "";
            foreach (ItemGuiaRemision Item in Items)
            {
                if (Filtro.Length > 0) { Filtro += ","; }
                Filtro += String.Format("'{0}'", Item.IDOrdenProduccion);
            }
            return Filtro;
        }

        
    }
}
