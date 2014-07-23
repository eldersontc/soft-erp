using System;
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
    public class CrearFacturacion : ControllerApp
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
                            SqlCmd.Parameters.AddWithValue("@Cantidad", ItemFacturacion.Cantidad);
                            SqlCmd.ExecuteNonQuery();
                        }
                        // Creamos la Factura
                        Sesion.Save(Facturacion);
                        Sesion.Flush();
                        // Creamos una Deuda.
                        if (Facturacion.TipoFacturacion.GeneraDeuda)
                        {
                            Deuda Deuda = new Deuda();
                            Deuda.Tipo = Facturacion.TipoFacturacion.TipoDeuda;
                            Deuda.TipoDocumento = Facturacion.TipoFacturacion.Comprobante;
                            Deuda.IDDocumento = Facturacion.ID;
                            Deuda.Descripcion = Facturacion.TipoDocumento.Nombre;
                            Deuda.IDSocioNegocio = (Facturacion.Cliente != null) ? Facturacion.Cliente.ID : null;
                            Deuda.Saldo = Facturacion.Total;
                            Deuda.Total = Facturacion.Total;
                            Sesion.Save(Deuda);
                        }
                        // Actualizamos la Numeración de la Factura
                        if (Facturacion.TipoFacturacion.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "Facturacion");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoFacturacion");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", Facturacion.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", Facturacion.TipoFacturacion.ID);
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
