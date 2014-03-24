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

namespace Soft.Facturacion.Win
{
    public partial class FrmTipoFacturacion : FrmParent 
    {
        public FrmTipoFacturacion()
        {
            InitializeComponent();
        }

        public TipoFacturacion TipoFacturacion { get { return (TipoFacturacion)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoFacturacion.Reporte != null) ? TipoFacturacion.Reporte.Nombre : "";
            txtCodigo.Text = TipoFacturacion.Codigo;
            txtNombre.Text = TipoFacturacion.Nombre;
            txtDescripcion.Text = TipoFacturacion.Descripcion;
            txtSerieCodigo.Text = TipoFacturacion.CodigoSerie;
            txtSerieLongitud.Value = TipoFacturacion.LongitudSerie;
            txtNumeracionActual.Value = TipoFacturacion.NumeracionActual;
            txtNumeracionLongitud.Value = TipoFacturacion.NumeracionLongitud;
            CheckActivo.Checked = TipoFacturacion.Activo;
            CheckNumeracionAutomatica.Checked = TipoFacturacion.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoFacturacion.GeneraNumeracionAlFinal;
            unePorcentajeImpuesto.Value = TipoFacturacion.PorcentajeImpuesto;
            uceComprobante.Text = TipoFacturacion.Comprobante;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoFacturacion.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoFacturacion.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoFacturacion.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoFacturacion.Descripcion = txtDescripcion.Text;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoFacturacion.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoFacturacion.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            TipoFacturacion.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoFacturacion.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            TipoFacturacion.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            TipoFacturacion.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
            TipoFacturacion.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
            ssReporte.Text = (TipoFacturacion.Reporte != null) ? TipoFacturacion.Reporte.Nombre : "";
        }

        private void unePorcentajeImpuesto_ValueChanged(object sender, EventArgs e)
        {
            TipoFacturacion.PorcentajeImpuesto = Convert.ToDecimal(unePorcentajeImpuesto.Value);
        }

        private void uceComprobante_ValueChanged(object sender, EventArgs e)
        {
            TipoFacturacion.Comprobante = uceComprobante.Text;
        }
    }
}
