﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;


namespace Soft.Inventario.Entidades
{
    public class Existencia: Parent
    {
        public Existencia(){
            Unidades = new List<ExistenciaUnidad>();
            Almacenes = new List<ExistenciaAlmacen>();
        }

        public virtual String Codigo { get; set; }
        public virtual String Nombre { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual Boolean EsCompra { get; set; }
        public virtual Boolean EsVenta { get; set; }
        public virtual Boolean EsServicio { get; set; }
        public virtual Boolean EsInventariable { get; set; }

        public virtual ClasificacionExistencia ClasificacionExistencia { get; set; }
        public virtual ItemClasificacionExistencia ItemClasificacionExistencia { get; set; }


        public virtual IList<ExistenciaUnidad>Unidades{ get; set; }
        public virtual IList<ExistenciaAlmacen>Almacenes{ get; set; }



        public virtual ExistenciaUnidad AddItem()
        {
            ExistenciaUnidad Item = new ExistenciaUnidad();
            this.Unidades.Add(Item);
            return Item;
        }

        public virtual ExistenciaAlmacen AddItemAlmacen()
        {
            ExistenciaAlmacen Item = new ExistenciaAlmacen();
            this.Almacenes.Add(Item);
            return Item;
        }

    }
}
