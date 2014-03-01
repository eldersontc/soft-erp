using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using NHibernate;
using Soft.Produccion.Entidades;
using System.Xml;
using System.Data.SqlClient;

namespace Soft.Produccion.Transaccional
{
    public class CrearOrdenProduccion : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        OrdenProduccion cp = (OrdenProduccion)m_ObjectFlow;
                        
                        
                        
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Validar_OrdendeProducion";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@IDItemPresupuesto", cp.IDItemPresupuesto);
                            SqlCmd.ExecuteNonQuery();
                            Sesion.Save(cp);   
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
