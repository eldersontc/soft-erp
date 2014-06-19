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
    public class CrearCotizacion : ControllerApp 
    {

        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Cotizacion cp = (Cotizacion)m_ObjectFlow;

                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;

                        SqlCmd.CommandText = "pSF_Actualizar_EstadoCotizacion_SolicitudCotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.Parameters.Clear();
                        SqlCmd.Parameters.AddWithValue("@IDSolicitudCotizacion", cp.IDSolicitudCotizacion);
                        SqlCmd.ExecuteNonQuery();

                        
                        Sesion.Save(cp);
                        Sesion.Flush();

                        if (cp.TipoDocumento.GeneraNumeracionAlFinal == true)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "Cotizacion");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoCotizacion");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", cp.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", cp.TipoDocumento.ID);
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
                    finally
                    {
                        base.Start();
                    }
                }
            }
        }
    }
}
