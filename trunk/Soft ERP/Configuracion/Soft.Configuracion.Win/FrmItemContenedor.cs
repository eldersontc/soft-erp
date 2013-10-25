using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Configuracion.Entidades;
using Soft.DataAccess;

namespace Soft.Configuracion.Win
{
    public partial class FrmItemContenedor : FrmParent
    {
        public FrmItemContenedor()
        {
            InitializeComponent();
        }

        public FrmItemContenedor(ItemContenedor Item)
        {
            InitializeComponent();
            base.m_ObjectFlow = Item;
        }

        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar() {
            if (this.ItemContenedor.Panel != null) { ssPanelPrincipal.Text = this.ItemContenedor.Panel.Nombre; }
            if (this.ItemContenedor.ItemPadre != null) { txtContenedor.Text = this.ItemContenedor.ItemPadre.Nombre; }
            txtNombre.Text = this.ItemContenedor.Nombre;
            uceEsContenedor.Checked = this.ItemContenedor.EsContenedor;
            uceEsPanelPrincipal.Checked = this.ItemContenedor.EsPanel;
            upbImagen.Image = base.GetImage(this.ItemContenedor.Imagen);
            txtNombre.Focus();
        }

        public ItemContenedor ItemContenedor { get { return (ItemContenedor)base.m_ObjectFlow; } }

        private void uceEsContenedor_CheckedChanged(object sender, EventArgs e)
        {
            if (uceEsContenedor.Checked)
            {
                ssPanelPrincipal.Enabled = false;
                uceEsPanelPrincipal.Checked = false;
            }
        }

        private void uceEsPanelPrincipal_CheckedChanged(object sender, EventArgs e)
        {
            if (uceEsPanelPrincipal.Checked)
            {
                ssPanelPrincipal.Enabled = true;
                uceEsContenedor.Checked = false;
            }
        }

        private void ssPanelPrincipal_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            this.ItemContenedor.Panel = (Soft.Configuracion.Entidades.Panel)FrmSeleccionarPanel.GetSelectedEntity(typeof(Soft.Configuracion.Entidades.Panel), "Panel");
            if (this.ItemContenedor.Panel != null) { ssPanelPrincipal.Text = this.ItemContenedor.Panel.Nombre; }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            this.ItemContenedor.Nombre = txtNombre.Text;
        }

        private void ubAceptar_Click(object sender, EventArgs e)
        {
            this.ItemContenedor.EsContenedor = uceEsContenedor.Checked;
            this.ItemContenedor.EsPanel = uceEsPanelPrincipal.Checked;
            base.m_ResultProcess = EnumResult.SUCESS;
            this.Close();
        }

        private void ubCancelar_Click(object sender, EventArgs e)
        {
            base.m_ResultProcess = EnumResult.CANCEL;
            this.Close();
        }

        private void ubBuscarImage_Click(object sender, EventArgs e)
        {
            ControllerApp FrmImage = new FrmSelectedImage();
            FrmImage.Start();
            if (FrmImage.m_ResultProcess == EnumResult.SUCESS)
            {
                this.ItemContenedor.Imagen = ((FrmSelectedImage)FrmImage).GetSelectedImage();
                upbImagen.Image = base.GetImage(this.ItemContenedor.Imagen);
            }
        }

    }
}
