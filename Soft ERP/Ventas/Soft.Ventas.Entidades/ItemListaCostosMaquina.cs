using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemListaCostosMaquina : Parent 
    {
        public ItemListaCostosMaquina() {
            Unidades = new List<UnidadListaCostosMaquina>();    
        }

        public virtual Maquina Maquina { get; set; }
        public virtual Decimal CostoPreparacion { get; set; }
        public virtual Decimal CostoProduccion { get; set; }
        public virtual IList<UnidadListaCostosMaquina> Unidades { get; set; }

        public virtual String FiltroUnidades
        {
            get
            {
                String Filtro = "ID NOT IN (";
                String IDs = "";
                foreach (UnidadListaCostosMaquina Item in Unidades)
                {
                    IDs = IDs + "'" + Item.Unidad.ID + "',";
                }
                Filtro = (IDs.Length > 0) ? Filtro + IDs.Substring(0, IDs.Length - 1) + ")" : "";
                return Filtro;
            }
        }

        public virtual UnidadListaCostosMaquina AddUnidad(Unidad Unidad)
        {
            UnidadListaCostosMaquina Item = new UnidadListaCostosMaquina();
            Item.Unidad = Unidad;
            Unidades.Add(Item);
            return Item;
        }
    }
}
