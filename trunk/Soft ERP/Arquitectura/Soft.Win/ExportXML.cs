using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soft.DataAccess;
using NHibernate;
using System.Windows.Forms;
using System.Xml;

namespace Soft.Win
{
    public class ExportXML : ControllerApp
    {
        public override void Start()
        {
            try
            {
                SaveFileDialog sfile = new SaveFileDialog();
                //sfile.InitialDirectory = @"C:\";
                sfile.Filter = "xml|*.xml";
                if (sfile.ShowDialog() == DialogResult.OK)
                {
                    String StrXML = Factory.ToXML(base.m_ObjectFlow);
                    XmlDocument XML = new XmlDocument();
                    XML.LoadXml(StrXML);
                    XML.Save(sfile.FileName);
                }
                m_ResultProcess = EnumResult.SUCESS;
            }
            catch (Exception ex)
            {
                m_ResultProcess = EnumResult.ERROR;
                MessageBox.Show(ex.StackTrace);
            }
            base.Start();
        }
    }
}
