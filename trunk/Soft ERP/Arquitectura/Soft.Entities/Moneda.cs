using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class Moneda : Parent 
    {
        public Moneda() { }

        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual String Simbolo { get; set; }
    }
}
