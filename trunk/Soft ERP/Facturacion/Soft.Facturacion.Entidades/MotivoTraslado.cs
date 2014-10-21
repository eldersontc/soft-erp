using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class MotivoTraslado: Parent
    {
        public MotivoTraslado(){
            if (NewInstance) {
                Activo = true;
            }

        }


        public virtual String Codigo { get; set; }
        public virtual Int32 Numero { get; set; }

    }
}
