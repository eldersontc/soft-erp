using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Inventario.Entidades
{
    [Serializable]
    public class SalidaInventario : Documento
    {
        public SalidaInventario() { }

        public virtual String OrdenProduccion { get; set; }
        public virtual SocioNegocio Proveedor { get; set; }
        public virtual Almacen Almacen { get; set; }

        public virtual ItemSalidaInventario AddItem()
        {
            ItemSalidaInventario Item = new ItemSalidaInventario();
            Items.Add(Item);
            return Item;
        }

        public virtual void GenerarNumCp()
        {
            String Result = "";
            if (NewInstance)
            {
                Result = TipoDocumento.GenerarNumerodeDocumento();
                Numeracion = Result;
            }
            if (!Result.Equals(""))
            {
                String SQL = "UPDATE TipoDocumentoInventario SET NumeracionActual = " + (TipoDocumento.NumeracionActual + 1) + " WHERE ID='" + TipoDocumento.ID + "'";
                HelperNHibernate.GetDataSet(SQL);
            }
        }

    }
}
