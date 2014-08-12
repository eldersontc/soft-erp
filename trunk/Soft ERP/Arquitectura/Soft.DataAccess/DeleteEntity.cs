using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using NHibernate;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data;
using Soft.DataAccess;
using Soft.Exceptions;
using System.Data.SqlClient;

namespace Soft.DataAccess
{
    public class DeleteEntity : ControllerApp
    {
        public override void Start()
        {
            try
            {
                this.IniciarTransaccion();
                this.Eliminar(base.m_ObjectFlow);
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
