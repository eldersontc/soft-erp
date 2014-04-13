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
    public partial class FrmTipoNotaDebito : FrmParent 
    {
        public FrmTipoNotaDebito()
        {
            InitializeComponent();
        }

        public TipoNotaDebito TipoNotaDebito { get { return (TipoNotaDebito)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoNotaDebito.Reporte != null) ? TipoNotaDebito.Reporte.Nombre : "";
            txtCodigo.Text = TipoNotaDebito.Codigo;
            txtNombre.Text = TipoNotaDebito.Nombre;
            txtDescripcion.Text = TipoNotaDebito.Descripcion;
            txtSerieCodigo.Text = TipoNotaDebito.CodigoSerie;
            txtSerieLongitud.Value = TipoNotaDebito.LongitudSerie;
            txtNumeracionActual.Value = TipoNotaDebito.NumeracionActual;
            txtNumeracionLongitud.Value = TipoNotaDebito.NumeracionLongitud;
            CheckActivo.Checked = TipoNotaDebito.Activo;
            CheckNumeracionAutomatica.Checked = TipoNotaDebito.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoNotaDebito.GeneraNumeracionAlFinal;
            unePorcentajeImpuesto.Value = TipoNotaDebito.PorcentajeImpuesto;
            uceGeneraDeuda.Checked = TipoNotaDebito.GeneraDeuda;
            uceTipoDeuda.Text = TipoNotaDebito.TipoDeuda;
            uceTipoDeuda.Enabled = TipoNotaDebito.GeneraDeuda;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.Descripcion = txtDescripcion.Text;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
            TipoNotaDebito.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
            ssReporte.Text = (TipoNotaDebito.Reporte != null) ? TipoNotaDebito.Reporte.Nombre : "";
        }

        private void unePorcentajeImpuesto_ValueChanged(object sender, EventArgs e)
        {
            TipoNotaDebito.PorcentajeImpuesto = Convert.ToDecimal(unePorcentajeImpuesto.Value);
        }

        private void uceGeneraDeuda_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoNotaDebito.GeneraDeuda = uceGeneraDeuda.Checked;
                uceTipoDeuda.Enabled = TipoNotaDebito.GeneraDeuda;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void uceTipoDeuda_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoNotaDebito.TipoDeuda = uceTipoDeuda.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }
    }
}
