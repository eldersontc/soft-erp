using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class TipoNotaDebito : TipoDocumento
    {
        public TipoNotaDebito()
        {
            if (NewInstance)
            {
                Activo = true;
                Entidad = "NotaDebito";
                EntidadTipoDocumento = "TipoNotaDebito";
            }
        }
    }
}
