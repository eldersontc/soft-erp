using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class RequerimientoMaterialItemCotizacion: Parent
    {
        public RequerimientoMaterialItemCotizacion()
        {
           
        }

        public virtual decimal Cantidad { get; set; }
        public virtual decimal Costo { get; set; }

        public virtual Existencia Material { get; set; }
        public virtual ExistenciaUnidad Unidad { get; set; }


    }
}
