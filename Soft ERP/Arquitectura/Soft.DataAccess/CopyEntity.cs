using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using NHibernate;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Collections;
using Soft.DataAccess;
using System.Windows.Forms;
using Soft.Exceptions;

namespace Soft.DataAccess
{
    public class CopyEntity : ControllerApp
    {
        public override void Start()
        {
            try
            {
                base.m_ObjectFlow = this.Copy();
                base.m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                base.m_ResultProcess = EnumResult.ERROR;
                SoftException.Control(ex);
            }
            base.Start();
        }

        public Parent Copy()
        {
            Parent Copy = null;
            using (ISession Session = m_SessionFactory.OpenSession())
            {
                this.EvictProperties((Parent)base.m_ObjectFlow);
                Session.Evict(base.m_ObjectFlow);
                ((Parent)base.m_ObjectFlow).ID = HelperNHibernate.GenerateID();
                Copy = (Parent)Session.Merge(base.m_ObjectFlow);
            }
            return Copy;
        }

        public void EvictProperties(Parent Entity) {
            using (ISession Session = m_SessionFactory.OpenSession())
            {
                Type EntityType = Entity.GetType();
                PropertyInfo[] properties = EntityType.GetProperties();
                foreach (PropertyInfo Property in properties)
                {
                    if (Property.PropertyType.Name.Equals("IList`1"))
                    {
                        IList List = (IList)Property.GetValue(Entity, null);
                        foreach (Parent Item in List)
                        {
                            this.EvictProperties(Item);
                            Session.Evict(Item);
                            ((Parent)Item).ID = HelperNHibernate.GenerateID();
                        }
                    }
                }
            }
        }

    }
}
