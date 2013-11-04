using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Reporte.Entidades
{
    public class ParametroReporte : Parent
    {
        public ParametroReporte() { }
        public virtual String Valor { get; set; }
        public virtual String Tipo { get; set; }
        public virtual String Propiedad { get; set; }
    }
}
