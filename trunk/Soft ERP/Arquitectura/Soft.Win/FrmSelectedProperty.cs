using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;
using Infragistics.Win.UltraWinGrid;
using Soft.DataAccess;

namespace Soft.Win
{
    public partial class FrmSelectedProperty : Form
    {
        public FrmSelectedProperty()
        {
            InitializeComponent();
            InitGrids();
        }

        const String colNombre = "Nombre";
        const String colPropiedad = "Propiedad";

        private String m_PropiedadResult = "";

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colPropiedad);
            column.DataType = typeof(String);

            ugPropiedad.DataSource = columns;
        }

        public void MostrarPropiedades(EntidadSF EntidadSF) {
            foreach (AtributoSF Atributo in EntidadSF.Atributos)
            {
                UltraGridRow Row = ugPropiedad.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Atributo.Propiedad;
                Row.Cells[colNombre].Value = Atributo.Nombre;
                Row.Cells[colPropiedad].Value = Atributo.Propiedad;
            }
        }

        public String GetSeletedProperty(String IDEntidadSF) {
            EntidadSF EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", IDEntidadSF);
            MostrarPropiedades(EntidadSF);
            ShowDialog();
            return m_PropiedadResult;
        }

        private void ugPropiedad_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            if(ugPropiedad.ActiveRow != null){
                m_PropiedadResult = Convert.ToString(ugPropiedad.ActiveRow.Tag);
            }
            Close();
        }

        private void ugPropiedad_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (ugPropiedad.ActiveRow != null)
                    {
                        m_PropiedadResult = Convert.ToString(ugPropiedad.ActiveRow.Tag);
                    }
                    Close();
                    break;
            }
        }

    }
}
