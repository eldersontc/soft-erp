using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Facturacion.Entidades;
using Soft.Exceptions;

namespace Soft.Facturacion.Win
{
    public partial class FrmMotivoTraslado : FrmParent
    {
        public FrmMotivoTraslado()
        {
            InitializeComponent();
        }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }
        public MotivoTraslado MotivoTraslado { get { return (MotivoTraslado)base.m_ObjectFlow; } }

        private Boolean ActualizandoIU = false;

        private void Mostrar() {
            ActualizandoIU = true;
            txtCodigo.Text = MotivoTraslado.Codigo;
            txtNombre.Text = MotivoTraslado.Nombre;
            txtNumero.Value = MotivoTraslado.Numero;
            CheckActivo.Checked = MotivoTraslado.Activo;
            ActualizandoIU = false;


        }

        private void txtCodigo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MotivoTraslado.Codigo =txtCodigo.Text ;

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MotivoTraslado.Nombre=txtNombre.Text  ;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNumero_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MotivoTraslado.Numero= Convert.ToInt32(txtNumero.Value);

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                if (ActualizandoIU) { return; }
                MotivoTraslado.Activo= CheckActivo.Checked;

            }
            catch (Exception)
            {
                
                throw;
            }
        }





    }
}
