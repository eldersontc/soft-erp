﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Entidades
{
    [Serializable]
    public class ItemMaquinaServicio : Parent
    {
        
        public ItemMaquinaServicio() { 
        
        }

        public virtual Existencia Servicio { get; set; }
        public virtual ExistenciaUnidad Unidad { get; set; }


    }
}
