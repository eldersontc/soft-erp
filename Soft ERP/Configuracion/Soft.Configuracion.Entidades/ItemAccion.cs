using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class ItemAccion: Parent 
    {
        public ItemAccion() { }

        public virtual String Clase { get; set; }
        public virtual Ensamblado Ensamblado { get; set; }
        public virtual String Parametro { get; set; }
        public virtual String Exito { get; set; }
        public virtual String Error { get; set; }
        public virtual Int32 Orden { get; set; }
    }
}
