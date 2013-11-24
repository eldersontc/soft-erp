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
    public partial class FrmTipoUnidad : FrmParent
    {
        public FrmTipoUnidad()
        {
            InitializeComponent();
        }


        public TipoUnidad TipoUnidad { get { return (TipoUnidad)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            Mostrar();
        }


        public void Mostrar()
        {
            txtNombre.Text = TipoUnidad.Nombre;
            CheckActivo.Checked = TipoUnidad.Activo;


        }


        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoUnidad.Activo = CheckActivo.Checked;
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            TipoUnidad.Nombre = txtNombre.Text;
        }

    }
}
