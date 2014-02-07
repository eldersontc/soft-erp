using System;
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
using Soft.Ventas.Entidades;
using Soft.Exceptions;

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

        //Constantes Maquinas
        const String colCodigoMaquina = "Codigo";
        const String colNombreMaquina = "Nombre";
        const String colMaquinaDefecto = "Por Defecto";

        


        public Existencia Existencia { get { return (Existencia)base.m_ObjectFlow; } }

        private ExistenciaUnidad ExistenciaUnidad;



        public override void Init()
        {
            this.InitGridUnidad();
            this.InitGridAlmancen();
            this.InitGridMaquina();
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



        public void InitGridMaquina()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colCodigoMaquina);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colNombreMaquina);
            column.DataType = typeof(String);
            column.ReadOnly = true;


            column = columns.Columns.Add(colMaquinaDefecto);
            column.DataType = typeof(Boolean);
            column.ReadOnly = false;


            grillaMaquinas.DataSource = columns;
            grillaMaquinas.DisplayLayout.Bands[0].Columns[colCodigoMaquina].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            grillaMaquinas.DisplayLayout.Bands[0].Columns[colNombreMaquina].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            grillaMaquinas.DisplayLayout.Bands[0].Columns[colMaquinaDefecto].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
        
        }


        public void Mostrar()
        {
            txtCodigo.Text = this.Existencia.Codigo;


            uceGramaje.Text = this.Existencia.UnidadMedidaDimensiones;
            chekEnCotizacion.Checked = this.Existencia.EsCotizacion;

            txtNombre.Text = this.Existencia.Nombre;
            txtDescripcion.Text = this.Existencia.Descripcion;
            CheckActivo.Checked = this.Existencia.Activo;
            checkEsCompra.Checked = this.Existencia.EsCompra;
            checkEsVenta.Checked = this.Existencia.EsVenta;
            checkesServicio.Checked = this.Existencia.EsServicio;
            checkEsInventariable.Checked = this.Existencia.EsInventariable;

            txtUbicacion.Text = this.Existencia.Ubicacion;
            txtLargo.Value = this.Existencia.Largo;
            txtAlto.Value = this.Existencia.Alto;
            txtGramaje.Value = this.Existencia.Gramaje;
            uneCostoUltimaCompra.Value = this.Existencia.CostoUltimaCompra;
            uneCostoPromedio.Value = this.Existencia.CostoPromedio;

            busClasificacion.Text = (this.Existencia.ClasificacionExistencia != null) ? this.Existencia.ClasificacionExistencia.Nombre : "";
            busItemClasificacion.Text = (this.Existencia.ItemClasificacionExistencia != null) ? this.Existencia.ItemClasificacionExistencia.Nombre : "";
            busMarca.Text = (this.Existencia.Marca != null) ? this.Existencia.Marca.Nombre : "";

            this.MostrarUnidades();
            this.MostrarAlmacenes();
            this.MostrarMaquinas();

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



        public void MostrarMaquinas()
        {
            base.ClearAllRows(ref grillaMaquinas);
            foreach (ExistenciaMaquina Item in this.Existencia.Maquinas)
            {
                UltraGridRow Row = grillaMaquinas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarMaquina(Row);
            }
        }

        public void MostrarMaquina(UltraGridRow Row)
        {
            ExistenciaMaquina item = (ExistenciaMaquina)Row.Tag;
            Row.Cells[colCodigoMaquina].Value = item.Maquina.Codigo;
            Row.Cells[colNombreMaquina].Value = item.Maquina.Nombre;
            Row.Cells[colMaquinaDefecto].Value = item.PorDefecto;
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

            if (Existencia.EsServicio == true) {
                Existencia.EsInventariable = false;
            }
            Mostrar();
        }

        private void checkEsInventariable_CheckedChanged(object sender, EventArgs e)
        {
            Existencia.EsInventariable = checkEsInventariable.Checked;
        }

        private void busClasificacion_Search(object sender, EventArgs e)
        {
            try
            {
                String filtro = "";

                if (busClasificacion.Text.Length > 0)
                {
                    filtro = "Nombre like '" + busClasificacion.Text + "%'";
                }
                FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
                ClasificacionExistencia clasificacion = null;

                clasificacion = (ClasificacionExistencia)FrmSeleccionarPanel.GetSelectedEntity(typeof(ClasificacionExistencia), "Clasificación de Existencia", filtro);


                if ((clasificacion != null))
                {
                    this.Existencia.ClasificacionExistencia = clasificacion;
                    Existencia.ItemClasificacionExistencia = null;
                }

                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busItemClasificacion_Search(object sender, EventArgs e)
        {

            if (Existencia.ClasificacionExistencia == null)
            {
                MessageBox.Show("Debe elegir una clasificacion", "Error",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }

            else { 
            String filtro = "IDClasificacionExistencia='" + this.Existencia.ClasificacionExistencia.ID + "'";
            if (busItemClasificacion.Text.Length > 0) {
                filtro = filtro + " and Nombre like '" + busItemClasificacion.Text + "%'";
            }
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            this.Existencia.ItemClasificacionExistencia = (ItemClasificacionExistencia)FrmSeleccionarPanel.GetSelectedEntity(typeof(ItemClasificacionExistencia), "ItemClasificacionExistencia", filtro);
            }
            Mostrar();
            
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
                    Item.FactorConversion = Convert.ToInt32(e.Cell.Text.Replace('_', ' '));
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

        private void btnNuevaMaquina_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            String filtro = "id not in (";
            String ids = "";

            foreach (ExistenciaMaquina Item in Existencia.Maquinas)
            {
                ids = ids + "'" + Item.Maquina.ID + "',";

            }

            if (ids.Length > 0)
            {
                filtro = filtro + ids.Substring(0, ids.Length - 1) + ")";
            }
            else
            {
                filtro = "";
            }


            Maquina maquina = (Maquina)FrmSeleccionarPanel.GetSelectedEntity(typeof(Maquina), "Máquina", filtro);

            if (maquina != null)
            {
                UltraGridRow Row = grillaMaquinas.DisplayLayout.Bands[0].AddNew();
                Row.Tag = this.Existencia.AddItemMaquina();
                ExistenciaMaquina item = (ExistenciaMaquina)Row.Tag;
                item.Maquina = maquina;
                MostrarMaquina(Row);
            }
        }

        private void btnEliminarMaquina_Click(object sender, EventArgs e)
        {
            if (grillaMaquinas.ActiveRow == null) { return; }
            this.Existencia.Maquinas.Remove((ExistenciaMaquina)this.grillaMaquinas.ActiveRow.Tag);
            this.grillaMaquinas.ActiveRow.Delete(false);
        }

        private void txtGramaje_ValueChanged(object sender, EventArgs e)
        {
            Existencia.Gramaje = Convert.ToInt32(txtGramaje.Value);
        }

        private void txtLargo_ValueChanged(object sender, EventArgs e)
        {
            Existencia.Largo = Convert.ToDecimal(txtLargo.Value);
        }

        private void txtAlto_ValueChanged(object sender, EventArgs e)
        {
            Existencia.Alto = Convert.ToDecimal(txtAlto.Value);
        }

        private void chekEnCotizacion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Existencia.EsCotizacion = chekEnCotizacion.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void uceGramaje_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Existencia.UnidadMedidaDimensiones = uceGramaje.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtUbicacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Existencia.Ubicacion = txtUbicacion.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

    }
}
