using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Seguridad.Entidades;
using NHibernate;
using System.Data;
using Soft.DataAccess;
using System.Windows.Forms;
using Soft.Exceptions;
using System.Data.SqlClient;
using NHibernate.Exceptions;

namespace Soft.DataAccess
{
    public class CreateEntity : ControllerApp
    {
        public override void Start()
        {
            try
            {
                this.IniciarTransaccion();
                this.Agregar(base.m_ObjectFlow);
                this.Agregar(Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Creación"));
                this.FinalizarTransaccion();
                this.m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                this.FinalizarTransaccion(true);
                this.m_ResultProcess = EnumResult.ERROR;
                SoftException.Control(ex);
            }
            finally
            {
                this.CerrarSesion();
            }
            base.Start();
        }
    }
}
