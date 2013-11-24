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
using Soft.DataAccess;
using Infragistics.Win;
using Infragistics.Win.UltraWinTree;

namespace Soft.Ventas.Win
{
    public partial class FrmPlantilla : FrmParent 
    {
        public FrmPlantilla()
        {
            InitializeComponent();
        }

        //Constantes
        const String colNobre = "Nombre";
        const String colUnidad = "Unidad";
        const String colCantidad = "Cantidad";

        public  Plantilla Plantilla { get { return (Plantilla)base.m_ObjectFlow; } }
        private ItemPlantilla ItemPlantilla = null;
        private Boolean ActualizandoIU = false;

        public override void Init()
        {
            InitGrid();
            Mostrar();
        }

        public void InitGrid()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();
            
            column = columns.Columns.Add(colNobre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colCantidad);
            column.DataType = typeof(int);

            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colNobre].Width = 200;
            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            utPlantilla.Nodes[0].Text = Plantilla.Nombre;
            txtCodigo.Text = Plantilla.Codigo;
            txtNombre.Text = Plantilla.Nombre;
            uceActivo.Checked = Plantilla.Activo;
            MostrarItems();
            DeshabilitarControles();
        }

        public void MostrarItems()
        {
            foreach (ItemPlantilla Item in Plantilla.Items)
            {
                UltraTreeNode Node = new UltraTreeNode();
                Node.Tag = Item;
                Node.Text = Item.Nombre;
                Node.LeftImages.Add(utPlantilla.Nodes[0].LeftImages[0]);
                utPlantilla.Nodes[0].Nodes.Add(Node);
                MostrarItem(Node);
            }
            utPlantilla.ExpandAll();
        }

        public void MostrarItem(UltraTreeNode Node)
        {
            ItemPlantilla Item = (ItemPlantilla)Node.Tag;
            txtNombreItem.Text = Item.Nombre;
            ssOperacion.Text = (Item.Operacion != null) ? Item.Operacion.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
            ssTipoUnidad.Text = (Item.TipoUnidad!= null) ? Item.TipoUnidad.Nombre : "";
            ssRelacionMedidas.Text = (Item.RelacionMedidas != null) ? Item.RelacionMedidas.Nombre : "";
            MostrarServicios(Item);
        }

        public void MostrarServicios(ItemPlantilla ItemPlantilla)
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemPlantillaServicio Item in ItemPlantilla.Servicios)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarServicio(Row);
            }
        }

        public void MostrarServicio(UltraGridRow Row)
        {
            ItemPlantillaServicio Item = (ItemPlantillaServicio)Row.Tag;
            if (Item.Servicio != null)
            {
                AgregarUnidades(Row);
                Row.Cells[colNobre].Value = Item.Servicio.Nombre;
                Row.Cells[colUnidad].Value = Item.Unidad.Nombre;
            }
            Row.Cells[colCantidad].Value = Item.Cantidad;
        }

        public void AgregarUnidades(UltraGridRow Row)
        {
            ItemPlantillaServicio Item = (ItemPlantillaServicio)Row.Tag;
            ValueList List = new ValueList();
            foreach (ExistenciaUnidad Unidad in Item.Servicio.Unidades)
            {
                if (Item.Unidad == null & Unidad.EsUnidadBase) { Item.Unidad = Unidad.Unidad; }
                List.ValueListItems.Add(Unidad.Unidad, Unidad.Unidad.Nombre);
            }
            Row.Cells[colUnidad].ValueList = List;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Plantilla.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            utPlantilla.Nodes[0].Text = txtNombre.Text;
            Plantilla.Nombre = txtNombre.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Plantilla.Activo = uceActivo.Checked;
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            if (ItemPlantilla == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String Filtro = "ID NOT IN (";
            String IDs = "";
            foreach (ItemPlantillaServicio Item in ItemPlantilla.Servicios)
            {
                IDs = IDs + "'" + Item.Servicio.ID + "',";
            }
            Filtro = (IDs.Length > 0) ? Filtro + IDs.Substring(0, IDs.Length - 1) + ") AND EsInventariable = 1" : " EsInventariable = 1";

            Existencia Existencia = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", Filtro);
            if (Existencia != null)
            {
                Existencia ExistenciaCompleta = (Existencia)HelperNHibernate.GetEntityByID("Existencia",Existencia.ID);
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ItemPlantilla.CrearServicio(ExistenciaCompleta);
                MostrarServicio(Row);
            }
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null || ItemPlantilla == null) { return; }
            ItemPlantilla.Servicios.Remove((ItemPlantillaServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
        }

        public void AddNode() {
            UltraTreeNode Node = new UltraTreeNode();
            ItemPlantilla Item = Plantilla.CrearItem();
            Node.LeftImages.Add(utPlantilla.Nodes[0].LeftImages[0]);
            Node.Tag = Item;
            utPlantilla.Nodes[0].Nodes.Add(Node);
            utPlantilla.ExpandAll();
            utPlantilla.ActiveNode = Node;
            Node.Selected = true;
            txtNombreItem.Focus();
        }

        private void ubNuevoItem_Click(object sender, EventArgs e)
        {
            AddNode();
        }

        private void ubEliminarItem_Click(object sender, EventArgs e)
        {
            UltraTreeNode Node = utPlantilla.ActiveNode;
            if (Node != null & Node.Parent != null)
            {
                Plantilla.Items.Remove((ItemPlantilla)Node.Tag);
                utPlantilla.Nodes[0].Nodes.Remove(Node);
            }
        }

        private void txtNombreItem_TextChanged(object sender, EventArgs e)
        {
            if (ItemPlantilla == null || ActualizandoIU) { return; }
            UltraTreeNode Node = utPlantilla.ActiveNode;
            Node.Text = txtNombreItem.Text;
            ItemPlantilla.Nombre = txtNombreItem.Text;
        }

        private void ugServicios_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            ItemPlantillaServicio Item = (ItemPlantillaServicio)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colUnidad:
                    Item.Unidad = (Unidad)e.Cell.ValueList.GetValue(e.Cell.ValueList.SelectedItemIndex);
                    break;
                case colCantidad:
                    Item.Cantidad = Convert.ToDecimal(e.Cell.Text);
                    break;
                default:
                    break;
            }
            MostrarServicio(e.Cell.Row);
        }

        private void ssRelacionMedidas_Search(object sender, EventArgs e)
        {
            if (ItemPlantilla == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemPlantilla.RelacionMedidas = (RelacionMedidas)FrmSeleccionar.GetSelectedEntity(typeof(RelacionMedidas), "Relación Medidas");
            ssRelacionMedidas.Text = (ItemPlantilla.RelacionMedidas != null) ? ItemPlantilla.RelacionMedidas.Nombre : "";
        }

        private void ssTipoUnidad_Search(object sender, EventArgs e)
        {
            if (ItemPlantilla == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemPlantilla.TipoUnidad = (TipoUnidad)FrmSeleccionar.GetSelectedEntity(typeof(TipoUnidad), "Tipo Unidad");
            ssTipoUnidad.Text = (ItemPlantilla.TipoUnidad != null) ? ItemPlantilla.TipoUnidad.Nombre : "";
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemPlantilla == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemPlantilla.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia"," EsInventariable = 1");
            ssMaterial.Text = (ItemPlantilla.Material != null) ? ItemPlantilla.Material.Nombre : "";
        }

        private void ssOperacion_Search(object sender, EventArgs e)
        {
            if (ItemPlantilla == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemPlantilla.Operacion = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia"," EsServicio = 1");
            ssOperacion.Text = (ItemPlantilla.Operacion != null) ? ItemPlantilla.Operacion.Nombre : "";
        }

        private void utPlantilla_AfterSelect(object sender, SelectEventArgs e)
        {
            UltraTreeNode Node = utPlantilla.ActiveNode;
            if (Node.Tag != null)
            {
                ItemPlantilla = (ItemPlantilla)Node.Tag;
                MostrarItem(utPlantilla.ActiveNode);
                utcDetalle.Enabled = true;
            }
            else { DeshabilitarControles(); }            
        }

        public void DeshabilitarControles() {
            ActualizandoIU = true;
            txtNombreItem.Text = "";
            ssMaterial.Text = "";
            ssTipoUnidad.Text = "";
            ssRelacionMedidas.Text = "";
            ssOperacion.Text = "";
            ClearAllRows(ref ugServicios);
            utcDetalle.Enabled = false;
            ActualizandoIU = false;
        }

    }
}
