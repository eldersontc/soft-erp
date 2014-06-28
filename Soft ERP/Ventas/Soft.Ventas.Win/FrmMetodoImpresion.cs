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
using Soft.Exceptions;

namespace Soft.Ventas.Win
{
    public partial class FrmMetodoImpresion : FrmParent
    {
        public FrmMetodoImpresion()
        {
            InitializeComponent();
        }


        public override void Init()
        {
            Mostrar();
        }

        public MetodoImpresion MetodoImpresion { get { return (MetodoImpresion)base.m_ObjectFlow; } }

        private Boolean ActualizandoIU = false;

        private void Mostrar() {

            ActualizandoIU = true;

            txtCodigo.Text = MetodoImpresion.Codigo;
            txtNombre.Text = MetodoImpresion.Nombre;
            uceActivo.Checked = MetodoImpresion.Activo;
            txtFactorPases.Value = MetodoImpresion.FactorPases;
            txtFactorCambios.Value = MetodoImpresion.FactorCambios;
            ActualizandoIU = false;
    
        }

        private void txtCodigo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MetodoImpresion.Codigo = txtCodigo.Text;


            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MetodoImpresion.Activo = uceActivo.Checked;


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
                MetodoImpresion.Nombre = txtNombre.Text;

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void txtFactorPases_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MetodoImpresion.FactorPases = Convert.ToInt32(txtFactorPases.Value);

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void txtFactorCambios_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ActualizandoIU) { return; }
                MetodoImpresion.FactorCambios = Convert.ToInt32(txtFactorCambios.Value);

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

       
    }
}
