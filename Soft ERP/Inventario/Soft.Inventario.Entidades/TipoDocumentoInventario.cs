using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    public class TipoDocumentoInventario: Parent
    {
        public TipoDocumentoInventario(){
        }

        public virtual String Codigo { get; set; }
        //public virtual String Nombre { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual Almacen Almacen { get; set; }
        public virtual String Operacion { get; set; }
        public virtual Boolean UnicoAlmacen { get; set; }
        public virtual Boolean RequiereSocioNegocio { get; set; }


        public virtual Boolean TieneImpuesto { get; set; }

        public virtual Decimal PorcentajeImpuesto { get; set; }
        public virtual Boolean AceptaCostoCero { get; set; }
        public virtual String CodigoSerie { get; set; }
        public virtual int LongitudSerie { get; set; }
        public virtual int NumeracionActual { get; set; }
        public virtual int NumeracionLongitud { get; set; }




    }
}
