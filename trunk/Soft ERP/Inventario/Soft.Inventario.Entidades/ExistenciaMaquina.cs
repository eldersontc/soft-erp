using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Ventas.Entidades;

namespace Soft.Inventario.Entidades
{
    public class ExistenciaMaquina : Parent
    {
        public ExistenciaMaquina() { }

        public virtual Boolean PorDefecto { get; set; }
        public virtual Maquina Maquina { get; set; }

    }
}
