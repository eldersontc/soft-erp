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
using Soft.Entities;

namespace Soft.Ventas.Transaccional
{
    public class CrearComprobantes:  ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Documento ob= (Documento) m_ObjectFlow;
                        TipoDocumento td = (TipoDocumento)HelperNHibernate.GetEntityByID(ob.TipoDocumento.EntidadTipoDocumento, ob.TipoDocumento.ID.ToString());
                        ob.TipoDocumento = td;

                        
                       

                        if (td.GeneraNumeracionAlFinal)
                        {

                            ob.GenerarNumCpAlFinal();
                        

                        //SqlCommand SqlCmd = new SqlCommand();
                        //SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        //Trans.Enlist(SqlCmd);
                        //SqlCmd.CommandText = "pSF_GenerarNumeracionAlFinal";
                        //SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //SqlCmd.Parameters.AddWithValue("@Comprobante", td.Entidad);
                        //SqlCmd.Parameters.AddWithValue("@IDComprobante", ob.ID);
                        //SqlCmd.Parameters.AddWithValue("@TipoDocumento", td.EntidadTipoDocumento);
                        //SqlCmd.ExecuteNonQuery();
                        //Trans.Commit();
                        }
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
