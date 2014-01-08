using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;

namespace Soft.Entities
{
    [Serializable]
    public class Documento : Parent
    {
        public Documento() { 
            FechaCreacion = DateTime.Now;
            Items = new List<ItemDocumento>();
        }

        public virtual String Numeracion { get; set; }
        public virtual String Observacion { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual IList<ItemDocumento> Items { get; set; }
        public virtual Decimal TipoCambioFecha { get; set; }
        public virtual Decimal TotalSoles { get; set; }


        private Decimal mSubTotal;
        public virtual Decimal SubTotal
        {
            get
            {
                Decimal SubTotal = 0;
                foreach (ItemDocumento Item in Items)
                {
                    SubTotal += Item.Total;
                }
                return SubTotal;
            }
            set { mSubTotal = value; }
        }

        private Decimal mImpuesto;
        public virtual Decimal Impuesto
        {
            get
            {
                Decimal Impuesto = 0;
                if (SubTotal > 0 & TipoDocumento != null && TipoDocumento.TieneImpuesto)
                {
                    Impuesto = SubTotal * (TipoDocumento.PorcentajeImpuesto / 100);
                }
                return Impuesto;
            }
            set { mImpuesto = value; }
        }

        private Decimal mTotal;
        public virtual Decimal Total
        {
            get
            {
                Decimal Total = 0;
                if (SubTotal > 0) { 
                    Total = SubTotal + Impuesto;
                    TotalSoles = Total / TipoCambioFecha;
                }
                return Total;
            }
            set { mTotal = value; }
        }

        public virtual void GenerarNumCpAlFinal()
        {
            String SQL2 = "UPDATE " + TipoDocumento.Entidad + " SET Numeracion ='" + TipoDocumento.ObtenerNumeroActual() + "' WHERE ID ='" + ID + "'";

                String SQL = "UPDATE " + TipoDocumento.EntidadTipoDocumento + " SET NumeracionActual = " + (TipoDocumento.NumeracionActual + 1) + " WHERE ID ='" + TipoDocumento.ID + "'";

                HelperNHibernate.GetDataSet(SQL);
                HelperNHibernate.GetDataSet(SQL2); 
        }

    }
}
