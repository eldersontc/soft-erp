﻿using System;
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
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Creación");
                        SolicitudCotizacion cp = (SolicitudCotizacion)m_ObjectFlow;

                        Sesion.Save(cp);
                        Sesion.Flush();

                        if (cp.TipoDocumento.GeneraNumeracionAlFinal)
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
                                SqlCmd.Parameters.AddWithValue("@IDDocumento", cp.ID);
                                SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", cp.TipoDocumento.ID);
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
