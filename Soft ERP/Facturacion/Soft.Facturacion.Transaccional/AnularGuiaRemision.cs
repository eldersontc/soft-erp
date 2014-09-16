using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Soft.Facturacion.Entidades;
using System.Data.SqlClient;
using Soft.Exceptions;

namespace Soft.Facturacion.Transaccional
{
    public class AnularGuiaRemision : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        GuiaRemision GuiaRemision = (GuiaRemision)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();

                        if (GuiaRemision.Anulado) 
                        {
                            throw new Exception(string.Format("La guía de remisión N° {0} ya está anulada.", GuiaRemision.Numeracion));
                        }

                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        SqlCmd.CommandText = "pSF_Actualizar_EstadoGuiaRemision_OrdenProduccion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Trans.Enlist(SqlCmd);

                        foreach (ItemGuiaRemision ItemGuiaRemision in GuiaRemision.Items)
                        {
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@IDOP", ItemGuiaRemision.IDOrdenProduccion);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", ItemGuiaRemision.Cantidad * -1);
                            SqlCmd.ExecuteNonQuery();
                        }

                        SqlCmd.CommandText = string.Format("UPDATE GuiaRemision SET Anulado = 1 WHERE ID = '{0}'", GuiaRemision.ID);
                        SqlCmd.CommandType = System.Data.CommandType.Text;
                        SqlCmd.Parameters.Clear();
                        SqlCmd.ExecuteNonQuery();

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
