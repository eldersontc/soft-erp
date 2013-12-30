using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Infragistics.Win.UltraWinExplorerBar;
using Soft.Configuracion.Entidades;
using Infragistics.Win.UltraWinTree;
using Infragistics.Win.UltraWinToolbars;
using Soft.DataAccess;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Soft.Seguridad.Entidades;

namespace Soft.Configuracion.Win
{
    public partial class FrmContenedor : FrmParent
    {
        public FrmContenedor()
        {
            InitializeComponent();
        }

        const String colNombre = "Nombre";
        const String colNombreAccion = "Nombre";
        const String colDescripcionAccion = "Descripción";

        public Contenedor Contenedor { get { return (Contenedor)base.m_ObjectFlow; } }

        private ItemContenedor ItemContenedor;

        private UltraExplorerBarGroup GroupActive;

        public override void Init()
        {
            InitGrids();
            MostrarItems();
            MostrarPerfiles();
        }

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNombre);
            column.DataType = typeof(String);

            ugPerfiles.DataSource = columns;

            columns = new DataTable();

            column = columns.Columns.Add(colNombreAccion);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colDescripcionAccion);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            ugAcciones.DataSource = columns;
        }

        public void MostrarItemContenedor()
        {
            ssPanel.Text = (ItemContenedor.Panel != null)?ItemContenedor.Panel.Nombre:"";
            ssCreacion.Text = (ItemContenedor.AccionCrear != null) ? ItemContenedor.AccionCrear.Nombre : "";
            ssModificacion.Text = (ItemContenedor.AccionModificar != null) ? ItemContenedor.AccionModificar.Nombre : "";
            ssEliminar.Text = (ItemContenedor.AccionEliminar != null) ? ItemContenedor.AccionEliminar.Nombre : "";
            ssDuplicacion.Text = (ItemContenedor.AccionCopiar != null) ? ItemContenedor.AccionCopiar.Nombre : ""; 
            txtContenedor.Text = (ItemContenedor.ItemPadre != null)?ItemContenedor.ItemPadre.Nombre: ""; 
            txtNombre.Text = ItemContenedor.Nombre;
            txtFiltro.Text = ItemContenedor.Filtro;
            uceEsContenedor.Checked = ItemContenedor.EsContenedor;
            uceEsPanel.Checked = ItemContenedor.EsPanel;
            upbImagen.Image = base.GetImage(ItemContenedor.Imagen);
            uceCreacion.Checked = ItemContenedor.Crear;
            uceModificacion.Checked = ItemContenedor.Modificar;
            uceEliminacion.Checked = ItemContenedor.Eliminar;
            uceDuplicacion.Checked = ItemContenedor.Copiar;
            uceAuditoria.Checked = ItemContenedor.Auditar;
            txtNombre.Focus();
            MostrarAcciones();
        }

        public void MostrarItems() {
            uebContenedor.Groups.Clear();
            foreach (ItemContenedor Item in Contenedor.Items)
            {
                if (Item.EsContenedor)
                {
                    UltraExplorerBarGroup Group = new UltraExplorerBarGroup();
                    UltraExplorerBarContainerControl Container = new UltraExplorerBarContainerControl();
                    UltraTree Tree = new UltraTree();
                    uebContenedor.Controls.Add(Container);
                    Tree.Dock = DockStyle.Fill;
                    Tree.NodeConnectorStyle = NodeConnectorStyle.None;
                    Tree.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.Standard;
                    Tree.DisplayStyle = UltraTreeDisplayStyle.WindowsVista;
                    Tree.AfterSelect += Tree_AfterSelect;
                    Container.Controls.Add(Tree);
                    Group.Text = Item.Nombre;
                    Group.Settings.AppearancesLarge.HeaderAppearance.Image = ilMain.Images[Item.Imagen];
                    Group.Tag = Item;
                    Group.Container = Container;
                    Group.Settings.Style = GroupStyle.ControlContainer;
                    uebContenedor.Groups.Add(Group);
                    AddNodesToTree(Tree.Nodes, Item);
                    Tree.ExpandAll();
                }
            }
        }

        public void MostrarPerfiles()
        {
            ugPerfiles.Selected.Rows.AddRange((UltraGridRow[])ugPerfiles.Rows.All);
            ugPerfiles.DeleteSelectedRows(false);
            foreach (ContenedorPerfil Perfil in Contenedor.Perfiles)
            {
                UltraGridRow Row = ugPerfiles.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Perfil;
                MostrarPerfil(Row);
            }
        }

        public void MostrarPerfil(UltraGridRow Row)
        {
            ContenedorPerfil Perfil = (ContenedorPerfil)Row.Tag;
            Row.Cells[colNombre].Value = Perfil.Perfil.Nombre;
        }

        public void MostrarAcciones()
        {
            ugAcciones.Selected.Rows.AddRange((UltraGridRow[])ugAcciones.Rows.All);
            ugAcciones.DeleteSelectedRows(false);
            foreach (ItemContenedorAccion Item in ItemContenedor.Acciones)
            {
                UltraGridRow Row = ugAcciones.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarAccion(Row);
            }
        }

        public void MostrarAccion(UltraGridRow Row)
        {
            ItemContenedorAccion Accion = (ItemContenedorAccion)Row.Tag;
            Row.Cells[colNombreAccion].Value = Accion.Accion.Nombre;
            Row.Cells[colDescripcionAccion].Value = Accion.Accion.Descripcion;
        }

        public void AddNodesToTree(TreeNodesCollection Nodes,ItemContenedor ItemPadre) {
            foreach (ItemContenedor Item in Contenedor.GetItemsByItemParent(ItemPadre.ID)){
                UltraTreeNode Node = new UltraTreeNode(Item.ID, Item.Nombre);
                Image Imagen = ilMain.Images[Item.Imagen];
                Node.Tag = Item;
                if (Imagen != null) { Node.LeftImages.Add(Imagen); };
                Nodes.Add(Node);
                AddNodesToTree(Node.Nodes, Item);
            }
        }

        public void HabilitarControles() {
            txtNombre.Enabled = true;
        }

        public void Tree_AfterSelect(Object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e) {
            ItemContenedor = (ItemContenedor)((UltraTree)sender).ActiveNode.Tag;
            ubAñadirModificar.Text = "Modificar";
            MostrarItemContenedor();
            HabilitarControles();
        }

        private void uebContenedor_GroupClick(object sender, GroupEventArgs e)
        {
            this.ItemContenedor = (ItemContenedor)uebContenedor.ActiveGroup.Tag;
            this.MostrarItemContenedor();
            this.GroupActive = uebContenedor.ActiveGroup;
            ubAñadirModificar.Text = "Modificar";
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ItemContenedor != null){ Contenedor.DeleteItem(ItemContenedor); }
            MostrarItems();
        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            ItemContenedor Item = new ItemContenedor();
            Item.ItemPadre = ItemContenedor;
            ItemContenedor = Item;
            MostrarItemContenedor();
            ubAñadirModificar.Text = "Añadir";
        }

        private void ubAñadirModificar_Click(object sender, EventArgs e)
        {
            switch (ubAñadirModificar.Text)
            {
                case "Añadir":
                    if (!Contenedor.Items.Contains(ItemContenedor)) {
                        if (ItemContenedor.EsContenedor) { ItemContenedor.ItemPadre = null; }
                        Contenedor.AddItem(ItemContenedor); 
                    }
                    ubAñadirModificar.Text = "Modificar";
                    break;
                default:
                    break;
            }
            MostrarItems();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            ItemContenedor.Nombre = txtNombre.Text;
        }

        private void ssPanel_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            ItemContenedor.Panel = (Soft.Configuracion.Entidades.Panel)FrmSeleccionarPanel.GetSelectedEntity(typeof(Soft.Configuracion.Entidades.Panel), "Panel");
            if (ItemContenedor.Panel != null) { ssPanel.Text = ItemContenedor.Panel.Nombre; }
        }

        private void uceEsContenedor_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.EsContenedor = uceEsContenedor.Checked;
            if (uceEsContenedor.Checked)
            {
                ssPanel.Enabled = false;
                uceEsPanel.Checked = false;
                txtContenedor.Text = "";
            }
        }

        private void uceEsPanel_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.EsPanel = uceEsPanel.Checked;
            if (uceEsPanel.Checked)
            {
                ssPanel.Enabled = true;
                uceEsContenedor.Checked = false;
                utcDetalle.Tabs["Acciones"].Enabled = true;
                uceCreacion.Enabled = true;
                uceModificacion.Enabled = true;
                uceEliminacion.Enabled = true;
                uceAuditoria.Enabled = true;
                uceDuplicacion.Enabled = true;
            }
            else {
                utcDetalle.Tabs["Acciones"].Enabled = false;
                uceCreacion.Enabled = false;
                uceModificacion.Enabled = false;
                uceEliminacion.Enabled = false;
                uceAuditoria.Enabled = false;
                uceDuplicacion.Enabled = false;
            }
        }

        private void ubBuscarImage_Click(object sender, EventArgs e)
        {
            FrmSelectedImage FrmImage = new FrmSelectedImage();
            ItemContenedor.Imagen = FrmImage.GetSelectedImage();
            upbImagen.Image = base.GetImage(this.ItemContenedor.Imagen);
        }

        private void ubNuevoPerfil_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmAccion = new FrmSelectedEntity();
            Collection Perfiles = FrmAccion.GetSelectedsEntities(typeof(Perfil), "Perfil");
            foreach (Perfil Perfil in Perfiles)
            {
                Contenedor.AddPerfil(Perfil);
            }
            MostrarPerfiles();
        }

        private void ubEliminarPerfil_Click(object sender, EventArgs e)
        {
            if (ugPerfiles.ActiveRow == null) { return; }
            Contenedor.Perfiles.Remove((ContenedorPerfil)ugPerfiles.ActiveRow.Tag);
            ugPerfiles.ActiveRow.Delete(false);
        }

        private void uceCreacion_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.Crear = uceCreacion.Checked;
            ssCreacion.Enabled = !ItemContenedor.Crear;
        }

        private void uceModificacion_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.Modificar = uceModificacion.Checked;
            ssModificacion.Enabled = !ItemContenedor.Modificar;
        }

        private void uceEliminacion_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.Eliminar = uceEliminacion.Checked;
            ssEliminar.Enabled = !ItemContenedor.Eliminar;
        }

        private void uceDuplicacion_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.Copiar = uceDuplicacion.Checked;
            ssDuplicacion.Enabled = !ItemContenedor.Copiar;
        }

        private void uceAuditoria_CheckedChanged(object sender, EventArgs e)
        {
            ItemContenedor.Auditar = uceAuditoria.Checked;
        }

        private void ubNuevaAccion_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmAccion = new FrmSelectedEntity();
            Collection Items = FrmAccion.GetSelectedsEntities(typeof(Accion), "Acción");
            foreach (Accion Item in Items)
            {
                ItemContenedor.AddAccion(Item);
            }
            MostrarAcciones();
        }

        private void ubEliminarAccion_Click(object sender, EventArgs e)
        {
            if (ugAcciones.ActiveRow == null) { return; }
            ItemContenedor.Acciones.Remove((ItemContenedorAccion)ugAcciones.ActiveRow.Tag);
            ugAcciones.ActiveRow.Delete(false);
        }

        private void ssCreacion_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarAccion = new FrmSelectedEntity();
            ItemContenedor.AccionCrear = (Accion)FrmSeleccionarAccion.GetSelectedEntity(typeof(Accion), "Acción");
            ssCreacion.Text = (ItemContenedor.AccionCrear != null) ? ItemContenedor.AccionCrear.Nombre : "";
        }

        private void ssModificacion_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarAccion = new FrmSelectedEntity();
            ItemContenedor.AccionModificar = (Accion)FrmSeleccionarAccion.GetSelectedEntity(typeof(Accion), "Acción");
            ssModificacion.Text = (ItemContenedor.AccionModificar != null) ? ItemContenedor.AccionModificar.Nombre : "";
        }

        private void ssEliminar_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarAccion = new FrmSelectedEntity();
            ItemContenedor.AccionEliminar = (Accion)FrmSeleccionarAccion.GetSelectedEntity(typeof(Accion), "Acción");
            ssEliminar.Text = (ItemContenedor.AccionEliminar != null) ? ItemContenedor.AccionEliminar.Nombre : "";
        }

        private void ssDuplicacion_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarAccion = new FrmSelectedEntity();
            ItemContenedor.AccionCopiar = (Accion)FrmSeleccionarAccion.GetSelectedEntity(typeof(Accion), "Acción");
            ssDuplicacion.Text = (ItemContenedor.AccionCopiar != null) ? ItemContenedor.AccionCopiar.Nombre : "";
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            ItemContenedor.Filtro = txtFiltro.Text;
        }

    }
}
