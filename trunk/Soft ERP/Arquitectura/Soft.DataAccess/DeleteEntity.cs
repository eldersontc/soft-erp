using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using NHibernate;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data;
using Soft.DataAccess;
using Soft.Exceptions;
using System.Data.SqlClient;

namespace Soft.DataAccess
{
    public class DeleteEntity : ControllerApp 
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession()) {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Sesion.Delete(base.m_ObjectFlow);
                        Trans.Commit();
                        base.m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        base.m_ResultProcess = EnumResult.ERROR;
                        //int code = ex.ErrorCode;
                        SoftException.Control(ex, MessageBoxIcon.Error);
                    }
                }   
            }
            base.Start();
        }

    }
}
