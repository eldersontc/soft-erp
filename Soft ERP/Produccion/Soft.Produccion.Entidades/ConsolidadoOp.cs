using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Produccion.Entidades
{
    public class ConsolidadoOp : Parent
    {
        public ConsolidadoOp()
        {
            Items = new List<ItemConsolidadoOp>();
            FechaCreacion = DateTime.Now; 
            if (NewInstance){
                FechaCreacion = DateTime.Now;
				EstadoAprobacion = "PENDIENTE";
            }
        }

        public virtual String Numeracion { get; set; }
        public virtual TipoDocumentoConsolidadoOp TipoDocumento { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual String EstadoAprobacion { get; set; }
        public virtual IList<ItemConsolidadoOp> Items { get; set; }

        public virtual SocioNegocio Responsable { get; set; }

        public virtual ItemConsolidadoOp AddItem(OrdenProduccion OrdenProduccion)
        {
            ItemConsolidadoOp Item = new ItemConsolidadoOp();
            Item.IDOrdenProduccion = OrdenProduccion.ID;
            Items.Add(Item);
            return Item;
        }

        public virtual String ObtenerFiltroOPs() {
            String Filtro = "";
            foreach (ItemConsolidadoOp Item in Items)
            {
                if (Filtro.Length > 0) { Filtro += ","; }
                Filtro += String.Format("'{0}'", Item.IDOrdenProduccion);
            }
            return Filtro;
        }

        public virtual ItemConsolidadoOp ObtenerItem(String IDItem)
        {
            return (ItemConsolidadoOp)Items.First(Item => ((ItemConsolidadoOp)Item).IDOrdenProduccion.Equals(IDItem));
        }

        public virtual ItemConsolidadoOp AddItem()
        {
            ItemConsolidadoOp Item = new ItemConsolidadoOp();
            Items.Add(Item);
            return Item;
        }

    }
}
