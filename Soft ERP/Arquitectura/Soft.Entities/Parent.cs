using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Soft.Entities
{
    [Serializable]
    public class Parent
    {
        public Parent() { 
            ID = Guid.NewGuid().ToString().ToUpper();
            NewInstance = true;
        }
        
        public virtual String ID{ get; set; }
        public virtual String Nombre { get; set; }
        public virtual bool Activo{ get; set; }
        public virtual bool NewInstance { get; set; }

        public virtual Object ValueByProperty(String Property) {
            Object ObjResult = null;
            Type Type = this.GetType();
            PropertyInfo pInfo = Type.GetProperty(Property);
            ObjResult = pInfo.GetValue(this, null);
            return ObjResult;
        }

    }
}
