using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    public class TipoDocumentoInventario: TipoDocumento 
    {
        public TipoDocumentoInventario(){}

        public virtual Almacen Almacen { get; set; }
        public virtual String Operacion { get; set; }
        public virtual Boolean UnicoAlmacen { get; set; }
        public virtual Boolean RequiereSocioNegocio { get; set; }
        public virtual Boolean AceptaCostoCero { get; set; }
        
    }
}
