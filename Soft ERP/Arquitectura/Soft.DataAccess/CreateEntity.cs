﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Seguridad.Entidades;
using NHibernate;
using System.Data;
using Soft.DataAccess;
using System.Windows.Forms;
using Soft.Seguridad.Entidades;

namespace Soft.DataAccess
{
    public class CreateEntity : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession()) {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow,"Creación");
                        Sesion.Save(base.m_ObjectFlow);
                        Sesion.Save(Auditoria);
                        Trans.Commit();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        m_ResultProcess = EnumResult.ERROR;
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                }
            }
            base.Start();
        }

    }
}