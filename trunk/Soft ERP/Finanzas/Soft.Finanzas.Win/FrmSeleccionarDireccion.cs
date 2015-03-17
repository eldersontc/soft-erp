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
using Soft.Exceptions;
using Soft.Finanzas.Entidades;

namespace Soft.Finanzas.Win
{
    public partial class FrmSeleccionarDireccion : FrmParent
    {
        public FrmSeleccionarDireccion()
        {
            InitializeComponent();
        }

        private ItemSalidaCaja itemSalidaCaja;

        public void Mostrar(ItemSalidaCaja itemSalidaCaja) 
        {
            busDepartamento.Text = (itemSalidaCaja.DepartamentoOrigen == null) ? string.Empty : itemSalidaCaja.DepartamentoOrigen.Nombre;
            busProvincia.Text = (itemSalidaCaja.ProvinciaOrigen == null) ? string.Empty : itemSalidaCaja.ProvinciaOrigen.Nombre;
            busDistrito.Text = (itemSalidaCaja.DistritoOrigen == null) ? string.Empty : itemSalidaCaja.DistritoOrigen.Nombre;
            busDepartamentoDestino.Text = (itemSalidaCaja.DepartamentoDestino == null) ? string.Empty : itemSalidaCaja.DepartamentoDestino.Nombre;
            busProvinciaDestino.Text = (itemSalidaCaja.ProvinciaDestino== null) ? string.Empty : itemSalidaCaja.ProvinciaDestino.Nombre;
            busDistritoDestino.Text = (itemSalidaCaja.DistritoDestino == null) ? string.Empty : itemSalidaCaja.DistritoDestino.Nombre;
            txtDireccion.Text = itemSalidaCaja.Direccion;
            uceTipoVehiculo.Text = itemSalidaCaja.TipoVehiculo;
        }

        public ItemSalidaCaja ObtenerItemSalidaCaja(ref ItemSalidaCaja item)
        {
            this.itemSalidaCaja = item;
            Mostrar(this.itemSalidaCaja);
            ShowDialog();
            string circunscripcion = "SERVICIO DE TRANSPORTE >> DE ";
            if (this.itemSalidaCaja.DepartamentoOrigen != null)
                circunscripcion += this.itemSalidaCaja.DepartamentoOrigen.Nombre + "/";
            if(this.itemSalidaCaja.ProvinciaOrigen != null)
                circunscripcion += this.itemSalidaCaja.ProvinciaOrigen.Nombre + "/";
            if (this.itemSalidaCaja.DistritoOrigen != null)
                circunscripcion += this.itemSalidaCaja.DistritoOrigen.Nombre + " A ";
            if (this.itemSalidaCaja.DepartamentoDestino != null)
                circunscripcion += this.itemSalidaCaja.DepartamentoDestino.Nombre + "/";
            if (this.itemSalidaCaja.ProvinciaDestino != null)
                circunscripcion += this.itemSalidaCaja.ProvinciaDestino.Nombre + "/";
            if (this.itemSalidaCaja.DistritoDestino!= null)
                circunscripcion += this.itemSalidaCaja.DistritoDestino.Nombre + " - ";
            circunscripcion += this.itemSalidaCaja.Direccion;
            circunscripcion += " TIPO VEHÍCULO : " + this.itemSalidaCaja.TipoVehiculo;
            this.itemSalidaCaja.Descripcion = circunscripcion;
            return this.itemSalidaCaja;
        }

        private void busDepartamento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                String Filtro = String.Format(" Nombre LIKE '{0}%'", busDepartamento.Text);
                itemSalidaCaja.DepartamentoOrigen = (Departamento)FrmSeleccionar.GetSelectedEntity(typeof(Departamento), "Departamento", Filtro);
                Mostrar(this.itemSalidaCaja);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busProvincia_Search(object sender, EventArgs e)
        {
            try 
	        {
                if (itemSalidaCaja.DepartamentoOrigen == null)
                {
                    throw new Exception("Debe de seleccionar un departamento...");
                }
                else 
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDDepartamento = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.DepartamentoOrigen.ID, busProvincia.Text);
                    itemSalidaCaja.ProvinciaOrigen = (Provincia)FrmSeleccionar.GetSelectedEntity(typeof(Provincia), "Provincia", Filtro);
                    Mostrar(this.itemSalidaCaja);
                }
	        }
	        catch (Exception ex)
	        {
                SoftException.Control(ex);
	        }
        }

        private void busDistrito_Search(object sender, EventArgs e)
        {
            try
            {
                if (itemSalidaCaja.ProvinciaOrigen == null)
                {
                    throw new Exception("Debe de seleccionar una provincia...");
                }
                else
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDProvincia = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.ProvinciaOrigen.ID, busDistrito.Text);
                    itemSalidaCaja.DistritoOrigen = (Distrito)FrmSeleccionar.GetSelectedEntity(typeof(Distrito), "Distrito", Filtro);
                    Mostrar(this.itemSalidaCaja);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtDireccion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                itemSalidaCaja.Direccion = txtDireccion.Text;
                Mostrar(this.itemSalidaCaja);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busDistritoDestino_Search(object sender, EventArgs e)
        {
            try
            {
                if (itemSalidaCaja.ProvinciaDestino == null)
                {
                    throw new Exception("Debe de seleccionar una provincia...");
                }
                else
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDProvincia = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.ProvinciaDestino.ID, busDistritoDestino.Text);
                    itemSalidaCaja.DistritoDestino = (Distrito)FrmSeleccionar.GetSelectedEntity(typeof(Distrito), "Distrito", Filtro);
                    Mostrar(this.itemSalidaCaja);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        public override void Aceptar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

        public override void Cancelar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            Close();
        }

        private void uceTipoVehiculo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                itemSalidaCaja.TipoVehiculo = uceTipoVehiculo.Text;
                Mostrar(this.itemSalidaCaja);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busDepartamentoDestino_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                String Filtro = String.Format(" Nombre LIKE '{0}%'", busDepartamentoDestino.Text);
                itemSalidaCaja.DepartamentoDestino = (Departamento)FrmSeleccionar.GetSelectedEntity(typeof(Departamento), "Departamento", Filtro);
                Mostrar(this.itemSalidaCaja);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busProvinciaDestino_Search(object sender, EventArgs e)
        {
            try
            {
                if (itemSalidaCaja.DepartamentoDestino == null)
                {
                    throw new Exception("Debe de seleccionar un departamento...");
                }
                else
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDDepartamento = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.DepartamentoDestino.ID, busProvinciaDestino.Text);
                    itemSalidaCaja.ProvinciaDestino = (Provincia)FrmSeleccionar.GetSelectedEntity(typeof(Provincia), "Provincia", Filtro);
                    Mostrar(this.itemSalidaCaja);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubCopiarDepartamento_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemSalidaCaja.DepartamentoOrigen != null) 
                {
                    itemSalidaCaja.DepartamentoDestino = itemSalidaCaja.DepartamentoOrigen;
                    Mostrar(this.itemSalidaCaja);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubCopiarProvincia_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemSalidaCaja.ProvinciaOrigen != null)
                {
                    itemSalidaCaja.ProvinciaDestino = itemSalidaCaja.ProvinciaOrigen;
                    Mostrar(this.itemSalidaCaja);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubCopiarDistrito_Click(object sender, EventArgs e)
        {
            try
            {
                if (itemSalidaCaja.DistritoOrigen != null)
                {
                    itemSalidaCaja.DistritoDestino = itemSalidaCaja.DistritoOrigen;
                    Mostrar(this.itemSalidaCaja);
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }
    }
}
