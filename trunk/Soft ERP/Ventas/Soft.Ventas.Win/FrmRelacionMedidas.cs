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
using Soft.Entities;


namespace Soft.Ventas.Win
{
    public partial class FrmRelacionMedidas  : FrmParent 
    {
        public FrmRelacionMedidas()
        {
            InitializeComponent();
        }

        public RelacionMedidas RelacionMedidas { get { return (RelacionMedidas)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            Mostrar();
        }


        public void Mostrar()
        {
            txtNombre.Text = RelacionMedidas.Nombre;
            CheckActivo.Checked = RelacionMedidas.Activo;
            txtAbierto.Value = RelacionMedidas.Abierta;
            txtCerrado.Value = RelacionMedidas.Cerrada;
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            RelacionMedidas.Nombre = txtNombre.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            RelacionMedidas.Activo = CheckActivo.Checked;
        }

        private void txtAbierto_ValueChanged(object sender, EventArgs e)
        {
            RelacionMedidas.Abierta = Convert.ToInt32(txtAbierto.Value);
        }

        private void txtCerrado_ValueChanged(object sender, EventArgs e)
        {
            RelacionMedidas.Cerrada = Convert.ToInt32(txtCerrado.Value);
        }

    }
}
