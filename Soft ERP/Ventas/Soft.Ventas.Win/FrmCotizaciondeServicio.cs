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
            try
            {
                ActualizandoIU = true;
                bussAcabado.Text = (Item.Servicio != null) ? Item.Servicio.Nombre : "";
                busUnidadAcabado.Text = (Item.UnidadServicio != null) ? Item.UnidadServicio.Unidad.Nombre : "";
                busMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
                busUnidadMaquina.Text = (Item.UnidadMaquina != null) ? Item.UnidadMaquina.Nombre : "";

                txtNombreMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
                if (Item.UnidadMaterial != null)
                {
                    if (Item.UnidadMaterial.Unidad != null)
                    {
                        busUnidadMaterial.Text = (Item.UnidadMaterial != null) ? Item.UnidadMaterial.Unidad.Nombre : "";
                    }

                }
                else { }

                txtCantidadAcabado.Value = Item.CantidadServicio;
                txtCantidadMaquina.Value = Item.CantidadMaquina;
                txtCantidadMaterial.Value = Item.CantidadMaterial;
                txtCostoAcabado.Value = Item.CostoServicio;
                txtCostoMaquina.Value = Item.CostoMaquina;
                txtCostoMaterial.Value = Item.CostoMaterial;
                ActualizandoIU = false;
            }
            catch (Exception ex)
            {
                ActualizandoIU = false;
                Soft.Exceptions.SoftException.ShowException(ex);
            }

           
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

                if (Item.Servicio.Codigo.Equals("FILMACION") || Item.Servicio.Codigo.Equals("FILMACION2"))
                {
                
                
              

                if (Item.Servicio.Unidades!=null) {
                    Item.UnidadServicio = Item.Servicio.UnidadBase;
                    if (ItemElemento.MetodoImpresion == null) {
                        Item.CantidadServicio = 0;
                    }
                    if (ItemElemento.MetodoImpresion.Equals("TIRA/RETIRA"))
                    {
                        Item.CantidadServicio = (ItemElemento.ImpresoTiraColor + ItemElemento.ImpresoRetiraColor) * ItemElemento.NumeroPliegos;
                    }
                    else
                    {
                        Item.CantidadServicio = ItemElemento.ImpresoTiraColor * ItemElemento.NumeroPliegos;
                    }

                    ObtenerCostoServicio();
                }

                }
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
                           String filtro = "IDExistencia='" + Item.Servicio.ID + "' and EstaListaPreciosExistencia='SI'";
                           FrmSelectedEntity formulario = new FrmSelectedEntity();
                           ExistenciaUnidad unidad = (ExistenciaUnidad)formulario.GetSelectedEntity(typeof(ExistenciaUnidad), "ExistenciaUnidad", filtro);

                           if (unidad != null && Item.UnidadServicio != null)
                           {
                               unidad = (ExistenciaUnidad)HelperNHibernate.GetEntityByID("ExistenciaUnidad", unidad.ID);
                               if (unidad.ID != Item.UnidadServicio.ID)
                               {
                                   Decimal cantidadanteriorunidadbase = Item.CantidadServicio * Item.UnidadServicio.FactorConversion;
                                   Decimal cantidadactual = Math.Truncate(cantidadanteriorunidadbase / unidad.FactorConversion);
                                   Item.UnidadServicio = unidad;
                                   Item.CantidadServicio = cantidadactual;
                                   ObtenerCostoServicio();
                               }
                           }
                           else
                           {
                               unidad = (ExistenciaUnidad)HelperNHibernate.GetEntityByID("ExistenciaUnidad", unidad.ID);
                               Item.UnidadServicio = unidad;
                               Item.CantidadServicio = 0;
                               ObtenerCostoServicio();
                           }
                       }


                       catch (Exception ex)
                       {
                           Soft.Exceptions.SoftException.ShowException(ex);
                       }

                       Mostrar();
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
                ExistenciaUnidad unidad = (ExistenciaUnidad)formulario.GetSelectedEntity(typeof(ExistenciaUnidad), "ExistenciaUnidad", filtro);

                if (unidad != null )
                {
                    unidad = (ExistenciaUnidad)HelperNHibernate.GetEntityByID("ExistenciaUnidad", unidad.ID);
         
                      Item.UnidadMaterial = unidad;
                        Item.CantidadMaterial =0;
                        ObtenerCostoMaterial();
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
            if (ActualizandoIU) { return; }
            try
            {
                foreach (ItemListaPreciosExistencia itemLPE  in lpe.Items)
                {
                    if (itemLPE.Existencia.Nombre.Equals(Item.Servicio.Nombre))
                    {
                        foreach (UnidadListaPreciosExistencia unidadLPE in itemLPE.Unidades)
                        {
                            if (unidadLPE.Unidad.Unidad.Nombre.Equals(Item.UnidadServicio.Unidad.Nombre))
                            {

                                
                                foreach (EscalaListaPreciosExistencia escala in unidadLPE.Escalas)
                                {
                                    Decimal cantidadBruta = Item.CantidadServicio;
                                    Decimal cantidadNeta = Item.CantidadServicio / escala.PorCada;
                                    Decimal cantidadRedondeada = Math.Truncate(cantidadNeta);
                                    Item.CostoServicio = 0;
                                    if ((cantidadNeta - cantidadRedondeada) > 0) {
                                        cantidadRedondeada += 1;
                                    }

                                    if ((escala.Desde == 0) && (escala.Hasta == 0))
                                        {
                                            Item.CostoServicio = cantidadRedondeada * escala.Costo;
                                            break;
                                        }
                                    else if ((escala.Desde <= Item.CantidadServicio) && (escala.Hasta >= Item.CantidadServicio))
                                        {
                                            Item.CostoServicio = cantidadRedondeada * escala.Costo;
                                            break;
                                        }
                                     else if ((escala.Hasta == 0))
                                        {
                                            Item.CostoServicio = cantidadRedondeada * escala.Costo;
                                            break;
                                        }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }

                SumarTotal();


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
 
                Existencia existenciaActualizada = (Existencia) HelperNHibernate.GetEntityByID("Existencia", Item.Material.ID);

                Decimal costo = 0;

                if (existenciaActualizada.CostoUltimaCompra > 0) {
                    costo = existenciaActualizada.CostoUltimaCompra;
                }else{
                    costo = existenciaActualizada.CostoReferencia;
                }

                Item.CostoMaterial = (Item.CantidadMaterial * costo) * Item.UnidadMaterial.FactorConversion;
                SumarTotal();
            }
            catch (Exception)
            {
                
            }
            Mostrar();
        }


        private void ObtenerCostoMaquina()
        {
            if (ActualizandoIU) { return; }
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
                if (Item.UnidadServicio == null) { return; }

                if (Item.UnidadServicio.Unidad.Nombre == "METRO CUADRADO")
                {

                    Decimal largo = ItemElemento.MedidaAbiertaLargo;
                    Decimal alto = ItemElemento.MedidaAbiertaAlto;
                    if (ItemElemento.UnidadMedidaAbierta.Equals("CM."))
                    {
                        largo = largo / 100;
                        alto = alto / 100;
                    }
                    txtCantidadAcabado.Value = (ItemElemento.CantidadElemento * alto * largo);
                    ObtenerCostoServicio();

                }

                if (Item.UnidadServicio.Unidad.Nombre == "MILLAR")
                {

                    Decimal cantidad = (ItemElemento.CantidadProduccion/ItemElemento.NumerodePases) / 1000 ;

                    txtCantidadAcabado.Value = cantidad;
                    ObtenerCostoServicio();

                }

                else {
                    Exception mensaje = new Exception("Solo se puede obtener cuando la unidad es METRO CUADRADO");
                    Soft.Exceptions.SoftException.ShowException(mensaje);
       
                }


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
                ObtenerCostoMaterial(); 
            }
            Mostrar();
        }

        private void bussAcabado_Clear(object sender, EventArgs e)
        {
            Item.Servicio = null;
            Item.UnidadServicio = null;
            Item.CantidadServicio = 0;
            Item.CostoServicio = 0;
            SumarTotal();
            Mostrar();
        }

        private void busUnidadAcabado_Clear(object sender, EventArgs e)
        {
            Item.UnidadServicio = null;
            Item.CantidadServicio = 0;
            Item.CostoServicio = 0;
            SumarTotal();
            Mostrar();
        }

        private void busMaquina_Clear(object sender, EventArgs e)
        {
            Item.Maquina = null;
            Item.UnidadMaquina = null;
            Item.CantidadMaquina = 0;
            Item.CostoMaquina = 0;
            SumarTotal();
            Mostrar();
        }

        private void busUnidadMaquina_Clear(object sender, EventArgs e)
        {
            Item.UnidadMaquina = null;
            Item.CantidadMaquina = 0;
            Item.CostoMaquina = 0;
            SumarTotal();
            Mostrar();
        }

        private void txtNombreMaterial_Clear(object sender, EventArgs e)
        {
            Item.Material = null;
            Item.UnidadMaterial = null;
            Item.CantidadMaterial = 0;
            Item.CostoMaterial = 0;
            SumarTotal();
            Mostrar();
        }

        private void busUnidadMaterial_Clear(object sender, EventArgs e)
        {
            Item.UnidadMaterial = null;
            Item.CantidadMaterial = 0;
            Item.CostoMaterial = 0;
            SumarTotal();
            Mostrar();
        }
        
    }
}
