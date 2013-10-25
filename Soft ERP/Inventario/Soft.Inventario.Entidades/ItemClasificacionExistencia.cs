using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class ItemClasificacionExistencia: Parent
    {
        public ItemClasificacionExistencia() { 
        }


        public virtual String Codigo { get; set; }
        public virtual String Nombre { get; set; }
        
    }
}
