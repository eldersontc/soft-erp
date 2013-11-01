using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class Accion: Parent 
    {
        public Accion() { Items = new List<ItemAccion>(); }

        #region "Propiedades"

        public virtual String Descripcion { get; set; }
        public virtual String Imagen { get; set; }
        public virtual IList<ItemAccion> Items { get; set; }
        public virtual Boolean FilaSeleccionada { get; set; }
        public virtual String Teclas { get; set; }

        #endregion

        #region "Métodos"

        public virtual ItemAccion AddItem()
        {
            ItemAccion Item = new ItemAccion();
            this.Items.Add(Item);
            return Item;
        }

        public virtual void AsignarParametro(String Parametro, String Nombre)
        {
            if (this.Items != null && this.Items.Count > 0)
            {
                ItemAccion Item = this.Items.First(a => a.Nombre.Equals(Nombre));
                Item.Parametro = Parametro;
            }
        }

        public virtual void AsignarClase(String Clase, String Nombre)
        {
            if (this.Items != null && this.Items.Count > 0)
            {
                ItemAccion Item = this.Items.First(a => a.Nombre.Equals(Nombre));
                Item.Clase = Clase;
            }
        }

        public virtual void AsignarEnsamblado(EntidadSF EntidadSF, String Nombre)
        {
            if (this.Items != null && this.Items.Count > 0)
            {
                ItemAccion Item = this.Items.First(a => a.Nombre.Equals(Nombre));
                Item.Ensamblado = EntidadSF.EnsambladoFormulario;
                Item.Clase = EntidadSF.NombreFormulario;
            }
        }

        public virtual ItemAccion ItemByName(String Nombre)
        {
            try
            {
                ItemAccion Item = this.Items.First(a => a.Nombre.Equals(Nombre));
                return Item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

    }
}
