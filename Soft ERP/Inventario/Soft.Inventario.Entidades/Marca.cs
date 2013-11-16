using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class Marca : Parent 
    {
        public Marca() { }
        public virtual String Codigo { get; set; }    }
}
