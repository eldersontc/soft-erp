using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class EscalaListaPreciosTransporte : Parent 
    {
        public EscalaListaPreciosTransporte() { }

        public virtual String Descripcion { get; set; }
        public virtual Int32 Desde { get; set; }
        public virtual Int32 Hasta { get; set; }
        public virtual Decimal Precio { get; set; }

    }
}
