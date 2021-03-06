﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Ventas.Entidades;
using Infragistics.Win.UltraWinTree;
using Infragistics.Win.UltraWinGrid;
using Soft.DataAccess;
using Soft.Entities;
using Soft.Inventario.Entidades;
using Microsoft.VisualBasic;
using Soft.Reporte.Entidades;
using Soft.Exceptions;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Soft.Produccion.Entidades;
using Infragistics.Win;
using System.Xml;

namespace Soft.Ventas.Win
{
    public partial class FrmCotizacion : FrmParent
    {
        public FrmCotizacion()
        {
            InitializeComponent();
        }

        public Cotizacion Cotizacion { get { return (Cotizacion)base.m_ObjectFlow; } }
        private ItemCotizacion ItemCotizacion = null;

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const string colTipoAMM = "Tipo";
        const string colDescripcionAMM = "Descripción";
        const string colCostoAMM = "Costo";
        
        private Boolean ActualizandoIU = false;

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colTipoAMM);
            column.DataType = typeof(string);

            column = columns.Columns.Add(colDescripcionAMM);
            column.DataType = typeof(string);

            column = columns.Columns.Add(colCostoAMM);
            column.DataType = typeof(decimal);

            ugServicios.DataSource = columns;

            ugServicios.DisplayLayout.Bands[0].Columns[colTipoAMM].Width = 50;
            ugServicios.DisplayLayout.Bands[0].Columns[colTipoAMM].CellActivation = Activation.NoEdit;
            ugServicios.DisplayLayout.Bands[0].Columns[colDescripcionAMM].Width = 150;
            ugServicios.DisplayLayout.Bands[0].Columns[colDescripcionAMM].CellActivation = Activation.NoEdit;
            ugServicios.DisplayLayout.Bands[0].Columns[colCostoAMM].Width = 70;
            ugServicios.DisplayLayout.Bands[0].Columns[colCostoAMM].CellActivation = Activation.NoEdit;
            ugServicios.DisplayLayout.Bands[0].Columns[colCostoAMM].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DoubleNonNegative;
            ugServicios.DisplayLayout.Bands[0].Columns[colCostoAMM].CellAppearance.TextHAlign = HAlign.Right;

            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            chekOcultaPreciosTotal.Checked=Cotizacion.OcultaPrecioEnPresupuesto ;

            ssTipoDocumento.Text = (Cotizacion.TipoDocumento != null) ? Cotizacion.TipoDocumento.Descripcion : "";
            ssCliente.Text = (Cotizacion.Cliente != null) ? Cotizacion.Cliente.Nombre : "";
            ssResponsable.Text = (Cotizacion.Responsable != null) ? Cotizacion.Responsable.Nombre : "";
            ssVendedor.Text = (Cotizacion.Vendedor != null) ? Cotizacion.Vendedor.Nombre : "";
            ssFormaPago.Text = (Cotizacion.ModalidadCredito != null) ? Cotizacion.ModalidadCredito.Descripcion : "";
            ssMoneda.Text = (Cotizacion.Moneda != null) ? Cotizacion.Moneda.Simbolo : "";
            ssContacto.Text = (Cotizacion.Contacto != null) ? Cotizacion.Contacto.Nombre : "";
            ssDireccionEntrega.Text = Cotizacion.DireccionEntrega;
            ssDireccionFactura.Text = Cotizacion.DireccionFacturacion;
            txtNumeracion.Text = Cotizacion.Numeracion;
            udtFechaCreacion.Value = Cotizacion.FechaCreacion;
            txtObservacion.Text = Cotizacion.Observacion;
            txtDescripcion.Text = Cotizacion.Descripcion;
            uneCantidad.Value = Cotizacion.Cantidad;
            txtNumeracion.Text = Cotizacion.Numeracion;
            unePorcentajeUtilidad.Value = Cotizacion.PorcentajeUtilidad;

            lblSubTotal.Text = string.Format("SUBTOTAL ({0})", (Cotizacion.Moneda == null) ? string.Empty : Cotizacion.Moneda.Simbolo);
            lblImpuesto.Text = string.Format("IMPUESTO ({0})", (Cotizacion.Moneda == null) ? string.Empty : Cotizacion.Moneda.Simbolo);
            lblTotal.Text = string.Format("TOTAL ({0})", (Cotizacion.Moneda == null) ? string.Empty : Cotizacion.Moneda.Simbolo);

            uneSubTotal.Value = Cotizacion.SubTotal;
            uneImpuesto.Value = Cotizacion.Impuesto;
            uneTotal.Value = Cotizacion.Total;

            lblCostoMillar.Text = string.Format("COSTO MILLAR ({0})", (Cotizacion.Moneda == null) ? string.Empty : Cotizacion.Moneda.Simbolo);
            lblCostoUnidad.Text = string.Format("COSTO UNIDAD ({0})", (Cotizacion.Moneda == null) ? string.Empty : Cotizacion.Moneda.Simbolo);

            uneCostoUnidad.Value = Cotizacion.Total / Cotizacion.Cantidad;
            uneCostoMillar.Value = (Cotizacion.Total / Cotizacion.Cantidad) * 1000;

            busListaCostoMaquina.Text = (Cotizacion.ListaCostosMaquina != null) ? Cotizacion.ListaCostosMaquina.Nombre : "";
            busListaPrecioMaterial.Text = (Cotizacion.ListaPreciosExistencia != null) ? Cotizacion.ListaPreciosExistencia.Nombre : "";
            busListaPreciosTransporte.Text = (Cotizacion.ListaPreciosTransporte != null) ? Cotizacion.ListaPreciosTransporte.Nombre : "";
            busLineaProduccion.Text = (Cotizacion.LineaProduccion != null) ? Cotizacion.LineaProduccion.Nombre : "";

            checkPrecioItems.Checked = Cotizacion.MuestraPrecioItemEnPresupuesto;

