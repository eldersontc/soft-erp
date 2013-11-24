using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class SolicitudCotizacion : Documento
    {
        public SolicitudCotizacion() { FechaCreacion = DateTime.Now; }
        
        public virtual SocioNegocio Cliente { get; set; }
        public virtual ModalidadCredito ModalidadCredito { get; set; }
        public override Decimal Total { get; set; }
        public virtual Decimal Cantidad { get; set; }
        public virtual String Descripcion { get; set; }

        public virtual ItemSolicitudCotizacion AddItem()
        {
            ItemSolicitudCotizacion Item = new ItemSolicitudCotizacion();
            Items.Add(Item);
            return Item;
        }

    }
}
