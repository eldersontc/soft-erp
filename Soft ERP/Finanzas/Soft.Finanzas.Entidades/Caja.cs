using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Finanzas.Entidades
{
    public class Caja : Parent
    {
        public Caja() { }

        public virtual string Codigo { get; set; }
        public virtual decimal SaldoActual { get; set; }
        public virtual Moneda Moneda { get; set; }
    }
}
