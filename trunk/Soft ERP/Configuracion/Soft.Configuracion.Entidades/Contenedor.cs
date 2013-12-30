using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Seguridad.Entidades;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class Contenedor: Parent 
    {
        public Contenedor() { 
            Items = new List<ItemContenedor>();
            Perfiles = new List<ContenedorPerfil>();
        }
        
        public virtual IList<ItemContenedor> Items { get; set; }
        public virtual IList<ContenedorPerfil> Perfiles { get; set; }

        public virtual void AddItem(ItemContenedor Item)
        {
            Items.Add(Item);
        }

        public virtual void AddPerfil(Perfil Perfil)
        {
            ContenedorPerfil ContenedorPerfil = new ContenedorPerfil();
            ContenedorPerfil.Perfil = Perfil;
            Perfiles.Add(ContenedorPerfil);
        }

        public virtual List<ItemContenedor> GetItemsByItemParent(String IDItemPadre)
        {
            List<ItemContenedor> ItemsResult = new List<ItemContenedor>();
            ItemsResult = (List<ItemContenedor>)this.Items.Where(It => It.ItemPadre != null).Where(It => It.ItemPadre.ID.Equals(IDItemPadre)).ToList();
            return ItemsResult;
        }

        public virtual void DeleteItem(ItemContenedor Item){
            Items.Remove(Item);
            foreach (ItemContenedor It in GetItemsByItemParent(Item.ID))
            {
                DeleteItem(It);
            }
        }

    }
}
