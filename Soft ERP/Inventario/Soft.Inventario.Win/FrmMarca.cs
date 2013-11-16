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

namespace Soft.Inventario.Win
{
    public partial class FrmMarca : FrmParent 
    {
        public FrmMarca()
        {
            InitializeComponent();
        }

        public Marca Marca { get { return (Marca)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = Marca.Codigo;
            txtNombre.Text = Marca.Nombre;
            CheckActivo.Checked = Marca.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Marca.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            Marca.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Marca.Nombre = txtNombre.Text;
        }

    }
}
