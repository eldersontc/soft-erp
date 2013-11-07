using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;

namespace Soft.Win
{
    public partial class FrmSocioNegocio : FrmParent
    {
        public FrmSocioNegocio()
        {
            InitializeComponent();
        }

        public SocioNegocio SocioNegocio { get { return (SocioNegocio)base.m_ObjectFlow; } }

        public override void Init()
        {
            Mostrar();
        }

        public void Mostrar()
        {
            if (SocioNegocio.TipoSocioNegocio != null) { ssTipoSocio.Text = SocioNegocio.TipoSocioNegocio.Descripcion; }
            txtCodigo.Text = SocioNegocio.Codigo;
            txtNombre.Text = SocioNegocio.Nombre;
            txtDescripcion.Text = SocioNegocio.Descripcion;
            udtAniversario.Value = SocioNegocio.Aniversario;
            uceActivo.Checked = SocioNegocio.Activo;
            uceCliente.Checked = SocioNegocio.Cliente;
            uceProveedor.Checked = SocioNegocio.Proveedor;
            uceEmpleado.Checked = SocioNegocio.Empleado;
            utSocioNegocio.Tabs["Cliente"].Enabled = SocioNegocio.Cliente;
            utSocioNegocio.Tabs["Proveedor"].Enabled = SocioNegocio.Proveedor;
            utSocioNegocio.Tabs["Empleado"].Enabled = SocioNegocio.Empleado;
        }

        private void ssTipoSocio_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SocioNegocio.TipoSocioNegocio = (TipoSocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(TipoSocioNegocio), "TipoSocioNegocio");
            Mostrar();
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocio.Activo = uceActivo.Checked;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            SocioNegocio.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            SocioNegocio.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            SocioNegocio.Descripcion = txtDescripcion.Text;
        }

        private void udtAniversario_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.Aniversario = (DateTime)udtAniversario.Value;
        }

        private void uceCliente_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocio.Cliente = uceCliente.Checked;
            Mostrar();
        }

        private void uceProveedor_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocio.Proveedor = uceProveedor.Checked;
            Mostrar();
        }

        private void uceEmpleado_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocio.Empleado = uceEmpleado.Checked;
            Mostrar();
        }
    }
}
