using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class ItemSocioNegocioBanco : Parent
    {
        public ItemSocioNegocioBanco() { }
        public virtual Banco Banco { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual String Descripcion { get; set; }

    }
}
