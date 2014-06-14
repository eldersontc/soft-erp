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


namespace Soft.Ventas.Transaccional
{
    class ValidarOrdenProduccion:ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Presupuesto Presupuesto = (Presupuesto)m_ObjectFlow;
                        if (!Presupuesto.EstadoAceptacion.Equals("ACEPTADO"))
                        {
                            throw new Exception(String.Format("El presupuesto número {0} aun no esta aceptado por el cliente", Presupuesto.Numeracion));
                        }

                        if (Presupuesto.EstadoOrdenProduccion.Equals("TOTAL"))
                        {
                            throw new Exception(String.Format("El presupuesto número {0} ya tiene generadas una orden de produccion", Presupuesto.Numeracion));
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
