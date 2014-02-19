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
    public partial class FrmTipoOrdenProduccion : FrmParent
    {
        public FrmTipoOrdenProduccion()
        {
            InitializeComponent();
        }


        public TipoOrdenProduccion TipoOrdenProduccion { get { return (TipoOrdenProduccion)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        private Boolean ActualizandoIU = false;

        public void Mostrar()
        {
            ActualizandoIU = true;
            ssReporte.Text = (TipoOrdenProduccion.Reporte != null) ? TipoOrdenProduccion.Reporte.Nombre : "";
            txtCodigo.Text = TipoOrdenProduccion.Codigo;
            txtNombre.Text = TipoOrdenProduccion.Nombre;
            txtDescripcion.Text = TipoOrdenProduccion.Descripcion;
            txtSerieCodigo.Text = TipoOrdenProduccion.CodigoSerie;
            txtSerieLongitud.Value = TipoOrdenProduccion.LongitudSerie;
            txtNumeracionActual.Value = TipoOrdenProduccion.NumeracionActual;
            txtNumeracionLongitud.Value = TipoOrdenProduccion.NumeracionLongitud;
            CheckActivo.Checked = TipoOrdenProduccion.Activo;
            CheckNumeracionAutomatica.Checked = TipoOrdenProduccion.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoOrdenProduccion.GeneraNumeracionAlFinal;
            ActualizandoIU = false;
        }

        private void txtCodigo_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) {return;}
            try
            {
                TipoOrdenProduccion.Codigo = txtCodigo.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.Activo= CheckActivo.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.Nombre = txtNombre.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtDescripcion_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.Descripcion = txtDescripcion.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtSerieCodigo_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.CodigoSerie = txtSerieCodigo.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
               TipoOrdenProduccion.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.NumeracionAutomatica= CheckNumeracionAutomatica.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
                TipoOrdenProduccion.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
          
        }

        private void ssReporte_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                TipoOrdenProduccion.Reporte = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }


    }
}
