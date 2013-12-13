using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ListaPreciosExistencia : Parent 
    {
        public ListaPreciosExistencia()
        {
            Items = new List<ItemListaPreciosExistencia>();
        }

        public virtual String Codigo { get; set; }
        public virtual IList<ItemListaPreciosExistencia> Items { get; set; }

        public virtual String FiltroExistencias 
        {
            get {
                String Filtro = "ID NOT IN (";
                String IDs = "";
                foreach (ItemListaPreciosExistencia Item in Items)
                {
                    IDs = IDs + "'" + Item.Existencia.ID + "',";
                }
                Filtro = (IDs.Length > 0)?Filtro + IDs.Substring(0, IDs.Length - 1) + ")" : "";
                return Filtro;
            }
        }

        public virtual ItemListaPreciosExistencia AddItem(Existencia Existencia)
        {
            ItemListaPreciosExistencia Item = new ItemListaPreciosExistencia();
            Item.Existencia = Existencia;
            Items.Add(Item);
            return Item;
        }

    }
}
