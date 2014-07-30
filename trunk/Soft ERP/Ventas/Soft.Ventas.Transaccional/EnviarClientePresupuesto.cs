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
using Soft.Seguridad.Entidades;

namespace Soft.Ventas.Transaccional
{
    public class EnviarClientePresupuesto : ControllerApp
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
                        Collection Presupuestos = (Collection)m_ObjectFlow;
                        Progreso.Start(Presupuestos.Count, "Aprobando Presupuestos ...");
                        foreach (Presupuesto Presupuesto in Presupuestos)
                        {
                            Auditoria Auditoria = Auditoria.ConstruirAuditoria(Presupuesto, "Enviar Cliente");

                            if (!Presupuesto.EstadoAprobacion.Equals("APROBADO")) {



                                throw new Exception(String.Format("El presupuesto número {0} no esta aprobado", Presupuesto.Numeracion));
                            }


                            if (!Presupuesto.EstadoAceptacion.Equals("PENDIENTE"))
                            {
                                throw new Exception(String.Format("El presupuesto número {0} ya fue enviado", Presupuesto.Numeracion));
                            }

							
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Enviar_Cliente_Presupuesto";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@ID", Presupuesto.ID);
                            SqlCmd.Parameters.AddWithValue("@EstadoAceptacion", "ENVIADO");
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
