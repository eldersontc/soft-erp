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

        public virtual ItemEntradaInventario AddItem()
        {
            ItemEntradaInventario Item = new ItemEntradaInventario();
            Items.Add(Item);
            return Item;
        }


        public virtual void GenerarNumCp(){
            String resultado="";
            if (NewInstance) {
                resultado = TipoDocumento.GenerarNumerodeDocumento();
                Numeracion = resultado;
            }

            if(! resultado.Equals("")){
                String sql = "update TipoDocumentoInventario set NumeracionActual=" + (TipoDocumento.NumeracionActual + 1) + " where id='" + TipoDocumento.ID + "'";
                HelperNHibernate.GetDataSet(sql);
            }

        }

    }
}
