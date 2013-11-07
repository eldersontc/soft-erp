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
    public partial class FrmTipoCambio : FrmParent
    {
        public FrmTipoCambio()
        {
            InitializeComponent();
        }

        public TipoCambio TipoCambio { get { return (TipoCambio)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }

        public void Mostrar()
        {
            ssMoneda.Text = (TipoCambio.Moneda != null) ? TipoCambio.Moneda.Simbolo : "";
            udtFecha.Value = TipoCambio.Fecha;
            uneCompra.Value = TipoCambio.TipoCambioCompra;
            uneVenta.Value = TipoCambio.TipoCambioVenta;
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            TipoCambio.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
            Mostrar();
        }

        private void udtFecha_ValueChanged(object sender, EventArgs e)
        {
            TipoCambio.Fecha = (DateTime)udtFecha.Value;
        }

        private void uneCompra_ValueChanged(object sender, EventArgs e)
        {
            TipoCambio.TipoCambioCompra = Convert.ToDecimal(uneCompra.Value);
        }

        private void uneVenta_ValueChanged(object sender, EventArgs e)
        {
            TipoCambio.TipoCambioVenta = Convert.ToDecimal(uneVenta.Value);
        }

    }
}
