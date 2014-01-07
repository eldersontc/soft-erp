using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using System.Drawing;
using System.IO;

namespace Soft.Seguridad.Entidades
{
    public class Usuario: Parent 
    {

        public Usuario() { }
        private Image m_Imagen;
        public virtual String UserID { get; set; }
        public virtual String Contrasena { get; set; }
        public virtual String NombreUsuario { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual String Skyn { get; set; }
        public virtual String Imagen { get; set; }
        public virtual Boolean SuperAdministrador { get; set; }

    }
}
