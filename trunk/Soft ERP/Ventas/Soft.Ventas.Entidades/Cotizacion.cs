using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;
using Soft.Produccion.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class Cotizacion : Documento 
    {
        public Cotizacion()
        {
            if (NewInstance)
            {
                FechaCreacion = DateTime.Now;
                EstadoAprobacion = "PENDIENTE";
            }
        }

        public virtual SocioNegocio Cliente { get; set; }
        public virtual ModalidadCredito ModalidadCredito { get; set; }
        public virtual Decimal Cantidad { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual SocioNegocio Vendedor { get; set; }
        public virtual Decimal PorcentajeUtilidad { get; set; }
        public virtual String EstadoAprobacion { get; set; }
        public virtual ItemSocioNegocioContacto Contacto { get; set; }
        public virtual String DireccionEntrega { get; set; }
        public virtual String DireccionFacturacion { get; set; }
        public virtual String IDSolicitudCotizacion { get; set; }
        public virtual ListaCostosMaquina ListaCostosMaquina { get; set; }
        public virtual ListaPreciosExistencia ListaPreciosExistencia { get; set; }
        public virtual ListaPreciosTransporte ListaPreciosTransporte { get; set; }



        public virtual bool MuestraPrecioItemEnPresupuesto { get; set; }


        public virtual bool OcultaPrecioEnPresupuesto { get; set; }


        public virtual LineaProduccion LineaProduccion { get; set; }


        private Decimal mTotal;
        public override Decimal Total
        {
            get
            {

               
                    Decimal PorcentajeUtilidad = base.Total * (this.PorcentajeUtilidad / 100);
                    mTotal = base.Total + PorcentajeUtilidad;
                    return mTotal;

                
            }
            set
            {
                mTotal = value;
            }
        }


        public virtual void AsignarListadeCostosDesdeTipoDocumento()
        {
                TipoCotizacion tipo = (TipoCotizacion)TipoDocumento;
                if (tipo != null)
                {
                    ListaCostosMaquina = tipo.ListaCostosMaquina;
                    ListaPreciosExistencia = tipo.ListaPreciosExistencia;
                    ListaPreciosTransporte = tipo.ListaPreciosTransporte;
                }
                else
                {
                    ListaCostosMaquina = null;
                    ListaPreciosExistencia = null;
                    ListaPreciosTransporte = null;
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
