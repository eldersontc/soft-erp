using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Seguridad.Entidades;

namespace Soft.Entities
{
    [Serializable]
    public class SocioNegocioEmpleado : Parent
    {
        public SocioNegocioEmpleado() { }
        public virtual Area Area { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
