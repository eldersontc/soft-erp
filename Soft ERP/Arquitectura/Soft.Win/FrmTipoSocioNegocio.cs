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
    public partial class FrmTipoSocioNegocio : FrmParent
    {
        public FrmTipoSocioNegocio()
        {
            InitializeComponent();
        }

        public TipoSocioNegocio TipoSocioNegocio { get { return (TipoSocioNegocio)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = TipoSocioNegocio.Codigo;
            txtDescripcion.Text = TipoSocioNegocio.Descripcion;
            uceActivo.Checked = TipoSocioNegocio.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoSocioNegocio.Codigo = txtCodigo.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoSocioNegocio.Descripcion = txtDescripcion.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoSocioNegocio.Activo = uceActivo.Checked;
        }
    }
}
