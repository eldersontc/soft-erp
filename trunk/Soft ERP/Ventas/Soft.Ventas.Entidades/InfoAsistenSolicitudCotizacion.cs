using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class InfoAsistenSolicitudCotizacion
    {

        public InfoAsistenSolicitudCotizacion() { }

        public virtual Plantilla Plantilla { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual Int32 CodigoGrupo { get; set; }
    }
}
