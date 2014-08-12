using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Seguridad.Entidades;
using NHibernate;
using Soft.DataAccess;
using Soft.Seguridad.Entidades;
using System.Windows.Forms;
using Soft.Exceptions;

namespace Soft.DataAccess
{
    public class ModifyEntity : ControllerApp
    {
        public override void Start()
        {
            try
            {
                this.IniciarTransaccion();
                this.Modificar(base.m_ObjectFlow);
                this.Agregar(Auditoria.ConstruirAuditoria(base.m_ObjectFlow, "Modificación"));
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
