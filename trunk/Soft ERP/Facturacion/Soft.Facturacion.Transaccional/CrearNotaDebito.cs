using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Facturacion.Entidades;
using NHibernate;
using System.Data.SqlClient;
using Soft.Exceptions;

namespace Soft.Facturacion.Transaccional
{
    public class CrearNotaDebito: ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        NotaDebito NotaDebito = (NotaDebito)m_ObjectFlow;
                        // Creamos la Nota de Débito
                        Sesion.Save(NotaDebito);
                        Sesion.Flush();
                        // Actualizamos la Numeración de la Nota de Débito
                        if (NotaDebito.TipoNotaDebito.GeneraNumeracionAlFinal)
                        {
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.Parameters.AddWithValue("@Documento", "NotaDebito");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoNotaDebito");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", NotaDebito.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", NotaDebito.TipoNotaDebito.ID);
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
