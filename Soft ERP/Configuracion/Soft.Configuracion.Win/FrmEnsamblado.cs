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
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Configuracion.Win
{
    public partial class FrmEnsamblado : FrmParent 
    {
        public FrmEnsamblado()
        {
            InitializeComponent();
        }

        public Ensamblado Ensamblado { get { return (Ensamblado)base.m_ObjectFlow; } }

        public override void Init()
        {
           base.Init();
           this.Mostrar();
        }

        public void Mostrar() {
            txtNombre.Text = this.Ensamblado.Nombre;
            txtEnsamblado.Text = this.Ensamblado.Ensamblado_;
            uceActivo.Checked = this.Ensamblado.Activo;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            this.Ensamblado.Nombre = txtNombre.Text;
        }

        private void txtEnsamblado_TextChanged(object sender, EventArgs e)
        {
            this.Ensamblado.Ensamblado_ = txtEnsamblado.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            this.Ensamblado.Activo = uceActivo.Checked;
        }

    }
}
