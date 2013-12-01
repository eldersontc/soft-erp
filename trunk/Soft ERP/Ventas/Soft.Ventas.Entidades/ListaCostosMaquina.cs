using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ListaCostosMaquina : Parent 
    {
        public ListaCostosMaquina() {
            Items = new List<ItemListaCostosMaquina>();
        }

        public virtual String Codigo { get; set; }
        public virtual IList<ItemListaCostosMaquina> Items { get; set; }

        public virtual String FiltroMaquinas 
        {
            get {
                String Filtro = "ID NOT IN (";
                String IDs = "";
                foreach (ItemListaCostosMaquina Item in Items)
                {
                    IDs = IDs + "'" + Item.Maquina.ID + "',";
                }
                Filtro = (IDs.Length > 0)?Filtro + IDs.Substring(0, IDs.Length - 1) + ")" : "";
                return Filtro;
            }
        }

        public virtual ItemListaCostosMaquina AddItem(Maquina Maquina) {
            ItemListaCostosMaquina Item = new ItemListaCostosMaquina();
            Item.Maquina = Maquina;
            Items.Add(Item);
            return Item;
        }

    }
}
