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
    public partial class FrmUnidad : FrmParent
    {
        public FrmUnidad()
        {
            InitializeComponent();
        }

        public Unidad Unidad { get { return (Unidad)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar()
        {

            txtCodigo.Text = Unidad.Codigo;
            txtNombre.Text = Unidad.Nombre;
            CheckActivo.Checked = Unidad.Activo;

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Unidad.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Unidad.Nombre = txtNombre.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            Unidad.Activo = CheckActivo.Checked;
        }


    }
}
