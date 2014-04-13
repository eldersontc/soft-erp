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
    public class CrearSalidaCaja : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        SalidaCaja SalidaCaja = (SalidaCaja)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        // Actualizamos el saldo de la Caja
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.CommandText = "pSF_Actualizar_Saldo_Caja";
                        Trans.Enlist(SqlCmd);
                        SqlCmd.Parameters.AddWithValue("@IDCaja", SalidaCaja.Caja.ID);
                        SqlCmd.Parameters.AddWithValue("@Monto", SalidaCaja.Total * -1);
                        SqlCmd.ExecuteNonQuery();
                        // Creamos la Salida de Caja
                        Sesion.Save(SalidaCaja);
                        Sesion.Flush();
                        // Creamos una Deuda.
                        if(SalidaCaja.TipoCaja.GeneraDeuda){
                            Deuda Deuda = new Deuda();
                            Deuda.Tipo = SalidaCaja.TipoCaja.TipoDeuda;
                            Deuda.TipoDocumento = "Salida de Caja";
                            Deuda.IDDocumento = SalidaCaja.ID;
                            Deuda.Descripcion = SalidaCaja.TipoDocumento.Nombre;
                            Deuda.IDSocioNegocio = (SalidaCaja.SocioNegocio != null) ? SalidaCaja.SocioNegocio.ID : null;
                            Deuda.Saldo = SalidaCaja.Total;
                            Deuda.Total = SalidaCaja.Total;
                            Sesion.Save(Deuda);
                        }
                        // Actualizamos la Numeración de la Salida de Caja
                        if (SalidaCaja.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "SalidaCaja");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoCaja");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", SalidaCaja.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", SalidaCaja.TipoDocumento.ID);
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
