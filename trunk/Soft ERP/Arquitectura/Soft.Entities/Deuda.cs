using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    public class Deuda : Parent
    {
        public Deuda() { }

        public virtual string Tipo { get; set; }
        public virtual string IDDocumento { get; set; }
        public virtual string TipoDocumento { get; set; }
        public virtual string Descripcion { get; set; }
        public virtual string IDSocioNegocio { get; set; }
        public virtual decimal Saldo { get; set; }
        public virtual decimal Total { get; set; }
    }
}
