using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.DataAccess;
using Soft.Ventas.Entidades;
using System.Windows.Forms;


namespace Soft.Produccion.Entidades
{
    [Serializable]
    public class OrdenProduccion : Documento 
    {
        public OrdenProduccion()
        {
            if (NewInstance)
            {
                Activo = true;
                this.Moneda = (Moneda)HelperNHibernate.GetEntityByID("Moneda", "1F0B7D4B-A581-476C-9039-3EFB6ADD4AC4");
                this.TipoCambioFecha = 1;
                this.FechaCreacion = DateTime.Now;
                this.FechaTentativaEntrega = DateTime.Now;
                this.EstadoAprobacion="PENDIENTE";
                this.EstadoFacturacion = "PENDIENTE";
            }

        }

        public virtual String IDItemPresupuesto { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual SocioNegocio Vendedor { get; set; }
        public virtual SocioNegocio Cotizador { get; set; }
        public virtual Decimal Cantidad { get; set; }
        public virtual Decimal CantidadFacturada { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual ItemSocioNegocioContacto Contacto { get; set; }
        public virtual String DireccionEntrega { get; set; }
        public virtual String DireccionFacturacion { get; set; }
        public virtual DateTime FechaTentativaEntrega { get; set; }
        public virtual String Prioridad { get; set; }
        public virtual String EstadoAprobacion { get; set; }
        public virtual String EstadoFacturacion { get; set; }
        public virtual ListaCostosMaquina ListaCostosMaquina { get; set; }
        public virtual ListaPreciosExistencia ListaPreciosExistencia { get; set; }
        public virtual ListaPreciosTransporte ListaPreciosTransporte { get; set; }


        public virtual LineaProduccion LineaProduccion { get; set; }

        public virtual Decimal Total { get; set; }
        
        public virtual void GenerarNumeracion()
        {
            if (NewInstance)
            {
                Numeracion = TipoDocumento.GenerarNumerodeDocumento();
            }
        }

        public virtual ItemOrdenProduccion AddItem()
        {
            ItemOrdenProduccion Item = new ItemOrdenProduccion();
            Items.Add(Item);
            return Item;
        }

    }
}
