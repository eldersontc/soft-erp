using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Ventas.Entidades;

namespace Soft.Produccion.Entidades
{
    public class InfoAsistenteGeneracionOPdesdePresupuesto
    {

        public InfoAsistenteGeneracionOPdesdePresupuesto() { FechaCreacion = DateTime.Now; }

        public virtual Presupuesto Presupuesto { get; set; }
        public virtual ItemPresupuesto ItemPresupuesto { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual Int32 CodigoGrupo { get; set; }
        public virtual ItemSocioNegocioContacto Contacto { get; set; }
        public virtual SocioNegocio Responsable { get; set; }

    }
}
