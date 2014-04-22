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
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        // Creamos la Entrada de Inventario.
                        Sesion.Save(EntradaInventario);
                        Sesion.Flush();
                        // Actualizamos los Stocks.
                        foreach (ItemEntradaInventario Item in EntradaInventario.Items)
                        {
                            SqlCmd.CommandText = "pSF_ActualizarStocks";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@IDAlmacen", EntradaInventario.Almacen.ID);
                            SqlCmd.Parameters.AddWithValue("@IDProducto", Item.Producto.ID);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", Item.Cantidad * Item.Factor);
                            SqlCmd.Parameters.AddWithValue("@Operacion", "Incrementar");
                            SqlCmd.ExecuteNonQuery();
                        }
                        // Actualizamos la Numeración de la Entrada de Inventario
                        if (EntradaInventario.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "EntradaInventario");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoDocumentoInventario");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", EntradaInventario.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", EntradaInventario.TipoDocumento.ID);
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
