using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Finanzas.Entidades;
using Soft.Exceptions;
using NHibernate;
using System.Data.SqlClient;
using Soft.Entities;

namespace Soft.Finanzas.Transaccional
{
    public class CrearEntradaCaja : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        EntradaCaja EntradaCaja = (EntradaCaja)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        // Actualizamos el saldo de la Caja
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.CommandText = "pSF_Actualizar_Saldo_Caja";
                        Trans.Enlist(SqlCmd);
                        SqlCmd.Parameters.AddWithValue("@IDCaja", EntradaCaja.Caja.ID);
                        SqlCmd.Parameters.AddWithValue("@Monto", EntradaCaja.Total);
                        SqlCmd.ExecuteNonQuery();
                        // Creamos la Salida de Caja
                        Sesion.Save(EntradaCaja);
                        Sesion.Flush();
                        // Creamos una Deuda.
                        if (EntradaCaja.TipoCaja.GeneraDeuda)
                        {
                            Deuda Deuda = new Deuda();
                            Deuda.Tipo = EntradaCaja.TipoCaja.TipoDeuda;
                            Deuda.TipoDocumento = "Entrada de Caja";
                            Deuda.IDDocumento = EntradaCaja.ID;
                            Deuda.Descripcion = EntradaCaja.TipoDocumento.Nombre;
                            Deuda.IDSocioNegocio = (EntradaCaja.SocioNegocio != null) ? EntradaCaja.SocioNegocio.ID : null;
                            Deuda.Saldo = EntradaCaja.Total;
                            Deuda.Total = EntradaCaja.Total;
                            Sesion.Save(Deuda);
                        }
                        // Actualizamos la Numeración de la Salida de Caja
                        if (EntradaCaja.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "EntradaCaja");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoCaja");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", EntradaCaja.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", EntradaCaja.TipoDocumento.ID);
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
