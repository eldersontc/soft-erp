using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Seguridad.Entidades;

namespace Soft.Seguridad.Win
{
    public partial class FrmPerfil : FrmParent 
    {
        public FrmPerfil()
        {
            InitializeComponent();
        }

        public Perfil Perfil { get { return (Perfil)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar()
        {
            txtNombre.Text = Perfil.Nombre;
            txtAbreviacion.Text = Perfil.Abreviacion;
            uceActivo.Checked = Perfil.Activo;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Perfil.Nombre = txtNombre.Text;
        }

        private void txtAbreviacion_TextChanged(object sender, EventArgs e)
        {
            Perfil.Abreviacion = txtAbreviacion.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Perfil.Activo = uceActivo.Checked;
        }

    }
}
