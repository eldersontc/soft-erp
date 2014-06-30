using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Produccion.Entidades;

namespace Soft.Produccion.Win
{
    public partial class FrmTipoDocumentoConsolidadoOp : FrmParent 
    {
        public FrmTipoDocumentoConsolidadoOp()
        {
            InitializeComponent();
        }

        public TipoDocumentoConsolidadoOp TipoDocumentoConsolidadoOp { get { return (TipoDocumentoConsolidadoOp)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoDocumentoConsolidadoOp.Reporte != null) ? TipoDocumentoConsolidadoOp.Reporte.Nombre : "";
            txtCodigo.Text = TipoDocumentoConsolidadoOp.Codigo;
            txtNombre.Text = TipoDocumentoConsolidadoOp.Nombre;
            txtDescripcion.Text = TipoDocumentoConsolidadoOp.Descripcion;
            txtSerieCodigo.Text = TipoDocumentoConsolidadoOp.CodigoSerie;
            txtSerieLongitud.Value = TipoDocumentoConsolidadoOp.LongitudSerie;
            txtNumeracionActual.Value = TipoDocumentoConsolidadoOp.NumeracionActual;
            txtNumeracionLongitud.Value = TipoDocumentoConsolidadoOp.NumeracionLongitud;
            CheckActivo.Checked = TipoDocumentoConsolidadoOp.Activo;
            CheckNumeracionAutomatica.Checked = TipoDocumentoConsolidadoOp.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoDocumentoConsolidadoOp.GeneraNumeracionAlFinal;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.Descripcion = txtDescripcion.Text;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoConsolidadoOp.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
            TipoDocumentoConsolidadoOp.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
            ssReporte.Text = (TipoDocumentoConsolidadoOp.Reporte != null) ? TipoDocumentoConsolidadoOp.Reporte.Nombre : "";
        }
    }
}
