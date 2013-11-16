﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Inventario.Entidades;
using Soft.Configuracion.Entidades;
using Soft.DataAccess;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Soft.Inventario.Win
{
    public partial class FrmExistencia : FrmParent
    {
        public FrmExistencia()
        {
            InitializeComponent();
        }



        //Constantes
        const String colUnidad = "Unidad";
        const String colEsBase = "Es Base";
        const String colFactor = "Factor de Conversion";


        //Constantes
        const String colAlmacen = "Almacen";
        const String colStockFisico = "Stock";
        const String colStockComprometido = "Comprometido";


        public Existencia Existencia { get { return (Existencia)base.m_ObjectFlow; } }

        private ExistenciaUnidad ExistenciaUnidad;



        public override void Init()
        {
            this.InitGridUnidad();
            this.InitGridAlmancen();
            this.Mostrar();
        }


        public void InitGridUnidad()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colEsBase);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colFactor);
            column.DataType = typeof(int);

            grillaUnidades.DataSource = columns;
            grillaUnidades.DisplayLayout.Bands[0].Columns[colUnidad].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            grillaUnidades.DisplayLayout.Bands[0].Columns[colEsBase].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            grillaUnidades.DisplayLayout.Bands[0].Columns[colFactor].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Integer;
        }

        public void InitGridAlmancen()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colAlmacen);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colStockFisico);
            column.DataType = typeof(Double);
            column.ReadOnly = true;

            column = columns.Columns.Add(colStockComprometido);
            column.DataType = typeof(Double);
            column.ReadOnly = true;
            
            grillaAlmacenes.DataSource = columns;
            grillaAlmacenes.DisplayLayout.Bands[0].Columns[colAlmacen].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            grillaAlmacenes.DisplayLayout.Bands[0].Columns[colStockFisico].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Double;
            grillaAlmacenes.DisplayLayout.Bands[0].Columns[colStockComprometido].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Double;
        }


        public void Mostrar()
        {
            txtCodigo.Text = this.Existencia.Codigo;
            txtNombre.Text = this.Existencia.Nombre;
            txtDescripcion.Text = this.Existencia.Descripcion;
            CheckActivo.Checked = this.Existencia.Activo;
            checkEsCompra.Checked = this.Existencia.EsCompra;
            checkEsVenta.Checked = this.Existencia.EsVenta;
            checkesServicio.Checked = this.Existencia.EsServicio;
            checkEsInventariable.Checked = this.Existencia.EsInventariable;

            busClasificacion.Text = (this.Existencia.ClasificacionExistencia != null) ? this.Existencia.ClasificacionExistencia.Nombre : "";
            busItemClasificacion.Text = (this.Existencia.ItemClasificacionExistencia != null) ? this.Existencia.ItemClasificacionExistencia.Nombre : "";
            busMarca.Text = (this.Existencia.Marca != null) ? this.Existencia.Marca.Nombre : "";

            this.MostrarUnidades();
            this.MostrarAlmacenes();

        }

        public void MostrarUnidades()
        {
            base.ClearAllRows(ref grillaUnidades);
            foreach (ExistenciaUnidad Item in this.Existencia.Unidades)
            {
                UltraGridRow Row = grillaUnidades.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarUnidad(Row);
            }
        }

        public void MostrarUnidad(UltraGridRow Row)
        {
            ExistenciaUnidad unidad = (ExistenciaUnidad)Row.Tag;
            Row.Cells[colUnidad].Value = unidad.Unidad.Nombre;
            Row.Cells[colEsBase].Value = unidad.EsUnidadBase;
            Row.Cells[colFactor].Value = unidad.FactorConversion;
        }

        public void MostrarAlmacenes()
        {
            base.ClearAllRows(ref grillaAlmacenes);
            foreach (ExistenciaAlmacen Item in this.Existencia.Almacenes)
            {
                UltraGridRow Row = grillaAlmacenes.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarAlmacen(Row);
            }
        }

        public void MostrarAlmacen(UltraGridRow Row)
        {
            ExistenciaAlmacen item = (ExistenciaAlmacen)Row.Tag;
            Row.Cells[colAlmacen].Value = item.Almacen.Nombre;
            Row.Cells[colStockFisico].Value = item.StockFisico;
            Row.Cells[colStockComprometido].Value = item.StockComprometido;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Existencia.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            Existencia.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Existencia.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            Existencia.Descripcion = txtDescripcion.Text;
        }

        private void checkEsCompra_CheckedChanged(object sender, EventArgs e)
        {
            Existencia.EsCompra = checkEsCompra.Checked;
        }

        private void checkEsVenta_CheckedChanged(object sender, EventArgs e)
        {
            Existencia.EsVenta = checkEsVenta.Checked;
        }

        private void checkesServicio_CheckedChanged(object sender, EventArgs e)
        {
            Existencia.EsServicio = checkesServicio.Checked;
        }

        private void checkEsInventariable_CheckedChanged(object sender, EventArgs e)
        {
            Existencia.EsInventariable = checkEsInventariable.Checked;
        }

        private void busClasificacion_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            this.Existencia.ClasificacionExistencia = (ClasificacionExistencia)FrmSeleccionarPanel.GetSelectedEntity(typeof(ClasificacionExistencia), "Clasificación de Existencia");
            if (this.Existencia.ClasificacionExistencia != null) {
                busClasificacion.Text = this.Existencia.ClasificacionExistencia.Nombre;
                busItemClasificacion.Enabled = true;
            }
        }

        private void busItemClasificacion_Search(object sender, EventArgs e)
        {
            String filtro = "IDClasificacionExistencia='" + this.Existencia.ClasificacionExistencia.ID + "'";
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            this.Existencia.ItemClasificacionExistencia = (ItemClasificacionExistencia)FrmSeleccionarPanel.GetSelectedEntity(typeof(ItemClasificacionExistencia), "ItemClasificacionExistencia", filtro);
            if (this.Existencia.ItemClasificacionExistencia != null)
            {
                busItemClasificacion.Text = this.Existencia.ItemClasificacionExistencia.Nombre;
                busItemClasificacion.Enabled = true;
            }
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            String filtro = "id not in (";
            String ids = "";

            foreach (ExistenciaUnidad Item in Existencia.Unidades)
            {
                ids = ids + "'" + Item.Unidad.ID + "',";
                
            }

            if (ids.Length > 0)
            {
                filtro = filtro + ids.Substring(0, ids.Length - 1) + ")";
            }
            else {
                filtro = "";
            }


            Unidad uni = (Unidad)FrmSeleccionarPanel.GetSelectedEntity(typeof(Unidad), "Unidad", filtro);

            if (uni != null) {
                UltraGridRow Row = grillaUnidades.DisplayLayout.Bands[0].AddNew();


                Row.Tag = this.Existencia.AddItem();
                ExistenciaUnidad itemunidad = (ExistenciaUnidad)Row.Tag;
                itemunidad.Unidad = uni;
                itemunidad.EsUnidadBase = true;
                itemunidad.FactorConversion = 1;

                MostrarUnidad(Row);
   
            }

            
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (grillaUnidades.ActiveRow == null) { return; }
            this.Existencia.Unidades.Remove((ExistenciaUnidad)this.grillaUnidades.ActiveRow.Tag);
            this.grillaUnidades.ActiveRow.Delete(false);
        }

        private void grillaUnidades_CellChange(object sender, CellEventArgs e)
        {
           ExistenciaUnidad Item = (ExistenciaUnidad)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colEsBase:
                    Item.EsUnidadBase = Convert.ToBoolean(e.Cell.Text);
                    break;
                case colFactor:
                    Item.FactorConversion = Convert.ToInt32(e.Cell.Text);
                    break;
                default:
                    break;
            }
            this.MostrarUnidad(e.Cell.Row);
        }

        private void grillaUnidades_ClickCellButton(object sender, CellEventArgs e)
        {
            ExistenciaUnidad Item = (ExistenciaUnidad)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colUnidad:
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    Item.Unidad = (Unidad)FrmSeleccionar.GetSelectedEntity(typeof(Unidad), "Unidad");
                    

                    break;
                default:
                    break;
            }
            this.MostrarUnidad(e.Cell.Row);
        }

        private void btnNuevoAlmacen_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            String filtro = "id not in (";
            String ids = "";

            foreach (ExistenciaAlmacen Item in Existencia.Almacenes)
            {
                ids = ids + "'" + Item.Almacen.ID + "',";

            }

            if (ids.Length > 0)
            {
                filtro = filtro + ids.Substring(0, ids.Length - 1) + ")";
            }
            else
            {
                filtro = "";
            }


            Almacen almacen = (Almacen)FrmSeleccionarPanel.GetSelectedEntity(typeof(Almacen), "Almacen", filtro);

            if (almacen != null)
            {
                UltraGridRow Row = grillaAlmacenes.DisplayLayout.Bands[0].AddNew();
                Row.Tag = this.Existencia.AddItemAlmacen();
                ExistenciaAlmacen item = (ExistenciaAlmacen)Row.Tag;
                item.Almacen = almacen;
                MostrarAlmacen(Row);
            }
        }

        private void btnEliminarAlmacen_Click(object sender, EventArgs e)
        {
            if (grillaAlmacenes.ActiveRow == null) { return; }
            this.Existencia.Almacenes.Remove((ExistenciaAlmacen)this.grillaAlmacenes.ActiveRow.Tag);
            this.grillaAlmacenes.ActiveRow.Delete(false);
        }

        private void busMarca_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            this.Existencia.Marca = (Marca)FrmSeleccionarPanel.GetSelectedEntity(typeof(Marca), "Marca");
            if (this.Existencia.Marca != null)
            {
                busMarca.Text = this.Existencia.Marca.Nombre;
            }
        }

    }
}