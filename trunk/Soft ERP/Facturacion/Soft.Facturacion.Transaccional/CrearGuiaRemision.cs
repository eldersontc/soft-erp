using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using System.Data.SqlClient;
using Soft.Facturacion.Entidades;
using Soft.Exceptions;

namespace Soft.Facturacion.Transaccional
{
    public class CrearGuiaRemision : ControllerApp
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
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        SqlCmd.CommandText = "pSF_Actualizar_EstadoGuiaRemision_OrdenProduccion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Trans.Enlist(SqlCmd);
                        foreach (ItemGuiaRemision ItemGuiaRemision in GuiaRemision.Items)
                        {
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@IDOP", ItemGuiaRemision.IDOrdenProduccion);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", ItemGuiaRemision.Cantidad);
                            SqlCmd.ExecuteNonQuery();
                        }
                        // Creamos la Factura
                        Sesion.Save(GuiaRemision);
                        Sesion.Flush();
                        // Actualizamos la Numeración de la Factura
                        if (GuiaRemision.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "GuiaRemision");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoEntrega");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", GuiaRemision.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", GuiaRemision.TipoDocumento.ID);
                            SqlCmd.ExecuteNonQuery();
                        }
                        Trans.Commit();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control(ex);
                    }
                    finally {
                        base.Start();
                    }
                }
            }
        }
    }
}
