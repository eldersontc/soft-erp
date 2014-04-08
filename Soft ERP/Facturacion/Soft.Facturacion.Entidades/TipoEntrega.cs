using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class TipoEntrega : TipoDocumento
    {
        public TipoEntrega()
        {
            if (NewInstance)
            {
                Activo = true;
                Entidad = "Entrefa";
                EntidadTipoDocumento = "TipoEntrega";
            }
        }

    }
}
