using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class TipoMaquina : Parent 
    {
        public TipoMaquina() {}
        public virtual String Descripcion { get; set; }
    }
}
