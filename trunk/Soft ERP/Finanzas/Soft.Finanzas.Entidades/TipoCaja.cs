using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Finanzas.Entidades
{
    public class TipoCaja : TipoDocumento 
    {
        public TipoCaja() { }

        public virtual Caja Caja { get; set; }
        public virtual string Movimiento { get; set; }
        public virtual bool GeneraDeuda { get; set; }
        public virtual string TipoDeuda { get; set; }
    }
}
