using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class ItemContenedorAccion: Parent
    {
        public ItemContenedorAccion() { }
        public virtual Accion Accion { get; set; }
    }
}
