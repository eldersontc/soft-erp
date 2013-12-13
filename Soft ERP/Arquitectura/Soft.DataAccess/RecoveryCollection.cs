using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Data;
using Soft.DataAccess;
using System.Windows.Forms;
using Soft.Entities;
using Soft.Exceptions;
using Microsoft.VisualBasic;

namespace Soft.DataAccess
{
    public class RecoveryCollection : ControllerApp
    {
        public override void Start()
        {
            try
            { 
                if (base.m_ItemsSelecteds.Count > 0)
                {
                    using (ISession Sesion = m_SessionFactory.OpenSession())
                    {
                        Collection ObjectsFlow = new Collection();
                        foreach (String ID in base.m_ItemsSelecteds)
                        {
                            Parent Entity = (Parent)Sesion.Get(base.m_EntidadSF.NombreClase, ID);
                            Entity.NewInstance = false;
                            ObjectsFlow.Add(Entity);
                        }
                        base.m_ObjectFlow = ObjectsFlow;
                        base.m_ResultProcess = EnumResult.SUCESS;
                    }
                }
            }
            catch (Exception ex)
            {
                base.m_ResultProcess = EnumResult.ERROR;
                SoftException.Control(ex.InnerException);
            }
            base.Start();
        }
    }
}
