using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class SocioNegocioCliente : Parent
    {
        public SocioNegocioCliente() { }
        public virtual SocioNegocioEmpleado SocioNegocioEmpleado { get; set; }
    }
}
