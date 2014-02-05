using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class SolicitudCotizacion : Documento
    {
        public SolicitudCotizacion() {
            if (NewInstance)
            {
                FechaCreacion = DateTime.Now;
                EstadoAprobacion = "PENDIENTE";
            }
        }
        
        public virtual SocioNegocio Cliente { get; set; }
        public virtual ModalidadCredito ModalidadCredito { get; set; }
        public override Decimal Total { get; set; }
        public virtual Decimal Cantidad { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual String EstadoAprobacion { get; set; }
        public virtual ItemSocioNegocioContacto Contacto { get; set; }
        public virtual String DireccionEntrega { get; set; }
        public virtual String DireccionFacturacion { get; set; }
        public virtual Int32 CodigoGrupo { get; set; }

        public virtual ItemSolicitudCotizacion AddItem()
        {
            ItemSolicitudCotizacion Item = new ItemSolicitudCotizacion();
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
                String SQL = "UPDATE TipoSolicitudCotizacion SET NumeracionActual = " + (TipoDocumento.NumeracionActual + 1) + " WHERE ID ='" + TipoDocumento.ID + "'";
                HelperNHibernate.GetDataSet(SQL);
            }
        }

    }
}
