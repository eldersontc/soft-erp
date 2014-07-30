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

namespace Soft.Ventas.Transaccional
{
    public class CrearPresupuesto : ControllerApp 
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
                        Presupuesto Presupuesto = (Presupuesto)m_ObjectFlow;
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_Actualizar_IDPresupuesto_Cotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDPresupuesto", Presupuesto.ID);
                        SqlCmd.Parameters.AddWithValue("@Items", ContruirXML(Presupuesto));
                        SqlCmd.ExecuteNonQuery();

                        if (EstaModificado(Presupuesto))
                        {
                            Presupuesto.EstadoAprobacion = "MODIFICADO";
                        }

                        Sesion.Save(Presupuesto);
                        Sesion.Flush();

                        // Actualizamos la Numeración de la Factura
                        if (Presupuesto.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "Presupuesto");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoPresupuesto");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", Presupuesto.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", Presupuesto.TipoDocumento.ID);
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

        public Boolean EstaModificado(Presupuesto Presupuesto) {
            Boolean Modificado = false;
            foreach (ItemPresupuesto Item in Presupuesto.Items)
            {
                if (Item.Recargo > 0) { Modificado = true; break; } 
            }
            return Modificado;
        }

        public String ContruirXML(Presupuesto Presupuesto) {
            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<Items/>");
            foreach (ItemPresupuesto Item in Presupuesto.Items)
            {
                XmlNode Node = XML.CreateNode(XmlNodeType.Element,"Item", "");
                XML.DocumentElement.AppendChild(Node);
                XmlAttribute Attribute = XML.CreateAttribute("IDCotizacion");
                Attribute.Value = Item.IDCotizacion;
                Node.Attributes.Append(Attribute);
            }
            return XML.OuterXml;
        }

    }
}
