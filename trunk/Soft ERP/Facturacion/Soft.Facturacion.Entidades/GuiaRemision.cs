using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class GuiaRemision : DocumentoGenerico
    {
        public GuiaRemision()
        {
            FechaCreacion = DateTime.Now;
            Items = new List<ItemGuiaRemision>();
        }

        //public virtual TipoEntrega TipoDocumento { get; set; }

        public virtual TipoEntrega TipoEntrega { get { return (TipoEntrega)TipoDocumento; } }

        //public virtual TipoFacturacion TipoEntrega { get { return (TipoFacturacion)TipoDocumento; } }

        //public virtual string Numeracion { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Moneda Moneda { get; set; }
        //public virtual DateTime FechaCreacion { get; set; }
        //public virtual string Observacion { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Impuesto { get; set; }
        public virtual decimal Total { get; set; }
        public virtual IList<ItemGuiaRemision> Items { get; set; }

        public virtual ItemSocioNegocioContacto Contacto { get; set; }

        public virtual string LicenciaConducir { get; set; }

        public virtual string NumeroDePlaca { get; set; }

        public virtual string ContactoNombre { get; set; }

        public virtual SocioNegocio Chofer { get; set; }

        public virtual MotivoTraslado MotivoTraslado { get; set; }

        public virtual Departamento Departamento { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Distrito Distrito { get; set; }
        public virtual string Direccion { get; set; }

        public virtual Boolean EditarDireccionEntrega { get; set; }

        public virtual bool Anulado { get; set; }

        public virtual void AddItem(string IDOP, Decimal Cantidad)
        {
            ItemGuiaRemision Item = new ItemGuiaRemision();
            Item.IDOrdenProduccion = IDOP;
            Item.Cantidad = Cantidad;
            Items.Add(Item);
        }


        public virtual ItemGuiaRemision AddItem()
        {
            ItemGuiaRemision Item = new ItemGuiaRemision();
            Items.Add(Item);
            return Item;
        }

        public virtual ItemGuiaRemision ObtenerItem(String IDItem)
        {
            return (ItemGuiaRemision)Items.First(Item => ((ItemGuiaRemision)Item).IDOrdenProduccion.Equals(IDItem));
        }

        public virtual string ObtenerFiltroOps()
        {
            string Filtro = "";
            foreach (ItemGuiaRemision Item in Items)
            {
                if (Filtro.Length > 0) { Filtro += ","; }
                Filtro += String.Format("'{0}'", Item.IDOrdenProduccion);
            }
            return Filtro;
        }


    }
}
