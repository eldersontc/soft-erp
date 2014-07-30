using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;
using Soft.Produccion.Entidades;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class SalidaInventario : Documento
    {
        public SalidaInventario() { }

        public virtual OrdenProduccion OrdenProduccion { get; set; }
        public virtual SocioNegocio Proveedor { get; set; }
        public virtual Almacen Almacen { get; set; }
        public virtual Moneda Moneda { get; set; }

        public virtual ItemSalidaInventario AddItem()
        {
            ItemSalidaInventario Item = new ItemSalidaInventario();
            Items.Add(Item);
            return Item;
        }

        public virtual void GenerarNumeracion()
        {
            if (NewInstance)
            {
                Numeracion = TipoDocumento.GenerarNumerodeDocumento();
            }
        }
    }
}
