using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;
using Soft.Ventas.Entidades;

namespace Soft.Produccion.Entidades
{
    [Serializable]
    public class ItemOrdenProduccionServicio : Parent
    {

        public ItemOrdenProduccionServicio() { }
        public virtual Existencia Servicio { get; set; }
        public virtual Existencia Material { get; set; }
        public virtual Maquina Maquina { get; set; }

        public virtual ExistenciaUnidad UnidadServicio { get; set; }
        public virtual Decimal CantidadServicio { get; set; }
        public virtual Decimal CostoServicio { get; set; }
        public virtual Decimal CantidadMaquina { get; set; }
        public virtual Decimal CostoMaquina { get; set; }
        public virtual Unidad UnidadMaquina { get; set; }

        public virtual ExistenciaUnidad UnidadMaterial { get; set; }
        public virtual Decimal CantidadMaterial { get; set; }
        public virtual Decimal CostoMaterial { get; set; }
        public virtual Decimal CostoTotalServicio { get; set; }
    }
}
