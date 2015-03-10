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
            busDepartamento.Text = (itemSalidaCaja.Departamento == null) ? string.Empty : itemSalidaCaja.Departamento.Nombre;
            busProvincia.Text = (itemSalidaCaja.Provincia == null) ? string.Empty : itemSalidaCaja.Provincia.Nombre;
            busDistrito.Text = (itemSalidaCaja.DistritoOrigen == null) ? string.Empty : itemSalidaCaja.DistritoOrigen.Nombre;
            busDistritoDestino.Text = (itemSalidaCaja.DistritoDestino == null) ? string.Empty : itemSalidaCaja.DistritoDestino.Nombre;
            txtDireccion.Text = itemSalidaCaja.Direccion;
            uceTipoVehiculo.Text = itemSalidaCaja.TipoVehiculo;
        }

        public ItemSalidaCaja ObtenerItemSalidaCaja(ref ItemSalidaCaja item)
        {
            this.itemSalidaCaja = item;
            Mostrar(this.itemSalidaCaja);
            ShowDialog();
            string circunscripcion = "SERVICIO DE TRANSPORTE >> ";
            if (this.itemSalidaCaja.Departamento != null)
                circunscripcion += this.itemSalidaCaja.Departamento.Nombre + "/";
            if(this.itemSalidaCaja.Provincia != null)
                circunscripcion += this.itemSalidaCaja.Provincia.Nombre + "/";
            if (this.itemSalidaCaja.DistritoOrigen != null)
                circunscripcion += this.itemSalidaCaja.DistritoOrigen.Nombre + "-";
            if (this.itemSalidaCaja.DistritoDestino!= null)
                circunscripcion += this.itemSalidaCaja.DistritoDestino.Nombre + "-";
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
                itemSalidaCaja.Departamento = (Departamento)FrmSeleccionar.GetSelectedEntity(typeof(Departamento), "Departamento", Filtro);
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
                if (itemSalidaCaja.Departamento == null)
                {
                    throw new Exception("Debe de seleccionar un departamento...");
                }
                else 
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDDepartamento = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.Departamento.ID, busProvincia.Text);
                    itemSalidaCaja.Provincia = (Provincia)FrmSeleccionar.GetSelectedEntity(typeof(Provincia), "Provincia", Filtro);
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
                if (itemSalidaCaja.Provincia == null)
                {
                    throw new Exception("Debe de seleccionar una provincia...");
                }
                else
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDProvincia = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.Provincia.ID, busDistrito.Text);
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
                if (itemSalidaCaja.Provincia == null)
                {
                    throw new Exception("Debe de seleccionar una provincia...");
                }
                else
                {
                    FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                    String Filtro = String.Format(" IDProvincia = '{0}' AND Nombre LIKE '{1}%'", itemSalidaCaja.Provincia.ID, busDistritoDestino.Text);
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
    }
}
