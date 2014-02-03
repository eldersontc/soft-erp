using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using System.Windows.Forms;
using Soft.Inventario.Entidades;
using System.Data.SqlClient;
using Soft.Entities;
using Soft.Exceptions;

namespace Soft.Inventario.Transaccional
{
    class CrearSalidaInvetarioNumeracion : ControllerApp 
    {

        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        SalidaInventario cp = (SalidaInventario)m_ObjectFlow;
                        if (cp.TipoDocumento.GeneraNumeracionAlFinal == true)
                        {
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_NumeracionFinal_SalidaInventario";
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
