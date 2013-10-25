using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
namespace Soft.Inventario.Entidades
{
    public class ExistenciaUnidad : Parent
    {
        public ExistenciaUnidad(){}


        public virtual Boolean EsUnidadBase { get; set; }
        public virtual int FactorConversion { get; set; }

        public virtual Unidad Unidad { get; set; }

    }
}
