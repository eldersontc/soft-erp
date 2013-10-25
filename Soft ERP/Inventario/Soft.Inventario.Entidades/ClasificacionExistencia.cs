using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class ClasificacionExistencia: Parent
    {

        public ClasificacionExistencia()
        {
            Items = new List<ItemClasificacionExistencia>();
        }

        public virtual String Codigo { get; set; }
        public virtual IList<ItemClasificacionExistencia> Items { get; set; }

        public virtual ItemClasificacionExistencia AddItem()
        {
            ItemClasificacionExistencia Item = new ItemClasificacionExistencia();
            Items.Add(Item);
            return Item;
        }

        public virtual ItemClasificacionExistencia ItemByName(String Nombre)
        {
            ItemClasificacionExistencia Item = this.Items.First(a => a.Nombre.Equals(Nombre));
            return Item;
        }

    }
}
