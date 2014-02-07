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
using Soft.Exceptions;

namespace Soft.Win
{
    public partial class FrmDetails : Form
    {
        private XmlDocument m_XMLCofiguration;
        private ItemContenedor m_ItemContenedor;
        private Dictionary<string, string> Filtros = new Dictionary<string, string>();

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
            ConfigurarPanel(ItemContenedor);
            Show();
        }

        public void ConfigurarPanel(ItemContenedor ItemContenedor)
        {
            String ConsultaSQL = String.Empty;
            String Ordenamiento = String.Empty;
            Text = String.Format(":: {0} ::", ItemContenedor.Nombre);
            ugDetails.DisplayLayout.Appearance.ImageBackground = Image.FromFile(String.Format("{0}{1}", FrmMain.CarpetaImagenes, FrmMain.Usuario.Imagen));
            ugDetails.DataSource = null;
            Soft.Configuracion.Entidades.Panel Panel = (Soft.Configuracion.Entidades.Panel)HelperNHibernate.GetEntityByField("Panel", "Nombre", ItemContenedor.Panel.Nombre);
            foreach (ColumnaPanel  Columna in Panel.Columnas)
            {
                UltraGridColumn Column = ugDetails.DisplayLayout.Bands[0].Columns.Add(Columna.CampoSQL);
                Column.Header.Caption = Columna.Nombre;
                Column.Width = Columna.Ancho;
                Column.Hidden = !Columna.Visible;
                if (Columna.Indice) { Ordenamiento = String.Format("ORDER BY {0}", Columna.CampoSQL);}
            }
            ConsultaSQL = String.Format("SELECT * FROM {0} {1} {2}", Panel.NombreVista, ItemContenedor.Filtro, Ordenamiento);
            ugDetails.DataSource = HelperNHibernate.GetDataSet(ConsultaSQL);
            RecuperarFiltros();
        }

        public void RecuperarFiltros() {
            foreach (KeyValuePair<string,string> Filtro in Filtros)
            {
                ugDetails.DisplayLayout.Bands[0].ColumnFilters[Filtro.Key].FilterConditions.Clear();
                ugDetails.DisplayLayout.Bands[0].ColumnFilters[Filtro.Key].FilterConditions.Add(FilterComparisionOperator.Contains, Filtro.Value);
            }
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
            this.ConfigurarPanel(m_ItemContenedor);
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

        private void ugDetails_FilterCellValueChanged(object sender, FilterCellValueChangedEventArgs e)
        {
            try
            {
                if (e.FilterCell.IsFilterRowCell)
                {
                    if (Filtros.ContainsKey(e.FilterCell.Column.Key))
                    {
                        Filtros[e.FilterCell.Column.Key] = e.FilterCell.Text;
                    }
                    else
                    {
                        Filtros.Add(e.FilterCell.Column.Key, e.FilterCell.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

    }
}
