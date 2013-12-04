using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class SocioNegocio : Parent
    {
        public SocioNegocio() {
            Direcciones = new List<ItemSocioNegocioDireccion>();
            Contactos = new List<ItemSocioNegocioContacto>();
            Bancos = new List<ItemSocioNegocioBanco>();
            Empleados = new List<SocioNegocioEmpleado>();
            Clientes = new List<SocioNegocioCliente>();
           
            if (NewInstance) {
                Aniversario = DateTime.Now;
                Activo = true;
            }
 
        }

        public virtual TipoSocioNegocio TipoSocioNegocio { get; set; }
        public virtual String Codigo { get; set; }
        public virtual String Descripcion { get; set; }
        public virtual DateTime Aniversario { get; set; }
        public virtual Boolean Cliente { get; set; }
        public virtual Boolean Proveedor { get; set; }
        public virtual Boolean Empleado { get; set; }

        public virtual String ApellidoPaterno { get; set; }
        public virtual String ApellidoMaterno { get; set; }
        public virtual String Nombre1 { get; set; }
        public virtual String Nombre2 { get; set; }
        public virtual String Correo { get; set; }
        public virtual String PaginaWeb { get; set; }

        public virtual void ConcatenarNombres()
        {
            if(ApellidoPaterno.Length>0 ){
                Nombre = ApellidoPaterno;
            }
            if (ApellidoMaterno != null) { 
                if (ApellidoMaterno.Length > 0)
                {
                    Nombre = ApellidoPaterno+" "+ApellidoMaterno;
                }
            }

            if (Nombre1 != null)
            {
                if (Nombre1.Length > 0)
                {
                    Nombre =ApellidoPaterno+" "+ApellidoMaterno+", " + Nombre1;
                }            
            }

            if (Nombre2 != null)
            {
                if (Nombre2.Length > 0)
                {
                    Nombre =ApellidoPaterno+" "+ApellidoMaterno+", " + Nombre1+ " " + Nombre2;
                }
            }
            
        }

        public virtual IList<ItemSocioNegocioDireccion> Direcciones { get; set; }
        public virtual IList<ItemSocioNegocioContacto> Contactos { get; set; }
        public virtual IList<ItemSocioNegocioBanco> Bancos { get; set; }

        public virtual IList<SocioNegocioEmpleado> Empleados { get; set; }

        public virtual IList<SocioNegocioCliente> Clientes { get; set; }


        public virtual ItemSocioNegocioDireccion AddItemDireccion()
        {
            ItemSocioNegocioDireccion Item = new ItemSocioNegocioDireccion();
            this.Direcciones.Add(Item);
            return Item;
        }


        public virtual ItemSocioNegocioContacto AddItemContacto()
        {
            ItemSocioNegocioContacto Item = new ItemSocioNegocioContacto();
            this.Contactos.Add(Item);
            return Item;
        }


        public virtual ItemSocioNegocioBanco AddItemBanco()
        {
            ItemSocioNegocioBanco Item = new ItemSocioNegocioBanco();
            this.Bancos.Add(Item);
            return Item;
        }


        public virtual SocioNegocioEmpleado AddItemEmpleado()
        {
            SocioNegocioEmpleado Item = new SocioNegocioEmpleado();
            this.Empleados.Add(Item);
            return Item;
        }


        public virtual SocioNegocioCliente AddItemCliente()
        {
            SocioNegocioCliente Item = new SocioNegocioCliente();
            this.Clientes.Add(Item);
            return Item;
        }

    }
}
