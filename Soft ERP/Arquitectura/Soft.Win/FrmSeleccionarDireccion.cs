using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Entities;

namespace Soft.Win
{
    public partial class FrmSeleccionarDireccion : FrmParent 
    {
        public FrmSeleccionarDireccion()
        {
            InitializeComponent();
        }

        private ItemSocioNegocioDireccion Item = new ItemSocioNegocioDireccion();
        private Boolean SWAcept = false;

        public ItemSocioNegocioDireccion ObtenerDireccion() {
            ShowDialog();
            if (SWAcept) { return Item; }
            return null;
        }

        public ItemSocioNegocioDireccion ModificarDireccion(ItemSocioNegocioDireccion ItemDireccion) {
            Item = ItemDireccion;
            Mostrar();
            ShowDialog();
            if (SWAcept) { return Item; }
            else { return ItemDireccion; }
        }

        public void Mostrar() {
            busDepartamento.Text = (Item.Departamento != null) ? Item.Departamento.Nombre : "";
            busProvincia.Text = (Item.Provincia != null) ? Item.Provincia.Nombre : "";
            busDistrito.Text = (Item.Distrito != null) ? Item.Distrito.Nombre : "";
            txtDireccion.Text = Item.Direccion ;
        }

        private void busDepartamento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String Filtro = String.Format(" Nombre LIKE '{0}%'",busDepartamento.Text);
            Item.Departamento  = (Departamento)FrmSeleccionar.GetSelectedEntity(typeof(Departamento), "Departamento", Filtro);
            if (Item.Departamento != null)
            {
                busDepartamento.Text = (Item.Departamento != null) ? Item.Departamento.Nombre : "";
            }
        }

        private void busProvincia_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String Filtro = String.Format (" IDDepartamento = '{0}' AND Nombre LIKE '{1}%'",Item.Departamento.ID,busProvincia.Text);
            Item.Provincia = (Provincia)FrmSeleccionar.GetSelectedEntity(typeof(Provincia), "Provincia", Filtro);
            if (Item.Provincia != null)
            {
                busProvincia.Text = (Item.Provincia != null) ? Item.Provincia.Nombre : "";
            }
        }

        private void busDistrito_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            String Filtro = String.Format(" IDProvincia = '{0}' AND Nombre LIKE '{1}%'", Item.Provincia.ID,busDistrito.Text);
            Item.Distrito = (Distrito)FrmSeleccionar.GetSelectedEntity(typeof(Distrito), "Distrito", Filtro);
            if (Item.Distrito != null)
            {
                busDistrito.Text = (Item.Distrito != null) ? Item.Distrito.Nombre : "";
            }
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            Item.Direccion = txtDireccion.Text;
        }

        public override void Aceptar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            SWAcept = true;
            Close();
        }

        public override void Cancelar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

    }
}
