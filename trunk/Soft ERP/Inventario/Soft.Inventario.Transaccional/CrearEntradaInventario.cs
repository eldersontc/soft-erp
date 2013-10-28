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

namespace Soft.Inventario.Transaccional
{
    public class CrearEntradaInventario : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        EntradaInventario EntradaInventario = (EntradaInventario)m_ObjectFlow;

                        Sesion.Save(EntradaInventario);

                        foreach (ItemEntradaInventario Item in EntradaInventario.Items)
                        {
                            SqlCommand SqlCmd = new SqlCommand();
                            SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                            Trans.Enlist(SqlCmd);
                            SqlCmd.CommandText = "pSF_ActualizarStocks";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@IDAlmacen", EntradaInventario.Almacen.ID);
                            SqlCmd.Parameters.AddWithValue("@IDProducto", Item.Producto.ID);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", Item.Cantidad);
                            SqlCmd.Parameters.AddWithValue("@Operacion", "Incrementar");
                            SqlCmd.ExecuteNonQuery();
                        }

                        Trans.Commit();
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        Trans.Rollback();
                        m_ResultProcess = EnumResult.ERROR;
                        MessageBox.Show(ex.StackTrace);
                    }
                }
            }
            base.Start();
        }

    }
}
