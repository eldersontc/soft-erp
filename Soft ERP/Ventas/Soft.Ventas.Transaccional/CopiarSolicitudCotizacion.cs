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
    public class CopiarSolicitudCotizacion : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                using (ITransaction Trans = Sesion.BeginTransaction())
                {
                    try
                    {
                        SolicitudCotizacion SolicitudCotizacion = (SolicitudCotizacion)m_ObjectFlow;
                        SolicitudCotizacion.Numeracion = "";
                        SolicitudCotizacion.EstadoAprobacion = "";
                        SolicitudCotizacion.EstadoCotizacion = "";
                        SolicitudCotizacion.FechaCreacion = DateTime.Now;
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                    catch (Exception ex)
                    {
                        m_ResultProcess = EnumResult.ERROR;
                        SoftException.Control(ex);
                    }
                }
            }
            base.Start();
        }

    }
}
