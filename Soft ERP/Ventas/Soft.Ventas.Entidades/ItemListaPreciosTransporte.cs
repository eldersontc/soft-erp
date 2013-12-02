using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemListaPreciosTransporte : Parent 
    {
        public ItemListaPreciosTransporte() {
            Escalas = new List<EscalaListaPreciosTransporte>();
        }

        public virtual Distrito Origen { get; set; }
        public virtual Distrito Destino { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual IList<EscalaListaPreciosTransporte> Escalas { get; set; }

        public virtual EscalaListaPreciosTransporte AddEscala()
        {
            EscalaListaPreciosTransporte Item = new EscalaListaPreciosTransporte();
            Escalas.Add(Item);
            return Item;
        }
    }
}
