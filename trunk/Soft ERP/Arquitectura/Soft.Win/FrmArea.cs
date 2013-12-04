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
    public partial class FrmArea : FrmParent
    {
        public FrmArea()
        {
            InitializeComponent();
        }

        public Area Area { get { return (Area)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = Area.Codigo;
            txtNombre.Text = Area.Nombre;
            uceActivo.Checked = Area.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Area.Codigo = txtCodigo.Text;
        }

    
     
        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Area.Activo = uceActivo.Checked;
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            Area.Nombre = txtNombre.Text;
        }
    }
}
