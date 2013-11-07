using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class SocioNegocio : Parent
    {
        public SocioNegocio() { Aniversario = DateTime.Now; }

        public virtual TipoSocioNegocio TipoSocioNegocio { get; set; }
        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual DateTime Aniversario { get; set; }
        public virtual Boolean Cliente { get; set; }
        public virtual Boolean Proveedor { get; set; }
        public virtual Boolean Empleado { get; set; }
    }
}
