using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Produccion.Entidades
{
    public class TipoDocumentoConsolidadoOp : TipoDocumento
    {


        public TipoDocumentoConsolidadoOp()
        {
            if (NewInstance)
            {
                Activo = true;
                Entidad = "ConsolidadoOp";
                EntidadTipoDocumento = "TipoDocumentoConsolidadoOp";
            }
        }

    }
}
