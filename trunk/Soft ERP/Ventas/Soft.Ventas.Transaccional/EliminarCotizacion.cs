using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Soft.Ventas.Entidades;
using System.Data.SqlClient;
using Soft.Exceptions;

namespace Soft.Ventas.Transaccional
{
    public class EliminarCotizacion : ControllerApp 
    {


        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Cotizacion Cotizacion = (Cotizacion)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_EliminarCotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDSolicitudCotizacion", Cotizacion.IDSolicitudCotizacion);
                        SqlCmd.ExecuteNonQuery();
                        Sesion.Delete(Cotizacion);
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
