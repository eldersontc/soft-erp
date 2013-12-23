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
using Soft.Inventario.Entidades;

namespace Soft.Ventas.Win
{
    public partial class FrmListaCostosMaquina : FrmParent 
    {
        public FrmListaCostosMaquina()
        {
            InitializeComponent();
        }

        //Constantes
        const String colMaquina = "Máquina";
        const String colCostoProduccion = "Costo Producción";
        const String colCostoPreparacion = "Costo Preparación";
        const String colUnidad = "Unidad";
        const String colDesde = "Desde";
        const String colHasta = "Hasta";
        const String colVelocidad = "Velocidad";
        const String colCosto = "Costo";

        public ListaCostosMaquina ListaCostosMaquina { get { return (ListaCostosMaquina)base.m_ObjectFlow; } }
        public ItemListaCostosMaquina ItemListaCostosMaquina;
        public UnidadListaCostosMaquina UnidadListaCostosMaquina;

        #region "Métodos"

        public override void Init()
        {
            InitGrid();
            Mostrar();
        }

        public void InitGrid()
        {
            // Maquinas
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colMaquina);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCostoPreparacion);
            column.DataType = typeof(Decimal);

            column = columns.Columns.Add(colCostoProduccion);
            column.DataType = typeof(Decimal);

            ugMaquinas.DataSource = columns;
            ugMaquinas.DisplayLayout.Bands[0].Columns[colMaquina].Width = 200;
            ugMaquinas.DisplayLayout.Bands[0].Columns[colMaquina].CellActivation = Activation.NoEdit;
            ugMaquinas.DisplayLayout.Bands[0].Columns[colCostoPreparacion].DefaultCellValue = 0;
            ugMaquinas.DisplayLayout.Bands[0].Columns[colCostoProduccion].DefaultCellValue = 0;
            MapKeys(ref ugMaquinas);

