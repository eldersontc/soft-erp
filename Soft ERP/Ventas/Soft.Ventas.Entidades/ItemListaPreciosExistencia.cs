using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemListaPreciosExistencia : Parent 
    {
        public ItemListaPreciosExistencia()
        {
            Unidades = new List<UnidadListaPreciosExistencia>();    
        }

        public virtual Existencia Existencia { get; set; }
        public virtual IList<UnidadListaPreciosExistencia> Unidades { get; set; }

        public virtual String FiltroUnidades
        {
            get
            {
                String Filtro = "ID NOT IN (";
                String IDs = "";
                foreach (UnidadListaPreciosExistencia Item in Unidades)
                {
                    IDs = IDs + "'" + Item.Unidad.ID + "',";
                }
                Filtro = (IDs.Length > 0) ? Filtro + IDs.Substring(0, IDs.Length - 1) + ")" : "";
                return Filtro;
            }
        }

        public virtual UnidadListaPreciosExistencia AddUnidad(Unidad Unidad)
        {
            UnidadListaPreciosExistencia Item = new UnidadListaPreciosExistencia();
            Item.Unidad = Unidad;
            Unidades.Add(Item);
            return Item;
        }
    }
}
