using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class TipoFacturacion : TipoDocumento
    {
        public TipoFacturacion()
        {
            if (NewInstance)
            {
                Activo = true;
                Entidad = "Facturacion";
                EntidadTipoDocumento = "TipoFacturacion";
            }
        }

        public virtual string Comprobante { get; set; }
        public virtual bool GeneraDeuda { get; set; }
        public virtual string TipoDeuda { get; set; }
    }
}
