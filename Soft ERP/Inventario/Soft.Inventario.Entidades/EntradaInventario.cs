using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class EntradaInventario : Parent
    {
        public EntradaInventario() { 
            Items = new List<ItemEntradaInventario>();
            FechaCreacion = DateTime.Now;
        }

        public virtual String Numeracion { get; set; }
        public virtual String OrdenCompra { get; set; }
        public virtual String Factura { get; set; }
        public virtual String Observacion { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual TipoDocumentoInventario TipoDocumento { get; set; }
        public virtual SocioNegocio Proveedor { get; set; }
        public virtual Almacen Almacen { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Decimal SubTotal { get; set; }
        public virtual Decimal Impuesto { get; set; }
        public virtual Decimal Total { get; set; }
        public virtual IList<ItemEntradaInventario> Items { get; set; }

    }
}
