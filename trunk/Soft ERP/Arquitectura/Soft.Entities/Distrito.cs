using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class Distrito : Parent
    {
        public Distrito() { }
        public virtual Provincia Provincia { get; set; }
    }
}
