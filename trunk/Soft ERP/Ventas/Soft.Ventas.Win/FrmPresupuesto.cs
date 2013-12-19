using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Ventas.Entidades;
using Soft.DataAccess;
using System.Xml;
using Infragistics.Win.UltraWinGrid;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.Exceptions;

namespace Soft.Ventas.Win
{
    public partial class FrmPresupuesto : FrmParent 
    {
        public FrmPresupuesto()
        {
            InitializeComponent();
        }

        public Presupuesto Presupuesto { get { return (Presupuesto)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const String colNumero = "Número";
        const String colCliente = "Cliente";
        const String colFecha = "Fecha";
        const String colTotal = "Total";

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNumero);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCliente);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colFecha);
            column.DataType = typeof(DateTime);

            column = columns.Columns.Add(colTotal);
            column.DataType = typeof(Decimal);

            ugCotizaciones.DataSource = columns;
            MapKeys(ref ugCotizaciones);
        }

        public void Mostrar()
        {
            ssTipoPresupuesto.Text = (Presupuesto.TipoDocumento != null) ? Presupuesto.TipoDocumento.Descripcion : "";
            ssCliente.Text = (Presupuesto.Cliente != null) ? Presupuesto.Cliente.Nombre : "";
            txtNumeracion.Text = Presupuesto.Numeracion;
            udtFechaCreacion.Value = Presupuesto.FechaCreacion;
            MostrarItems();
        }

        public void MostrarItems() {
            if (Presupuesto.Items.Count == 0) {
                Presupuesto.Total = 0;
                uneTotal.Value = 0;
                return; 
            }
            base.ClearAllRows(ref ugCotizaciones);
            String Filtro = String.Format(" IDCotizacion IN ({0})", Presupuesto.ObtenerFiltroCotizaciones());
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_ItemPresupuestoxPresupuesto", Filtro);
            if (XML.HasChildNodes)
            {
                Decimal mTotal = 0;
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    UltraGridRow Row = ugCotizaciones.DisplayLayout.Bands[0].AddNew();
                    Row.Tag = NodoItem.SelectSingleNode("@ID").Value;
                    Row.Cells[colNumero].Value = NodoItem.SelectSingleNode("@Numeracion").Value;
                    Row.Cells[colCliente].Value = NodoItem.SelectSingleNode("@Cliente").Value;
                    Row.Cells[colFecha].Value = NodoItem.SelectSingleNode("@Fecha").Value;
                    Row.Cells[colTotal].Value = NodoItem.SelectSingleNode("@Total").Value;
                    mTotal += Convert.ToDecimal(Row.Cells[colTotal].Value);
                }
                uneTotal.Value = mTotal; ;
            }
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarCliente = new FrmSelectedEntity();
            Presupuesto.Cliente = (SocioNegocio)FrmSeleccionarCliente.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Cliente = 1");
            ssCliente.Text = (Presupuesto.Cliente != null) ? Presupuesto.Cliente.Nombre : "";
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            Presupuesto.Numeracion = txtNumeracion.Text;
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            Presupuesto.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void ubAgregar_Click(object sender, EventArgs e)
        {
            Collection Cotizaciones = new Collection();
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String Filtro = Presupuesto.ObtenerFiltroCotizaciones();
            Filtro = (Filtro.Length > 0) ? String.Format(" ID NOT IN ({0}) AND IDCliente = '{1}'", Filtro, Presupuesto.Cliente.ID) : String.Format(" IDCliente = '{0}'", Presupuesto.Cliente.ID);
            Cotizaciones = FrmSeleccionar.GetSelectedsEntities(typeof(Cotizacion), "Selección de Cotizaciones", Filtro);
            foreach (Cotizacion Item in Cotizaciones)
            {
                Presupuesto.AddItem(Item);
            }
            MostrarItems();
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugCotizaciones.ActiveRow == null) { return; }
            Presupuesto.Items.Remove((ItemPresupuesto)Presupuesto.Items.First(i=> ((ItemPresupuesto)i).IDCotizacion.Equals(Convert.ToString(ugCotizaciones.ActiveRow.Tag))));
            ugCotizaciones.ActiveRow.Delete(false);
            MostrarItems();
        }

        private void ssTipoPresupuesto_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                TipoPresupuesto TipoDocumento = (TipoPresupuesto)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoPresupuesto), "Tipo de Presupuesto");
                if ((Presupuesto.TipoDocumento == null) || (Presupuesto.TipoDocumento.Codigo != TipoDocumento.Codigo))
                {
                    Presupuesto.TipoDocumento = (TipoPresupuesto)HelperNHibernate.GetEntityByID("TipoPresupuesto", TipoDocumento.ID);
                    ssTipoPresupuesto.Text = (Presupuesto.TipoDocumento != null) ? Presupuesto.TipoDocumento.Descripcion : "";
                    Presupuesto.GenerarNumCp();
                    txtNumeracion.Text = Presupuesto.Numeracion;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

    }
}
