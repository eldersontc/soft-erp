using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class ItemSocioNegocioContacto : Parent
    {
        public ItemSocioNegocioContacto() { }
        public virtual String Cargo { get; set; }
        public virtual String Telefono { get; set; }
        public virtual String Correo { get; set; }

    }
}
