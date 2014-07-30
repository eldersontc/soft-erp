using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Soft.Ventas.Entidades;
using System.Data.SqlClient;
using Soft.Exceptions;
using Soft.Seguridad.Entidades;

namespace Soft.Ventas.Transaccional
{
    public class ModificarSolicitudCotizacion : ControllerApp
    {

        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Modificación");
                        SolicitudCotizacion cp = (SolicitudCotizacion)m_ObjectFlow;

                        SolicitudCotizacion cpactual = (SolicitudCotizacion)HelperNHibernate.GetEntityByID("SolicitudCotizacion", cp.ID);

                        if (cpactual.EstadoAprobacion.Equals("PENDIENTE"))
                        {
                            Sesion.Update(cp);
                            Sesion.Save(Auditoria);
                            Trans.Commit();
                            m_ResultProcess = EnumResult.SUCESS;
                        }
                        else {
                            throw new Exception("Estado "+cpactual.EstadoAprobacion+" del documento no permite modificaciones");
                        }
                        

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
