using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    public class Alerta : Parent
    {
        public virtual string IDUsuario { get; set; }
        public virtual string IDEntidad { get; set; }
        public virtual string Entidad { get; set; }
        public virtual string Identificador { get; set; }
        public virtual string Estado { get; set; }
        public virtual string Tipo { get; set; }
        public virtual DateTime Fecha { get; set; }
    }
}
