using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Finanzas.Entidades;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Soft.Exceptions;
using Soft.Entities;

namespace Soft.Finanzas.Win
{
    public partial class FrmEntradaCaja : FrmParent
    {
        public FrmEntradaCaja()
        {
            InitializeComponent();
        }



        #region "Propiedades"

        const String colCodigo = "Código";
        const String colDescripcion = "Descripción";
        const String colCantidad = "Cantidad";
        const String colPrecio = "Precio";
        const String colTotal = "Total";
        const String colObservacion = "Observación";

        public EntradaCaja EntradaCaja { get { return (EntradaCaja)base.m_ObjectFlow; } }

        #endregion

        #region "Métodos"

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colCodigo);
            column.DataType = typeof(string);

            column = columns.Columns.Add(colDescripcion);
            column.DataType = typeof(string);

            column = columns.Columns.Add(colCantidad);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colPrecio);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colTotal);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colObservacion);
            column.DataType = typeof(String);

            ugItems.DataSource = columns;
            ugItems.DisplayLayout.Bands[0].Columns[colDescripcion].Width = 250;
            ugItems.DisplayLayout.Bands[0].Columns[colCantidad].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colCantidad].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colPrecio].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colPrecio].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colTotal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colTotal].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colTotal].CellActivation = Activation.NoEdit;
            MapKeys(ref ugItems);
        }

        Boolean ActualizandoUI = false;
        public void Mostrar()
        {
            try
            {
                ActualizandoUI = true;
                if (EntradaCaja.TipoDocumento != null)
                {
                    ssTipoDocumento.Text = EntradaCaja.TipoDocumento.Descripcion;
                    txtNumeracion.Enabled = (EntradaCaja.TipoDocumento.NumeracionAutomatica) ? false : true;
                    lblSocioNegocio.Text = EntradaCaja.TipoDocumento.TipoSocioDeNegocio;
                }
                if (EntradaCaja.Caja != null)
                {
                    ssCaja.Text = EntradaCaja.Caja.Nombre;
                    uneSaldoActual.Value = EntradaCaja.Caja.SaldoActual;
                }
                ssSocioNegocio.Text = (EntradaCaja.SocioNegocio != null) ? EntradaCaja.SocioNegocio.Nombre : "";
                ssResponsable.Text = (EntradaCaja.Responsable != null) ? EntradaCaja.Responsable.Nombre : "";
                ssMoneda.Text = (EntradaCaja.Moneda != null) ? EntradaCaja.Moneda.Simbolo : "";
                txtNumeracion.Text = EntradaCaja.Numeracion;
                udtFechaCreacion.Value = EntradaCaja.FechaCreacion;
                txtObservacion.Text = EntradaCaja.Observacion;
                MostrarItems();
                MostrarTotales();
                if (!EntradaCaja.NewInstance) { DeshabilitarControles(); }

                ActualizandoUI =false;
            }
            catch (Exception ex)
            {
                ActualizandoUI = false;
                SoftException.Control(ex);
            }
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugItems);
            foreach (ItemEntradaCaja Item in EntradaCaja.Items)
            {
                UltraGridRow Row = ugItems.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemEntradaCaja Item = (ItemEntradaCaja)Row.Tag;

            Row.Cells[colCodigo].Value = Item.Codigo;
            Row.Cells[colDescripcion].Value = Item.Descripcion;
            Row.Cells[colPrecio].Value = Item.Precio;
            Row.Cells[colCantidad].Value = Item.Cantidad;
            Row.Cells[colTotal].Value = Item.Total;
            Row.Cells[colObservacion].Value = Item.Observacion;
        }

        public void MostrarTotales()
        {
            EntradaCaja.CalcularTotales();
            uneSubTotal.Value = EntradaCaja.SubTotal;
            uneImpuesto.Value = EntradaCaja.Impuesto;
            uneTotal.Value = EntradaCaja.Total;
        }

        public void DeshabilitarControles()
        {
            ssTipoDocumento.Enabled = false;
            ssSocioNegocio.Enabled = false;
            ssResponsable.Enabled = false;
            ssMoneda.Enabled = false;
            ssCaja.Enabled = false;
            txtNumeracion.Enabled = false;
            udtFechaCreacion.Enabled = false;
            ubNuevoItem.Enabled = false;
            ubEliminarItem.Enabled = false;
            txtObservacion.Enabled = false;
        }

        #endregion

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                try
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    EntradaCaja.TipoDocumento = (TipoCaja)FrmSeleccionar.GetSelectedEntity(typeof(TipoCaja), "Tipo Caja", "Movimiento = 'Entrada'", All: true);
                    if (EntradaCaja.TipoDocumento != null)
                    {
                        EntradaCaja.GenerarNumeracion();
                        EntradaCaja.Responsable = FrmMain.ObtenerResponsable();
                        Mostrar();
                    }
                }
                catch (Exception ex)
                {
                    SoftException.Control(ex);
                }

            }
            catch (Exception ex)
            {
                
                SoftException.ShowException(ex);
            }

        }

        private void ssTipoDocumento_Clear(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                EntradaCaja.TipoDocumento = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssSocioNegocio_Search(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                try
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    EntradaCaja.SocioNegocio = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", string.Format("{0} = 1", EntradaCaja.TipoDocumento.TipoSocioDeNegocio));
                    Mostrar();
                }
                catch (Exception ex)
                {
                    SoftException.Control(ex);
                }

            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }
        }

        private void ssSocioNegocio_Clear(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                EntradaCaja.SocioNegocio = null;
                Mostrar();
            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {

                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                EntradaCaja.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
                Mostrar();
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Clear(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                EntradaCaja.Moneda = null;
                Mostrar();
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ssCaja_Search(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                if (EntradaCaja.Moneda == null) { throw new Exception("Seleccione una Moneda ..."); }
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                EntradaCaja.Caja = (Caja)FrmSeleccionar.GetSelectedEntity(typeof(Caja), "Caja", string.Format("Moneda = '{0}'", EntradaCaja.Moneda.Simbolo), All: true);
                Mostrar();   
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ssCaja_Clear(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                EntradaCaja.Caja = null;
                Mostrar();   

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {

                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                EntradaCaja.Responsable = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                Mostrar();
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ssResponsable_Clear(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                EntradaCaja.Responsable = null;
                Mostrar();

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ubNuevoItem_Click(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                UltraGridRow RowNuevo = ugItems.DisplayLayout.Bands[0].AddNew();
                RowNuevo.Tag = EntradaCaja.AddItem();

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ubEliminarItem_Click(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                if (ugItems.ActiveRow == null) { return; }
                EntradaCaja.Items.Remove((ItemEntradaCaja)ugItems.ActiveRow.Tag);
                ugItems.ActiveRow.Delete(false);
                MostrarTotales(); 

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void ugItems_CellChange(object sender, CellEventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                ItemEntradaCaja Item = (ItemEntradaCaja)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colCodigo:
                        Item.Codigo = e.Cell.Text;
                        break;
                    case colDescripcion:
                        Item.Descripcion = e.Cell.Text;
                        break;
                    case colPrecio:
                        Item.Precio = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        break;
                    case colCantidad:
                        Item.Cantidad = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        break;
                    case colObservacion:
                        Item.Observacion = e.Cell.Text;
                        break;
                    default:
                        break;
                }
                MostrarItem(e.Cell.Row);
                MostrarTotales();

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void txtObservacion_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoUI) { return; }
            try
            {
                EntradaCaja.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }




    }
}
