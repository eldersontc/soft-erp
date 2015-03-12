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
using Soft.Seguridad.Entidades;


namespace Soft.Ventas.Transaccional
{
    public class CrearSolicitudCotizacion : ControllerApp
    {
        public override void Start()
        {
            //try
            //{
            //    this.IniciarTransaccion();
            //    SolicitudCotizacion solicitudCotizacion = (SolicitudCotizacion)m_ObjectFlow;
            //    if (solicitudCotizacion.TipoDocumento.GeneraNumeracionAlFinal)
            //    {
            //        SqlCommand sqlCmd = new SqlCommand();
            //        sqlCmd.Connection = (SqlConnection)m_Sesion.Connection;
            //        m_Transaccion.Enlist(sqlCmd);
            //        {
            //            sqlCmd.CommandText = "pSF_Generar_Numeracion";
            //            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            //            sqlCmd.Parameters.Clear();
            //            sqlCmd.Parameters.AddWithValue("@Documento", "SolicitudCotizacion");
            //            sqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoSolicitudCotizacion");
            //            sqlCmd.Parameters.AddWithValue("@IDDocumento", solicitudCotizacion.ID);
            //            sqlCmd.Parameters.AddWithValue("@IDTipoDocumento", solicitudCotizacion.TipoDocumento.ID);
            //            sqlCmd.ExecuteNonQuery();
            //        }
            //    }
            //    this.Agregar(solicitudCotizacion);
            //    this.FinalizarTransaccion();

            //}
            //catch (Exception ex)
            //{
            //    this.FinalizarTransaccion(true);
            //    m_ResultProcess = EnumResult.ERROR;
            //    SoftException.Control(ex);
            //}
            //finally
            //{
            //    this.CerrarSesion();
            //}
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Creación");
                        SolicitudCotizacion solicitud = (SolicitudCotizacion)m_ObjectFlow;

                        Sesion.Save(solicitud);
                        Sesion.Flush();

                        if (solicitud.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            {
                                SqlCmd.CommandText = "pSF_Generar_Numeracion";
                                SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                SqlCmd.Parameters.Clear();
                                SqlCmd.Parameters.AddWithValue("@Documento", "SolicitudCotizacion");
                                SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoSolicitudCotizacion");
                                SqlCmd.Parameters.AddWithValue("@IDDocumento", solicitud.ID);
                                SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", solicitud.TipoDocumento.ID);
                                SqlCmd.ExecuteNonQuery();
                            }
                        }
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
                }
            }
            base.Start();
        }
    }
}
