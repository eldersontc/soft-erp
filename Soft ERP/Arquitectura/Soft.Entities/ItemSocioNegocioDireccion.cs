using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class ItemSocioNegocioDireccion : Parent
    {
        public ItemSocioNegocioDireccion() { }

        public virtual Departamento Departamento { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Distrito Distrito { get; set; }
        public virtual String Direccion { get; set; }
        public virtual Boolean EsDireccionEntrega { get; set; }
        public virtual Boolean EsDireccionFacturacion { get; set; }

    }
}
