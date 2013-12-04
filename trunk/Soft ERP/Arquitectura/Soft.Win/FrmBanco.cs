using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;

namespace Soft.Win
{
    public partial class FrmBanco : FrmParent
    {
        public FrmBanco()
        {
            InitializeComponent();
        }

        public Banco Banco { get { return (Banco)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = Banco.Codigo;
            txtNombre.Text = Banco.Nombre;
            uceActivo.Checked = Banco.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Banco.Codigo = txtCodigo.Text;
        }

    
     
        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Banco.Activo = uceActivo.Checked;
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            Banco.Nombre = txtNombre.Text;
        }
    }
}
