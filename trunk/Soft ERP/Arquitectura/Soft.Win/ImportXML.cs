using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using System.Windows.Forms;
using System.Xml;
using NHibernate;
using Soft.Entities;

namespace Soft.Win
{
    public class ImportXML : ControllerApp
    {
        public override void Start()
        {
            using (ISession Sesion = m_SessionFactory.OpenSession())
            {
                try
                {
                    String Operacion = "";
                    OpenFileDialog fop = new OpenFileDialog();
                    //fop.InitialDirectory = @"C:\";
                    fop.Filter = "xml|*.xml";
                    if (fop.ShowDialog() == DialogResult.OK)
                    {
                        XmlDocument XML = new XmlDocument();
                        XML.Load(fop.FileName);
                        base.m_ObjectFlow = (Parent)Factory.ToObject(XML.OuterXml, m_EntidadSF.EnsambladoClase.Ensamblado_);
                        Operacion = (Sesion.Get(base.m_EntidadSF.NombreClase, ((Parent)base.m_ObjectFlow).ID) != null) ? "Modificar" : "Crear";
                        EjecutarOperacion(Operacion);
                        m_ResultProcess = EnumResult.SUCESS;
                    }
                }
                catch (Exception ex)
                {
                    m_ResultProcess = EnumResult.ERROR;
                    MessageBox.Show(ex.StackTrace);
                }
            }
            base.Start();
        }

        public void EjecutarOperacion(String Operacion) {
            if (Operacion.Equals("Modificar"))
            {
                if (m_ItemContenedor.Modificar)
                {
                    FrmMain.ModificarEstandar(base.m_ObjectFlow);
                }
                else if (m_ItemContenedor.AccionModificar != null)
                {
                    FrmMain.ModificarPersonalizado(base.m_ObjectFlow, base.m_ItemContenedor.AccionModificar);
                }
            }
            else
            {
                if (m_ItemContenedor.Crear)
                {
                    FrmMain.CrearEstandar(base.m_ObjectFlow);
                }
                else if (m_ItemContenedor.AccionCrear != null)
                {
                    FrmMain.CrearPersonalizado(base.m_ObjectFlow, base.m_ItemContenedor.AccionCrear);
                }
            }
        }
    }
}
