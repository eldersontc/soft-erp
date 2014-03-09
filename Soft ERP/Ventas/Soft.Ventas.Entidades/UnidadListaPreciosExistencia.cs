using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Inventario.Entidades;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class UnidadListaPreciosExistencia : Parent 
    {
        public UnidadListaPreciosExistencia()
        {
            Escalas = new List<EscalaListaPreciosExistencia>();        
        }

        public virtual ExistenciaUnidad Unidad { get; set; }
        public virtual IList<EscalaListaPreciosExistencia> Escalas { get; set; }

        public virtual EscalaListaPreciosExistencia AddEscala()
        {
            EscalaListaPreciosExistencia Item = new EscalaListaPreciosExistencia();
            Escalas.Add(Item);
            return Item;
        }
    }
}
