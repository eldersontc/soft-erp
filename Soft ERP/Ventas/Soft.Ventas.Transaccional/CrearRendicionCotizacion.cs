using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using NHibernate;
using Soft.Seguridad.Entidades;
using Soft.Ventas.Entidades;
using System.Data.SqlClient;

namespace Soft.Ventas.Transaccional
{
    public class CrearRendicionCotizacion : ControllerApp 
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Creación");
                        RendicionCotizacion Rendicion = (RendicionCotizacion)m_ObjectFlow;

                        string sql = string.Format("UPDATE Cotizacion SET EstadoRendicion = 'TOTAL' WHERE ID = '{0}'", Rendicion.IDCotizacion);

                        var dbCommand = Sesion.Connection.CreateCommand();
                        Sesion.Transaction.Enlist(dbCommand);
                        dbCommand.CommandText = sql;
                        dbCommand.ExecuteNonQuery();

                        Sesion.Save(Rendicion);
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
                    finally
                    {
                        base.Start();
                    }
                }
            }
        }
    }
}
