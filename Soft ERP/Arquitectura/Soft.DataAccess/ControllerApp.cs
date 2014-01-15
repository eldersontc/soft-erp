using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Soft.Entities;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using Soft.Configuracion.Entidades;
using Soft.Exceptions;
using System.Drawing;

namespace Soft.DataAccess
{
    public class ControllerApp : Form
    {
        public Object m_ObjectFlow;
        public EnumResult m_ResultProcess;
        public String m_Parameter;
        public List<String> m_ItemsSelecteds;
        public Accion m_AccionActual;
        public ItemAccion m_ItemAccionAcual;
        public EntidadSF m_EntidadSF;
        public ItemContenedor m_ItemContenedor;
        public Boolean m_Modal;

        protected static Configuration m_Configuration = new Configuration();
        protected static ISessionFactory m_SessionFactory;

        public ControllerApp() { }

        public static void LoadConfiguration()
        {
            try
            {
                m_Configuration.Configure("hibernate.cfg.xml");
                m_SessionFactory = m_Configuration.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());
                Application.Exit();
            }
        }

        public virtual void Start()
        {
            // All code here.
            if (m_ItemAccionAcual == null) { return; }
            if (m_ResultProcess == EnumResult.SUCESS)
            {
                this.ContinuarFlujo(m_ItemAccionAcual.Exito);
            }
            else if (m_ResultProcess == EnumResult.ERROR)
            {
                this.ContinuarFlujo(m_ItemAccionAcual.Error);
            }
            else
            {
                return;
            }
        }

        public void ContinuarFlujo(String Siguiente)
        {
            if (Siguiente.Equals("Salir")) { return; }
            ItemAccion m_Item = m_AccionActual.ItemByName(Siguiente.Trim());
            ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(m_Item.Ensamblado.Ensamblado_, m_Item.Clase);
            Controlador.m_Parameter = m_Item.Parametro;
            Controlador.m_AccionActual = m_AccionActual;
            Controlador.m_ItemAccionAcual = m_Item;
            Controlador.m_ObjectFlow = m_ObjectFlow;
            Controlador.m_EntidadSF = m_EntidadSF;
            Controlador.m_ItemsSelecteds = m_ItemsSelecteds;
            Controlador.m_ItemContenedor = m_ItemContenedor;
            Controlador.Start();
        }

        public enum EnumResult
        {
            DEFAULT,
            SUCESS,
            ERROR,
            CANCEL
        }

    }
}
