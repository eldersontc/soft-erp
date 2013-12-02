using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ListaPreciosTransporte : Parent 
    {
        public ListaPreciosTransporte() {
            Items = new List<ItemListaPreciosTransporte>();
        }

        public virtual String Codigo { get; set; }
        public virtual IList<ItemListaPreciosTransporte> Items { get; set; }

        public virtual ItemListaPreciosTransporte AddItem()
        {
            ItemListaPreciosTransporte Item = new ItemListaPreciosTransporte();
            Items.Add(Item);
            return Item;
        }

    }
}
