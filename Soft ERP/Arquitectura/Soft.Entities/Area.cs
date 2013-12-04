using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class Area : Parent 
    {
        public Area()
        {
            if (NewInstance) {
                Activo = true;
            }
        
        }

        public virtual String Codigo { get; set; }
    }
}