            MostrarItems();
            ActualizandoIU = false;
        }

        public void MostrarItems()
        {
            utCotizacion.Nodes.Clear();

            foreach (ItemCotizacion Item in Cotizacion.Items)
            {
                UltraTreeNode Node = new UltraTreeNode();
                Node.Tag = Item;
                Node.Text = Item.Nombre;
                utCotizacion.Nodes.Add(Node);
            }
            if (utCotizacion.Nodes.Count > 0)
            {
                utCotizacion.ActiveNode = utCotizacion.Nodes[0];
                utCotizacion.Nodes[0].Selected = true;
            }
            utCotizacion.ExpandAll();
        }

        public void MostrarItem(UltraTreeNode Node)
        {
            ActualizandoIU = true;
            ItemCotizacion Item = null;

            Node.Text = ItemCotizacion.Nombre;
            Item = (ItemCotizacion)Node.Tag;


            txtNombre.Text = ItemCotizacion.Nombre;
            labelSobranPaginas.Text = "";
            ItemCotizacion = Item;
            GrupoMedidaAbierta.Visible = Item.TieneMedidaAbierta;
            GrupoMedidaCerrada.Visible = Item.TieneMedidaCerrada;
            GruposTiras.Visible = Item.TieneTiraRetira;
            ssMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";


            txtMedidaAnchoCaja.Value = Item.MedidaAnchoCaja;

            if (Item.TieneTipoUnidad)
            {
                GrupoMedidaAbierta.Text = "MEDIDA DE " + Item.TipoUnidad;
            }
            else
            {
                GrupoMedidaAbierta.Text = "MEDIDA ABIERTA";
            }


            busMetodoImpresion.Text = (Item.MetodoImpresionOffset != null) ? Item.MetodoImpresionOffset.Codigo : "";

            txtNumeroCambios.Value = Item.NumeroCambios;

            lblTipoUnidad.Text = Item.TipoUnidad;
            txtObservacionItem.Text = Item.Observacion;
            txtCantidadItem.Value = Item.CantidadUnidad;
            txtCantidadProduccion.Value = Item.CantidadElemento;
            txtMedidaAbiertoLargo.Value = Item.MedidaAbiertaLargo;
            txtMedidaAbiertoAlto.Value = Item.MedidaAbiertaAlto;
            txtMedidaCerradaLargo.Value = Item.MedidaCerradaLargo;
            txtMedidaCerradaAlto.Value = Item.MedidaCerradaAlto;
            txtImpresoTiraColor.Value = Item.ImpresoTiraColor;
            txtImpresoRetiraColor.Value = Item.ImpresoRetiraColor;
            uneCostoMaquina.Value = Item.CostoMaquina;
            uneCostoMaterial.Value = Item.CostoMaterial;
            uneCosto.Value = Item.Costo;
            uneSeparacionX.Value = Item.SeparacionX;
            uneSeparacionY.Value = Item.SeparacionY;
            txtFormatoImpresionAlto.Value = Item.FormatoImpresionAlto;
            txtFormatoImpresionLargo.Value = Item.FormatoImpresionLargo;
            txtNroPiezasPrecorte.Value = Item.NroPiezasPrecorte;
            txtNroPiezasImpresion.Value = Item.NroPiezasImpresion;
            txtImpresionAlto.Value = Item.MedidaAbiertaAlto;
            txtImpresionLargo.Value = Item.MedidaAbiertaLargo;
            ssMaquina.Visible = Item.TieneMaquina;
            lblMaquina.Visible = Item.TieneMaquina;
            ssMaterial.Visible = Item.TieneMaterial;
            lblMaterial.Visible = Item.TieneMaterial;
            txtCostoServicio.Value = Item.CostoServicio;
            //lblCostoMaquina.Visible = Item.TieneMaquina;
            uneCostoMaquina.Visible = Item.TieneMaquina;
            //lblCostoMaterial.Visible = Item.TieneMaterial;
            uneCostoMaterial.Visible = Item.TieneMaterial;
            txtMedidaAnchoCaja.Visible = Item.TieneFondo;
            labelFondoCaja.Visible = Item.TieneFondo;

            comboMedida.Text = Item.UnidadMedidaAbierta;
            lblTipoUnidad.Visible = Item.TieneTipoUnidad;
            txtCantidadItem.Visible = Item.TieneTipoUnidad;
            checkMuestraPrecioEnPresupuesto.Checked = Item.MuestraPrecioEnPresupuesto;



            if (Item.TieneTipoUnidad == false)
            {
                txtCantidadItem.Value = 0;
            }


            txtDemasia.Value = Item.CantidadDemasia;
            txtPases.Value = Item.NumerodePases;
            txtHojasMaquina.Value = (Item.CantidadMaterial) * Item.NroPiezasPrecorte;
            txtTiraje.Value = Item.CantidadProduccion;

            LabelMateriaPrima.Text = "";
            if (Item.NumeroPliegos > 0)
            {
                LabelMateriaPrima.Text = Item.NumeroPliegos + " pliegos de : ";
            }

            LabelMateriaPrima.Text += Math.Round(Item.CantidadMaterial, 0).ToString() + " + " + Math.Round(Item.CantidadDemasiaMaterial, 0).ToString() + " = " + Math.Round((Item.CantidadMaterial + Item.CantidadDemasiaMaterial), 0).ToString() + " Hjs/Resma";
            Item.LabelMaterial = LabelMateriaPrima.Text;



            LabelMaterialAlmancen.Text = "";
            if (Item.NumeroPliegos > 0)
            {
                LabelMaterialAlmancen.Text = " Total Material : ";
            }

            LabelMaterialAlmancen.Text += " = " + Math.Round((Item.CantidadMaterial + Item.CantidadDemasiaMaterial), 0).ToString() + " Hjs/Resma";
            Item.LabelMaterial = LabelMaterialAlmancen.Text;




            LabelProduccion.Text = "";
            if (Item.NumeroPliegos > 0)
            {
                LabelProduccion.Text = Item.NumeroPliegos + " pliegos de : ";
            }
            LabelProduccion.Text += Math.Round(((Item.CantidadMaterial + Item.CantidadDemasiaMaterial) * Item.NroPiezasPrecorte), 0).ToString() + " Hjs/Maquina";

            Item.LabelProduccion = LabelProduccion.Text;
            txtPliegos.Value = Item.NumeroPliegos;


            // if (Item.MetodoImpresion != null) {

            //}


            checkGraficoImpresionManual.Checked = Item.GraficoImpresionManual;

            utcItemCotizacion.Tabs["Graficos"].Visible = Item.TieneGraficos;
            txtDemasia.Value = Item.CantidadDemasia;

            if (Item.TieneGraficos)
            {
                try
                {
                    upbImpresion.Visible = true;
                    txtNroPiezasImpresion.ReadOnly = true;
                    if (Item.GraficoImpresionManual)
                    {
                        upbImpresion.Visible = false;
                        txtNroPiezasImpresion.ReadOnly = false;
                    }
                    else if (Item.GraficoImpresionGirado)
                    {
                        GenerarGraficoImpresionRotado();
                    }
                    else
                    {
                        GenerarGraficoImpresionNormal();
                    }



                    if (Item.GraficoPrecorteGirado)
                    {

                        GenerarGraficoPrecorteRotado();

                    }
                    else { GenerarGraficoPrecorteNormal(); }
                }
                catch (Exception)
                {

                }
            }

            labelSobranPaginas.Text="";
            if(Item.PaginasSobrantes >0){
                labelSobranPaginas.Text = "Sobran " + Item.PaginasSobrantes + " paginas";
            }

            uceIncluirEnPresupuesto.Checked = Item.IncluirEnPrespuesto;

            MostrarAMMS(Item);
            ActualizandoIU = false;
        }

        public void MostrarAMMS(ItemCotizacion ItemCotizacion)
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemCotizacionServicio Item in ItemCotizacion.Servicios)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarAAM(Row);
            }
            MostrarTotalAMM(ItemCotizacion);
        }

        private void MostrarAAM(UltraGridRow Row) 
        {
            ItemCotizacionServicio Item = (ItemCotizacionServicio)Row.Tag;
            if (Item.Servicio != null)
            {
                Row.Cells[colTipoAMM].Value = "ACA";
                Row.Cells[colDescripcionAMM].Value = Item.Servicio.Descripcion;
                Row.Cells[colCostoAMM].Value = Item.CostoServicio;
            }
            if (Item.Maquina != null)
            {
                Row.Cells[colTipoAMM].Value = "MAQ";
                Row.Cells[colDescripcionAMM].Value = Item.Maquina.Descripcion;
                Row.Cells[colCostoAMM].Value = Item.CostoMaquina;
            }
            if (Item.Material != null)
            {
                Row.Cells[colTipoAMM].Value = "MAT";
                Row.Cells[colDescripcionAMM].Value = Item.Material.Descripcion;
                Row.Cells[colCostoAMM].Value = Item.CostoMaterial;
            }
        }

        private void MostrarTotalAMM(ItemCotizacion itemSe)
        {

            Decimal total = 0;
            foreach (ItemCotizacionServicio itemServicio in itemSe.Servicios)
            {
                total += itemServicio.CostoTotalServicio;
            }
            itemSe.CostoServicio = total;
            txtCostoServicio.Value = itemSe.CostoServicio;
        }

        public void DeshabilitarControles()
        {
            txtMedidaAbiertoLargo.Value = 0;
            txtMedidaAbiertoAlto.Value = 0;
            txtMedidaCerradaLargo.Value = 0;
            txtMedidaCerradaAlto.Value = 0;
            txtImpresoTiraColor.Value = 0;
            txtImpresoRetiraColor.Value = 0;
            txtCantidadItem.Value = 0;
            ssMaquina.Text = "";
            ssMaterial.Text = "";
            txtObservacionItem.Text = "";
            ClearAllRows(ref ugServicios);
            utcItemCotizacion.Enabled = false;
        }

        public void AgregarServicios(String Codigo, String Descripcion, UltraGridRow Row)
        {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacionServicio Item = (ItemCotizacionServicio)Row.Tag;
            String Filtro = String.Format(" Codigo LIKE '{0}%' AND Nombre LIKE '{1}%'", Codigo, Descripcion);
            Productos = FrmSeleccionar.GetSelectedsEntities(typeof(Existencia), "Selección de Servicios", Filtro);
            if (Productos.Count == 1)
            {
                Existencia Servicio = (Existencia)Productos[1];
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Servicio.ID);
                //Item.CantidadFinal = 1;
                MostrarAAM(Row);
            }
            else if (Productos.Count > 1)
            {
                Existencia Producto = (Existencia)Productos[1];
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                MostrarAAM(Row);
                for (int i = 2; i <= Productos.Count; i++)
                {
                    UltraGridRow RowNuevo = ugServicios.DisplayLayout.Bands[0].AddNew();
                    ItemCotizacionServicio ItemNuevo = ItemCotizacion.AddServicio();
                    Existencia ProductoNuevo = (Existencia)Productos[i];
                    ItemNuevo.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ProductoNuevo.ID);
                    RowNuevo.Tag = ItemNuevo;
                    MostrarAAM(RowNuevo);
                }
            }
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
            TipoCotizacion TipoDocumento = (TipoCotizacion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoCotizacion), "Tipo de Cotización");

            if ((Cotizacion.TipoDocumento == null) || (Cotizacion.TipoDocumento.Codigo != TipoDocumento.Codigo))
            {
                Cotizacion.TipoDocumento = (TipoCotizacion)HelperNHibernate.GetEntityByID("TipoCotizacion", TipoDocumento.ID);
                Cotizacion.AsignarListadeCostosDesdeTipoDocumento();
                foreach (ItemCotizacion ItemCotizacion in Cotizacion.Items)
                {
                    CalcularProduccionItem(ItemCotizacion);
                    //MostrarItem(utCotizacion.ActiveNode);
                }


                try
                {
                    FrmSelectedEntity FrmSeleccionarEmpleado = new FrmSelectedEntity();
                    String filtro = "IDUsuario='" + FrmMain.Usuario.ID + "'";
                    SocioNegocio sn = (SocioNegocio)FrmSeleccionarEmpleado.GetSelectedEntity(typeof(SocioNegocio), "Empleado", filtro);

                    Cotizacion.Responsable = (SocioNegocio)HelperNHibernate.GetEntityByID("SocioNegocio", sn.ID);
                }
                catch (Exception)
                {
                }


            }
            Mostrar();
            /*ssTipoDocumento.Text = (Cotizacion.TipoDocumento != null) ? Cotizacion.TipoDocumento.Descripcion : "";
            ssResponsable.Text = (Cotizacion.Responsable != null) ? Cotizacion.Responsable.Nombre : "";*/
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            Cotizacion.Numeracion = txtNumeracion.Text;
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
            Cotizacion.Cliente = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Cliente = 1");
            ssCliente.Text = (Cotizacion.Cliente != null) ? Cotizacion.Cliente.Nombre : "";
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            Cotizacion.Descripcion = txtDescripcion.Text;
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            Cotizacion.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssResponsable.Text = (Cotizacion.Responsable != null) ? Cotizacion.Responsable.Nombre : "";
        }

        private void ssFormaPago_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Cotizacion.ModalidadCredito = (ModalidadCredito)FrmSeleccionar.GetSelectedEntity(typeof(ModalidadCredito), "Modalidad de Crédito");
            ssFormaPago.Text = (Cotizacion.ModalidadCredito != null) ? Cotizacion.ModalidadCredito.Descripcion : "";
        }

        private void ssVendedor_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarVendedor = new FrmSelectedEntity();
            Cotizacion.Vendedor = (SocioNegocio)FrmSeleccionarVendedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssVendedor.Text = (Cotizacion.Vendedor != null) ? Cotizacion.Vendedor.Nombre : "";
        }

        private void uneCantidad_ValueChanged(object sender, EventArgs e)
        {
            Cotizacion.Cantidad = Convert.ToInt32(uneCantidad.Value);
        }

        private void utCotizacion_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {
            UltraTreeNode Node = utCotizacion.ActiveNode;
            if (Node != null)
            {
                ItemCotizacion = (ItemCotizacion)Node.Tag;
                utcItemCotizacion.Tabs[0].Text = Node.Text;
                MostrarItem(Node);
            }
        }

        private void txtMedidaAbiertoLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.MedidaAbiertaLargo = Convert.ToDecimal(txtMedidaAbiertoLargo.Value);
            txtImpresionLargo.Value = ItemCotizacion.MedidaAbiertaLargo;
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.MedidaAbiertaAlto = Convert.ToDecimal(txtMedidaAbiertoAlto.Value);
            txtImpresionAlto.Value = ItemCotizacion.MedidaAbiertaAlto;
        }

        private void txtMedidaCerradaLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.MedidaCerradaLargo = Convert.ToDecimal(txtMedidaCerradaLargo.Value);
        }

        private void txtMedidaCerradaAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.MedidaCerradaAlto = Convert.ToDecimal(txtMedidaCerradaAlto.Value);
        }

        private void txtImpresoTiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.ImpresoTiraColor = Convert.ToInt32(txtImpresoTiraColor.Value);
        }

        private void txtCantidadItem_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.CantidadUnidad = Convert.ToInt32(txtCantidadItem.Value);
        }

        private void txtImpresoRetiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.ImpresoRetiraColor = Convert.ToInt32(txtImpresoRetiraColor.Value);
        }

        private void ssMaquina_Search(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            String filtro = "";
            if (ItemCotizacion.Operacion != null)
            {
                filtro = "IDOperacion='" + ItemCotizacion.Operacion.ID + "'";
            }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacion.Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Maquina Operacion", filtro);

            if (ItemCotizacion.Maquina != null)
            {
                ItemCotizacion.Maquina = (Maquina)HelperNHibernate.GetEntityByID("Maquina", ItemCotizacion.Maquina.ID);
                //InsertarServiciosAutogenerados();
                ItemCotizacion.FormatoImpresionAlto = ItemCotizacion.Maquina.PliegoAltoMaximo;
                ItemCotizacion.FormatoImpresionLargo = ItemCotizacion.Maquina.PliegoAnchoMaximo;
                txtFormatoImpresionAlto.Value = ItemCotizacion.FormatoImpresionAlto;
                txtFormatoImpresionLargo.Value = ItemCotizacion.FormatoImpresionLargo;
            }
            ssMaquina.Text = (ItemCotizacion.Maquina != null) ? ItemCotizacion.Maquina.Nombre : "";
        }

        private void ObtenerCostoServicio(ItemCotizacionServicio Item)
        {
            try
            {
                foreach (ItemListaPreciosExistencia itemLPE in Cotizacion.ListaPreciosExistencia.Items)
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
                                    if ((cantidadNeta - cantidadRedondeada) > 0)
                                    {
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

                SumarTotalServicio(Item);


            }
            catch (Exception)
            {
            }
            Mostrar();
        }

        public void SumarTotalServicio(ItemCotizacionServicio Item)
        {
            Item.CostoTotalServicio = Item.CostoMaquina + Item.CostoMaterial + Item.CostoServicio;

        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            string filtro = "EsInventariable = 1";
            if (ssMaterial.Text.Length > 0)
            {
                filtro += " and Nombre like '%" + ssMaterial.Text + "%'";
            }

            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacion.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", filtro);
            if (ItemCotizacion.Material != null)
            {
                ItemCotizacion.Material = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ItemCotizacion.Material.ID);
            }
            ssMaterial.Text = (ItemCotizacion.Material != null) ? ItemCotizacion.Material.Nombre : "";
        }

        private void txtObservacionItem_TextChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.Observacion = txtObservacionItem.Text;
        }

        private void ubNuevoServicio_Click(object sender, EventArgs e)
        {
            FrmCotizaciondeServicio AgregarServicio = new FrmCotizaciondeServicio();
            ItemCotizacionServicio item = AgregarServicio.ObtenerServicio(Cotizacion, ItemCotizacion);
            if (item != null)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = item;
                Row.Cells[colTipoAMM].Activate();
                ugServicios.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                ItemCotizacion.Servicios.Add(item);
                MostrarTotalAMM(ItemCotizacion);
                MostrarAAM(Row);
            }
        }

        private void ubEliminarServicio_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null) { return; }
            ItemCotizacion.Servicios.Remove((ItemCotizacionServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
            MostrarTotalAMM(ItemCotizacion);

        }

        public void ugServicios_CellKeyEnter(UltraGridCell Cell)
        {
            //try
            //{
            //    if (Cell == null || ItemCotizacion == null) { return; }
            //    ItemCotizacionServicio Item = (ItemCotizacionServicio)Cell.Row.Tag;
            //    switch (Cell.Column.Key)
            //    {
            //        case colServicio:
            //            if (Cell.Text.Equals("")) { break; }
            //            AgregarServicios("%", Cell.Text, Cell.Row);
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //catch (Exception)
            //{ }
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            Cotizacion.Observacion = txtObservacion.Text;
        }

        private void unePorcentajeUtilidad_ValueChanged(object sender, EventArgs e)
        {
            Cotizacion.PorcentajeUtilidad = Convert.ToDecimal(unePorcentajeUtilidad.Value);
        }

        ListaCostosMaquina lcm = null;
        ListaPreciosExistencia lpe = null;
        ListaPreciosTransporte lpt = null;
        Boolean acatualizalistas = true;
        private void Costeo()
        {
            try
            {
                if (acatualizalistas)
                {
                    lcm = (ListaCostosMaquina)HelperNHibernate.GetEntityByID("ListaCostosMaquina", Cotizacion.ListaCostosMaquina.ID);
                    lpe = (ListaPreciosExistencia)HelperNHibernate.GetEntityByID("ListaPreciosExistencia", Cotizacion.ListaPreciosExistencia.ID);
                    lpt = (ListaPreciosTransporte)HelperNHibernate.GetEntityByID("ListaPreciosTransporte", Cotizacion.ListaPreciosTransporte.ID);
                    acatualizalistas = false;
                }

                foreach (ItemCotizacion itemcotizacion2 in Cotizacion.Items)
                {
                    CosteoElemento(itemcotizacion2);
                }
            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }

            //Cotizacion.SubTotal = Cotizacion.SubTotal;
        }

        private void CosteoElemento(ItemCotizacion itemCotizacion2)
        {
            if (itemCotizacion2.Maquina != null)
            {
                itemCotizacion2.CostoMaquina = obtenerItemListaCostosMaquina(itemCotizacion2);
            }
            else
            {
                itemCotizacion2.CostoMaquina = 0;
            }


            if (itemCotizacion2.Material != null)
            {
                itemCotizacion2.CostoMaterial = obtenerItemListaCostosMaterial(itemCotizacion2);
            }
            else
            {
                itemCotizacion2.CostoMaterial = 0;
            }

            Decimal totalservicio = 0;
            foreach (ItemCotizacionServicio item in itemCotizacion2.Servicios)
            {
                totalservicio += item.CostoTotalServicio;
            }
            itemCotizacion2.CostoServicio = totalservicio;
            itemCotizacion2.Cantidad = 1;
            itemCotizacion2.Costo = Math.Round((itemCotizacion2.CostoMaquina + itemCotizacion2.CostoMaterial + itemCotizacion2.CostoServicio) / itemCotizacion2.CantidadElemento, 2) * itemCotizacion2.CantidadElemento;
            itemCotizacion2.Precio = itemCotizacion2.Costo;
        }

        private Decimal obtenerItemListaCostosMaterial(ItemCotizacion itemCotizacion)
        {
            Decimal resultado = 0;
            try
            {

                Decimal costoMaterial = 0;

                if (itemCotizacion.Material.CostoUltimaCompra > 0)
                {
                    costoMaterial = itemCotizacion.Material.CostoUltimaCompra;
                }
                else
                {
                    costoMaterial = itemCotizacion.Material.CostoReferencia;
                }



                if (itemCotizacion.NumeroPliegos == 0)
                {
                    itemCotizacion.NumeroPliegos = 1;
                }

                resultado = costoMaterial * (itemCotizacion.CantidadMaterial + itemCotizacion.CantidadDemasiaMaterial) * itemCotizacion.NumeroPliegos;

            }
            catch (Exception)
            {
            }


            return resultado;

        }



        private Decimal obtenerItemListaCostosMaquina(ItemCotizacion itemCotizacion)
        {
            //Decimal resultado = 0;
            //try
            //{
            //    ItemListaCostosMaquina ilcm = obtenerItemListaCostosMaquina(itemCotizacion.Maquina);
            //    UnidadListaCostosMaquina Uilcm = obtenerUnidadLCM(ilcm);
            //    EscalaListaCostosMaquina Elcm = obtenerEscalaLCM(Uilcm, itemCotizacion);

            //    Int32 multiplicador = 1;
            //    if (Uilcm.Unidad.Nombre.Equals("MILLAR"))
            //    {
            //        multiplicador = 1000;
            //        resultado = (itemCotizacion.CantidadProduccion) / multiplicador;
            //        Int32 entero = Convert.ToInt32(resultado);
            //        Decimal residuo = (resultado - entero) * 100;
            //        if (residuo >= 20 && residuo <= 100)
            //        {
            //            resultado = (entero + 1) * Elcm.Costo * itemCotizacion.NumerodePases * itemCotizacion.NumeroPliegos;
            //        }
            //        else if (entero == 0 && residuo > 0)
            //        {
            //            resultado = (1) * Elcm.Costo * itemCotizacion.NumerodePases * itemCotizacion.NumeroPliegos;
            //        }
            //        else
            //        {
            //            resultado = (entero) * Elcm.Costo * itemCotizacion.NumerodePases * itemCotizacion.NumeroPliegos;
            //        }

            //        Decimal factorColores = 0;
            //        //VERIFICANDO METODO
            //        if (itemCotizacion.MetodoImpresion.Equals("TIRA/RETIRA"))
            //        {
            //            factorColores = Convert.ToDecimal(itemCotizacion.ImpresoTiraColor) / 4;
            //            factorColores += Convert.ToDecimal(itemCotizacion.ImpresoRetiraColor) / 4;
            //        }
            //        else
            //        {
            //            factorColores = Convert.ToDecimal(itemCotizacion.ImpresoTiraColor) / 4;

            //        }

            //        resultado = resultado * factorColores;


            //    }

            //    else
            //    {
            //        multiplicador = 1;
            //        resultado = itemCotizacion.CantidadProduccion / multiplicador;
            //        Int32 entero = Convert.ToInt32(resultado);
            //        Decimal residuo = resultado - entero;
            //        if (residuo > 0 && residuo <= 1)
            //        {
            //            resultado = (entero + 1) * Elcm.Costo;
            //        }
            //        else
            //        {
            //            resultado = (entero) * Elcm.Costo;
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //}
            //return resultado;




            Decimal resultado = 0;
            try
            {
                ItemListaCostosMaquina ilcm = obtenerItemListaCostosMaquina(itemCotizacion.Maquina);
                UnidadListaCostosMaquina Uilcm = obtenerUnidadLCM(ilcm);
                EscalaListaCostosMaquina Elcm = obtenerEscalaLCM(Uilcm, itemCotizacion);

                Int32 multiplicador = 1;
                if (Uilcm.Unidad.Nombre.Equals("MILLAR"))
                {
                    multiplicador = 1000;
                    resultado = (itemCotizacion.CantidadProduccion * itemCotizacion.NumerodePases) / multiplicador;
                    Int32 entero = Convert.ToInt32(resultado);
                    Decimal residuo = (resultado - entero) * 100;
                    if (residuo >= 20 && residuo <= 100)
                    {
                        resultado = (entero + 1) * Elcm.Costo;
                    }
                    else if (entero == 0 && residuo > 0)
                    {
                        resultado = (1) * Elcm.Costo;
                    }
                    else
                    {
                        resultado = (entero) * Elcm.Costo;
                    }

                    Decimal factorColores = 0;
                    Decimal factorColores2 = 0;

                    //VERIFICANDO METODO
                    decimal PrecioCambiado = 0;
                    if (itemCotizacion.MetodoImpresionOffset.FactorCambios==2 )
                    {
                        factorColores = Convert.ToDecimal(itemCotizacion.ImpresoTiraColor) / 4;
                        factorColores2 = Convert.ToDecimal(itemCotizacion.ImpresoRetiraColor) / 4;


                        //PRIMER PASE
                        PrecioCambiado = resultado * factorColores;
                        PrecioCambiado += resultado * factorColores2;


                    }
                    else
                    {
                        factorColores = Convert.ToDecimal(itemCotizacion.ImpresoTiraColor) / 4;
                        if (itemCotizacion.NumeroPliegos == 1)
                        {
                            PrecioCambiado = resultado * factorColores;
                        }
                    }

                    resultado = PrecioCambiado * itemCotizacion.NumeroPliegos;



                }

                else
                {
                    multiplicador = 1;
                    resultado = itemCotizacion.CantidadProduccion / multiplicador;
                    Int32 entero = Convert.ToInt32(resultado);
                    Decimal residuo = resultado - entero;
                    if (residuo > 0 && residuo <= 1)
                    {
                        resultado = (entero + 1) * Elcm.Costo;
                    }
                    else
                    {
                        resultado = (entero) * Elcm.Costo;
                    }
                }
            }
            catch (Exception)
            {

            }
            return resultado;

        }

        private ItemListaCostosMaquina obtenerItemListaCostosMaquina(Maquina maquina)
        {
            ItemListaCostosMaquina resultado = null;
            if (maquina == null)
            {
                return resultado;
            }


            foreach (ItemListaCostosMaquina item in lcm.Items)
            {

                if (item.Maquina.ID == maquina.ID)
                {
                    resultado = item;
                    break;
                }
            }
            return resultado;
        }
        private UnidadListaCostosMaquina obtenerUnidadLCM(ItemListaCostosMaquina ilcm)
        {
            UnidadListaCostosMaquina Uilcm = null;

            if (ilcm == null)
            {
                return Uilcm;
            }

            foreach (UnidadListaCostosMaquina unidad in ilcm.Unidades)
            {
                Uilcm = unidad;
                break;
            }
            return Uilcm;
        }
        private EscalaListaCostosMaquina obtenerEscalaLCM(UnidadListaCostosMaquina Uilcm, ItemCotizacion itemcotizacion)
        {
            EscalaListaCostosMaquina eUilcm = null;

            if (Uilcm == null)
            {
                return eUilcm;
            }

            CalcularProduccionItem(itemcotizacion);


            Decimal cantidadProduccion = 0;
            if (Uilcm.Unidad.Nombre.Equals("MILLAR"))
            {

                cantidadProduccion = itemcotizacion.CantidadProduccion / 1000;

                Decimal entero = Convert.ToInt32(cantidadProduccion);

                Decimal dif = (cantidadProduccion - entero) * 100;
                if (dif < 20)
                {

                    cantidadProduccion = entero;
                }



            }
            else
            {
                cantidadProduccion = itemcotizacion.CantidadProduccion;

            }


            foreach (EscalaListaCostosMaquina escala in Uilcm.Escalas)
            {


                if ((escala.Desde == 0) && (escala.Hasta == 0))
                {
                    eUilcm = escala;
                    break;
                }
                else if ((escala.Desde <= cantidadProduccion) && (escala.Hasta >= cantidadProduccion))
                {
                    eUilcm = escala;
                    break;
                }
                else if ((escala.Hasta == 0))
                {
                    eUilcm = escala;
                    break;
                }
            }
            return eUilcm;

        }



        private void CalcularProduccionItem(ItemCotizacion itemcosteado)
        {
            try
            {
                if (itemcosteado == null) { return; }

                if (itemcosteado.Operacion.Codigo.Equals("IMPRVINIL") || itemcosteado.Operacion.Nombre.Equals("IMPRESION BANNER"))
                {
                    Decimal largo = itemcosteado.MedidaAbiertaLargo;
                    Decimal alto = itemcosteado.MedidaAbiertaAlto;
                    
                    if (itemcosteado.UnidadMedidaAbierta.Equals("CM."))
                    {
                        largo = largo / 100;
                        alto = alto / 100;
                    }

                    if (ItemCotizacion.TieneGraficos)
                    {
                        itemcosteado.CantidadMaterial = Math.Round((itemcosteado.CantidadElemento * (largo * alto)), 0);
                    }
                    else 
                    {
                        itemcosteado.CantidadMaterial = (itemcosteado.CantidadElemento * (largo * alto));
                    }
                    
                    itemcosteado.CantidadDemasiaMaterial = itemcosteado.CantidadDemasia;

                    itemcosteado.CantidadProduccion = itemcosteado.CantidadMaterial + itemcosteado.CantidadDemasiaMaterial;
                }
                else
                {

                    Decimal mat = 0;
                    if (itemcosteado.CantidadUnidad > 0 && itemcosteado.TieneTipoUnidad)
                    {
                        mat = (itemcosteado.CantidadElemento / (itemcosteado.NroPiezasPrecorte));
                    }
                    else
                    {

                        mat = (itemcosteado.CantidadElemento / (itemcosteado.NroPiezasPrecorte * itemcosteado.NroPiezasImpresion));
                    }

                    Int32 mate = Convert.ToInt32(mat);

                    if ((mat - mate) > 0)
                    {
                        itemcosteado.CantidadMaterial = mate + 1;
                    }
                    else
                    {
                        itemcosteado.CantidadMaterial = mate;
                    }


                    //itemcosteado.CantidadMaterial += itemcosteado.CantidadDemasia;
                    try
                    {
                        itemcosteado.CantidadDemasiaMaterial = itemcosteado.CantidadDemasia / itemcosteado.NroPiezasPrecorte;
                    }
                    catch (Exception)
                    {
                    }






                    Int32 pases = 1;





                    if (itemcosteado.MetodoImpresion.Equals("TIRA Y RETIRA"))
                    {
                        pases = 2;
                    }
                    else if (itemcosteado.MetodoImpresion.Equals("CONTRAPINZA"))
                    {
                        pases = 2;
                    }



                    labelSobranPaginas.Text = "";


                    if (itemcosteado.CantidadUnidad == 0)
                    {
                        itemcosteado.NumeroPliegos = 1;
                    }
                    else
                    {
                        Decimal pliegos = itemcosteado.CantidadUnidad / (itemcosteado.NroPiezasImpresion * 2);
                        Decimal entero = Math.Truncate(pliegos);
                        Decimal paginasresiduo = entero - pliegos;
                        itemcosteado.PaginasSobrantes = 0;
                        if (paginasresiduo < 0) {
                            itemcosteado.PaginasSobrantes = Convert.ToInt32(itemcosteado.CantidadUnidad - (entero * itemcosteado.NroPiezasImpresion * 2)) ;    
                        }
                        
                        itemcosteado.NumeroPliegos = Convert.ToInt32(entero);

                        if (itemcosteado.NumeroPliegos == 0)
                        {

                            itemcosteado.NumeroPliegos = 1;
                        }
                    }
                    //itemcosteado.CantidadProduccion = (itemcosteado.CantidadMaterial + itemcosteado.CantidadDemasiaMaterial) * itemcosteado.NumerodePases * itemcosteado.NroPiezasPrecorte * pases;
                    itemcosteado.CantidadProduccion = (itemcosteado.CantidadMaterial + itemcosteado.CantidadDemasiaMaterial)  * itemcosteado.NroPiezasPrecorte * pases;

                }
            }
            catch (Exception)
            {
                //SoftException.ShowException(ex);
            }

        }


        private void ubRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cotizacion.ListaCostosMaquina == null) {
                    throw new Exception("Seleecione una lista de costos de Maquina");
                }

                if (Cotizacion.ListaPreciosExistencia == null)
                {
                    throw new Exception("Seleecione una lista de costos de Materiales y existencias");
                }

                if (Cotizacion.ListaPreciosTransporte == null)
                {
                    throw new Exception("Seleecione una lista de transportes");
                }


                Costeo();
                Mostrar();
                MostrarItem(utCotizacion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }

        }

        public void GenerarGraficoImpresionNormal()
        {
            
            if (ItemCotizacion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoGrafico = 0;
            Int32 AltoGrafico = 0;
            Int32 LargoPictureBox = 0;
            Int32 AltoPictureBox = 0;

            if (ItemCotizacion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo), Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox / 2;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto), Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }
            else if (ItemCotizacion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo), Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto), Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox / 2;
            }
            else
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo), Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto), Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }

            Int32 LargoPieza = Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo * 10);
            Int32 AltoPieza = Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto * 10);


            LargoPictureBox = LargoPictureBox * 10;
            LargoGrafico = LargoGrafico * 10;
            AltoPictureBox = AltoPictureBox * 10;
            AltoGrafico = AltoGrafico * 10;


            upbImpresion.Width = LargoPictureBox;
            upbImpresion.Height = AltoPictureBox;

            Bitmap b;
            b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
            upbImpresion.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);

            int CantidadPiezas = 0;
            for (int x = LargoPieza; x <= LargoGrafico; x += LargoPieza)
            {

                Boolean MargenInicio = false;
                Boolean MargenFin = false;


                for (int y = AltoPieza; y <= AltoGrafico; y += AltoPieza)
                {
                    if (MargenInicio == false)
                    {
                        //y += 10;
                        MargenInicio = true;
                    }
                    g.DrawRectangle(MyPen, new Rectangle(x - LargoPieza, y - AltoPieza, LargoPieza, AltoPieza));
                    CantidadPiezas += 1;
                    //y += ItemCotizacion.SeparacionY / 10;
                    y += ItemCotizacion.SeparacionY;
                }
                //x += ItemCotizacion.SeparacionX / 10;
                if (MargenFin == false)
                {
                    //x += 10;
                    MargenFin = true;
                }
                x += ItemCotizacion.SeparacionX;
            }

            if (ItemCotizacion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                Font Font = new System.Drawing.Font("Arial Narrow", 80, FontStyle.Bold);
                Brush Brush = new SolidBrush(System.Drawing.Color.Red);
                Pen Pen = new Pen(System.Drawing.Color.Red, 4);

                g.DrawImage((Image)upbImpresion.Image, LargoGrafico, 0);
                g.DrawLine(Pen, LargoGrafico, 0, LargoGrafico, AltoGrafico);
                g.DrawString("T", Font, Brush, (LargoGrafico / 2) - 10, (AltoGrafico / 2) - 10);
                g.DrawString("R", Font, Brush, ((LargoGrafico / 2) * 3) - 10, (AltoGrafico / 2) - 10);

                CantidadPiezas = CantidadPiezas * 2;
            }
            else if (ItemCotizacion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                Font Font = new System.Drawing.Font("Arial Narrow", 80, FontStyle.Regular);
                Brush Brush = new SolidBrush(System.Drawing.Color.Red);
                Pen Pen = new Pen(System.Drawing.Color.Red, 4);

                g.DrawImage((Image)upbImpresion.Image, 0, AltoGrafico);
                g.DrawLine(Pen, 0, AltoGrafico, LargoGrafico, AltoGrafico);
                g.DrawString("R", Font, Brush, (LargoGrafico / 2) - 10, (AltoGrafico / 2) - 10);
                g.DrawString("T", Font, Brush, (LargoGrafico / 2) - 10, ((AltoGrafico / 2) * 3) - 10);
                CantidadPiezas = CantidadPiezas * 2;
            }

            g.DrawRectangle(MyPen, new Rectangle(0, 0, upbImpresion.Width - 1, upbImpresion.Height - 1));

            upbImpresion.Width = upbImpresion.Width / 6;
            upbImpresion.Height = upbImpresion.Height / 6;



            ItemCotizacion.NroPiezasImpresion = CantidadPiezas;
            txtNroPiezasImpresion.Value = CantidadPiezas;
        }

        public void GenerarGraficoImpresionRotado()
        {
            if (ItemCotizacion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoGrafico = 0;
            Int32 AltoGrafico = 0;
            Int32 LargoPictureBox = 0;
            Int32 AltoPictureBox = 0;

            if (ItemCotizacion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo), Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox / 2;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto), Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }
            else if (ItemCotizacion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo), Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto), Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox / 2;
            }
            else
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo), Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto), Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }

            Int32 LargoPieza = Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo*10);
            Int32 AltoPieza = Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto*10);




            //ELEVAMOS 10 VECES MAS
            //LargoPieza = LargoPieza * 10;
            //AltoPieza = AltoPieza * 10;


            LargoPictureBox = LargoPictureBox * 10;
            LargoGrafico = LargoGrafico * 10;
            AltoPictureBox = AltoPictureBox * 10;
            AltoGrafico = AltoGrafico * 10;

            upbImpresion.Width = LargoPictureBox;
            upbImpresion.Height = AltoPictureBox;

            Bitmap b;
            b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
            upbImpresion.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);

            int CantidadPiezas = 0;
            for (int x = AltoPieza; x <= LargoGrafico; x += AltoPieza)
            {
                for (int y = LargoPieza; y <= AltoGrafico; y += LargoPieza)
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - AltoPieza, y - LargoPieza, AltoPieza, LargoPieza));
                    CantidadPiezas += 1;
                    //y += ItemCotizacion.SeparacionY / 10;
                    y += ItemCotizacion.SeparacionY;

                }
                //x += ItemCotizacion.SeparacionX / 10;
                x += ItemCotizacion.SeparacionX;

            }

            if (ItemCotizacion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                Font Font = new System.Drawing.Font("Arial Narrow", 80, FontStyle.Regular);
                Brush Brush = new SolidBrush(System.Drawing.Color.Red);
                Pen Pen = new Pen(System.Drawing.Color.Red, 4);

                g.DrawImage((Image)upbImpresion.Image, LargoGrafico, 0);
                g.DrawLine(Pen, LargoGrafico, 0, LargoGrafico, AltoGrafico);
                g.DrawString("T", Font, Brush, (LargoGrafico / 2) - 10, (AltoGrafico / 2) - 10);
                g.DrawString("R", Font, Brush, ((LargoGrafico / 2) * 3) - 10, (AltoGrafico / 2) - 10);

                CantidadPiezas = CantidadPiezas * 2;
            }
            else if (ItemCotizacion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                Font Font = new System.Drawing.Font("Arial Narrow", 80, FontStyle.Regular);
                Brush Brush = new SolidBrush(System.Drawing.Color.Red);
                Pen Pen = new Pen(System.Drawing.Color.Red, 4);

                g.DrawImage((Image)upbImpresion.Image, 0, AltoGrafico);
                g.DrawLine(Pen, 0, AltoGrafico, LargoGrafico, AltoGrafico);
                g.DrawString("T", Font, Brush, (LargoGrafico / 2) - 10, (AltoGrafico / 2) - 10);
                g.DrawString("R", Font, Brush, (LargoGrafico / 2) - 10, ((AltoGrafico / 2) * 3) - 10);

                CantidadPiezas = CantidadPiezas * 2;
            }

            g.DrawRectangle(MyPen, new Rectangle(0, 0, upbImpresion.Width - 1, upbImpresion.Height - 1));

            upbImpresion.Width = upbImpresion.Width / 6;
            upbImpresion.Height = upbImpresion.Height / 6;


            ItemCotizacion.NroPiezasImpresion = CantidadPiezas;
            txtNroPiezasImpresion.Value = CantidadPiezas;
        }

        public void GenerarGraficoPrecorteNormal()
        {

            if (ItemCotizacion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoTotal = Convert.ToInt32(ItemCotizacion.Material.Largo);
            Int32 AltoTotal = Convert.ToInt32(ItemCotizacion.Material.Alto);

            Int32 LargoPieza = Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo);
            Int32 AltoPieza = Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto);

            upbPrecorte.Width = LargoTotal;
            upbPrecorte.Height = AltoTotal;

            Bitmap b;
            b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            upbPrecorte.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, LargoTotal - 1, AltoTotal - 1));
            int CantidadPiezas = 0;
            for (int x = LargoPieza; x <= upbPrecorte.Width; x += LargoPieza)
            {
                for (int y = AltoPieza; y <= upbPrecorte.Height; y += AltoPieza)
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - LargoPieza, y - AltoPieza, LargoPieza, AltoPieza));
                    CantidadPiezas += 1;
                }
            }
            ItemCotizacion.NroPiezasPrecorte = CantidadPiezas;
            txtNroPiezasPrecorte.Value = CantidadPiezas;
        }

        public void GenerarGraficoPrecorteRotado()
        {

            if (ItemCotizacion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemCotizacion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoTotal = Convert.ToInt32(ItemCotizacion.Material.Largo);
            Int32 AltoTotal = Convert.ToInt32(ItemCotizacion.Material.Alto);

            Int32 LargoPieza = Convert.ToInt32(ItemCotizacion.FormatoImpresionLargo);
            Int32 AltoPieza = Convert.ToInt32(ItemCotizacion.FormatoImpresionAlto);

            upbPrecorte.Width = LargoTotal;
            upbPrecorte.Height = AltoTotal;
            Bitmap b;
            b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            upbPrecorte.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(myPen, new Rectangle(0, 0, LargoTotal - 1, AltoTotal - 1));
            int CantidadPiezas = 0;
            for (int x = AltoPieza; x <= upbPrecorte.Width; x += AltoPieza)
            {
                for (int y = LargoPieza; y <= upbPrecorte.Height; y += LargoPieza)
                {
                    g.DrawRectangle(myPen, new Rectangle(x - AltoPieza, y - LargoPieza, AltoPieza, LargoPieza));
                    CantidadPiezas += 1;
                }
            }
            ItemCotizacion.NroPiezasPrecorte = CantidadPiezas;
            txtNroPiezasPrecorte.Value = CantidadPiezas;
        }

        private void uneCosto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.Costo = Convert.ToDecimal(uneCosto.Value);
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            Cotizacion.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void busListaPrecioMaterial_Search(object sender, EventArgs e)
        {

            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Cotizacion.ListaPreciosExistencia = (ListaPreciosExistencia)FrmSeleccionar.GetSelectedEntity(typeof(ListaPreciosExistencia), "Lista Precios Producto Servicio", " Activo = 1");
            busListaPrecioMaterial.Text = (Cotizacion.ListaPreciosExistencia != null) ? Cotizacion.ListaPreciosExistencia.Nombre : "";

        }

        private void busListaCostoMaquina_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Cotizacion.ListaCostosMaquina = (ListaCostosMaquina)FrmSeleccionar.GetSelectedEntity(typeof(ListaCostosMaquina), "Lista de Costos Máquina", " Activo = 1");
            busListaCostoMaquina.Text = (Cotizacion.ListaCostosMaquina != null) ? Cotizacion.ListaCostosMaquina.Nombre : "";
        }

        private void busListaPreciosTransporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Cotizacion.ListaPreciosTransporte = (ListaPreciosTransporte)FrmSeleccionar.GetSelectedEntity(typeof(ListaPreciosTransporte), "Lista Precios Transporte", " Activo = 1");
            busListaPreciosTransporte.Text = (Cotizacion.ListaPreciosTransporte != null) ? Cotizacion.ListaPreciosTransporte.Nombre : "";
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
                Cotizacion.Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");

                String filtro = "";
                if (Cotizacion.Moneda != null)
                {
                    if (Cotizacion.Moneda.Simbolo.Equals("US $"))
                    {
                        filtro = "IDMoneda='" + Cotizacion.Moneda.ID + "' and Fecha='" + Cotizacion.FechaCreacion.ToShortDateString() + "'";
                        XmlDocument XML = HelperNHibernate.ExecuteSQL("SELECT TipoCambioVenta FROM vSF_TipoCambio", filtro);
                        if (XML.HasChildNodes && XML.DocumentElement.ChildNodes.Count > 0)
                        {
                            Cotizacion.TipoCambioFecha = Convert.ToDecimal(XML.DocumentElement.ChildNodes[0].SelectSingleNode("@TipoCambioVenta").Value);
                        }
                        else 
                        {
                            throw new Exception("No hay tipo de cambio registrado a la fecha : " + Cotizacion.FechaCreacion.ToShortDateString());
                        }
                    }
                    else
                    {
                        Cotizacion.TipoCambioFecha = 1;
                    }
                }

                Mostrar();
            }
            catch (Exception ex)
            {

                Soft.Exceptions.SoftException.ShowException(ex);
            }


        }

        private void ssContacto_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarContacto = new FrmSelectedEntity();
            Cotizacion.Contacto = (ItemSocioNegocioContacto)FrmSeleccionarContacto.GetSelectedEntity(typeof(ItemSocioNegocioContacto), "Contacto", String.Format("IDSocioNegocio = '{0}'", Cotizacion.Cliente.ID));
            ssContacto.Text = (Cotizacion.Contacto != null) ? Cotizacion.Contacto.Nombre : "";
        }

        private void ssDireccionEntrega_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
            ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionEntrega = 1", Cotizacion.Cliente.ID));
            if (Direccion != null)
            {
                Cotizacion.DireccionEntrega = Direccion.Direccion;
                ssDireccionEntrega.Text = Direccion.Direccion;
            }
        }

        private void ssDireccionFactura_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
            ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionFacturacion = 1", Cotizacion.Cliente.ID));
            if (Direccion != null)
            {
                Cotizacion.DireccionFacturacion = Direccion.Direccion;
                ssDireccionFactura.Text = Direccion.Direccion;
            }
        }

        private void ubMostrarGraficoPrecorte_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.GraficoPrecorteGirado = false;
                GenerarGraficoPrecorteNormal();
                CalcularProduccionItem(ItemCotizacion);
                MostrarItem(utCotizacion.ActiveNode);

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubGirarGraficoPrecorte_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.GraficoPrecorteGirado = true;
                GenerarGraficoPrecorteRotado();
                CalcularProduccionItem(ItemCotizacion);

                MostrarItem(utCotizacion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubImprimirGraficoPrecorte_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap b = new Bitmap((Image)upbPrecorte.Image);
                String RutaGrafico = String.Format("{0}Grafico_Precorte_{1}.png", FrmMain.CarpetaImagenes, ItemCotizacion.ID);
                b.Save(RutaGrafico);
                Soft.Reporte.Entidades.Reporte Reporte = (Soft.Reporte.Entidades.Reporte)HelperNHibernate.GetEntityByField("Reporte", "Codigo", "VEN-0006");
                ParametroReporte Parametro = Reporte.Parametros[0];
                Parametro.Valor = RutaGrafico;
                PrintReport ControladorImpresion = new PrintReport();
                ControladorImpresion.m_ObjectFlow = Reporte;
                ControladorImpresion.Start();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void uneSeparacionX_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                if (ActualizandoIU) { return; }

                ItemCotizacion.SeparacionX = Convert.ToInt32(uneSeparacionX.Value);
                if (ItemCotizacion.GraficoImpresionGirado == true)
                {
                    GenerarGraficoImpresionRotado();
                }
                else
                {
                    GenerarGraficoImpresionNormal();
                }

                CalcularProduccionItem(ItemCotizacion);
                MostrarItem(utCotizacion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void uneSeparacionY_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                if (ActualizandoIU) { return; }

                ItemCotizacion.SeparacionY = Convert.ToInt32(uneSeparacionY.Value);
                if (ItemCotizacion.GraficoImpresionGirado == true)
                {
                    GenerarGraficoImpresionRotado();
                }
                else
                {
                    GenerarGraficoImpresionNormal();
                }
                CalcularProduccionItem(ItemCotizacion);
                MostrarItem(utCotizacion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubMostrarGraficoImpresion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion.MetodoImpresion == null) {
                    throw new Exception("Seleccione un tipo de metodo de impresion primero");
                }
                if (ItemCotizacion.NroPiezasPrecorte == 0)
                {
                    throw new Exception("No papel de precorte");
                }
                if (ItemCotizacion == null) { return; }
                if (ItemCotizacion.GraficoImpresionManual == false)
                {
                    ItemCotizacion.GraficoImpresionGirado = false;
                    GenerarGraficoImpresionNormal();
                }
                CalcularProduccionItem(ItemCotizacion);
                MostrarItem(utCotizacion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubGirarGraficoImpresion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.GraficoImpresionGirado = true;
                GenerarGraficoImpresionRotado();
                CalcularProduccionItem(ItemCotizacion);
                MostrarItem(utCotizacion.ActiveNode);

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubImprimirGraficoImpresion_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap b = new Bitmap((Image)upbImpresion.Image);
                String RutaGrafico = String.Format("{0}Grafico_Impresion_{1}.png", FrmMain.CarpetaImagenes, ItemCotizacion.ID);
                b.Save(RutaGrafico);
                Soft.Reporte.Entidades.Reporte Reporte = (Soft.Reporte.Entidades.Reporte)HelperNHibernate.GetEntityByField("Reporte", "Codigo", "VEN-0005");
                ParametroReporte Parametro = Reporte.Parametros[0];
                Parametro.Valor = RutaGrafico;
                PrintReport ControladorImpresion = new PrintReport();
                ControladorImpresion.m_ObjectFlow = Reporte;
                ControladorImpresion.Start();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarServicio();
            MostrarTotalAMM(ItemCotizacion);
        }

        private void ModificarServicio()
        {
            if (ugServicios.ActiveRow != null)
            {
                ItemCotizacionServicio itemCotizacionServicio = (ItemCotizacionServicio)ugServicios.ActiveRow.Tag;
                FrmCotizaciondeServicio AgregarServicio = new FrmCotizaciondeServicio(Cotizacion, itemCotizacionServicio, ItemCotizacion);
                ItemCotizacionServicio item = AgregarServicio.ObtenerServicio(Cotizacion, ItemCotizacion);
                if (item != null)
                {
                    ugServicios.ActiveRow.Tag = item;
                    ugServicios.ActiveRow.Cells[colTipoAMM].Activate();
                    MostrarAAM(ugServicios.ActiveRow);
                }
            }
            else
            {
                Exception ex = new Exception("Debe seleccionar un servicio para poder modificar");
                Soft.Exceptions.SoftException.ShowException(ex);
            }
        }

        private void ugServicios_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            ModificarServicio();
        }

        private void txtFormatoImpresionLargo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.FormatoImpresionLargo = Convert.ToDecimal(txtFormatoImpresionLargo.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtFormatoImpresionAlto_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.FormatoImpresionAlto = Convert.ToDecimal(txtFormatoImpresionAlto.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtDemasia_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.CantidadDemasia = Convert.ToDecimal(txtDemasia.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssMaquina_Clear(object sender, EventArgs e)
        {
            try
            {
                ItemCotizacion.Maquina = null;
                ssMaquina.Text = null;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void ssMaterial_Clear(object sender, EventArgs e)
        {
            try
            {
                ItemCotizacion.Material = null;
                ssMaterial.Text = null;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtCantidadProduccion_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.CantidadElemento = Convert.ToInt32(txtCantidadProduccion.Value);
        }

        private void txtDemasia_ValueChanged_1(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.CantidadDemasia = Convert.ToInt32(txtDemasia.Value);
            CalcularProduccionItem(ItemCotizacion);
            MostrarItem(utCotizacion.ActiveNode);
        }

        private void txtNroPiezasImpresion_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.NroPiezasImpresion = Convert.ToInt32(txtNroPiezasImpresion.Value);
            CalcularProduccionItem(ItemCotizacion);
            MostrarItem(utCotizacion.ActiveNode);
        }

        private void checkGraficoImpresionManual_CheckedChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.GraficoImpresionManual = checkGraficoImpresionManual.Checked;
            CalcularProduccionItem(ItemCotizacion);
            MostrarItem(utCotizacion.ActiveNode);
        }

        private void comboMedida_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.UnidadMedidaAbierta = comboMedida.Text;
            CalcularProduccionItem(ItemCotizacion);
            MostrarItem(utCotizacion.ActiveNode);
        }

        private void btnTodosM2Servicios_Click(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }



        }

        private void ActuallizarMetrosCuadrados()
        {

            foreach (ItemCotizacion item in Cotizacion.Items)
            {
                foreach (ItemCotizacionServicio itemservicio in item.Servicios)
                {

                    if (itemservicio.UnidadServicio.Unidad.Codigo.Equals("M2"))
                    {

                    }


                }

            }

        }

        private void busLineaProduccion_Search(object sender, EventArgs e)
        {


            try
            {

                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                LineaProduccion LineaProduccion = (LineaProduccion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(LineaProduccion), "Linea de Produccion");
                if ((LineaProduccion != null))
                {

                    Cotizacion.LineaProduccion = LineaProduccion;
                }
                Mostrar();
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void checkMuestraPrecioEnPresupuesto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                if (ActualizandoIU) { return; }
                ItemCotizacion.MuestraPrecioEnPresupuesto = checkMuestraPrecioEnPresupuesto.Checked;

            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void busMetodoImpresion_Search(object sender, EventArgs e)
        {

            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {

                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                MetodoImpresion metodoImpresion = (MetodoImpresion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(MetodoImpresion), "Metodo de Impresion");
                if ((metodoImpresion != null))
                {
                    ItemCotizacion.MetodoImpresion = metodoImpresion.Codigo;
                    ItemCotizacion.NumerodePases = metodoImpresion.FactorPases;
                    ItemCotizacion.NumeroCambios = metodoImpresion.FactorCambios;

                    ItemCotizacion.MetodoImpresionOffset = metodoImpresion;
                    CalcularProduccionItem(ItemCotizacion);
                    MostrarItem(utCotizacion.ActiveNode);
                }
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }
        }

        private void btnBORRAR_Click(object sender, EventArgs e)
        {

            try
            {

                if (ItemCotizacion == null) { return; }
                if (ActualizandoIU) { return; }

                Cotizacion.Items.Remove(ItemCotizacion);

                Mostrar();

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void checkPrecioItems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cotizacion.MuestraPrecioItemEnPresupuesto = checkPrecioItems.Checked;
            }
            catch (Exception ex)
            {
                
                SoftException.Control(ex);
            }
        }

        private void chekOcultaPreciosTotal_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Cotizacion.OcultaPrecioEnPresupuesto = chekOcultaPreciosTotal.Checked;
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion ItemCopia = ItemCotizacion.Copiar();
                Cotizacion.Items.Add(ItemCopia);
                MostrarItems();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNombre_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                if (ItemCotizacion == null) { return; }
                if (ActualizandoIU) { return; }
                ItemCotizacion.Nombre = txtNombre.Text;
                MostrarItem(utCotizacion.ActiveNode);

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void txtMedidaAnchoCaja_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (ItemCotizacion == null) { return; }
                ItemCotizacion.MedidaAnchoCaja = Convert.ToDecimal(txtMedidaAnchoCaja.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busLineaProduccion_Clear(object sender, EventArgs e)
        {
            try
            {
                Cotizacion.LineaProduccion = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }



        }

        private void labelSobranPaginas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion item = Cotizacion.AddItem();
                //CALCULAMOS LOS PLIEGOS SOBRANTES
                if (ItemCotizacion.PaginasSobrantes > 0)
                {



                    item.Activo = true;


                    item.TieneFondo = ItemCotizacion.TieneFondo;

                    item.TieneGraficos = ItemCotizacion.TieneGraficos;
                    item.TieneMaquina = ItemCotizacion.TieneMaquina;
                    item.TieneMaterial = ItemCotizacion.TieneMaterial;

                    item.TieneMedidaAbierta = ItemCotizacion.TieneMedidaAbierta;
                    item.TieneMedidaCerrada = ItemCotizacion.TieneMedidaCerrada;
                    item.TieneTipoUnidad = ItemCotizacion.TieneTipoUnidad;
                    item.TieneTiraRetira = ItemCotizacion.TieneTiraRetira;



                    item.Nombre = "1 pliego de " + ItemCotizacion.PaginasSobrantes+ "paginas sobrantes";
                    item.Cantidad = ItemCotizacion.Cantidad;
                    item.CantidadUnidad = ItemCotizacion.PaginasSobrantes;
                    item.PaginasSobrantes = 0;

                    item.TipoUnidad = ItemCotizacion.TipoUnidad;
                    item.MedidaAbiertaAlto = ItemCotizacion.MedidaAbiertaAlto;
                    item.MedidaAbiertaLargo = ItemCotizacion.MedidaAbiertaLargo;
                    item.MedidaCerradaAlto = ItemCotizacion.MedidaCerradaAlto;
                    item.MedidaCerradaLargo = ItemCotizacion.MedidaCerradaLargo;
                    item.UnidadMedidaAbierta = ItemCotizacion.UnidadMedidaAbierta;
                    
                    item.ImpresoTiraColor = ItemCotizacion.ImpresoTiraColor;
                    item.ImpresoRetiraColor = ItemCotizacion.ImpresoRetiraColor;
                    item.TipoUnidad = ItemCotizacion.TipoUnidad;
                    item.CantidadElemento = ItemCotizacion.CantidadElemento;
                    item.Operacion = ItemCotizacion.Operacion;
                    item.Maquina = ItemCotizacion.Maquina;
                    item.Material = ItemCotizacion.Material;
                    item.FormatoImpresionLargo = ItemCotizacion.FormatoImpresionLargo;
                    item.FormatoImpresionAlto = ItemCotizacion.FormatoImpresionAlto;


                    item.MetodoImpresion = ItemCotizacion.MetodoImpresion;
                    item.MetodoImpresionOffset = ItemCotizacion.MetodoImpresionOffset;


                    ItemCotizacion.PaginasSobrantes = 0;
                    ItemCotizacion.CantidadUnidad -= ItemCotizacion.PaginasSobrantes;

                    Mostrar();



                    
                }
            }
            catch (Exception)
            {
                
                throw;
            }


        }

        private void uceIncluirEnPresupuesto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemCotizacion == null) { return; }
                ItemCotizacion.IncluirEnPrespuesto = uceIncluirEnPresupuesto.Checked;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }
    }
}
