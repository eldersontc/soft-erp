using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Soft.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Infragistics.Win.UltraWinDataSource;
using Soft.DataAccess;
using Soft.Configuracion.Entidades;

namespace Soft.Win
{
    public partial class FrmDetails : Form
    {
        private XmlDocument m_XMLCofiguration;
        private ItemContenedor m_ItemContenedor;

        public FrmDetails()
        {
            InitializeComponent();
        }

        public FrmDetails(Form FormParent,ItemContenedor ItemContenedor)
        {
            MdiParent = FormParent;
            m_ItemContenedor = ItemContenedor;
            Tag = ItemContenedor;
            InitializeComponent();
            ConfigurePanel(ItemContenedor);
            Show();
        }

        public void ConfigurePanel(ItemContenedor ItemContenedor)
        {
            String Query = String.Empty;
            String NameView = String.Empty;
            Text = String.Format(":: {0} ::", ItemContenedor.Nombre);
            ugDetails.DisplayLayout.Appearance.ImageBackground = Image.FromFile(String.Format("{0}{1}", FrmMain.CarpetaImagenes, FrmMain.Usuario.Imagen));
            ugDetails.DataSource = null;
            m_XMLCofiguration = HelperNHibernate.ExecuteView("vSF_ColumnasxPanel", String.Format(" NombrePanel = '{0}' ORDER BY Orden", ItemContenedor.Panel.Nombre));
            foreach (XmlNode NodoItem in m_XMLCofiguration.DocumentElement.ChildNodes)
            {   
                if (Query.Length > 0) { Query += ","; }
                if (NameView == String.Empty) { NameView = NodoItem.SelectSingleNode("@NombreVista").Value; }
                UltraGridColumn Column = ugDetails.DisplayLayout.Bands[0].Columns.Add(NodoItem.SelectSingleNode("@CampoSQL").Value);
                Column.Header.Caption = NodoItem.SelectSingleNode("@NombreColumna").Value;
                Column.Hidden = !Convert.ToBoolean(Convert.ToInt32(NodoItem.SelectSingleNode("@Visible").Value));
                Column.Width = Convert.ToInt32(NodoItem.SelectSingleNode("@Ancho").Value);
                Column.Key = NodoItem.SelectSingleNode("@CampoSQL").Value;
                Query += NodoItem.SelectSingleNode("@CampoSQL").Value;    
            }
            Query = String.Format("SELECT {0} FROM {1} ", Query, NameView);
            ugDetails.DataSource = HelperNHibernate.GetDataSet(Query);
            if (ugDetails.Rows.Count > 0) { ugDetails.Rows[0].Selected = true; }
        }

        public List<String> GetIDs() {
            List<String> IDs = new List<String>();
            foreach (UltraGridRow Row in ugDetails.Selected.Rows){
                String ID = Convert.ToString(Row.Cells["ID"].Value);
                IDs.Add(ID);
            }
            return IDs;
        }

        public Int32 CountRows { get { return ugDetails.Rows.Count; } }

        public void RefreshView() {
            this.ConfigurePanel(m_ItemContenedor);
        }

        private void ugDetails_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            FrmMain.ModificarEntidad();
        }

        private void ugDetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    FrmMain.ModificarEntidad();
                    break;
            }
        }

    }
}
