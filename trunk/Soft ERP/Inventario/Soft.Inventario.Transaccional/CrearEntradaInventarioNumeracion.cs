﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using Soft.Exceptions;
using NHibernate;
using Soft.Inventario.Entidades;
using System.Xml;
using System.Data.SqlClient;


namespace Soft.Inventario.Transaccional
{
    class CrearEntradaInventarioNumeracion : ControllerApp 
    {

        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        EntradaInventario cp = (EntradaInventario)m_ObjectFlow;
                        if (cp.TipoDocumento.GeneraNumeracionAlFinal == true)
                        {
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_NumeracionFinal_EntradaInventario";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@IDCp", cp.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoCp", cp.TipoDocumento.ID);
                            SqlCmd.ExecuteNonQuery();
                            Trans.Commit();
                            m_ResultProcess = EnumResult.SUCESS;
                        }
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
