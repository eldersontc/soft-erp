using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Entities;
using Soft.Finanzas.Entidades;
using Soft.Exceptions;
using Soft.Reporte.Entidades;

namespace Soft.Finanzas.Win
{
    public partial class FrmTipoCaja : FrmParent
    {
        public FrmTipoCaja()
        {
            InitializeComponent();
        }

        public TipoCaja TipoCaja { get { return (TipoCaja)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoCaja.Reporte != null) ? TipoCaja.Reporte.Nombre : "";
            txtCodigo.Text = TipoCaja.Codigo;
            txtNombre.Text = TipoCaja.Nombre;
            txtDescripcion.Text = TipoCaja.Descripcion;
            txtSerieCodigo.Text = TipoCaja.CodigoSerie;
            txtSerieLongitud.Value = TipoCaja.LongitudSerie;
            txtNumeracionActual.Value = TipoCaja.NumeracionActual;
            txtNumeracionLongitud.Value = TipoCaja.NumeracionLongitud;
            CheckActivo.Checked = TipoCaja.Activo;
            CheckNumeracionAutomatica.Checked = TipoCaja.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoCaja.GeneraNumeracionAlFinal;
            unePorcentajeImpuesto.Value = TipoCaja.PorcentajeImpuesto;
            cboMovimiento.Text = TipoCaja.Movimiento;
            cboSocioNegocio.Text = TipoCaja.TipoSocioDeNegocio;
            uceGeneraDeuda.Checked = TipoCaja.GeneraDeuda;
            uceTipoDeuda.Text = TipoCaja.TipoDeuda;
            uceTipoDeuda.Enabled = TipoCaja.GeneraDeuda;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.Codigo = txtCodigo.Text;
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
                TipoCaja.Nombre = txtNombre.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.Descripcion = txtDescripcion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void cboMovimiento_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.Movimiento = cboMovimiento.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void cboSocioNegocio_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.TipoSocioDeNegocio = cboSocioNegocio.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void unePorcentajeImpuesto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.PorcentajeImpuesto = Convert.ToDecimal(unePorcentajeImpuesto.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.CodigoSerie = txtSerieCodigo.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                TipoCaja.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionar.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
                ssReporte.Text = (TipoCaja.Reporte != null) ? TipoCaja.Reporte.Nombre : "";
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
                TipoCaja.Activo = CheckActivo.Checked;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void uceGeneraDeuda_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoCaja.GeneraDeuda = uceGeneraDeuda.Checked;
                uceTipoDeuda.Enabled = TipoCaja.GeneraDeuda;
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
                TipoCaja.TipoDeuda = uceTipoDeuda.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }
    }
}
