using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Finanzas.Entidades;
using Soft.Exceptions;
using Soft.Entities;

namespace Soft.Finanzas.Win
{
    public partial class FrmCaja : FrmParent
    {
        public FrmCaja()
        {
            InitializeComponent();
        }

        public Caja Caja { get { return (Caja)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssMoneda.Text = (Caja.Moneda  != null) ? Caja.Moneda.Simbolo : "";
            txtCodigo.Text = Caja.Codigo;
            txtNombre.Text = Caja.Nombre;
            uneSaldoActual.Value = Caja.SaldoActual;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Caja.Codigo = txtCodigo.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Caja.Nombre = txtNombre.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                Caja.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Clear(object sender, EventArgs e)
        {
            try
            {
                Caja.Moneda = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }
    }
}
