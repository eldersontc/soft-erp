using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class ColumnaPanel: Parent 
    {
        public ColumnaPanel() {
        }

        public virtual String CampoSQL { get; set; }
        public virtual int Ancho { get; set; }
        public virtual Boolean Visible { get; set; }
        public virtual String Estilo { get; set; }
        public virtual String Propiedad { get; set; }
        public virtual Boolean Establecer { get; set; }
        public virtual int Orden { get; set; }
    }
}
