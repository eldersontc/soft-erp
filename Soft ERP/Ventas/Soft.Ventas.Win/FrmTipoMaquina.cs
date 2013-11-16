using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;
using Soft.DataAccess;
using Soft.Win;
using Soft.Ventas.Entidades;

namespace Soft.Ventas.Win
{
    public partial class FrmTipoMaquina : FrmParent 
    {
        public FrmTipoMaquina()
        {
            InitializeComponent();
        }

        public TipoMaquina TipoMaquina { get { return (TipoMaquina)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            txtDescripcion.Text = TipoMaquina.Descripcion;
            uceActivo.Checked = TipoMaquina.Activo;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoMaquina.Descripcion = txtDescripcion.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoMaquina.Activo = uceActivo.Checked;
        }
    }
}
