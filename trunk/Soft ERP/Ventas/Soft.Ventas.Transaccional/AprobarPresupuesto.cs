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
    public class AprobarPresupuesto : ControllerApp
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
                            if (Presupuesto.EstadoAprobacion.Equals("APROBADO"))
                            {
                                throw new Exception(String.Format("El presupuesto número {0} ya se encuentra APROBADO.", Presupuesto.Numeracion));
                            }
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Aprobar_Desaprobar_Presupuesto";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@ID", Presupuesto.ID);
                            SqlCmd.Parameters.AddWithValue("@EstadoAprobacion", "APROBADO");
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
                        SoftException.Control(ex);
                    }
                }
            }
            base.Start();
        }
    }
}
