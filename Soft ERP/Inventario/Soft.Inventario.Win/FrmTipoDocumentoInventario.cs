using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Inventario.Entidades;

namespace Soft.Inventario.Win
{
    public partial class FrmTipoDocumentoInventario : FrmParent
    {
        public FrmTipoDocumentoInventario()
        {
            InitializeComponent();
        }

        public TipoDocumentoInventario TipoDocumentoInventario { get { return (TipoDocumentoInventario)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = TipoDocumentoInventario.Codigo;
            txtNombre.Text = TipoDocumentoInventario.Nombre;
            txtDescripcion.Text = TipoDocumentoInventario.Descripcion;
            CheckUnicoAlmacen.Checked = TipoDocumentoInventario.UnicoAlmacen;

            comboOperacion.Text = TipoDocumentoInventario.Operacion;

            if (TipoDocumentoInventario.Almacen != null)
            {
                busAlmacen.Text = TipoDocumentoInventario.Almacen.Nombre;
            }
            CheckRequiereCliente.Checked = TipoDocumentoInventario.RequiereSocioNegocio;

            checkTieneImpuesto.Checked = TipoDocumentoInventario.TieneImpuesto;
            txtProcentajeImpuesto.Value = TipoDocumentoInventario.PorcentajeImpuesto;

            CheckAceptaCostoCero.Checked = TipoDocumentoInventario.AceptaCostoCero;

            txtSerieCodigo.Text = TipoDocumentoInventario.CodigoSerie;
            txtSerieLongitud.Value = TipoDocumentoInventario.LongitudSerie;

            txtNumeracionActual.Value = TipoDocumentoInventario.NumeracionActual;
            txtNumeracionLongitud.Value = TipoDocumentoInventario.NumeracionLongitud;

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.Codigo = txtCodigo.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.Activo = CheckActivo.Checked;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.Descripcion = txtDescripcion.Text;
        }

        private void busAlmacen_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            this.TipoDocumentoInventario.Almacen = (Almacen)FrmSeleccionarPanel.GetSelectedEntity(typeof(Almacen), "Almacen");
            if (this.TipoDocumentoInventario.Almacen != null)
            {
                busAlmacen.Text = this.TipoDocumentoInventario.Almacen.Nombre;
                busAlmacen.Enabled = true;
            }
        }

        private void comboOperacion_ValueChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.Operacion = comboOperacion.Text;
        }

        private void CheckUnicoAlmacen_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.UnicoAlmacen = CheckUnicoAlmacen.Checked;
        }

        private void CheckRequiereCliente_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.RequiereSocioNegocio = CheckRequiereCliente.Checked;
        }

        private void checkTieneImpuesto_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.TieneImpuesto = checkTieneImpuesto.Checked;
        }

 
        private void CheckAceptaCostoCero_CheckedChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.AceptaCostoCero = CheckAceptaCostoCero.Checked;
        }

        private void txtSerieCodigo_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.CodigoSerie = txtSerieCodigo.Text;
        }

        private void txtSerieLongitud_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.LongitudSerie = Convert.ToInt32(txtSerieLongitud.Text);
        }


        private void txtNumeracionActual_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.NumeracionActual = Convert.ToInt32(txtNumeracionActual.Text);
        }

        private void txtNumeracionLongitud_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.NumeracionLongitud = Convert.ToInt32(txtNumeracionLongitud.Text);
        }

        private void txtProcentajeImpuesto_TextChanged(object sender, EventArgs e)
        {
            TipoDocumentoInventario.PorcentajeImpuesto = Convert.ToInt32(txtProcentajeImpuesto.Text);
        }
    }
}
