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
    public class CrearSalidaInventario : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        SalidaInventario SalidaInventario = (SalidaInventario)m_ObjectFlow;

                        Sesion.Save(SalidaInventario);
                        Sesion.Flush();

                        foreach (ItemSalidaInventario Item in SalidaInventario.Items)
                        {
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_ActualizarStocks";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@IDAlmacen", SalidaInventario.Almacen.ID);
                            SqlCmd.Parameters.AddWithValue("@IDProducto", Item.Producto.ID);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", Item.Cantidad * Item.Factor);
                            SqlCmd.Parameters.AddWithValue("@Operacion", "Decrementar");
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
                }
            }
            base.Start();
        }

    }
}
