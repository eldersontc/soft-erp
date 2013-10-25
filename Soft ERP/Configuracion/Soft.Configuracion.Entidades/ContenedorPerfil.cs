using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Seguridad.Entidades;

namespace Soft.Configuracion.Entidades
{
    [Serializable]
    public class ContenedorPerfil : Parent
    {
        public ContenedorPerfil() { }
        public virtual Perfil Perfil { get; set; }
    }
}