            //Unidades
            columns = new DataTable();
            
            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);

            ugUnidades.DataSource = columns;
            MapKeys(ref ugUnidades);

            //Escalas
            columns = new DataTable();

            column = columns.Columns.Add(colDesde);
            column.DataType = typeof(Int32);

            column = columns.Columns.Add(colHasta);
            column.DataType = typeof(Int32);

            column = columns.Columns.Add(colVelocidad);
            column.DataType = typeof(Decimal);

            column = columns.Columns.Add(colCosto);
            column.DataType = typeof(Decimal);

            ugEscalas.DataSource = columns;
            ugEscalas.DisplayLayout.Bands[0].Columns[colDesde].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colHasta].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colVelocidad].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugEscalas.DisplayLayout.Bands[0].Columns[colVelocidad].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colVelocidad].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            ugEscalas.DisplayLayout.Bands[0].Columns[colCosto].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugEscalas.DisplayLayout.Bands[0].Columns[colCosto].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colCosto].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            MapKeys(ref ugEscalas);

        }

        public void Mostrar()
        {
            txtCodigo.Text = ListaCostosMaquina.Codigo;
            txtNombre.Text = ListaCostosMaquina.Nombre;
            uceActivo.Checked = ListaCostosMaquina.Activo;
            MostrarItems();
        }

        public void MostrarItems()
        {
            base.ClearAllRows(ref ugMaquinas);
            foreach (ItemListaCostosMaquina Item in ListaCostosMaquina.Items)
            {
                UltraGridRow Row = ugMaquinas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            ItemListaCostosMaquina Item = (ItemListaCostosMaquina)Row.Tag;
            Row.Cells[colMaquina].Value = (Item.Maquina != null)?Item.Maquina.Nombre:"";
            Row.Cells[colCostoPreparacion].Value = Item.CostoPreparacion;
            Row.Cells[colCostoProduccion].Value = Item.CostoProduccion;
            MostrarUnidades(Item);
        }

        public void MostrarUnidades(ItemListaCostosMaquina ItemListaCostosMaquina)
        {
            base.ClearAllRows(ref ugUnidades);
            foreach (UnidadListaCostosMaquina Item in ItemListaCostosMaquina.Unidades)
            {
                UltraGridRow Row = ugUnidades.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarUnidad(Row);
            }
        }

        public void MostrarUnidad(UltraGridRow Row)
        {
            UnidadListaCostosMaquina Item = (UnidadListaCostosMaquina)Row.Tag;
            Row.Cells[colUnidad].Value = (Item.Unidad != null) ? Item.Unidad.Nombre : "";
            MostrarEscalas(Item);
        }

        public void MostrarEscalas(UnidadListaCostosMaquina UnidadListaCostosMaquina)
        {
            base.ClearAllRows(ref ugEscalas);
            foreach (EscalaListaCostosMaquina Item in UnidadListaCostosMaquina.Escalas)
            {
                UltraGridRow Row = ugEscalas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarEscala(Row);
            }
        }

        public void MostrarEscala(UltraGridRow Row)
        {
            EscalaListaCostosMaquina Item = (EscalaListaCostosMaquina)Row.Tag;
            Row.Cells[colDesde].Value = Item.Desde;
            Row.Cells[colHasta].Value = Item.Hasta;
            Row.Cells[colVelocidad].Value = Item.Velocidad;
            Row.Cells[colCosto].Value = Item.Costo;
        }

        #endregion

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ListaCostosMaquina.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ListaCostosMaquina.Nombre = txtNombre.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            ListaCostosMaquina.Activo = uceActivo.Checked;
        }

        private void ubNuevaMaquina_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Maquina Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Máquina", ListaCostosMaquina.FiltroMaquinas);
            if (Maquina != null)
            {
                UltraGridRow Row = ugMaquinas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ListaCostosMaquina.AddItem(Maquina);
                ItemListaCostosMaquina = (ItemListaCostosMaquina)Row.Tag;
                MostrarItem(Row);
            }
        }

        private void ubEliminarMaquina_Click(object sender, EventArgs e)
        {
            if (ugMaquinas.ActiveRow == null) { return; }
            ListaCostosMaquina.Items.Remove((ItemListaCostosMaquina)ugMaquinas.ActiveRow.Tag);
            ugMaquinas.ActiveRow.Delete(false);
        }

        private void ubNuevaUnidad_Click(object sender, EventArgs e)
        {
            if (ItemListaCostosMaquina == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Unidad Unidad = (Unidad)FrmSeleccionar.GetSelectedEntity(typeof(Unidad), "Unidad", ItemListaCostosMaquina.FiltroUnidades);
            if (Unidad != null)
            {
                UltraGridRow Row = ugUnidades.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ItemListaCostosMaquina.AddUnidad(Unidad);
                UnidadListaCostosMaquina = (UnidadListaCostosMaquina)Row.Tag;
                MostrarUnidad(Row);
            }
        }

        private void ubElimiarUnidad_Click(object sender, EventArgs e)
        {
            if (ugUnidades.ActiveRow == null) { return; }
            ItemListaCostosMaquina.Unidades.Remove((UnidadListaCostosMaquina)ugUnidades.ActiveRow.Tag);
            ugUnidades.ActiveRow.Delete(false);
        }

        private void ubNuevaEscala_Click(object sender, EventArgs e)
        {
            if (UnidadListaCostosMaquina == null) { return; }
            UltraGridRow Row = ugEscalas.DisplayLayout.Bands[0].AddNew();
            Row.Tag = UnidadListaCostosMaquina.AddEscala();
        }

        private void ubElminarEscala_Click(object sender, EventArgs e)
        {
            if (ugEscalas.ActiveRow == null) { return; }
            UnidadListaCostosMaquina.Escalas.Remove((EscalaListaCostosMaquina)ugEscalas.ActiveRow.Tag);
            ugEscalas.ActiveRow.Delete(false);
        }

        private void ugMaquinas_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (ugMaquinas.ActiveRow == null) { return; }
            ItemListaCostosMaquina = (ItemListaCostosMaquina)ugMaquinas.ActiveRow.Tag;
            MostrarUnidades(ItemListaCostosMaquina);
        }

        private void ugUnidades_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (ugUnidades.ActiveRow == null) { return; }
            UnidadListaCostosMaquina = (UnidadListaCostosMaquina)ugUnidades.ActiveRow.Tag;
            MostrarEscalas(UnidadListaCostosMaquina);
        }

        private void ugMaquinas_CellChange(object sender, CellEventArgs e)
        {
            ItemListaCostosMaquina Item = (ItemListaCostosMaquina)e.Cell.Row.Tag;
            ugMaquinas.UpdateData();
            switch (e.Cell.Column.Key)
            {
                case colCostoPreparacion:
                    Item.CostoPreparacion = Convert.ToDecimal((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colCostoProduccion:
                    Item.CostoProduccion = Convert.ToDecimal((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                default:
                    break;
            }
            MostrarItem(e.Cell.Row);
        }

        private void ugEscalas_CellChange(object sender, CellEventArgs e)
        {
            EscalaListaCostosMaquina Escala = (EscalaListaCostosMaquina)e.Cell.Row.Tag;
            ugEscalas.UpdateData();
            switch (e.Cell.Column.Key)
            {
                case colDesde:
                    Escala.Desde = Convert.ToInt32((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colHasta:
                    Escala.Hasta = Convert.ToInt32((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colVelocidad:
                    Escala.Velocidad = Convert.ToDecimal((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colCosto:
                    Escala.Costo = Convert.ToDecimal((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                default:
                    break;
            }
            MostrarEscala(e.Cell.Row);
        }

    }
}
