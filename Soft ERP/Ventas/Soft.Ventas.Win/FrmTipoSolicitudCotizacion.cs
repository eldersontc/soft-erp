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
using Soft.Reporte.Entidades;

namespace Soft.Ventas.Win
{
    public partial class FrmTipoSolicitudCotizacion : FrmParent 
    {
        public FrmTipoSolicitudCotizacion()
        {
            InitializeComponent();
        }

        public TipoSolicitudCotizacion TipoSolicitudCotizacion { get { return (TipoSolicitudCotizacion)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            ssReporte.Text = (TipoSolicitudCotizacion.Reporte != null) ? TipoSolicitudCotizacion.Reporte.Nombre : "";
            txtCodigo.Text = TipoSolicitudCotizacion.Codigo;
            txtNombre.Text = TipoSolicitudCotizacion.Nombre;
            txtDescripcion.Text = TipoSolicitudCotizacion.Descripcion;
            txtSerieCodigo.Text = TipoSolicitudCotizacion.CodigoSerie;
            txtSerieLongitud.Value = TipoSolicitudCotizacion.LongitudSerie;
            txtNumeracionActual.Value = TipoSolicitudCotizacion.NumeracionActual;
            txtNumeracionLongitud.Value = TipoSolicitudCotizacion.NumeracionLongitud;
            CheckActivo.Checked = TipoSolicitudCotizacion.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.Descripcion = txtDescripcion.Text;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
            TipoSolicitudCotizacion.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
            ssReporte.Text = (TipoSolicitudCotizacion.Reporte != null) ? TipoSolicitudCotizacion.Reporte.Nombre : "";
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
        }

        private void txtSerieCodigo_TextChanged_1(object sender, EventArgs e)
        {
            TipoSolicitudCotizacion.CodigoSerie = txtSerieCodigo.Text;
        }

    }
}
