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
    public partial class FrmTipoEntrega : FrmParent
    {
        public FrmTipoEntrega()
        {
            InitializeComponent();
        }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }
        public TipoEntrega TipoEntrega { get { return (TipoEntrega)base.m_ObjectFlow; } }


        public void Mostrar()
        {
            ssReporte.Text = (TipoEntrega.Reporte != null) ? TipoEntrega.Reporte.Nombre : "";
            txtCodigo.Text = TipoEntrega.Codigo;
            txtNombre.Text = TipoEntrega.Nombre;
            txtDescripcion.Text = TipoEntrega.Descripcion;
            txtSerieCodigo.Text = TipoEntrega.CodigoSerie;
            txtSerieLongitud.Value = TipoEntrega.LongitudSerie;
            txtNumeracionActual.Value = TipoEntrega.NumeracionActual;
            txtNumeracionLongitud.Value = TipoEntrega.NumeracionLongitud;
            CheckActivo.Checked = TipoEntrega.Activo;
            CheckNumeracionAutomatica.Checked = TipoEntrega.NumeracionAutomatica;
            CheckGeneraNumeracionalFinal.Checked = TipoEntrega.GeneraNumeracionAlFinal;
        }


        private void txtCodigo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.Codigo = txtCodigo.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.Nombre = txtNombre.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.Activo = CheckActivo.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtDescripcion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.Descripcion = txtDescripcion.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

      
        private void txtSerieCodigo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.CodigoSerie = txtSerieCodigo.Text;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtSerieLongitud_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtNumeracionActual_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtNumeracionLongitud_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void CheckNumeracionAutomatica_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.NumeracionAutomatica = CheckNumeracionAutomatica.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void CheckGeneraNumeracionalFinal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.GeneraNumeracionAlFinal = CheckGeneraNumeracionalFinal.Checked;
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void ssReporte_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarReporte = new FrmSelectedEntity();
                TipoEntrega.Reporte = (Soft.Reporte.Entidades.Reporte)FrmSeleccionarReporte.GetSelectedEntity(typeof(Soft.Reporte.Entidades.Reporte), "Reporte");
                ssReporte.Text = (TipoEntrega.Reporte != null) ? TipoEntrega.Reporte.Nombre : "";
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            } 
        }

        private void ssReporte_Clear(object sender, EventArgs e)
        {
            try
            {
                TipoEntrega.Reporte = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }
    }
}
