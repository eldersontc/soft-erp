using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Soft.Win;
using System.Windows.Forms;
using Soft.Inventario.Entidades;

namespace Soft.Inventario.Win
{
    public partial class FrmAlmacen : FrmParent
    {
        public FrmAlmacen()
        {
            InitializeComponent();
        }


        public Almacen Almacen { get { return (Almacen)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar()
        {
            txtCodigo.Text = Almacen.Codigo ;
            txtNombre.Text = Almacen.Nombre;
            txtDescripcion.Text = Almacen.Descripcion;
            CheckActivo.Checked = Almacen.Activo;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Almacen.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Almacen.Nombre = txtNombre.Text;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            Almacen.Descripcion = txtDescripcion.Text;
        }

        private void CheckActivo_CheckedChanged(object sender, EventArgs e)
        {
            Almacen.Activo = CheckActivo.Checked;
        }



    }
}
