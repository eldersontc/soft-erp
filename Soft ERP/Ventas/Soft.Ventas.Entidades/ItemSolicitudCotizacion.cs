using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemSolicitudCotizacion  : ItemDocumento  
    {
        public ItemSolicitudCotizacion() { }

        public virtual Existencia Existencia { get; set; }
        public virtual String Observacion { get; set; }
        public virtual Unidad Unidad { get; set; }
    }
}
