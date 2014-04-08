﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class Facturacion : Parent
    {
        public Facturacion() {
            FechaCreacion = DateTime.Now;
            Items = new List<ItemFacturacion>(); 
        }

        public virtual TipoFacturacion TipoFacturacion { get; set; }
        public virtual string Numeracion { get; set; }
        public virtual SocioNegocio Cliente { get; set; }
        public virtual SocioNegocio Responsable { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual DateTime FechaCreacion { get; set; }
        public virtual string Observacion { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal Impuesto { get; set; }
        public virtual decimal Total { get; set; }
        public virtual IList<ItemFacturacion> Items { get; set; }

        public virtual ItemFacturacion AddItem() {
            ItemFacturacion Item = new ItemFacturacion();
            Items.Add(Item);
            return Item;
        }

        public virtual ItemFacturacion ObtenerItem(String IDItem)
        {
            return (ItemFacturacion)Items.First(Item => ((ItemFacturacion)Item).IDOrdenProduccion.Equals(IDItem));
        }

        public virtual string ObtenerFiltroOps()
        {
            string Filtro = "";
            foreach (ItemFacturacion Item in Items)
            {
                if (Filtro.Length > 0) { Filtro += ","; }
                Filtro += String.Format("'{0}'", Item.IDOrdenProduccion);
            }
            return Filtro;
        }

        public virtual void CalcularTotales() {
            decimal Subtotal = 0;
            foreach (ItemFacturacion Item in Items)
            {
                Subtotal += Item.Total;
            }
            this.SubTotal = Subtotal;
            this.Impuesto = (TipoFacturacion != null && TipoFacturacion.PorcentajeImpuesto > 0) ? (SubTotal * (TipoFacturacion.PorcentajeImpuesto / 100)) : 0;
            this.Total = SubTotal + Impuesto;     
        }
    }
}
