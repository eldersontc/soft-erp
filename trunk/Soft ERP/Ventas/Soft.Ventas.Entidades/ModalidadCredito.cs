using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class ModalidadCredito : Parent 
    {
        public ModalidadCredito() { }
        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual Int32 Dias { get; set; }
    }
}
