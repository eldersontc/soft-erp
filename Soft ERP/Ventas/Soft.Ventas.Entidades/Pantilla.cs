using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Pantilla
    {
        public Pantilla() {
            Items = new List<ItemPlantilla>();
        }
        public virtual String Codigo { get; set; }
        public virtual IList<ItemPlantilla> Items { get; set; }
    }
}
