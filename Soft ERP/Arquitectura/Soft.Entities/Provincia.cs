using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft.Entities
{
    [Serializable]
    public class Provincia : Parent 
    {
        public Provincia(){ }
        public virtual Departamento Departamento { get; set; }
    }
}
