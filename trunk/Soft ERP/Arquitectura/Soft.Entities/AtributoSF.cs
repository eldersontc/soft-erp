using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class AtributoSF : Parent
    {
        public AtributoSF() { }

        public virtual String Campo { get; set; }
        public virtual String Propiedad { get; set; }
        public virtual Boolean Obligatorio { get; set; }
    }
}
