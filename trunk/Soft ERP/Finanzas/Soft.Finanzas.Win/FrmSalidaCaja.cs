using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Entities;
using Soft.Finanzas.Entidades;
using Soft.Exceptions;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;

namespace Soft.Finanzas.Win
{
    public partial class FrmSalidaCaja : FrmParent
    {
        public FrmSalidaCaja()
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

        public SalidaCaja SalidaCaja { get { return (SalidaCaja)base.m_ObjectFlow; } }

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

        public void Mostrar()
        {
            if (SalidaCaja.TipoDocumento != null)
            {
                ssTipoDocumento.Text = SalidaCaja.TipoDocumento.Descripcion;
                txtNumeracion.Enabled = (SalidaCaja.TipoDocumento.NumeracionAutomatica) ? false : true;
                lblSocioNegocio.Text = SalidaCaja.TipoDocumento.TipoSocioDeNegocio;
            }
            if (SalidaCaja.Caja != null) {
                ssCaja.Text = SalidaCaja.Caja.Nombre;
                uneSaldoActual.Value = SalidaCaja.Caja.SaldoActual;
            }
            ssSocioNegocio.Text = (SalidaCaja.SocioNegocio != null) ? SalidaCaja.SocioNegocio.Nombre : "";
            ssResponsable.Text = (SalidaCaja.Responsable != null) ? SalidaCaja.Responsable.Nombre : "";
            ssMoneda.Text = (SalidaCaja.Moneda != null) ? SalidaCaja.Moneda.Simbolo : "";
            txtNumeracion.Text = SalidaCaja.Numeracion;
            udtFechaCreacion.Value = SalidaCaja.FechaCreacion;
            txtObservacion.Text = SalidaCaja.Observacion;
            MostrarItems();
            MostrarTotales();
            if (!SalidaCaja.NewInstance) { DeshabilitarControles(); }
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugItems);
            foreach (ItemSalidaCaja Item in SalidaCaja.Items)
            {
                UltraGridRow Row = ugItems.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemSalidaCaja Item = (ItemSalidaCaja)Row.Tag;

            Row.Cells[colCodigo].Value = Item.Codigo;
            Row.Cells[colDescripcion].Value = Item.Descripcion;
            Row.Cells[colPrecio].Value = Item.Precio;
            Row.Cells[colCantidad].Value = Item.Cantidad;
            Row.Cells[colTotal].Value = Item.Total;
            Row.Cells[colObservacion].Value = Item.Observacion;
        }

        public void MostrarTotales()
        {
            SalidaCaja.CalcularTotales();
            uneSubTotal.Value = SalidaCaja.SubTotal;
            uneImpuesto.Value = SalidaCaja.Impuesto;
            uneTotal.Value = SalidaCaja.Total;
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

        #region "Eventos"

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                SalidaCaja.TipoDocumento = (TipoCaja)FrmSeleccionar.GetSelectedEntity(typeof(TipoCaja), "Tipo Caja","Movimiento = 'Salida'", All: true);
                if (SalidaCaja.TipoDocumento != null)
                {
                    SalidaCaja.GenerarNumeracion();
                    SalidaCaja.Responsable = FrmMain.ObtenerResponsable();
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SalidaCaja.Numeracion = txtNumeracion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssSocioNegocio_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                SalidaCaja.SocioNegocio = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", string.Format("{0} = 1", SalidaCaja.TipoDocumento.TipoSocioDeNegocio));
                if (SalidaCaja.SocioNegocio != null)
                    ssSocioNegocio.Text = SalidaCaja.SocioNegocio.Nombre;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                SalidaCaja.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                SalidaCaja.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
                if (SalidaCaja.Moneda != null) {
                    ssMoneda.Text = SalidaCaja.Moneda.Simbolo;
                    SalidaCaja.Caja = null;
                    ssCaja.Text = "";
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCaja_Search(object sender, EventArgs e)
        {
            try
            {
                if (SalidaCaja.Moneda == null) { throw new Exception("Seleccione una Moneda ..."); }
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                SalidaCaja.Caja = (Caja)FrmSeleccionar.GetSelectedEntity(typeof(Caja), "Caja", string.Format("Moneda = '{0}'", SalidaCaja.Moneda.Simbolo), All: true);
                if (SalidaCaja.Caja != null) {
                    ssCaja.Text = SalidaCaja.Caja.Nombre;
                    uneSaldoActual.Value = SalidaCaja.Caja.SaldoActual;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                SalidaCaja.Responsable = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                if (SalidaCaja.Responsable != null)
                    ssResponsable.Text = SalidaCaja.Responsable.Nombre;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubNuevoItem_Click(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow RowNuevo = ugItems.DisplayLayout.Bands[0].AddNew();
                RowNuevo.Tag = SalidaCaja.AddItem();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubEliminarItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ugItems.ActiveRow == null) { return; }
                SalidaCaja.Items.Remove((ItemSalidaCaja)ugItems.ActiveRow.Tag);
                ugItems.ActiveRow.Delete(false);
                MostrarTotales(); 
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ugItems_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                ItemSalidaCaja Item = (ItemSalidaCaja)e.Cell.Row.Tag;
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

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SalidaCaja.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        #endregion

    }
}
