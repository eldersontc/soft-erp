using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class RelacionMedidas : Parent
    {
        public RelacionMedidas() { }
        //public virtual String Nombre { get; set; }
        public virtual Int32 Abierta { get; set; }
        public virtual Int32 Cerrada { get; set; }
    }
}
