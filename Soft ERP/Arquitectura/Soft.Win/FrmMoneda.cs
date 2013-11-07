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
    public partial class FrmMoneda : FrmParent
    {
        public FrmMoneda()
        {
            InitializeComponent();
        }

        public Moneda Moneda { get { return (Moneda)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = Moneda.Codigo;
            txtDescripcion.Text = Moneda.Descripcion;
            txtSimbolo.Text = Moneda.Simbolo;
            uceActivo.Checked = Moneda.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Moneda.Codigo = txtCodigo.Text;
        }

        private void txtSimbolo_TextChanged(object sender, EventArgs e)
        {
            Moneda.Simbolo = txtSimbolo.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            Moneda.Descripcion = txtDescripcion.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Moneda.Activo = uceActivo.Checked;
        }
    }
}
