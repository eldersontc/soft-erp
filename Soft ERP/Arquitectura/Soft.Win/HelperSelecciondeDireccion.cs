using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;
using Soft.DataAccess;

namespace Soft.Win
{
    public partial class HelperSelecciondeDireccion : Form
    {
        public Departamento departamento;
        public Provincia provincia;
        public Distrito distrito;
        public String direccion;
        public Boolean correcto;

        public HelperSelecciondeDireccion()
        {
            InitializeComponent();
            busProvincia.Enabled = false;
            busDistrito.Enabled = false;
            Mostrar();
        }



        



        public void Mostrar()
        {

            if (departamento != null)
            {
                busDepartamento.Enabled = true;
                busDepartamento.Text = departamento.Nombre;
            }
            else
            {
                busDepartamento.Text = "";
            }

            if (provincia != null)
            {
                busProvincia.Enabled = true;
                busProvincia.Text = provincia.Nombre;
            }
            else
            {
                busProvincia.Text = "";
            }


            if (distrito != null)
            {
                busDistrito.Enabled = true;
                busDistrito.Text = distrito.Nombre;
            }else{
                busDistrito.Text = "";
            
            }

            txtDireccion.Text = direccion;

        }

        private void busDepartamento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String filtro="";
            if (busDepartamento.Text.Length > 0) {
                filtro = "Nombre like '" + busDepartamento.Text + "%'";
            } 

            departamento = (Departamento)FrmSeleccionar.GetSelectedEntity(typeof(Departamento), "Departamento",filtro);
            if (departamento != null) {
                busDepartamento.Text = (departamento != null) ? departamento.Nombre : "";
                provincia = null;
                distrito = null;
                busProvincia.Enabled = true;
                busDistrito.Enabled = false;
                Mostrar();
            }
        }

        private void busProvincia_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();

            String filtro = "IDDepartamento='" + departamento.ID + "'" ;
            if (busProvincia.Text.Length > 0)
            {
                filtro += " and Nombre like '" + busProvincia.Text + "%'";
            }


            
            provincia = (Provincia)FrmSeleccionar.GetSelectedEntity(typeof(Provincia), "Provincia",filtro);
            if (provincia != null)
            {
                busProvincia.Text = (provincia != null) ? provincia.Nombre : "";
                distrito = null;
                busDistrito.Enabled = true;
                Mostrar();
            }
        }

        private void busDistrito_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();


            String filtro = "IDProvincia='" + provincia.ID + "'";
            if (busDistrito.Text.Length > 0)
            {
                filtro += " and Nombre like '" + busDistrito.Text + "%'";
            }

            distrito = (Distrito)FrmSeleccionar.GetSelectedEntity(typeof(Distrito), "Distrito",filtro);
            if (distrito != null)
            {
                busDistrito.Text = (distrito!= null) ? distrito.Nombre : "";
                Mostrar();
            }
        }

        private void txtDireccion_ValueChanged(object sender, EventArgs e)
        {
            direccion = txtDireccion.Text;
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            correcto = EsValido();
            if (correcto == true)
            {
                Close();
            }
            else {
                MessageBox.Show("Todos los campos son obligatorios");
            }
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {
            correcto = false;
            Close();
        }



        private Boolean EsValido()
        {

            if (departamento== null)
            {
                return false;
            }


            if (provincia == null)
            {
                return false;
            }


            if (distrito == null)
            {
                return false;
            }

            
            if (direccion.Length <=0) {
                return false;
            }

            return true;
            
        }
        
    }
}
