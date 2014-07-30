using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using NHibernate;
using Soft.Produccion.Entidades;
using System.Xml;
using System.Data.SqlClient;
using Soft.Seguridad.Entidades;

namespace Soft.Produccion.Transaccional
{
    public class CrearOrdenProduccion : ControllerApp
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
                   
                        OrdenProduccion OrdenProduccion = (OrdenProduccion)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        // Valida la OP.
                        SqlCmd.CommandText = "pSF_Validar_Orden_Producion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDItemPresupuesto", OrdenProduccion.IDItemPresupuesto);
                        SqlCmd.ExecuteNonQuery();
                        // Creamos la OP.
                        Sesion.Save(OrdenProduccion);
                        Sesion.Flush();
                        // Actualizamos la Numeración de la Salida de Inventario
                        if (OrdenProduccion.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "OrdenProduccion");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoOrdenProduccion");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", OrdenProduccion.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", OrdenProduccion.TipoDocumento.ID);
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
                }
            }
            base.Start();
        }
    }
}
