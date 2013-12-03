using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemCotizacionServicio : Parent 
    {
        public ItemCotizacionServicio() { }
        public virtual Existencia Servicio { get; set; }
        public virtual Existencia Material { get; set; }
        public virtual Maquina Maquina { get; set; }
    }
}
