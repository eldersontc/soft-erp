using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Produccion.Entidades
{
    public class LineaProduccion:Parent
    {

        public LineaProduccion() { }



        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }



    }
}
