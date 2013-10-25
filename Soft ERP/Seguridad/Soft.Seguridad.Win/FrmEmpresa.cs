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
    public partial class FrmEmpresa : FrmParent
    {
        public FrmEmpresa()
        {
            InitializeComponent();
        }

        public Empresa Empresa { get { return (Empresa)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar()
        {
            txtRazonSocial.Text = Empresa.RazonSocial;
            txtRUC.Text = Empresa.RUC;
        }

        private void txtRazonSocial_TextChanged(object sender, EventArgs e)
        {
            Empresa.RazonSocial = txtRazonSocial.Text;
        }

        private void txtRUC_TextChanged(object sender, EventArgs e)
        {
            Empresa.RUC = txtRUC.Text;
        }
    }
}
