using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Reporte.Entidades
{
    public class Reporte : Parent 
    {
        public Reporte() { Parametros = new List<ParametroReporte>(); }

        public virtual String Codigo { get; set; }
        public virtual String Ubicacion { get; set; }
        public virtual String SQL { get; set; }
        public virtual Boolean Especifico { get; set; }
        public virtual EntidadSF EntidadSF { get; set; }
        public virtual IList<ParametroReporte> Parametros { get; set; }

        public virtual ParametroReporte AddParametro()
        {
            ParametroReporte Parametro = new ParametroReporte();
            Parametros.Add(Parametro);
            return Parametro;
        }

        public virtual IList<ParametroReporte> ParametrosSQL 
        {
            get { return (IList<ParametroReporte>)Parametros.Select(p => p.Tipo.Equals("Propiedad")); }
        }

        public virtual IList<ParametroReporte> ParametrosCrystal
        {
            get { return (IList<ParametroReporte>)Parametros.Select(p => !p.Tipo.Equals("Propiedad")); }
        }

    }
}
