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

namespace Soft.Ventas.Transaccional
{
    public class DesaprobarCotizacion : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Collection Cotizaciones = (Collection)m_ObjectFlow;
                        foreach (Cotizacion Cotizacion in Cotizaciones)
                        {
                            if (Cotizacion.EstadoAprobacion.Equals("DESAPROBADO"))
                            {
                                throw new Exception(String.Format("La cotización número {0} ya se encuentra DESAPROBADA.", Cotizacion.Numeracion));
                            }
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Aprobar_Desaprobar_Cotizacion";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@ID", Cotizacion.ID);
                            SqlCmd.Parameters.AddWithValue("@EstadoAprobacion", "DESAPROBADO");
                            SqlCmd.ExecuteNonQuery();
                        }
                        Trans.Commit();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control(ex.InnerException);
                    }
                }
            }
            base.Start();
        }
    }
}
