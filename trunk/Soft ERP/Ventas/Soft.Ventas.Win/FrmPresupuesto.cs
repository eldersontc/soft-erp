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
        const String colRecargo = "Recargo";
        const String colTotalFinal = "Total Final";

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNumero);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colCliente);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colFecha);
            column.DataType = typeof(DateTime);
            column.ReadOnly = true;

            column = columns.Columns.Add(colTotal);
            column.DataType = typeof(Decimal);
            column.ReadOnly = true;

            column = columns.Columns.Add(colRecargo);
            column.DataType = typeof(Decimal);

            column = columns.Columns.Add(colTotalFinal);
            column.DataType = typeof(Decimal);


            ugCotizaciones.DataSource = columns;


            ugCotizaciones.DisplayLayout.Bands[0].Columns[colNumero].CellActivation = Activation.ActivateOnly;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colCliente].CellActivation = Activation.ActivateOnly;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colFecha].CellActivation = Activation.ActivateOnly;

            
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotal].Width = 80;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Double;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotal].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotal].CellActivation = Activation.ActivateOnly;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotal].Format = "N3";
            
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colRecargo].Width = 80;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colRecargo].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Double;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colRecargo].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colRecargo].Format = "N3";

            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotalFinal].Width = 80;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotalFinal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Double;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotalFinal].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            ugCotizaciones.DisplayLayout.Bands[0].Columns[colTotalFinal].Format = "N3";



            MapKeys(ref ugCotizaciones);
        }

        public void Mostrar()
        {
            ssTipoPresupuesto.Text = (Presupuesto.TipoDocumento != null) ? Presupuesto.TipoDocumento.Descripcion : "";
            ssCliente.Text = (Presupuesto.Cliente != null) ? Presupuesto.Cliente.Nombre : "";
            txtNumeracion.Text = Presupuesto.Numeracion;
            udtFechaCreacion.Value = Presupuesto.FechaCreacion;
            txtOrdenCompraCliente.Text = Presupuesto.OrdenCompraCliente;
            txtInstruccionesCliente.Text = Presupuesto.InstruccionesCliente;

            MostrarItems(false);
        }

        public void MostrarItems(Boolean Nuevo) {
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
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    ItemPresupuesto Item = Presupuesto.ObtenerItem(NodoItem.SelectSingleNode("@IDCotizacion").Value);
                    Item.Total = Convert.ToDecimal(NodoItem.SelectSingleNode("@Total").Value);
                    UltraGridRow Row = ugCotizaciones.DisplayLayout.Bands[0].AddNew();
                    Row.Tag = Item;
                    Row.Cells[colNumero].Value = NodoItem.SelectSingleNode("@Numeracion").Value;
                    Row.Cells[colCliente].Value = NodoItem.SelectSingleNode("@Cliente").Value;
                    Row.Cells[colFecha].Value = NodoItem.SelectSingleNode("@Fecha").Value;
                    Row.Cells[colTotal].Value = Convert.ToDecimal(NodoItem.SelectSingleNode("@Total").Value);
                    Row.Cells[colRecargo].Value = Item.Recargo;
                    if (Nuevo) { Item.TotalFinal = Item.Total; }
                    Row.Cells[colTotalFinal].Value = Item.TotalFinal;
                }
            }
            uneTotal.Value = Presupuesto.Total;
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarCliente = new FrmSelectedEntity();
            Presupuesto.Cliente = (SocioNegocio)FrmSeleccionarCliente.GetSelectedEntity(typeof(SocioNegocio), "Cliente Pendiente de Presupuesto");
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
            MostrarItems(true);
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugCotizaciones.ActiveRow == null) { return; }
            Presupuesto.Items.Remove((ItemPresupuesto)ugCotizaciones.ActiveRow.Tag);
            ugCotizaciones.ActiveRow.Delete(false);
            MostrarItems(false);
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

        public void ugCotizaciones_CellKeyEnter(UltraGridCell Cell)
        {
           /* try
            {
                ItemPresupuesto Item = (ItemPresupuesto)Cell.Row.Tag;
                switch (Cell.Column.Key)
                {
                    case colTotalFinal:
                        Item.TotalFinal = Convert.ToDecimal(Cell.Value);
                        break;
                    case colRecargo:
                        Item.TotalFinal = Convert.ToDecimal(Cell.Value) + Item.Total;
                        break;
                    default:
                        break;
                }
                MostrarItems(false);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }*/
        }

        private void ugCotizaciones_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                ItemPresupuesto Item = (ItemPresupuesto)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colTotalFinal:
                        Item.TotalFinal = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        e.Cell.Row.Cells[colRecargo].Value =Item.TotalFinal-Item.Total;
                        uneTotal.Value = Presupuesto.Total;
                        break;
                    case colRecargo:
                        Item.TotalFinal = Convert.ToDecimal(e.Cell.Text.Replace('_', ' ')) + Item.Total;
                        e.Cell.Row.Cells[colTotalFinal].Value = Item.TotalFinal;
                        uneTotal.Value = Presupuesto.Total;
                        break;
                    default:
                        break;
                }
               // MostrarItems(false);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void txtOrdenCompraCliente_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Presupuesto.OrdenCompraCliente = txtOrdenCompraCliente.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
            
        }

        private void txtInstruccionesCliente_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Presupuesto.InstruccionesCliente = txtInstruccionesCliente.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

    }
}
