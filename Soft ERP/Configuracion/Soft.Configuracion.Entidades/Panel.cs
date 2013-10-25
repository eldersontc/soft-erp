using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class Panel: Parent 
    {
        public Panel() { Columnas = new List<ColumnaPanel>();}
        
        public virtual String NombreVista { get; set; }
        public virtual EntidadSF EntidadSF { get; set; }
        public virtual IList<ColumnaPanel> Columnas { get; set; }
    }
}
