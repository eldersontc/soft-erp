using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;
using System.Windows.Forms;

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
        public virtual Moneda Moneda { get; set; }

        public virtual ItemEntradaInventario AddItem()
        {
            ItemEntradaInventario Item = new ItemEntradaInventario();
            Items.Add(Item);
            return Item;
        }

        public virtual void GenerarNumeracion(){
            if (NewInstance) {
                Numeracion = TipoDocumento.GenerarNumerodeDocumento();
            }
        }

    }
}
