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
                        SqlCommand SqlCmd = new SqlCommand();
                        SqlCmd.Connection = (SqlConnection)Sesion.Connection;
                        Trans.Enlist(SqlCmd);
                        // Creamos la Salida de Inventario.
                        Sesion.Save(SalidaInventario);
                        Sesion.Flush();
                        // Actualizamos los Stocks.
                        foreach (ItemSalidaInventario Item in SalidaInventario.Items)
                        {
                            SqlCmd.CommandText = "pSF_ActualizarStocks";
                            SqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlCmd.Parameters.AddWithValue("@IDAlmacen", SalidaInventario.Almacen.ID);
                            SqlCmd.Parameters.AddWithValue("@IDProducto", Item.Producto.ID);
                            SqlCmd.Parameters.AddWithValue("@Cantidad", Item.Cantidad * Item.Factor);
                            SqlCmd.Parameters.AddWithValue("@Operacion", "Decrementar");
                            SqlCmd.ExecuteNonQuery();
                        }
                        // Actualizamos la Numeración de la Salida de Inventario
                        if (SalidaInventario.TipoDocumento.GeneraNumeracionAlFinal)
                        {
                            SqlCmd.CommandText = "pSF_Generar_Numeracion";
                            SqlCmd.Parameters.Clear();
                            SqlCmd.Parameters.AddWithValue("@Documento", "SalidaInventario");
                            SqlCmd.Parameters.AddWithValue("@TipoDocumento", "TipoDocumentoInventario");
                            SqlCmd.Parameters.AddWithValue("@IDDocumento", SalidaInventario.ID);
                            SqlCmd.Parameters.AddWithValue("@IDTipoDocumento", SalidaInventario.TipoDocumento.ID);
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
