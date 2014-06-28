using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using Soft.Ventas.Entidades;
using Soft.Exceptions;

namespace Soft.Ventas.Transaccional
{
    public class ModificarCotizacion:ControllerApp 
 
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        Cotizacion cp = (Cotizacion)m_ObjectFlow;

                        Cotizacion cpactual = (Cotizacion)HelperNHibernate.GetEntityByID("Cotizacion ", cp.ID);

                        if (cpactual.EstadoAprobacion.Equals("PENDIENTE"))
                        {
                            Sesion.Update(cp);
                            Trans.Commit();
                            m_ResultProcess = EnumResult.SUCESS;
                        }
                        else
                        {
                            throw new Exception("Estado " + cpactual.EstadoAprobacion + " del documento no permite modificaciones");
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
