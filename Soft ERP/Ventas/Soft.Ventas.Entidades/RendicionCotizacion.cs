using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    public class RendicionCotizacion : Parent
    {
        public RendicionCotizacion()
        {
            if (NewInstance)
            {
                Items = new List<ItemRendicionCotizacion>();
                FechaCreacion = DateTime.Now;
            }
        }

        public virtual DateTime FechaCreacion { get; set; }
        public virtual string IDCotizacion { get; set; }
        public virtual string NumeroCotizacion { get; set; }
        
        private decimal TotalCotizacion_;
        public virtual decimal TotalCotizacion
        {
            get
            {
                TotalCotizacion_ = 0;
                foreach (ItemRendicionCotizacion Item in Items)
                {
                    TotalCotizacion_ += Item.TotalCotizacion;
                }
                return TotalCotizacion_;
            }
            set
            {
                TotalCotizacion_ = value;
            }
        }

        private decimal TotalReal_;
        public virtual decimal TotalReal
        {
            get
            {
                TotalReal_ = 0;
                foreach (ItemRendicionCotizacion Item in Items)
                {
                    TotalReal_ += Item.TotalReal;
                }
                return TotalReal_;
            }
            set
            {
                TotalReal_ = value;
            }
        }

        public virtual IList<ItemRendicionCotizacion> Items { get; set; }
    }
}
