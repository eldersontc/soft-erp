using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Ventas.Entidades;
using NHibernate;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Soft.Entities;
using Soft.Exceptions;
using Soft.Win;

namespace Soft.Ventas.Transaccional
{
    public class DesaprobarSolicitudCotizacion : ControllerApp 
    {
        public override void Start()
        {

            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    FrmProgress Progreso = new FrmProgress();
                    try
                    {
                        Collection Solicitudes = (Collection)m_ObjectFlow;
                        Progreso.Start(Solicitudes.Count, "Desaprobando Solicitudes ...");
                        foreach (SolicitudCotizacion Solicitud in Solicitudes)
                        {
                            if (Solicitud.EstadoAprobacion.Equals("DESAPROBADO"))
                            {
                                throw new Exception(String.Format("La solicitud de cotización número {0} ya se encuentra DESAPROBADA.", Solicitud.Numeracion));
                            }
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Aprobar_Desaprobar_SolicitudCotizacion";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@ID", Solicitud.ID);
                            SqlCmd.Parameters.AddWithValue("@EstadoAprobacion", "DESAPROBADO");
                            SqlCmd.ExecuteNonQuery();
                            Progreso.Next();
                        }
                        Trans.Commit();
                        Progreso.Close();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        Progreso.Close();
                        m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control((ex.InnerException != null) ? ex.InnerException : ex);
                    }
                }
            }
            base.Start();
        }
    }
}
