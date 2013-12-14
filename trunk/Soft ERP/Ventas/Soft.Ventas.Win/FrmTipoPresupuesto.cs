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
    public partial class FrmTipoPresupuesto : FrmParent 
    {
        public FrmTipoPresupuesto()
        {
            InitializeComponent();
        }

        public TipoPresupuesto TipoPresupuesto { get { return (TipoPresupuesto)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoPresupuesto.Reporte != null) ? TipoPresupuesto.Reporte.Nombre : "";
            txtCodigo.Text = TipoPresupuesto.Codigo;
            txtNombre.Text = TipoPresupuesto.Nombre;
            txtDescripcion.Text = TipoPresupuesto.Descripcion;
            txtSerieCodigo.Text = TipoPresupuesto.CodigoSerie;
            txtSerieLongitud.Value = TipoPresupuesto.LongitudSerie;
            txtNumeracionActual.Value = TipoPresupuesto.NumeracionActual;
            txtNumeracionLongitud.Value = TipoPresupuesto.NumeracionLongitud;
            CheckActivo.Checked = TipoPresupuesto.Activo;
            CheckNumeracionAutomatica.Checked = TipoPresupuesto.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoPresupuesto.GeneraNumeracionAlFinal;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.Descripcion = txtDescripcion.Text;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            TipoPresupuesto.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
            TipoPresupuesto.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
            ssReporte.Text = (TipoPresupuesto.Reporte != null) ? TipoPresupuesto.Reporte.Nombre : "";
        }
    }
}
