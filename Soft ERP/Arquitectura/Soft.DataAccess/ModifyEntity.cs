using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Seguridad.Entidades;
using NHibernate;
using Soft.DataAccess;
using Soft.Seguridad.Entidades;
using System.Windows.Forms;
using Soft.Exceptions;

namespace Soft.DataAccess
{
    public class ModifyEntity : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession()) {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Modificación");
                        Sesion.Update(base.m_ObjectFlow);
                        Sesion.Save(Auditoria);
                        Trans.Commit();
                        base.m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        base.m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control(ex.InnerException);
                    }
                }
            }
            base.Start();
        }

    }
}
