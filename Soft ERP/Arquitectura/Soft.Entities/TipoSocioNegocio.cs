using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class TipoSocioNegocio: Parent 
    {
        public TipoSocioNegocio() { }
        
        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }
    }
}
