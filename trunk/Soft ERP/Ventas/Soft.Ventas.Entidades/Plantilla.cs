using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;
using Soft.Produccion.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Plantilla : Parent
    {
        public Plantilla() {
            Items = new List<ItemPlantilla>();
        }
        public virtual String Codigo { get; set; }

        public virtual LineaProduccion LineaProduccion { get; set; }

        public virtual IList<ItemPlantilla> Items { get; set; }


        public virtual ItemPlantilla CrearItem() {
            ItemPlantilla Item = new ItemPlantilla();
            Items.Add(Item);
            return Item;
        }
    }
}
