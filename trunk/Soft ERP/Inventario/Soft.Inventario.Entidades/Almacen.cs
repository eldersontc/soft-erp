using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    public class Almacen : Parent 
    {
        public Almacen() { }
        public virtual String Codigo { get; set; }
        //public virtual String Nombre { get; set; }
        public virtual String Descripcion { get; set; }
        
    }
}
