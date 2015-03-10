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
using Infragistics.Win.UltraWinGrid;
using Soft.Exceptions;
using Infragistics.Win;

namespace Soft.Ventas.Win
{
    public partial class FrmRendicionCotizacion : FrmParent
    {
        public FrmRendicionCotizacion()
        {
            InitializeComponent();
        }

        //Constantes
        const string colCodigo = "CÓDIGO";
        const string colDescripcion = "DESCRIPCIÓN";
        const string colUnidad = "UNIDAD";
        const string colCantidadCotizacion = "CANTIDAD COTIZACIÓN";
        const string colPrecioCotizacion = "PRECIO COTIZACIÓN";
        const string colTotalCotizacion = "TOTAL COTIZACIÓN";
        const string colCantidadReal= "CANTIDAD REAL";
        const string colPrecioReal = "PRECIO REAL";
        const string colTotalReal = "TOTAL REAL";

        public RendicionCotizacion RendicionCotizacion { get { return (RendicionCotizacion)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrid();
            Mostrar();
        }

        public void InitGrid()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colCodigo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colDescripcion);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCantidadCotizacion);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colPrecioCotizacion);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colTotalCotizacion);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colCantidadReal);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colPrecioReal);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colTotalReal);
            column.DataType = typeof(decimal);

            ugItems.DataSource = columns;
            ugItems.DisplayLayout.Bands[0].Columns[colDescripcion].Width = 200;
            ugItems.DisplayLayout.Bands[0].Columns[colCantidadCotizacion].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colCantidadCotizacion].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colCantidadCotizacion].CellActivation = Activation.NoEdit;

            ugItems.DisplayLayout.Bands[0].Columns[colPrecioCotizacion].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colPrecioCotizacion].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colPrecioCotizacion].CellActivation = Activation.NoEdit;

            ugItems.DisplayLayout.Bands[0].Columns[colTotalCotizacion].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colTotalCotizacion].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colTotalCotizacion].CellActivation = Activation.NoEdit;

            ugItems.DisplayLayout.Bands[0].Columns[colCantidadReal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colCantidadReal].CellAppearance.TextHAlign = HAlign.Right;

            ugItems.DisplayLayout.Bands[0].Columns[colPrecioReal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colPrecioReal].CellAppearance.TextHAlign = HAlign.Right;

            ugItems.DisplayLayout.Bands[0].Columns[colTotalReal].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugItems.DisplayLayout.Bands[0].Columns[colTotalReal].CellAppearance.TextHAlign = HAlign.Right;
            ugItems.DisplayLayout.Bands[0].Columns[colTotalReal].CellActivation = Activation.NoEdit;

            MapKeys(ref ugItems);
        }

        public void Mostrar()
        {
            udteFechaCreacion.Value = RendicionCotizacion.FechaCreacion;
            uteNumeroCotizacion.Text = RendicionCotizacion.NumeroCotizacion;
            MostrarItems();
            MostrarTotales();
        }

        public void MostrarTotales() 
        {
            uneTotalCotizacion.Value = RendicionCotizacion.TotalCotizacion;
            uneTotalReal.Value = RendicionCotizacion.TotalReal;
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugItems);
            foreach (ItemRendicionCotizacion Item in RendicionCotizacion.Items)
            {
                UltraGridRow Row = ugItems.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemRendicionCotizacion Item = (ItemRendicionCotizacion)Row.Tag;
            Row.Cells[colCodigo].Value = Item.Codigo;
            Row.Cells[colDescripcion].Value = Item.Descripcion;
            Row.Cells[colUnidad].Value = Item.Unidad;
            Row.Cells[colCantidadCotizacion].Value = Item.CantidadCotizacion;
            Row.Cells[colPrecioCotizacion].Value = Item.PrecioCotizacion;
            Row.Cells[colTotalCotizacion].Value = Item.TotalCotizacion;
            Row.Cells[colCantidadReal].Value = Item.CantidadReal;
            Row.Cells[colPrecioReal].Value = Item.PrecioReal;
            Row.Cells[colTotalReal].Value = Item.TotalReal;
        }

        private void udteFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                RendicionCotizacion.FechaCreacion = (DateTime)udteFechaCreacion.Value;
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
                ItemRendicionCotizacion Item = (ItemRendicionCotizacion)e.Cell.Row.Tag;
                switch (e.Cell.Column.Key)
                {
                    case colCantidadReal:
                        Item.CantidadReal = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        Item.TotalReal = Item.CantidadReal * Item.PrecioReal;
                        MostrarItem(e.Cell.Row);
                        MostrarTotales();
                        break;
                    case colPrecioReal:
                        Item.PrecioReal = Convert.ToDecimal(e.Cell.Text.Replace('_', ' '));
                        Item.TotalReal = Item.CantidadReal * Item.PrecioReal;
                        MostrarItem(e.Cell.Row);
                        MostrarTotales();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }
    }
}
