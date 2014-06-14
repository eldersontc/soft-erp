using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Facturacion.Entidades;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Soft.Entities;
using Soft.Exceptions;
using Soft.Win;
using NHibernate;


namespace Soft.Facturacion.Transaccional
{
    public class EntregaFacturacion : ControllerApp    
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        FrmProgress Progreso = new FrmProgress();

                        Collection cps = (Collection)m_ObjectFlow;
                        Progreso.Start(cps.Count, "");

                        foreach (Soft.Facturacion.Entidades.Facturacion cp in cps)
                        {

                            if (cp.EstadoEntrega.Equals("ENTREGADO"))
                            {
                                throw new Exception(String.Format("El comprobante de venta número {0} ya se encuentra ENTREGADO.", cp.Numeracion));
                            }
          
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_Actualizar_EstadoEntrega_Facturacion";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@ID", cp.ID);
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
