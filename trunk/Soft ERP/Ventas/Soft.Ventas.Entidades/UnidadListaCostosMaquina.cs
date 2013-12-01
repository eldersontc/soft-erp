using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Inventario.Entidades;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class UnidadListaCostosMaquina : Parent 
    {
        public UnidadListaCostosMaquina() {
            Escalas = new List<EscalaListaCostosMaquina>();        
        }

        public virtual Unidad Unidad { get; set; }
        public virtual IList<EscalaListaCostosMaquina> Escalas { get; set; }

        public virtual EscalaListaCostosMaquina AddEscala()
        {
            EscalaListaCostosMaquina Item = new EscalaListaCostosMaquina();
            Escalas.Add(Item);
            return Item;
        }
    }
}
