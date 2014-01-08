using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class TipoCotizacion : TipoDocumento 
    {
        public TipoCotizacion() {

            if (NewInstance)
            {
                Activo = true;
                Entidad = "Cotizacion";
                EntidadTipoDocumento = "TipoCotizacion";
            }
        }

    }
}
