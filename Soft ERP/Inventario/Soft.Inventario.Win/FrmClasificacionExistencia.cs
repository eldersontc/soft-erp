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
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Soft.Inventario.Win
{
    public partial class FrmClasificacionExistencia : FrmParent
    {
        public FrmClasificacionExistencia()
        {
            InitializeComponent();
        }

        public ClasificacionExistencia ClasificacionExistencia { get { return (ClasificacionExistencia)base.m_ObjectFlow; } }


        public override void Init()
        {
            this.InitGrids();
            this.Mostrar();
        }


        const String colNombre = "Nombre";
        const String colCodigo = "Codigo";
        const String colActivo = "Activo";
 

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colCodigo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);
           
            column = columns.Columns.Add(colActivo);
            column.DataType = typeof(Boolean);

            grillaTiposClasificacion.DataSource = columns;
            grillaTiposClasificacion.DisplayLayout.Bands[0].Columns[colCodigo].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            grillaTiposClasificacion.DisplayLayout.Bands[0].Columns[colNombre].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            grillaTiposClasificacion.DisplayLayout.Bands[0].Columns[colActivo].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
            MapKeys(ref grillaTiposClasificacion);
        }

        public void Mostrar()
        {
            txtCodigo.Text = this.ClasificacionExistencia.Codigo;
            txtNombre.Text = this.ClasificacionExistencia.Nombre;
            CheckActivo.Checked = this.ClasificacionExistencia.Activo;
            this.MostrarItems();
        }


        public void MostrarItems()
        {
            base.ClearAllRows(ref grillaTiposClasificacion);
            foreach (ItemClasificacionExistencia Item in this.ClasificacionExistencia.Items)
            {
                UltraGridRow Row = grillaTiposClasificacion.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarItem(Row);
            }
        }


        public void MostrarItem(UltraGridRow Row)
        {
            ItemClasificacionExistencia Item = (ItemClasificacionExistencia)Row.Tag;
            Row.Cells[colCodigo].Value = Item.Codigo;
            Row.Cells[colNombre].Value = Item.Nombre;
            Row.Cells[colActivo].Value = Item.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ClasificacionExistencia.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ClasificacionExistencia.Nombre = txtNombre.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            ClasificacionExistencia.Activo = CheckActivo.Checked;
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = grillaTiposClasificacion.DisplayLayout.Bands[0].AddNew();
            Row.Tag = this.ClasificacionExistencia.AddItem();
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (grillaTiposClasificacion.ActiveRow == null) { return; }
            this.ClasificacionExistencia.Items.Remove((ItemClasificacionExistencia)this.grillaTiposClasificacion.ActiveRow.Tag);
            this.grillaTiposClasificacion.ActiveRow.Delete(false);
        }

        private void grillaTiposClasificacion_CellChange(object sender, CellEventArgs e)
        {
            ItemClasificacionExistencia Item = (ItemClasificacionExistencia)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colCodigo:
                    Item.Codigo = Convert.ToString(e.Cell.Text);
                    break;
                case colNombre:
                    Item.Nombre = Convert.ToString(e.Cell.Text);
                    break;
                case colActivo:
                    Item.Activo = Convert.ToBoolean(e.Cell.Text);
                    break;
                default:
                    break;
            }
            this.MostrarItem(e.Cell.Row);
        }

        private void tabItems_Paint(object sender, PaintEventArgs e)
        {

        }



    }
}
