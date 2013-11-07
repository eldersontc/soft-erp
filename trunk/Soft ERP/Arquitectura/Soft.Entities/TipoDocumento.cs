﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Reporte.Entidades;

namespace Soft.Entities
{
    [Serializable]
    public class TipoDocumento : Parent 
    {
        public TipoDocumento() { }
        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual String CodigoSerie { get; set; }
        public virtual int LongitudSerie { get; set; }
        public virtual int NumeracionActual { get; set; }
        public virtual int NumeracionLongitud { get; set; }
        public virtual Boolean TieneImpuesto { get; set; }
        public virtual Decimal PorcentajeImpuesto { get; set; }
        public virtual Soft.Reporte.Entidades.Reporte Reporte { get; set; }
    }
}
