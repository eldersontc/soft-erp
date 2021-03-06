﻿using System;
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
    public class ModificarPresupuesto : ControllerApp 
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Auditoria Auditoria = Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Modificación");
     
                        Presupuesto Presupuesto = (Presupuesto)m_ObjectFlow;

                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        SqlCmd.CommandText = "pSF_Actualizar_IDPresupuesto_Cotizacion";
                        SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCmd.Parameters.AddWithValue("@IDPresupuesto", Presupuesto.ID);
                        SqlCmd.Parameters.AddWithValue("@Items", ContruirXML(Presupuesto));
                        SqlCmd.ExecuteNonQuery();

						if (Presupuesto.EstadoAceptacion.Equals("ENVIADO")) {
                            throw new Exception(String.Format("El presupuesto número {0} ya fue enviado al cliente", Presupuesto.Numeracion));
                        }
                        if (EstaModificado(Presupuesto))
                        {
                            Presupuesto.EstadoAprobacion = "MODIFICADO";
                        }

                        Sesion.Update(Presupuesto);
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

        public Boolean EstaModificado(Presupuesto Presupuesto)
        {
            Boolean Modificado = false;


            foreach (ItemPresupuesto Item in Presupuesto.Items)
            {
                ItemPresupuesto ItemBD = (ItemPresupuesto)HelperNHibernate.GetEntityByID("ItemPresupuesto", Item.ID);

                if (Item.Recargo != ItemBD.Recargo) { Modificado = true; break; }
            }
            return Modificado;
        }

        public String ContruirXML(Presupuesto Presupuesto)
        {
            XmlDocument XML = new XmlDocument();
            XML.LoadXml("<Items/>");
            foreach (ItemPresupuesto Item in Presupuesto.Items)
            {
                XmlNode Node = XML.CreateNode(XmlNodeType.Element, "Item", "");
                XML.DocumentElement.AppendChild(Node);
                XmlAttribute Attribute = XML.CreateAttribute("IDCotizacion");
                Attribute.Value = Item.IDCotizacion;
                Node.Attributes.Append(Attribute);
            }
            return XML.OuterXml;
        }
    }
}
