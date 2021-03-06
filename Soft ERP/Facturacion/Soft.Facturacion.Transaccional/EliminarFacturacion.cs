﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using System.Data.SqlClient;
using Soft.Facturacion.Entidades;
using Soft.Exceptions;
using Soft.Entities;

namespace Soft.Facturacion.Transaccional
{
    public class EliminarFacturacion : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Soft.Facturacion.Entidades.Facturacion Facturacion = (Soft.Facturacion.Entidades.Facturacion)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        SqlCmd.CommandText = "pSF_Actualizar_EstadoFacturacion_OrdenProduccion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Trans.Enlist(SqlCmd);
                        foreach (ItemFacturacion ItemFacturacion in Facturacion.Items)
                        {
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@IDOP", ItemFacturacion.IDOrdenProduccion);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", ItemFacturacion.Cantidad * -1);
                            SqlCmd.ExecuteNonQuery();
                        }
                        Sesion.Delete(Facturacion);
                        // Eliminamos la Deuda.
                        Deuda Deuda = (Deuda)HelperNHibernate.GetEntityByField("Deuda", "IDDocumento", Facturacion.ID);
                        if (Deuda != null)
                        {
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
                    finally {
                        base.Start();
                    }
                }
            }
        }
    }
}
