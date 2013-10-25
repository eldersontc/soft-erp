using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Seguridad.Entidades
{
    [Serializable]
    public class Empresa : Parent 
    {
        public Empresa() { }
        public virtual String RazonSocial { get; set; }
        public virtual String RUC { get; set; }
    }
}
