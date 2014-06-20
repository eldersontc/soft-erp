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
    public partial class FrmLineaProduccion : FrmParent
    {
        public FrmLineaProduccion()
        {
            InitializeComponent();
        }

        public LineaProduccion LineaProduccion { get { return (LineaProduccion)base.m_ObjectFlow; } }


        public override void Init()
        {
            base.Init();
            Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = LineaProduccion.Codigo;
            txtNombre.Text = LineaProduccion.Nombre;
            txtDescripcion.Text = LineaProduccion.Descripcion;
            CheckActivo.Checked = LineaProduccion.Activo;
        }

        private void txtCodigo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                LineaProduccion.Codigo = txtCodigo.Text;
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
                LineaProduccion.Activo = CheckActivo.Checked;
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
                LineaProduccion.Nombre = txtNombre.Text;
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
                LineaProduccion.Descripcion = txtDescripcion.Text;
            }
            catch (Exception ex)
            {

                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }
    }
}
