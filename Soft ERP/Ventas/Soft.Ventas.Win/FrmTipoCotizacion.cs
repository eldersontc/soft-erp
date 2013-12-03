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

namespace Soft.Ventas.Win
{
    public partial class FrmTipoCotizacion : FrmParent 
    {
        public FrmTipoCotizacion()
        {
            InitializeComponent();
        }

        public TipoCotizacion TipoCotizacion { get { return (TipoCotizacion)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoCotizacion.Reporte != null) ? TipoCotizacion.Reporte.Nombre : "";
            txtCodigo.Text = TipoCotizacion.Codigo;
            txtNombre.Text = TipoCotizacion.Nombre;
            txtDescripcion.Text = TipoCotizacion.Descripcion;
            txtSerieCodigo.Text = TipoCotizacion.CodigoSerie;
            txtSerieLongitud.Value = TipoCotizacion.LongitudSerie;
            txtNumeracionActual.Value = TipoCotizacion.NumeracionActual;
            txtNumeracionLongitud.Value = TipoCotizacion.NumeracionLongitud;
            CheckActivo.Checked = TipoCotizacion.Activo;
            CheckNumeracionAutomatica.Checked = TipoCotizacion.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoCotizacion.GeneraNumeracionAlFinal;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoCotizacion.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoCotizacion.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoCotizacion.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoCotizacion.Descripcion = txtDescripcion.Text;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoCotizacion.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoCotizacion.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            TipoCotizacion.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoCotizacion.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            TipoCotizacion.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            TipoCotizacion.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
            TipoCotizacion.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
            ssReporte.Text = (TipoCotizacion.Reporte != null) ? TipoCotizacion.Reporte.Nombre : "";
        }
    }
}
