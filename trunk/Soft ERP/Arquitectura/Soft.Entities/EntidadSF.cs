using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Configuracion.Entidades;

namespace Soft.Entities
{
    [Serializable]
    public class EntidadSF: Parent
    {
        public EntidadSF() { Atributos = new List<AtributoSF>(); }

        public virtual String NombreClase { get; set; }
        public virtual String NombreFormulario { get; set; }
        public virtual String Tabla { get; set; }
        public virtual Ensamblado EnsambladoClase { get; set; }
        public virtual Ensamblado EnsambladoFormulario { get; set; }
        public virtual IList<AtributoSF> Atributos { get; set; }

    }
}
