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
    public partial class FrmListaPreciosExistencia : FrmParent
    {
        public FrmListaPreciosExistencia()
        {
            InitializeComponent();
        }

        public ListaPreciosExistencia ListaPreciosExistencia { get { return (ListaPreciosExistencia)base.m_ObjectFlow; } }

        public ItemListaPreciosExistencia ItemListaPreciosExistencia;
        public UnidadListaPreciosExistencia UnidadListaPreciosExistencia;


        //Constantes
        const String colExistenciaCodigo = "Codigo";
        const String colExistenciaNombre = "Nombre";

        const String colUnidad = "Unidad";

        const String colDesde = "Desde";
        const String colHasta = "Hasta";
        const String colCosto = "Costo";



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

            column = columns.Columns.Add(colExistenciaCodigo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colExistenciaNombre);
            column.DataType = typeof(String);


            grillaExistencias.DataSource = columns;
            grillaExistencias.DisplayLayout.Bands[0].Columns[colExistenciaCodigo].Width = 100;
            grillaExistencias.DisplayLayout.Bands[0].Columns[colExistenciaCodigo].CellActivation = Activation.NoEdit;

            grillaExistencias.DisplayLayout.Bands[0].Columns[colExistenciaNombre].Width = 200;
            grillaExistencias.DisplayLayout.Bands[0].Columns[colExistenciaNombre].CellActivation = Activation.NoEdit;

            MapKeys(ref grillaExistencias);

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


            column = columns.Columns.Add(colCosto);
            column.DataType = typeof(Decimal);

            ugEscalas.DataSource = columns;
            ugEscalas.DisplayLayout.Bands[0].Columns[colDesde].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colHasta].DefaultCellValue = 0;
            ugEscalas.DisplayLayout.Bands[0].Columns[colCosto].DefaultCellValue = 0;
            MapKeys(ref ugEscalas);

        }


        public void Mostrar()
        {
            txtCodigo.Text = ListaPreciosExistencia.Codigo;
            txtNombre.Text = ListaPreciosExistencia.Nombre;
            uceActivo.Checked = ListaPreciosExistencia.Activo;
            MostrarItems();
        }


        public void MostrarItems()
        {
            foreach (ItemListaPreciosExistencia Item in ListaPreciosExistencia.Items)
            {
                UltraGridRow Row = grillaExistencias.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarItem(Row);
            }
        }


        public void MostrarItem(UltraGridRow Row)
        {
            ItemListaPreciosExistencia Item = (ItemListaPreciosExistencia)Row.Tag;
            Row.Cells[colExistenciaCodigo].Value = (Item.Existencia!= null) ? Item.Existencia.Codigo : "";
            Row.Cells[colExistenciaNombre].Value = (Item.Existencia != null) ? Item.Existencia.Nombre: "";
        }



        public void MostrarUnidades(ItemListaPreciosExistencia ItemListaPreciosExistencia)
        {
            base.ClearAllRows(ref ugUnidades);
            foreach (UnidadListaPreciosExistencia Item in ItemListaPreciosExistencia.Unidades)
            {
                UltraGridRow Row = ugUnidades.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarUnidad(Row);
            }
        }

        public void MostrarUnidad(UltraGridRow Row)
        {
            UnidadListaPreciosExistencia Item = (UnidadListaPreciosExistencia)Row.Tag;
            Row.Cells[colUnidad].Value = (Item.Unidad != null) ? Item.Unidad.Nombre : "";
        }


        public void MostrarEscalas(UnidadListaPreciosExistencia UnidadListaPreciosExistencia)
        {
            base.ClearAllRows(ref ugEscalas);
            foreach (EscalaListaPreciosExistencia Item in UnidadListaPreciosExistencia.Escalas)
            {
                UltraGridRow Row = ugEscalas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarEscala(Row);
            }
        }

        public void MostrarEscala(UltraGridRow Row)
        {
            EscalaListaPreciosExistencia Item = (EscalaListaPreciosExistencia)Row.Tag;
            Row.Cells[colDesde].Value = Item.Desde;
            Row.Cells[colHasta].Value = Item.Hasta;
            Row.Cells[colCosto].Value = Item.Costo;
        }

        private void txtCodigo_ValueChanged(object sender, EventArgs e)
        {
            ListaPreciosExistencia.Codigo = txtCodigo.Text;
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            ListaPreciosExistencia.Nombre = txtNombre.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            ListaPreciosExistencia.Activo = uceActivo.Checked;
        }

        private void ubNuevaExistencia_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Existencia Existencia = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", ListaPreciosExistencia.FiltroExistencias);
            if (Existencia != null)
            {
                UltraGridRow Row = grillaExistencias.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ListaPreciosExistencia.AddItem(Existencia);
                ItemListaPreciosExistencia = (ItemListaPreciosExistencia)Row.Tag;
                MostrarItem(Row);
            }
        }

        private void ubEliminarExistencia_Click(object sender, EventArgs e)
        {
            if (grillaExistencias.ActiveRow == null) { return; }
            ListaPreciosExistencia.Items.Remove((ItemListaPreciosExistencia)grillaExistencias.ActiveRow.Tag);
            grillaExistencias.ActiveRow.Delete(false);
        }

        private void ubNuevaUnidad_Click(object sender, EventArgs e)
        {
            String filtro = "IDExistencia='"+ItemListaPreciosExistencia.Existencia.ID+"'";

            if (ItemListaPreciosExistencia.FiltroUnidades.Length > 0) {
                filtro = " and " + ItemListaPreciosExistencia.FiltroUnidades;
            }
            if (ItemListaPreciosExistencia == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Unidad Unidad = (Unidad)FrmSeleccionar.GetSelectedEntity(typeof(Unidad), "ExistenciaUnidad", filtro);
            if (Unidad != null)
            {
                UltraGridRow Row = ugUnidades.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ItemListaPreciosExistencia.AddUnidad(Unidad);
                UnidadListaPreciosExistencia = (UnidadListaPreciosExistencia)Row.Tag;
                MostrarUnidad(Row);
            }
        }

        private void ubElimiarUnidad_Click(object sender, EventArgs e)
        {
            if (ugUnidades.ActiveRow == null) { return; }
            ItemListaPreciosExistencia.Unidades.Remove((UnidadListaPreciosExistencia)ugUnidades.ActiveRow.Tag);
            ugUnidades.ActiveRow.Delete(false);
        }

        private void ubNuevaEscala_Click(object sender, EventArgs e)
        {
            if (UnidadListaPreciosExistencia == null) { return; }
            UltraGridRow Row = ugEscalas.DisplayLayout.Bands[0].AddNew();
            Row.Tag = UnidadListaPreciosExistencia.AddEscala();
        }

        private void ubElminarEscala_Click(object sender, EventArgs e)
        {
            if (ugEscalas.ActiveRow == null) { return; }
            UnidadListaPreciosExistencia.Escalas.Remove((EscalaListaPreciosExistencia)ugEscalas.ActiveRow.Tag);
            ugEscalas.ActiveRow.Delete(false);
        }

        private void grillaExistencias_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (grillaExistencias.ActiveRow == null) { return; }
            ItemListaPreciosExistencia = (ItemListaPreciosExistencia)grillaExistencias.ActiveRow.Tag;
            MostrarUnidades(ItemListaPreciosExistencia);
        }

        private void ugUnidades_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            if (ugUnidades.ActiveRow == null) { return; }
            UnidadListaPreciosExistencia = (UnidadListaPreciosExistencia)ugUnidades.ActiveRow.Tag;
            MostrarEscalas(UnidadListaPreciosExistencia);
        }

        private void ugEscalas_CellChange(object sender, CellEventArgs e)
        {
            EscalaListaPreciosExistencia Escala = (EscalaListaPreciosExistencia)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colDesde:
                    Escala.Desde = Convert.ToInt32((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
                    break;
                case colHasta:
                    Escala.Hasta = Convert.ToInt32((e.Cell.Value == DBNull.Value) ? 0 : e.Cell.Value);
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
