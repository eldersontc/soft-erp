using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Facturacion.Entidades;
using Soft.Exceptions;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.Produccion.Entidades;
using System.Xml;
using Soft.DataAccess;
using Infragistics.Win.UltraWinGrid;
using Soft.Facturacion.Entidades;
using Infragistics.Win;

namespace Soft.Facturacion.Win
{
    public partial class FrmNotaDebito : FrmParent
    {
        public FrmNotaDebito()
        {
            InitializeComponent();
        }

        #region "Propiedades"

        const String colCodigo = "Código";
        const String colDescripcion = "Descripción";
        const String colPrecio = "Precio";
        const String colCantidad = "Cantidad";
        const String colObservacion = "Observación";
        const String colTotal = "Total";

        public NotaDebito NotaDebito { get { return (NotaDebito)base.m_ObjectFlow; } }

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
            if (NotaDebito.TipoDocumento != null)
            {
                ssTipoDocumento.Text = NotaDebito.TipoDocumento.Descripcion;
                txtNumeracion.Enabled = (NotaDebito.TipoDocumento.NumeracionAutomatica) ? false : true;
            }
            ssCliente.Text  = (NotaDebito.Cliente != null) ? NotaDebito.Cliente.Nombre : "";
            ssResponsable.Text = (NotaDebito.Responsable != null) ? NotaDebito.Responsable.Nombre : "";
            ssMoneda.Text = (NotaDebito.Moneda != null) ? NotaDebito.Moneda.Simbolo : "";
            txtNumeracion.Text = NotaDebito.Numeracion;
            udtFechaCreacion.Value = NotaDebito.FechaCreacion;
            txtObservacion.Text = NotaDebito.Observacion;
            txtNroFactura.Text = NotaDebito.NroFactura;
            MostrarItems();
            MostrarTotales();
            if (!NotaDebito.NewInstance) { DeshabilitarControles(); }
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugItems);
            foreach (ItemNotaDebito Item in NotaDebito.Items)
            {
                UltraGridRow Row = ugItems.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemNotaDebito Item = (ItemNotaDebito)Row.Tag;

            Row.Cells[colCodigo].Value = Item.Codigo;
            Row.Cells[colDescripcion].Value = Item.Descripcion;
            Row.Cells[colPrecio].Value = Item.Precio;
            Row.Cells[colCantidad].Value = Item.Cantidad;
            Row.Cells[colTotal].Value = Item.Total;
            Row.Cells[colObservacion].Value = Item.Observacion;
        }

        public void MostrarTotales() {
            NotaDebito.CalcularTotales();
            uneSubTotal.Value = NotaDebito.SubTotal;
            uneImpuesto.Value = NotaDebito.Impuesto;
            uneTotal.Value = NotaDebito.Total;
        }

        public void DeshabilitarControles() {
            ssTipoDocumento.Enabled = false;
            ssCliente.Enabled = false;
            ssResponsable.Enabled = false;
            ssMoneda.Enabled = false;
            txtNumeracion.Enabled = false;
            udtFechaCreacion.Enabled = false;
            ubAgregarOP.Enabled = false;
            ubQuitarOP.Enabled = false;
            txtObservacion.Enabled = false;
        }

        #endregion

        #region "Eventos"

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                NotaDebito.TipoDocumento = (TipoNotaDebito)FrmSeleccionar.GetSelectedEntity(typeof(TipoNotaDebito), "Tipo Nota Débito", All:true);
                if (NotaDebito.TipoNotaDebito != null)
                {
                    NotaDebito.GenerarNumeracion();
                    NotaDebito.Responsable = FrmMain.ObtenerResponsable();
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
                NotaDebito.Numeracion = txtNumeracion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                NotaDebito.Cliente = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Cliente");
                if (NotaDebito.Cliente != null)
                {
                    ssCliente.Text = NotaDebito.Cliente.Nombre;
                }
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
                NotaDebito.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
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
                NotaDebito.Responsable = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                if (NotaDebito.Responsable != null)
                {
                    ssResponsable.Text = NotaDebito.Responsable.Nombre;
                }
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
                NotaDebito.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
                if (NotaDebito.Moneda != null)
                {
                    ssMoneda.Text = NotaDebito.Moneda.Simbolo;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubAgregarOP_Click(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow RowNuevo = ugItems.DisplayLayout.Bands[0].AddNew();
                RowNuevo.Tag = NotaDebito.AddItem();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubQuitarOP_Click(object sender, EventArgs e)
        {
            try
            {
                if (ugItems.ActiveRow == null) { return; }
                NotaDebito.Items.Remove((ItemNotaDebito)ugItems.ActiveRow.Tag);
                ugItems.ActiveRow.Delete(false);
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
                NotaDebito.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ugItems_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                ItemNotaDebito Item = (ItemNotaDebito)e.Cell.Row.Tag;
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

        #endregion
    }
}
