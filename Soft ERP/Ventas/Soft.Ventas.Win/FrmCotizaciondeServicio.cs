using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Ventas.Entidades;
using Soft.Inventario.Entidades;
using Soft.DataAccess;

namespace Soft.Ventas.Win
{
    public partial class FrmCotizaciondeServicio : FrmParent 
    {
        public FrmCotizaciondeServicio()
        {
            InitializeComponent();
        }

        private Cotizacion cotizacion = null;
        private ItemCotizacionServicio Item = new ItemCotizacionServicio();
        private Boolean SWAcept = false;

        public ItemCotizacionServicio ObtenerServicio(Cotizacion m_Cotizacion)
        {
            cotizacion = m_Cotizacion;
            ShowDialog();
            if (SWAcept) { return Item; }
            return null;
        }

        private void Mostrar() {

            bussAcabado.Text = (Item.Servicio != null) ? Item.Servicio.Nombre : "";
            busUnidadAcabado.Text = (Item.UnidadServicio != null) ? Item.UnidadServicio.Nombre : "";
            busMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            txtNombreMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
            busUnidadMaterial.Text = (Item.UnidadMaterial != null) ? Item.UnidadMaterial.Nombre : "";
            txtCantidadAcabado.Value = Item.CantidadServicio;
            txtCantidadMaquina.Value = Item.CantidadMaquina;
            txtCantidadMaterial.Value = Item.CantidadMaterial;
        }

        private void bussAcabado_Search(object sender, EventArgs e)
        {
              try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                string filtro = "IDListaPreciosExistencia='"+cotizacion.ListaPreciosExistencia.ID+"'";
                Existencia servicio = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "ItemListaPreciosExistencia",filtro);
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", servicio.ID);
                Mostrar();
            }
            catch (Exception ex)
            {

                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void busUnidadAcabado_Search(object sender, EventArgs e)
        {
            try
            {
                foreach (var unidad in Item.Servicio.Unidades)
                {
                    ExistenciaUnidad eu = (ExistenciaUnidad)unidad;
                    Item.UnidadServicio = eu.Unidad;
                }
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtCantidadAcabado_ValueChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void busMaquina_Search(object sender, EventArgs e)
        {
            try
                {

                String filtro = "Nombre like '%"+busMaquina.Text+"%'";

                FrmSelectedEntity formulario = new FrmSelectedEntity();
                Maquina maquina = (Maquina)formulario.GetSelectedEntity(typeof(Maquina), "Máquina", filtro);
                Item.Maquina = (Maquina)HelperNHibernate.GetEntityByID("Maquina", maquina.ID);
                Mostrar();

            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void busTipoCosteo_Search(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtCantidadMaquina_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Item.CantidadMaquina = Convert.ToDecimal(txtCantidadMaquina.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtNombreMaterial_Search(object sender, EventArgs e)
        {
            try
            {

                String filtro = "Nombre like '%" +txtNombreMaterial.Text + "%'";

                FrmSelectedEntity formulario = new FrmSelectedEntity();
                Existencia existencia = (Existencia)formulario.GetSelectedEntity(typeof(Existencia), "Existencia", filtro);
                Item.Material = (Existencia)HelperNHibernate.GetEntityByID("Existencia", existencia.ID);
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void busUnidadMaterial_Search(object sender, EventArgs e)
        {
            try
            {
                String filtro = "Nombre like '%" + busUnidadMaterial.Text + "%' and IDExistencia='"+Item.Material.ID+"'";



                FrmSelectedEntity formulario = new FrmSelectedEntity();
                Unidad unidad = (Unidad)formulario.GetSelectedEntity(typeof(Unidad), "ExistenciaUnidad", filtro);
                Item.UnidadMaterial = (Unidad)HelperNHibernate.GetEntityByID("Unidad", unidad.ID);
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtCantidadMaterial_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Item.CantidadMaterial = Convert.ToDecimal(txtCantidadMaterial.Value);
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }



    }
}
