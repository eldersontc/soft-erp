using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Soft.Facturacion.Entidades;
using Soft.Entities;
using Soft.Exceptions;

namespace Soft.Facturacion.Transaccional
{
    public class EliminarNotaDebito : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        NotaDebito NotaDebito = (NotaDebito)m_ObjectFlow;
                        // Eliminamos la Nota de Débito
                        Sesion.Delete(NotaDebito);
                        // Eliminamos la Deuda.
                        Deuda Deuda = (Deuda)HelperNHibernate.GetEntityByField("Deuda", "IDDocumento", NotaDebito.ID);
                        if (Deuda != null)
                        {
                            if (Deuda.Total > Deuda.Saldo)
                                throw new Exception("Ya se realizaron pagos de este documento.");
                            else
                                Sesion.Delete(Deuda);
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
                    finally
                    {
                        base.Start();
                    }
                }
            }
        }
    }
}
