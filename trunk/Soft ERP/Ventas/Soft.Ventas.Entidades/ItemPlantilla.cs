using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Inventario.Entidades;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemPlantilla : Parent 
    {
        public ItemPlantilla() {
            Servicios = new List<ItemPlantillaServicio>();
        }

        public virtual IList<ItemPlantillaServicio> Servicios { get; set; }
        public virtual Existencia Operacion { get; set; }
        public virtual Existencia Material { get; set; }
        public virtual TipoUnidad TipoUnidad { get; set; }
        public virtual RelacionMedidas RelacionMedidas { get; set; }

        public virtual ItemPlantillaServicio CrearServicio(Existencia Servicio)
        {
            ItemPlantillaServicio Item = new ItemPlantillaServicio();
            Item.Servicio = Servicio;
            Item.Unidad = Servicio.UnidadBase();
            Item.Cantidad = 1;
            Servicios.Add(Item);
            return Item;
        }
    }
}
