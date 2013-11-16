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
    public partial class FrmModalidadCredito : FrmParent 
    {
        public FrmModalidadCredito()
        {
            InitializeComponent();
        }

        public ModalidadCredito ModalidadCredito { get { return (ModalidadCredito)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = ModalidadCredito.Codigo;
            txtDescripcion.Text = ModalidadCredito.Descripcion;
            uneDias.Value = ModalidadCredito.Dias;
            uceActivo.Checked = ModalidadCredito.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            ModalidadCredito.Codigo = txtCodigo.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            ModalidadCredito.Activo = uceActivo.Checked;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            ModalidadCredito.Descripcion = txtDescripcion.Text;
        }

        private void uneDias_ValueChanged(object sender, EventArgs e)
        {
            ModalidadCredito.Dias = Convert.ToInt32(uneDias.Value);
        }

    }
}
