using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class ItemContenedor: Parent
    {
        #region "Constructores"

        public ItemContenedor() { Acciones = new List<ItemContenedorAccion>(); }

        #endregion

        #region "Propiedades"

        public virtual Boolean EsContenedor { get; set; }
        public virtual Boolean EsPanel { get; set; }
        public virtual String Imagen { get; set; }
        public virtual Panel Panel { get; set; }
        public virtual ItemContenedor ItemPadre { get; set; }
        public virtual Boolean Crear { get; set; }
        public virtual Boolean Modificar { get; set; }
        public virtual Boolean Eliminar { get; set; }
        public virtual Boolean Copiar { get; set; }
        public virtual Boolean Auditar { get; set; }
        public virtual Accion AccionCrear { get; set; }
        public virtual Accion AccionModificar { get; set; }
        public virtual Accion AccionEliminar { get; set; }
        public virtual Accion AccionCopiar { get; set; }
        public virtual IList<ItemContenedorAccion> Acciones { get; set; }
        
        #endregion

        #region "Métodos"

        public virtual void AddAccion(Accion Accion)
        {
            ItemContenedorAccion Item = new ItemContenedorAccion();
            Item.Accion = Accion;
            Acciones.Add(Item);
        }

        #endregion

    }
}
