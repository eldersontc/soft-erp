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

namespace Soft.Produccion.Transaccional
{
    public class EliminarConsolidadoOp : ControllerApp
    {

        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        ConsolidadoOp ConsolidadoOp = (ConsolidadoOp)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_Actualizar_IDConsolidadoOP_OrdenProduccion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDConsolidadoOp", ConsolidadoOp.ID);
                        SqlCmd.Parameters.AddWithValue("@Items", "<Items/>");
                        SqlCmd.ExecuteNonQuery();
                        Sesion.Delete(ConsolidadoOp);
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
