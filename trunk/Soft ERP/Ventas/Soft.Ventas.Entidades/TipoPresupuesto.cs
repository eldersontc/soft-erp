using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class TipoPresupuesto : TipoDocumento 
    {
        public TipoPresupuesto() {
            if (NewInstance)
            {
                Activo = true;
                Entidad = "Presupuesto";
                EntidadTipoDocumento = "TipoPresupuesto";
            }
        }

    }
}
