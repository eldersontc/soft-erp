using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class Ensamblado: Parent 
    {
        public Ensamblado() { }
        public virtual String Ensamblado_ { get; set; }
    }
}
