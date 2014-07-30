using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using NHibernate;
using Soft.Ventas.Entidades;
using System.Xml;
using System.Data.SqlClient;
using Soft.Seguridad.Entidades;
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Transaccional
{
    public class CrearCotizacion : ControllerApp 
    {

        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Creación");
                        Cotizacion cp = (Cotizacion)m_ObjectFlow;

                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;

                        SqlCmd.CommandText = "pSF_Actualizar_EstadoCotizacion_SolicitudCotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.Parameters.Clear();
                        SqlCmd.Parameters.AddWithValue("@IDSolicitudCotizacion", cp.IDSolicitudCotizacion);
                        SqlCmd.ExecuteNonQuery();


                        foreach (ItemCotizacion item in cp.Items)
                        {
                            item.RequerimientosServicio.Clear();
                            item.RequerimientosMaterial.Clear();
                            foreach (ItemCotizacionServicio itemSer in item.Servicios)
                            {

                                if (itemSer.Material != null)
                                {
                                    RequerimientoMaterialItemCotizacion reqmaterial = new RequerimientoMaterialItemCotizacion();
                                    reqmaterial.Material = itemSer.Material;
                                    reqmaterial.Unidad = itemSer.UnidadMaterial;
                                    reqmaterial.Cantidad = itemSer.CantidadMaterial;
                                    reqmaterial.Costo = itemSer.CostoMaterial;

                                    item.RequerimientosMaterial.Add(reqmaterial);
                                }
                                if (itemSer.Servicio != null)
                                {
                                    RequerimientoServicioItemCotizacion reqservicio = new RequerimientoServicioItemCotizacion();
                                    reqservicio.Servicio = itemSer.Servicio;
                                    reqservicio.Unidad = itemSer.UnidadServicio;
                                    reqservicio.Cantidad = itemSer.CantidadServicio;
                                    reqservicio.Costo = itemSer.CostoServicio;

                                    item.RequerimientosServicio.Add(reqservicio);
                                }
                              

                            }
                        }

                        Sesion.Save(cp);
                        Sesion.Flush();

                        if (cp.TipoDocumento.GeneraNumeracionAlFinal == true)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "Cotizacion");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoCotizacion");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", cp.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", cp.TipoDocumento.ID);
                            SqlCmd.ExecuteNonQuery();
                        }





                        Sesion.Save(Auditoria);
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
