using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class Banco : Parent 
    {
        public Banco() {
            if (NewInstance) {
                Activo = true;
            }
        
        }

        public virtual String Codigo { get; set; }
    }
}
