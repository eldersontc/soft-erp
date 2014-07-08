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
    public partial class FrmPresupuestoAceptacionCliente : FrmParent
    {
        public FrmPresupuestoAceptacionCliente()
        {
            InitializeComponent();
        }

        public Presupuesto Presupuesto { get { return (Presupuesto)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }


        public void Mostrar()
        {
            labelNumeroPresupuesto.Text = Presupuesto.Numeracion;
            labelClilente.Text = (Presupuesto.Cliente != null) ? Presupuesto.Cliente.Nombre : "";
            txtOrdenCompraCliente.Text = Presupuesto.OrdenCompraCliente;
            txtInstruccionesCliente.Text = Presupuesto.InstruccionesCliente;
        }

        private void txtOrdenCompraCliente_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Presupuesto.OrdenCompraCliente = txtOrdenCompraCliente.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtInstruccionesCliente_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Presupuesto.InstruccionesCliente = txtInstruccionesCliente.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }


    }
}
