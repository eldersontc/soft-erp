using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Seguridad.Entidades
{
    public class Perfil: Parent 
    {
        public Perfil() { }
        public virtual String Abreviacion { get; set; }
    }
}
