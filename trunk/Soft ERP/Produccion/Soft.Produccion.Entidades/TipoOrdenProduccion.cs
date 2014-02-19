using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Produccion.Entidades
{
    public class TipoOrdenProduccion : TipoDocumento 
    {
        public TipoOrdenProduccion()
        {
            if (NewInstance)
            {
                Activo = true;
                Entidad = "OrdenProduccion";
                EntidadTipoDocumento = "TipoOrdenProduccion";
            }
        }
    }
}
