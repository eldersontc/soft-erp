using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class MetodoImpresion:Parent
    {
        public MetodoImpresion() {
            if (NewInstance) {
                Activo = true;
            }
        }

        public virtual String Codigo { get; set; }
        public virtual Int32 FactorPases { get; set; }
        public virtual Int32 FactorCambios { get; set; }


    }
}
