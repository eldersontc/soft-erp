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
            Item=new ItemCotizacionServicio();
        }

        //public FrmCotizaciondeServicio(ItemCotizacionServicio m_Item)
        //{
        //    InitializeComponent();
        //    Item = m_Item;
        //    Mostrar();
        //}

        public FrmCotizaciondeServicio(Cotizacion m_Cotizacion,ItemCotizacionServicio m_Item, ItemCotizacion m_ItemElemento)
        {
            InitializeComponent();
            cotizacion = m_Cotizacion;
            lcm = cotizacion.ListaCostosMaquina;
            lpe = cotizacion.ListaPreciosExistencia;
            lpt = cotizacion.ListaPreciosTransporte;
            ItemElemento = m_ItemElemento;
            Item = m_Item;
            Mostrar();
        }



        private Cotizacion cotizacion = null;
        private ItemCotizacionServicio Item = null;
        private ItemCotizacion ItemElemento = null;

        private Boolean SWAcept = false;
        private ListaCostosMaquina lcm = null;
        private ListaPreciosExistencia lpe = null;
        private ListaPreciosTransporte lpt = null;


        public ItemCotizacionServicio ObtenerServicio(Cotizacion m_Cotizacion, ItemCotizacion m_ItemElemento)
        {
            cotizacion = m_Cotizacion;
            lcm = cotizacion.ListaCostosMaquina;
            lpe = cotizacion.ListaPreciosExistencia;
            lpt = cotizacion.ListaPreciosTransporte;
            ItemElemento = m_ItemElemento;
            ShowDialog();
            if (SWAcept) { return Item; }
            return null;
        }
        private Boolean ActualizandoIU = false;
        private void Mostrar() {
            ActualizandoIU = true;
            bussAcabado.Text = (Item.Servicio != null) ? Item.Servicio.Nombre : "";
            busUnidadAcabado.Text = (Item.UnidadServicio != null) ? Item.UnidadServicio.Nombre : "";
            busMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            busUnidadMaquina.Text = (Item.UnidadMaquina != null) ? Item.UnidadMaquina.Nombre : "";
            
            txtNombreMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
            busUnidadMaterial.Text = (Item.UnidadMaterial != null) ? Item.UnidadMaterial.Nombre : "";
            txtCantidadAcabado.Value = Item.CantidadServicio;
            txtCantidadMaquina.Value = Item.CantidadMaquina;
            txtCantidadMaterial.Value = Item.CantidadMaterial;
            txtCostoAcabado.Value = Item.CostoServicio;
            txtCostoMaquina.Value = Item.CostoMaquina;
            txtCostoMaterial.Value = Item.CostoMaterial;

            ActualizandoIU = false;
        }

        private void bussAcabado_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
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
            if (ActualizandoIU) { return; }
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
            if (ActualizandoIU) { return; }
            try
            {
                Item.CantidadServicio = Convert.ToDecimal(txtCantidadAcabado.Value);
                ObtenerCostoServicio();

            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void busMaquina_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
                {

                String filtro = "Nombre like '%"+busMaquina.Text+"%' and IDLista='"+lcm.ID+"'";

                FrmSelectedEntity formulario = new FrmSelectedEntity();
                Maquina maquina = (Maquina)formulario.GetSelectedEntity(typeof(Maquina), "MaquinaListaPrecio", filtro);
                if (maquina != null) { 
                Item.Maquina = (Maquina)HelperNHibernate.GetEntityByID("Maquina", maquina.ID);
                Mostrar();
                }

            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void txtCantidadMaquina_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                Item.CantidadMaquina = Convert.ToDecimal(txtCantidadMaquina.Value);
                ObtenerCostoMaquina();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtNombreMaterial_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
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
            if (ActualizandoIU) { return; }
            try
            {
                String filtro = "IDExistencia='"+Item.Material.ID+"'";



                FrmSelectedEntity formulario = new FrmSelectedEntity();
                Unidad unidad = (Unidad)formulario.GetSelectedEntity(typeof(Unidad), "ExistenciaUnidad", filtro);


                if (Item.UnidadMaterial != null) {
                    if (unidad != null) {
                        if (!Item.UnidadMaterial.Nombre.Equals(unidad.Nombre))
                        {

                            ExistenciaUnidad eu = null;
                            foreach (ExistenciaUnidad Itemunidad in Item.Material.Unidades)
                            {
                                if (Itemunidad.Unidad.Nombre.Equals(Item.UnidadMaterial.Nombre))
                                {
                                    eu = Itemunidad;
                                    break;
                                }
                            }

                            ExistenciaUnidad eu2 = null;
                            foreach (ExistenciaUnidad Itemunidad in Item.Material.Unidades)
                            {
                                if (Itemunidad.Unidad.Nombre.Equals(unidad.Nombre))
                                {
                                    eu2 = Itemunidad;
                                    break;
                                }
                            }

                            Item.UnidadMaterial = (Unidad)HelperNHibernate.GetEntityByID("Unidad", unidad.ID);

                            if (eu.FactorConversion > eu2.FactorConversion)
                            {
                                Item.CantidadMaterial = Item.CantidadMaterial / eu.FactorConversion;
                            }
                            else {
                                Item.CantidadMaterial = Item.CantidadMaterial * eu.FactorConversion;
                            }
                        }
                    }
                    Item.UnidadMaterial = (Unidad)HelperNHibernate.GetEntityByID("Unidad", unidad.ID);
                }
                Mostrar();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void txtCantidadMaterial_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                Item.CantidadMaterial = Convert.ToDecimal(txtCantidadMaterial.Value);
                ObtenerCostoMaterial();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }

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
            SWAcept = false;
            Close();
        }


        private void txtCostoAcabado_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            Item.CostoServicio = Convert.ToDecimal ( txtCostoAcabado.Value);
            SumarTotal();
        }

        private void txtCostoMaquina_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            Item.CostoMaquina = Convert.ToDecimal (txtCostoMaquina.Value);
            SumarTotal();
        }

        private void txtCostoMaterial_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            Item.CostoMaterial =Convert.ToDecimal ( txtCostoMaterial.Value);
            SumarTotal();
        }

        public void SumarTotal() {
            if (ActualizandoIU) { return; }
            Item.CostoTotalServicio = Item.CostoMaterial + Item.CostoMaquina + Item.CostoServicio;
            txtCostoTotal.Value  = Item.CostoTotalServicio ;
        }



        private void ObtenerCostoServicio() {
            try
            {
                foreach (ItemListaPreciosExistencia itemLPE  in lpe.Items)
                {
                    if (itemLPE.Existencia.Nombre.Equals(Item.Servicio.Nombre))
                    {
                        foreach (UnidadListaPreciosExistencia unidadLPE in itemLPE.Unidades)
                        {
                            if (unidadLPE.Unidad.Nombre.Equals(Item.UnidadServicio.Nombre))
                            {

                                
                                foreach (EscalaListaPreciosExistencia escala in unidadLPE.Escalas)
                                {
                                    if ((escala.Desde == 0) && (escala.Hasta == 0))
                                        {
                                            Item.CostoServicio = Item.CantidadServicio * escala.Costo;
                                            break;
                                        }
                                    else if ((escala.Desde <= Item.CantidadServicio) && (escala.Hasta >= Item.CantidadServicio))
                                        {
                                            Item.CostoServicio = Item.CantidadServicio * escala.Costo;
                                            break;
                                        }
                                     else if ((escala.Hasta == 0))
                                        {
                                            Item.CostoServicio = Item.CantidadServicio * escala.Costo;
                                            break;
                                        }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }


            }
            catch (Exception)
            {
            }
            Mostrar();
        }

        private void ObtenerCostoMaterial() {
            if (ActualizandoIU) { return; }
            try
            {
                ExistenciaUnidad eu = null;
                foreach (ExistenciaUnidad Itemunidad in Item.Material.Unidades)
                {
                    if (Itemunidad.Unidad.Nombre.Equals(Item.UnidadMaterial.Nombre))
                    {
                        eu = Itemunidad;
                        break;
                    }
                }

                Existencia existenciaActualizada = (Existencia) HelperNHibernate.GetEntityByID("Existencia", Item.Material.ID);

                Item.CostoMaterial = (Item.CantidadMaterial * existenciaActualizada.CostoUltimaCompra) / eu.FactorConversion;
            }
            catch (Exception)
            {
                
            }
            Mostrar();
        }


        private void ObtenerCostoMaquina()
        {
            try
            {
                foreach (ItemListaCostosMaquina itemLCM in lcm.Items)
                {
                    if (itemLCM.Maquina.Nombre.Equals(Item.Maquina.Nombre))
                    {
                        foreach (UnidadListaCostosMaquina unidadLPM in itemLCM.Unidades)
                        {
                            if (unidadLPM.Unidad.Nombre.Equals(Item.UnidadMaquina.Nombre))
                            {
                                foreach (EscalaListaCostosMaquina escala in unidadLPM.Escalas)
                                {
                                    if ((escala.Desde == 0) && (escala.Hasta == 0))
                                    {
                                        Item.CostoMaquina = Item.CantidadMaquina * escala.Costo;
                                        break;
                                    }
                                    else if ((escala.Desde <= Item.CantidadMaquina) && (escala.Hasta >= Item.CantidadMaquina))
                                    {
                                        Item.CostoMaquina = Item.CantidadMaquina * escala.Costo;
                                        break;
                                    }
                                    else if ((escala.Hasta == 0))
                                    {
                                        Item.CostoMaquina = Item.CantidadMaquina * escala.Costo;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }


            }
            catch (Exception)
            {
            }
            Mostrar();
        }

        private void busUnidadMaquina_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                String filtro = "Nombre like '%" + busUnidadMaquina.Text + "%' and IDMaquina='" + Item.Maquina.ID + "' and IDLista='"+lcm.ID+"'";


                FrmSelectedEntity formulario = new FrmSelectedEntity();
                Unidad unidad = (Unidad)formulario.GetSelectedEntity(typeof(Unidad), "UnidadListaCostoMaquina", filtro);
                if (unidad != null) { 
                Item.UnidadMaquina = (Unidad)HelperNHibernate.GetEntityByID("Unidad", unidad.ID);
                Mostrar();
                }

            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void btnObtenerCostoElemento_Click(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                Decimal largo = ItemElemento.MedidaAbiertaLargo;
                Decimal alto = ItemElemento.MedidaAbiertaAlto;

                if (ItemElemento.UnidadMedidaAbierta.Equals("CM."))
                {
                    largo = largo / 100;
                    alto = alto / 100;
                }

                txtCantidadAcabado.Value = (ItemElemento.CantidadElemento * alto * largo);

                SumarTotal();
            }
            catch (Exception ex)
            {
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void btnObtenerCantidadMaterial_Click(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            if (busUnidadMaterial.Text.Equals("METRO"))
            {
                FrmCalculoMetros form = new FrmCalculoMetros(Item, ItemElemento);
                Item=form.m_item;
                ObtenerCostoMaterial();
            }
            else if (busUnidadMaterial.Text.Equals("METRO CUADRADO"))
            {
                Decimal largo = ItemElemento.MedidaAbiertaLargo;
                Decimal alto = ItemElemento.MedidaAbiertaAlto;

                if (ItemElemento.UnidadMedidaAbierta.Equals("CM."))
                {
                    largo = largo / 100;
                    alto = alto / 100;
                }
                Item.CantidadMaterial = largo * alto * ItemElemento.CantidadElemento;
            }
            Mostrar();
        }



    }
}
