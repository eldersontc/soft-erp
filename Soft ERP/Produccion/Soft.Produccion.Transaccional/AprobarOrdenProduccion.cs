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
using Soft.Seguridad.Entidades;



namespace Soft.Produccion.Transaccional
{
    public class AprobarOrdenProduccion : ControllerApp
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
                        Collection Cps = (Collection)m_ObjectFlow;


                        Progreso.Start(Cps.Count, "Aprobando Cotizaciones ...");
                        foreach (OrdenProduccion Cp in Cps)
                        {
                            Auditoria Auditoria = Auditoria.ConstruirAuditoria(Cp, "Aprobacion");
                            if (Cp.EstadoAprobacion.Equals("APROBADO"))
                            {
                                throw new Exception(String.Format("La cotización número {0} ya se encuentra APROBADA.", Cp.Numeracion));
                            }
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Aprobar_Desaprobar_OrdenProduccion";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@ID", Cp.ID);
                            SqlCmd.Parameters.AddWithValue("@EstadoAprobacion", "APROBADO");
                            SqlCmd.ExecuteNonQuery();
                            Sesion.Save(Auditoria);
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
