using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Plantilla : Parent
    {
        public Plantilla() {
            Items = new List<ItemPlantilla>();
        }
        public virtual String Codigo { get; set; }
        public virtual IList<ItemPlantilla> Items { get; set; }

        public virtual ItemPlantilla CrearItem(Existencia Existencia) {
            ItemPlantilla Item = new ItemPlantilla();
            Item.Existencia = Existencia;
            Item.Unidad = Existencia.UnidadBase();
            Item.Cantidad = 1;
            Items.Add(Item);
            return Item;
        }
    }
}
