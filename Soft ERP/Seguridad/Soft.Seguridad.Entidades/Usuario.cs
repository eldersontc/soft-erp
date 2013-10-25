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
        public virtual Byte[] Imagen { get; set; }

        public virtual Image ObtenerImagen
        {
            get
            {
                if (m_Imagen == null)
                {
                    m_Imagen = ToImage(Imagen);
                }
                return m_Imagen;
            }
        }

        public virtual Image ToImage(Byte[] Bytes)
        {
            Image Image;
            using (MemoryStream ms = new MemoryStream(Bytes, 0, Bytes.Length))
            {
                ms.Write(Bytes, 0, Bytes.Length);
                Image = Image.FromStream(ms, true);
            }
            return Image;
        }

    }
}
