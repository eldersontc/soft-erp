﻿using System;
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
    public class EliminarSalidaCaja : ControllerApp
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
                        SqlCmd.Parameters.AddWithValue("@Monto", SalidaCaja.Total);
                        SqlCmd.ExecuteNonQuery();
                        // Eliminamos la Salida de Caja
                        Sesion.Delete(SalidaCaja);
                        // Eliminamos la Deuda.
                        Deuda Deuda = (Deuda)HelperNHibernate.GetEntityByField("Deuda", "IDDocumento", SalidaCaja.ID);
                        if (Deuda != null) {
                            if (Deuda.Total > Deuda.Saldo)
                                throw new Exception("Ya se realizaron pagos de este documento.");
                            else
                                Sesion.Delete(Deuda);
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
