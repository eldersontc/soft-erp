﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class EscalaListaCostosMaquina : Parent 
    {
        public EscalaListaCostosMaquina() { }
        public virtual Int32 Desde { get; set; }
        public virtual Int32 Hasta { get; set; }
        public virtual Decimal Velocidad { get; set; }
        public virtual Decimal Costo { get; set; }
    }
}
