using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Data.SqlClient;
using Soft.Entities;
using Soft.Exceptions;
using Soft.Win;
using Soft.Produccion.Entidades;
using System.Xml;
using Soft.Seguridad.Entidades;


namespace Soft.Produccion.Transaccional
{
    public class ModificarConsolidadoOp: ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Modificar");
                        ConsolidadoOp ConsolidadoOp = (ConsolidadoOp)m_ObjectFlow;

                        if (!ConsolidadoOp.EstadoAprobacion.Equals("PENDIENTE")) {
                            throw new Exception("El consolidado numero " + ConsolidadoOp.Numeracion +" se encuentra "+ConsolidadoOp.EstadoAprobacion);
                        }


                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_Actualizar_IDConsolidadoOP_OrdenProduccion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDConsolidadoOp", ConsolidadoOp.ID);
                        SqlCmd.Parameters.AddWithValue("@Items", ContruirXML(ConsolidadoOp));
                        SqlCmd.ExecuteNonQuery();


                        Sesion.Update(ConsolidadoOp);
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

        public String ContruirXML(ConsolidadoOp ConsolidadoOp)
        {
            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<Items/>");
            foreach (ItemConsolidadoOp Item in ConsolidadoOp.Items)
            {
                XmlNode Node = XML.CreateNode(XmlNodeType.Element, "Item", "");
                XML.DocumentElement.AppendChild(Node);
                XmlAttribute Attribute = XML.CreateAttribute("IDOrdenProduccion");
                Attribute.Value = Item.IDOrdenProduccion;
                Node.Attributes.Append(Attribute);
            }
            return XML.OuterXml;
        }

    }
}
