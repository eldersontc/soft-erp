using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class EntradaInventario : Documento
    {
        public EntradaInventario() { }

        public virtual String OrdenCompra { get; set; }
        public virtual String Factura { get; set; }
        public virtual SocioNegocio Proveedor { get; set; }
        public virtual Almacen Almacen { get; set; }

        public virtual ItemEntradaInventario AddItem()
        {
            ItemEntradaInventario Item = new ItemEntradaInventario();
            Items.Add(Item);
            return Item;
        }

    }
}
