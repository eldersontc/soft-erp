using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Soft.Entities;
using Soft.Exceptions;
using Soft.Win;
using Soft.Produccion.Entidades;
using System.Xml;
using Soft.Seguridad.Entidades;

namespace Soft.Produccion.Transaccional
{
    public class ModificarOp : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Modificar");
                        OrdenProduccion OrdenProduccion = (OrdenProduccion)m_ObjectFlow;

                        if (!OrdenProduccion.EstadoAprobacion.Equals("PENDIENTE"))
                        {
                            throw new Exception("El Orden Produccion numero " + OrdenProduccion.Numeracion + " se encuentra " + OrdenProduccion.EstadoAprobacion);
                        }




                        Sesion.Update(OrdenProduccion);
                        Sesion.Save(Auditoria);
                        Trans.Commit();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control(ex);
                    }
                }
            }
            base.Start();
        }


    }
}
