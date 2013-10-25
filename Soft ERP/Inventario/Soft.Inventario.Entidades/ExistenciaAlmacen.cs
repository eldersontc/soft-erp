using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    public class ExistenciaAlmacen: Parent
    {

        public ExistenciaAlmacen() { }

        public virtual Almacen Almacen{ get; set; }
        public virtual decimal StockFisico { get; set; }
        public virtual decimal StockComprometido { get; set; }

    
    }

}
