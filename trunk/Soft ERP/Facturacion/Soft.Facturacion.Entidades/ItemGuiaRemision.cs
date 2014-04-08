﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Facturacion.Entidades
{
    public class ItemGuiaRemision : Parent
    {
        public ItemGuiaRemision() { }

        public virtual string IDOrdenProduccion { get; set; }
        public virtual decimal Total { get; set; }
    }
}
