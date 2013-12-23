using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using NHibernate;
using Soft.Ventas.Entidades;
using System.Xml;
using System.Data.SqlClient;

namespace Soft.Ventas.Transaccional
{
    public class EliminarPresupuesto : ControllerApp 
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Presupuesto Presupuesto = (Presupuesto)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_Actualizar_IDPresupuesto_Cotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDPresupuesto", Presupuesto.ID);
                        SqlCmd.Parameters.AddWithValue("@Items", "<Items/>");
                        SqlCmd.ExecuteNonQuery();
                        Sesion.Delete(Presupuesto);
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
