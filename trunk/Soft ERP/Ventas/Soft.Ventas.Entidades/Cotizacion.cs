using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Cotizacion : Documento 
    {
        public Cotizacion() { FechaCreacion = DateTime.Now; }

        public virtual SocioNegocio Cliente { get; set; }
        public virtual ModalidadCredito ModalidadCredito { get; set; }
        public virtual Decimal Cantidad { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual SocioNegocio Vendedor { get; set; }
        public virtual Decimal PorcentajeUtilidad { get; set; }
        public virtual String EstadoAprobacion { get; set; }

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
                String SQL = "UPDATE TipoCotizacion SET NumeracionActual = " + (TipoDocumento.NumeracionActual + 1) + " WHERE ID ='" + TipoDocumento.ID + "'";
                HelperNHibernate.GetDataSet(SQL);
            }
        }

        public virtual ItemCotizacion AddItem()
        {
            ItemCotizacion Item = new ItemCotizacion();
            Items.Add(Item);
            return Item;
        }

    }
}
