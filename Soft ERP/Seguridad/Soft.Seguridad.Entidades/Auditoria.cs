using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.Entities;
using Soft.Win;
using Soft.DataAccess;

namespace Soft.Seguridad.Entidades
{
    public class Auditoria : Parent 
    {

        public Auditoria() { }

        #region "Propiedades"

        public virtual String Fecha { get; set; }
        public virtual String Hora { get; set; }
        public virtual String Usuario { get; set; }
        public virtual String Accion { get; set; }
        public virtual String XML { get; set; }
        public virtual String IDObjectFlow { get; set; }

        #endregion

        #region "Métodos"

        public static Auditoria ConstruirAuditoria(Object ObjectFlow, String Accion)
        {
            Auditoria Auditoria = new Auditoria();
            Auditoria.Accion = Accion;
            Auditoria.Fecha = DateTime.Now.ToShortDateString();
            Auditoria.Hora = DateTime.Now.ToShortTimeString();
            Auditoria.IDObjectFlow = ((Parent)ObjectFlow).ID;
            Auditoria.XML = Factory.ToXML(ObjectFlow);
            Auditoria.Usuario = FrmMain.Usuario.UserID;
            return Auditoria;
        }

        #endregion

    }
}
