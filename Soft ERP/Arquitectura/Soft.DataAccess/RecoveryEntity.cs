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

namespace Soft.DataAccess
{
    public class RecoveryEntity : ControllerApp
    {
        public override void Start()
        {
            try
            {
                if (base.m_ItemsSelecteds.Count > 0)
                {
                    using (ISession Sesion = m_SessionFactory.OpenSession())
                    {
                        String ID = base.m_ItemsSelecteds[0];
                        base.m_ObjectFlow = Sesion.Get(base.m_EntidadSF.NombreClase, ID);
                        ((Parent)base.m_ObjectFlow).NewInstance = false;
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
