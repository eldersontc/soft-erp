using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Soft.Ventas.Entidades;
using Soft.Exceptions;

namespace Soft.Ventas.Transaccional
{
    public class EliminarRendicionCotizacion : ControllerApp 
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        RendicionCotizacion Rendicion = (RendicionCotizacion)m_ObjectFlow;

                        string sql = string.Format("UPDATE Cotizacion SET EstadoRendicion = 'PENDIENTE' WHERE ID = '{0}'", Rendicion.IDCotizacion);

                        var dbCommand = Sesion.Connection.CreateCommand();
                        Sesion.Transaction.Enlist(dbCommand);
                        dbCommand.CommandText = sql;
                        dbCommand.ExecuteNonQuery();

                        Sesion.Delete(Rendicion);
                        Trans.Commit();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control(ex.InnerException);
                    }
                }
            }
            base.Start();
        }
    }
}
