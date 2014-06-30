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
using Soft.Produccion.Win;
using System.Xml;

namespace Soft.Produccion.Win
{
    public partial class FrmOrdenProduccion : FrmParent
    {
        public FrmOrdenProduccion()
        {
            InitializeComponent();
        }

        public OrdenProduccion OrdenProduccion { get { return (OrdenProduccion)base.m_ObjectFlow; } }
        private ItemOrdenProduccion ItemOrdenProduccion = null;

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const String colServicio = "Servicio";
        const String colServicioCantidad = "Cantidad Servicio";

        const String colMaterial = "Material";
        const String colMaterialCantidad = "Cantidad Material";


        const String colMaquina = "Máquina";
        const String colMaquinaCantidad = "Cantidad Maquina";


        private Boolean ActualizandoIU = false;

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colServicio);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colServicioCantidad);
            column.DataType = typeof(Decimal);

            column = columns.Columns.Add(colMaterial);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colMaterialCantidad);
            column.DataType = typeof(Decimal);


            column = columns.Columns.Add(colMaquina);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colMaquinaCantidad);
            column.DataType = typeof(Decimal);


            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colServicio].Width = 100;
            ugServicios.DisplayLayout.Bands[0].Columns[colServicioCantidad].Width = 50;

            ugServicios.DisplayLayout.Bands[0].Columns[colMaterial].Width = 100;
            ugServicios.DisplayLayout.Bands[0].Columns[colMaterialCantidad].Width = 50;

            ugServicios.DisplayLayout.Bands[0].Columns[colMaquina].Width = 100;
            ugServicios.DisplayLayout.Bands[0].Columns[colMaquinaCantidad].Width = 50;


            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            ssTipoDocumento.Text = (OrdenProduccion.TipoDocumento != null) ? OrdenProduccion.TipoDocumento.Descripcion : "";
            ssCliente.Text = (OrdenProduccion.Cliente != null) ? OrdenProduccion.Cliente.Nombre : "";
            ssResponsable.Text = (OrdenProduccion.Responsable != null) ? OrdenProduccion.Responsable.Nombre : "";
            ssVendedor.Text = (OrdenProduccion.Vendedor != null) ? OrdenProduccion.Vendedor.Nombre : "";
            ssCotizador.Text = (OrdenProduccion.Cotizador != null) ? OrdenProduccion.Cotizador.Nombre : "";

            ssContacto.Text = (OrdenProduccion.Contacto != null) ? OrdenProduccion.Contacto.Nombre : "";
            ssDireccionEntrega.Text = OrdenProduccion.DireccionEntrega;
            ssDireccionFactura.Text = OrdenProduccion.DireccionFacturacion;
            txtNumeracion.Text = OrdenProduccion.Numeracion;
            udtFechaCreacion.Value = OrdenProduccion.FechaCreacion;
            txtObservacion.Text = OrdenProduccion.Observacion;
            txtDescripcion.Text = OrdenProduccion.Descripcion;
            uneCantidad.Value = OrdenProduccion.Cantidad;
            txtNumeracion.Text = OrdenProduccion.Numeracion;

            udtFechaTentativaEntrega.Value = OrdenProduccion.FechaTentativaEntrega;
            cboPrioridad.Text = OrdenProduccion.Prioridad;

            MostrarCotizacionPresupuesto();
            busListaCostoMaquina.Text = (OrdenProduccion.ListaCostosMaquina != null) ? OrdenProduccion.ListaCostosMaquina.Nombre : "";
            busListaPrecioMaterial.Text = (OrdenProduccion.ListaPreciosExistencia != null) ? OrdenProduccion.ListaPreciosExistencia.Nombre : "";
            busListaPreciosTransporte.Text = (OrdenProduccion.ListaPreciosTransporte != null) ? OrdenProduccion.ListaPreciosTransporte.Nombre : "";
            busLineaProduccion.Text = (OrdenProduccion.LineaProduccion != null) ? OrdenProduccion.LineaProduccion.Nombre : "";

            MostrarItems();
            ActualizandoIU = false;
        }


        private void MostrarCotizacionPresupuesto()
        {
            String Filtro = String.Format(" ID ='{0}'", OrdenProduccion.ID);
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_PresupuestoCotizacionDesdeOP", Filtro);
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    txtCotizacion.Text = NodoItem.SelectSingleNode("@Cotizacion").Value;
                    txtPresupuesto.Text = NodoItem.SelectSingleNode("@Presupuesto").Value;
                }
            }
        }

        public void MostrarItems()
        {
            utOrdenProduccion.Nodes.Clear();

            foreach (ItemOrdenProduccion Item in OrdenProduccion.Items)
            {
                UltraTreeNode Node = new UltraTreeNode();
                Node.Tag = Item;
                Node.Text = Item.Nombre;
                utOrdenProduccion.Nodes.Add(Node);
            }
            if (utOrdenProduccion.Nodes.Count > 0)
            {
                utOrdenProduccion.ActiveNode = utOrdenProduccion.Nodes[0];
                utOrdenProduccion.Nodes[0].Selected = true;
            }
            utOrdenProduccion.ExpandAll();
        }

        public void MostrarItem(UltraTreeNode Node)
        {
            ActualizandoIU = true;
            ItemOrdenProduccion Item = (ItemOrdenProduccion)Node.Tag;
            ItemOrdenProduccion = Item;
            GrupoMedidaAbierta.Visible = Item.TieneMedidaAbierta;
            GrupoMedidaCerrada.Visible = Item.TieneMedidaCerrada;
            GruposTiras.Visible = Item.TieneTiraRetira;
            ssMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
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
            


            comboMedida.Text = Item.UnidadMedidaAbierta;
            lblTipoUnidad.Visible = Item.TieneTipoUnidad;
            txtCantidadItem.Visible = Item.TieneTipoUnidad;
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
            ubeMetodo.Text = Item.MetodoImpresion;
            //}


            checkGraficoImpresionManual.Checked = Item.GraficoImpresionManual;

            utcItemOrdenProduccion.Tabs["Graficos"].Visible = Item.TieneGraficos;
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


            MostrarServicios(Item);
            ActualizandoIU = false;
        }

        public void MostrarServicios(ItemOrdenProduccion ItemCotizacion)
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemOrdenProduccionServicio Item in ItemOrdenProduccion.Servicios)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarServicio(Row);
            }

            MostrarTotalServicio(ItemOrdenProduccion);
        }

        public void MostrarServicio(UltraGridRow Row)
        {
            ItemOrdenProduccionServicio Item = (ItemOrdenProduccionServicio)Row.Tag;
            Row.Cells[colServicio].Activation = (Item.Servicio != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colServicio].Value = (Item.Servicio != null) ? Item.Servicio.Nombre : "";

            Row.Cells[colServicioCantidad].Activation = (Item.Servicio != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colServicioCantidad].Value = (Item.Servicio != null) ? Item.CantidadServicio : 0;

            Row.Cells[colMaterial].Activation = (Item.Material != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaterial].Value = (Item.Material != null) ? Item.Material.Nombre : "";

            Row.Cells[colMaterialCantidad].Activation = (Item.Material != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaterialCantidad].Value = (Item.Material != null) ? Item.CantidadMaterial : 0;

            Row.Cells[colMaquina].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquina].Value = (Item.Maquina != null) ? Item.Maquina.Nombre : "";

            Row.Cells[colMaquinaCantidad].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquinaCantidad].Value = (Item.Maquina != null) ? Item.CantidadMaquina : 0;
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
            utcItemOrdenProduccion.Enabled = false;
        }

        public void AgregarServicios(String Codigo, String Descripcion, UltraGridRow Row)
        {
            Collection Productos = new Collection();
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemOrdenProduccionServicio Item = (ItemOrdenProduccionServicio)Row.Tag;
            String Filtro = String.Format(" Codigo LIKE '{0}%' AND Nombre LIKE '{1}%'", Codigo, Descripcion);
            Productos = FrmSeleccionar.GetSelectedsEntities(typeof(Existencia), "Selección de Servicios", Filtro);
            if (Productos.Count == 1)
            {
                Existencia Servicio = (Existencia)Productos[1];
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Servicio.ID);
                //Item.CantidadFinal = 1;
                MostrarServicio(Row);
            }
            else if (Productos.Count > 1)
            {
                Existencia Producto = (Existencia)Productos[1];
                Item.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Producto.ID);
                MostrarServicio(Row);
                for (int i = 2; i <= Productos.Count; i++)
                {
                    UltraGridRow RowNuevo = ugServicios.DisplayLayout.Bands[0].AddNew();
                    ItemOrdenProduccionServicio ItemNuevo = ItemOrdenProduccion.AddServicio();
                    Existencia ProductoNuevo = (Existencia)Productos[i];
                    ItemNuevo.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ProductoNuevo.ID);
                    RowNuevo.Tag = ItemNuevo;
                    MostrarServicio(RowNuevo);
                }
            }
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
            TipoCotizacion TipoDocumento = (TipoCotizacion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoCotizacion), "Tipo de Cotización");

            if ((OrdenProduccion.TipoDocumento == null) || (OrdenProduccion.TipoDocumento.Codigo != TipoDocumento.Codigo))
            {
                OrdenProduccion.TipoDocumento = (TipoCotizacion)HelperNHibernate.GetEntityByID("TipoCotizacion", TipoDocumento.ID);

                try
                {
                    FrmSelectedEntity FrmSeleccionarEmpleado = new FrmSelectedEntity();
                    String filtro = "IDUsuario='" + FrmMain.Usuario.ID + "'";
                    SocioNegocio sn = (SocioNegocio)FrmSeleccionarEmpleado.GetSelectedEntity(typeof(SocioNegocio), "Empleado", filtro);

                    OrdenProduccion.Responsable = (SocioNegocio)HelperNHibernate.GetEntityByID("SocioNegocio", sn.ID);
                }
                catch (Exception)
                {
                }


            }
            Mostrar();
            /*ssTipoDocumento.Text = (OrdenProduccion.TipoDocumento != null) ? OrdenProduccion.TipoDocumento.Descripcion : "";
            ssResponsable.Text = (OrdenProduccion.Responsable != null) ? OrdenProduccion.Responsable.Nombre : "";*/
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            OrdenProduccion.Numeracion = txtNumeracion.Text;
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
            OrdenProduccion.Cliente = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Cliente = 1");
            ssCliente.Text = (OrdenProduccion.Cliente != null) ? OrdenProduccion.Cliente.Nombre : "";
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            OrdenProduccion.Descripcion = txtDescripcion.Text;
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            OrdenProduccion.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssResponsable.Text = (OrdenProduccion.Responsable != null) ? OrdenProduccion.Responsable.Nombre : "";
        }

        private void ssVendedor_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarVendedor = new FrmSelectedEntity();
            OrdenProduccion.Vendedor = (SocioNegocio)FrmSeleccionarVendedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            ssVendedor.Text = (OrdenProduccion.Vendedor != null) ? OrdenProduccion.Vendedor.Nombre : "";
        }

        private void uneCantidad_ValueChanged(object sender, EventArgs e)
        {
            OrdenProduccion.Cantidad = Convert.ToInt32(uneCantidad.Value);
        }

        private void utCotizacion_AfterSelect(object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {
            UltraTreeNode Node = utOrdenProduccion.ActiveNode;
            if (Node != null)
            {
                ItemOrdenProduccion = (ItemOrdenProduccion)Node.Tag;
                utcItemOrdenProduccion.Tabs[0].Text = Node.Text;
                MostrarItem(Node);
            }
        }

        private void txtMedidaAbiertoLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.MedidaAbiertaLargo = Convert.ToDecimal(txtMedidaAbiertoLargo.Value);
            txtImpresionLargo.Value = ItemOrdenProduccion.MedidaAbiertaLargo;
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.MedidaAbiertaAlto = Convert.ToDecimal(txtMedidaAbiertoAlto.Value);
            txtImpresionAlto.Value = ItemOrdenProduccion.MedidaAbiertaAlto;
        }

        private void txtMedidaCerradaLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.MedidaCerradaLargo = Convert.ToDecimal(txtMedidaCerradaLargo.Value);
        }

        private void txtMedidaCerradaAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.MedidaCerradaAlto = Convert.ToDecimal(txtMedidaCerradaAlto.Value);
        }

        private void txtImpresoTiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.ImpresoTiraColor = Convert.ToInt32(txtImpresoTiraColor.Value);
        }

        private void txtCantidadItem_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.CantidadUnidad = Convert.ToInt32(txtCantidadItem.Value);
        }

        private void txtImpresoRetiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.ImpresoRetiraColor = Convert.ToInt32(txtImpresoRetiraColor.Value);
        }

        private void ssMaquina_Search(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            String filtro = "";
            if (ItemOrdenProduccion.Operacion != null)
            {
                filtro = "IDOperacion='" + ItemOrdenProduccion.Operacion.ID + "'";
            }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemOrdenProduccion.Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Maquina Operacion", filtro);

            if (ItemOrdenProduccion.Maquina != null)
            {
                ItemOrdenProduccion.Maquina = (Maquina)HelperNHibernate.GetEntityByID("Maquina", ItemOrdenProduccion.Maquina.ID);
                ItemOrdenProduccion.FormatoImpresionAlto = ItemOrdenProduccion.Maquina.PliegoAltoMaximo;
                ItemOrdenProduccion.FormatoImpresionLargo = ItemOrdenProduccion.Maquina.PliegoAnchoMaximo;
                txtFormatoImpresionAlto.Value = ItemOrdenProduccion.FormatoImpresionAlto;
                txtFormatoImpresionLargo.Value = ItemOrdenProduccion.FormatoImpresionLargo;
            }
            ssMaquina.Text = (ItemOrdenProduccion.Maquina != null) ? ItemOrdenProduccion.Maquina.Nombre : "";
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            string filtro = "EsInventariable = 1";
            if (ssMaterial.Text.Length > 0)
            {
                filtro += " and Nombre like '%" + ssMaterial.Text + "%'";
            }

            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemOrdenProduccion.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", filtro);
            if (ItemOrdenProduccion.Material != null)
            {
                ItemOrdenProduccion.Material = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ItemOrdenProduccion.Material.ID);
            }
            ssMaterial.Text = (ItemOrdenProduccion.Material != null) ? ItemOrdenProduccion.Material.Nombre : "";
        }

        private void txtObservacionItem_TextChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.Observacion = txtObservacionItem.Text;
        }

        private void ubNuevoServicio_Click(object sender, EventArgs e)
        {
            FrmOrdenProducciondeServicio AgregarServicio = new FrmOrdenProducciondeServicio();
            ItemOrdenProduccionServicio item = AgregarServicio.ObtenerServicio(OrdenProduccion, ItemOrdenProduccion);
            if (item != null)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = item;
                Row.Cells[colServicio].Activate();
                ugServicios.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                ItemOrdenProduccion.Servicios.Add(item);
                MostrarTotalServicio(ItemOrdenProduccion);
                MostrarServicio(Row);
            }

        }


        private void MostrarTotalServicio(ItemOrdenProduccion itemSe)
        {

            Decimal total = 0;
            foreach (ItemOrdenProduccionServicio itemServicio in itemSe.Servicios)
            {
                total += itemServicio.CostoTotalServicio;
            }
            itemSe.CostoServicio = total;
            
        }


        private void ubEliminarServicio_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null) { return; }
            ItemOrdenProduccion.Servicios.Remove((ItemOrdenProduccionServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
            MostrarTotalServicio(ItemOrdenProduccion);

        }

        public void ugServicios_CellKeyEnter(UltraGridCell Cell)
        {
            try
            {
                if (Cell == null || ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccionServicio Item = (ItemOrdenProduccionServicio)Cell.Row.Tag;
                switch (Cell.Column.Key)
                {
                    case colServicio:
                        if (Cell.Text.Equals("")) { break; }
                        AgregarServicios("%", Cell.Text, Cell.Row);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            OrdenProduccion.Observacion = txtObservacion.Text;
        }

   
        ListaCostosMaquina lcm = null;
        ListaPreciosExistencia lpe = null;
        ListaPreciosTransporte lpt = null;
        Boolean acatualizalistas = true;
   
  


        private void CalcularProduccionItem(ItemOrdenProduccion itemcosteado)
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

                    itemcosteado.CantidadMaterial = Math.Round((itemcosteado.CantidadElemento * (largo * alto)), 0);
                    itemcosteado.CantidadDemasiaMaterial = itemcosteado.CantidadDemasia;



                    itemcosteado.CantidadProduccion = itemcosteado.CantidadMaterial + itemcosteado.CantidadDemasiaMaterial;

                    //itemcosteado.CantidadMaterial += itemcosteado.CantidadDemasia;
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






                    if (itemcosteado.CantidadUnidad == 0)
                    {
                        itemcosteado.NumeroPliegos = 1;
                    }
                    else
                    {
                        Decimal pliegos = itemcosteado.CantidadUnidad / (itemcosteado.NroPiezasImpresion * 2);
                        Decimal entero = Math.Truncate(pliegos);
                        Decimal paginasresiduo = entero - pliegos;

                        itemcosteado.NumeroPliegos = Convert.ToInt32(entero);

                        if (itemcosteado.NumeroPliegos == 0)
                        {
                            itemcosteado.NumeroPliegos = 1;
                        }
                    }




                    itemcosteado.CantidadProduccion = (itemcosteado.CantidadMaterial + itemcosteado.CantidadDemasiaMaterial) * itemcosteado.NumerodePases * itemcosteado.NroPiezasPrecorte * pases;


                }
            }
            catch (Exception)
            {
                //SoftException.ShowException(ex);
            }

        }




        public void GenerarGraficoImpresionNormal()
        {
            if (ItemOrdenProduccion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoGrafico = 0;
            Int32 AltoGrafico = 0;
            Int32 LargoPictureBox = 0;
            Int32 AltoPictureBox = 0;

            if (ItemOrdenProduccion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox / 2;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }
            else if (ItemOrdenProduccion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox / 2;
            }
            else
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }

            Int32 LargoPieza = Convert.ToInt32(ItemOrdenProduccion.MedidaAbiertaLargo) * 10;
            Int32 AltoPieza = Convert.ToInt32(ItemOrdenProduccion.MedidaAbiertaAlto) * 10;


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
                    //y += ItemOrdenProduccion.SeparacionY / 10;
                    y += ItemOrdenProduccion.SeparacionY;
                }
                //x += ItemOrdenProduccion.SeparacionX / 10;
                if (MargenFin == false)
                {
                    //x += 10;
                    MargenFin = true;
                }
                x += ItemOrdenProduccion.SeparacionX;
            }

            if (ItemOrdenProduccion.MetodoImpresion.Equals("TIRA Y RETIRA"))
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
            else if (ItemOrdenProduccion.MetodoImpresion.Equals("CONTRAPINZA"))
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



            ItemOrdenProduccion.NroPiezasImpresion = CantidadPiezas;
            txtNroPiezasImpresion.Value = CantidadPiezas;
        }

        public void GenerarGraficoImpresionRotado()
        {
            if (ItemOrdenProduccion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoGrafico = 0;
            Int32 AltoGrafico = 0;
            Int32 LargoPictureBox = 0;
            Int32 AltoPictureBox = 0;

            if (ItemOrdenProduccion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox / 2;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }
            else if (ItemOrdenProduccion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox / 2;
            }
            else
            {
                LargoPictureBox = Math.Max(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto));
                LargoGrafico = LargoPictureBox;
                AltoPictureBox = Math.Min(Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto), Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo));
                AltoGrafico = AltoPictureBox;
            }

            Int32 LargoPieza = Convert.ToInt32(ItemOrdenProduccion.MedidaAbiertaLargo);
            Int32 AltoPieza = Convert.ToInt32(ItemOrdenProduccion.MedidaAbiertaAlto);




            //ELEVAMOS 10 VECES MAS
            LargoPieza = LargoPieza * 10;
            AltoPieza = AltoPieza * 10;


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
                    //y += ItemOrdenProduccion.SeparacionY / 10;
                    y += ItemOrdenProduccion.SeparacionY;

                }
                //x += ItemOrdenProduccion.SeparacionX / 10;
                x += ItemOrdenProduccion.SeparacionX;

            }

            if (ItemOrdenProduccion.MetodoImpresion.Equals("TIRA Y RETIRA"))
            {
                Font Font = new System.Drawing.Font("Arial Narrow", 80, FontStyle.Regular);
                Brush Brush = new SolidBrush(System.Drawing.Color.Red);
                Pen Pen = new Pen(System.Drawing.Color.Red, 3);

                g.DrawImage((Image)upbImpresion.Image, LargoGrafico, 0);
                g.DrawLine(Pen, LargoGrafico, 0, LargoGrafico, AltoGrafico);
                g.DrawString("T", Font, Brush, (LargoGrafico / 2) - 10, (AltoGrafico / 2) - 10);
                g.DrawString("R", Font, Brush, ((LargoGrafico / 2) * 3) - 10, (AltoGrafico / 2) - 10);

                CantidadPiezas = CantidadPiezas * 2;
            }
            else if (ItemOrdenProduccion.MetodoImpresion.Equals("CONTRAPINZA"))
            {
                Font Font = new System.Drawing.Font("Arial Narrow", 80, FontStyle.Regular);
                Brush Brush = new SolidBrush(System.Drawing.Color.Red);
                Pen Pen = new Pen(System.Drawing.Color.Red, 3);

                g.DrawImage((Image)upbImpresion.Image, 0, AltoGrafico);
                g.DrawLine(Pen, 0, AltoGrafico, LargoGrafico, AltoGrafico);
                g.DrawString("T", Font, Brush, (LargoGrafico / 2) - 10, (AltoGrafico / 2) - 10);
                g.DrawString("R", Font, Brush, (LargoGrafico / 2) - 10, ((AltoGrafico / 2) * 3) - 10);

                CantidadPiezas = CantidadPiezas * 2;
            }

            g.DrawRectangle(MyPen, new Rectangle(0, 0, upbImpresion.Width - 1, upbImpresion.Height - 1));

            upbImpresion.Width = upbImpresion.Width / 6;
            upbImpresion.Height = upbImpresion.Height / 6;


            ItemOrdenProduccion.NroPiezasImpresion = CantidadPiezas;
            txtNroPiezasImpresion.Value = CantidadPiezas;
        }

        public void GenerarGraficoPrecorteNormal()
        {

            if (ItemOrdenProduccion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoTotal = Convert.ToInt32(ItemOrdenProduccion.Material.Largo);
            Int32 AltoTotal = Convert.ToInt32(ItemOrdenProduccion.Material.Alto);

            Int32 LargoPieza = Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo);
            Int32 AltoPieza = Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto);

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
            ItemOrdenProduccion.NroPiezasPrecorte = CantidadPiezas;
            txtNroPiezasPrecorte.Value = CantidadPiezas;
        }

        public void GenerarGraficoPrecorteRotado()
        {

            if (ItemOrdenProduccion.MedidaAbiertaLargo == 0) { MessageBox.Show("No se ha asignado el largo de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.MedidaAbiertaAlto == 0) { MessageBox.Show("No se ha asignado el alto de la medida abierta", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Material == null) { MessageBox.Show("No se ha asignado ningún material.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            if (ItemOrdenProduccion.Maquina == null) { MessageBox.Show("No se ha asignado ninguna máquina.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            Int32 LargoTotal = Convert.ToInt32(ItemOrdenProduccion.Material.Largo);
            Int32 AltoTotal = Convert.ToInt32(ItemOrdenProduccion.Material.Alto);

            Int32 LargoPieza = Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionLargo);
            Int32 AltoPieza = Convert.ToInt32(ItemOrdenProduccion.FormatoImpresionAlto);

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
            ItemOrdenProduccion.NroPiezasPrecorte = CantidadPiezas;
            txtNroPiezasPrecorte.Value = CantidadPiezas;
        }



        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            OrdenProduccion.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void busListaPrecioMaterial_Search(object sender, EventArgs e)
        {

            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            OrdenProduccion.ListaPreciosExistencia = (ListaPreciosExistencia)FrmSeleccionar.GetSelectedEntity(typeof(ListaPreciosExistencia), "Lista Precios Producto Servicio", " Activo = 1");
            busListaPrecioMaterial.Text = (OrdenProduccion.ListaPreciosExistencia != null) ? OrdenProduccion.ListaPreciosExistencia.Nombre : "";

        }

        private void busListaCostoMaquina_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            OrdenProduccion.ListaCostosMaquina = (ListaCostosMaquina)FrmSeleccionar.GetSelectedEntity(typeof(ListaCostosMaquina), "Lista de Costos Máquina", " Activo = 1");
            busListaCostoMaquina.Text = (OrdenProduccion.ListaCostosMaquina != null) ? OrdenProduccion.ListaCostosMaquina.Nombre : "";
        }

        private void busListaPreciosTransporte_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            OrdenProduccion.ListaPreciosTransporte = (ListaPreciosTransporte)FrmSeleccionar.GetSelectedEntity(typeof(ListaPreciosTransporte), "Lista Precios Transporte", " Activo = 1");
            busListaPreciosTransporte.Text = (OrdenProduccion.ListaPreciosTransporte != null) ? OrdenProduccion.ListaPreciosTransporte.Nombre : "";
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarMoneda = new FrmSelectedEntity();
                OrdenProduccion.Moneda = (Moneda)FrmSeleccionarMoneda.GetSelectedEntity(typeof(Moneda), "Moneda");

                String filtro = "";
                if (OrdenProduccion.Moneda != null)
                {
                    if (OrdenProduccion.Moneda.Simbolo.Equals("US $"))
                    {
                        filtro = "IDMoneda='" + OrdenProduccion.Moneda.ID + "' and Fecha='" + OrdenProduccion.FechaCreacion.Date + "'";
                        FrmSelectedEntity FrmSelectedMoneda = new FrmSelectedEntity();
                        TipoCambio tc = (TipoCambio)FrmSelectedMoneda.GetSelectedEntity(typeof(TipoCambio), "Tipo de Cambio", filtro);
                        OrdenProduccion.TipoCambioFecha = tc.TipoCambioVenta;
                    }
                    else
                    {
                        OrdenProduccion.TipoCambioFecha = 1;
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
            OrdenProduccion.Contacto = (ItemSocioNegocioContacto)FrmSeleccionarContacto.GetSelectedEntity(typeof(ItemSocioNegocioContacto), "Contacto", String.Format("IDSocioNegocio = '{0}'", OrdenProduccion.Cliente.ID));
            ssContacto.Text = (OrdenProduccion.Contacto != null) ? OrdenProduccion.Contacto.Nombre : "";
        }

        private void ssDireccionEntrega_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
            ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionEntrega = 1", OrdenProduccion.Cliente.ID));
            if (Direccion != null)
            {
                OrdenProduccion.DireccionEntrega = Direccion.Direccion;
                ssDireccionEntrega.Text = Direccion.Direccion;
            }
        }

        private void ssDireccionFactura_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
            ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionFacturacion = 1", OrdenProduccion.Cliente.ID));
            if (Direccion != null)
            {
                OrdenProduccion.DireccionFacturacion = Direccion.Direccion;
                ssDireccionFactura.Text = Direccion.Direccion;
            }
        }

        private void ubMostrarGraficoPrecorte_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccion.GraficoPrecorteGirado = false;
                GenerarGraficoPrecorteNormal();
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);

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
                if (ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccion.GraficoPrecorteGirado = true;
                GenerarGraficoPrecorteRotado();
                CalcularProduccionItem(ItemOrdenProduccion);

                MostrarItem(utOrdenProduccion.ActiveNode);
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
                String RutaGrafico = String.Format("{0}Grafico_Precorte_{1}.png", FrmMain.CarpetaImagenes, ItemOrdenProduccion.ID);
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
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }

                ItemOrdenProduccion.SeparacionX = Convert.ToInt32(uneSeparacionX.Value);
                if (ItemOrdenProduccion.GraficoImpresionGirado == true)
                {
                    GenerarGraficoImpresionRotado();
                }
                else
                {
                    GenerarGraficoImpresionNormal();
                }

                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
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
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }

                ItemOrdenProduccion.SeparacionY = Convert.ToInt32(uneSeparacionY.Value);
                if (ItemOrdenProduccion.GraficoImpresionGirado == true)
                {
                    GenerarGraficoImpresionRotado();
                }
                else
                {
                    GenerarGraficoImpresionNormal();
                }
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
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
                if (ItemOrdenProduccion == null) { return; }

                if (ItemOrdenProduccion.GraficoImpresionManual == false)
                {
                    ItemOrdenProduccion.GraficoImpresionGirado = false;
                    GenerarGraficoImpresionNormal();
                }


                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
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
                if (ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccion.GraficoImpresionGirado = true;
                GenerarGraficoImpresionRotado();
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);

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
                String RutaGrafico = String.Format("{0}Grafico_Impresion_{1}.png", FrmMain.CarpetaImagenes, ItemOrdenProduccion.ID);
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
            MostrarTotalServicio(ItemOrdenProduccion);
        }

        private void ModificarServicio()
        {
            if (ugServicios.ActiveRow != null)
            {
                ItemOrdenProduccionServicio ItemOrdenProduccionServicio = (ItemOrdenProduccionServicio)ugServicios.ActiveRow.Tag;
                FrmOrdenProducciondeServicio AgregarServicio = new FrmOrdenProducciondeServicio(OrdenProduccion, ItemOrdenProduccionServicio, ItemOrdenProduccion);
                ItemOrdenProduccionServicio item = AgregarServicio.ObtenerServicio(OrdenProduccion, ItemOrdenProduccion);
                if (item != null)
                {
                    ugServicios.ActiveRow.Tag = item;
                    ugServicios.ActiveRow.Cells[colServicio].Activate();
                    MostrarServicio(ugServicios.ActiveRow);
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

        private void ubeMetodo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }
                ItemOrdenProduccion.MetodoImpresion = ubeMetodo.Text;
                ItemOrdenProduccion.NumerodePases = Convert.ToInt32(ubeMetodo.SelectedItem.Tag);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void txtFormatoImpresionLargo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccion.FormatoImpresionLargo = Convert.ToDecimal(txtFormatoImpresionLargo.Value);
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
                if (ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccion.FormatoImpresionAlto = Convert.ToDecimal(txtFormatoImpresionAlto.Value);
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
                if (ItemOrdenProduccion == null) { return; }
                ItemOrdenProduccion.CantidadDemasia = Convert.ToDecimal(txtDemasia.Value);
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
                ItemOrdenProduccion.Maquina = null;
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
                ItemOrdenProduccion.Material = null;
                ssMaterial.Text = null;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtCantidadProduccion_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.CantidadElemento = Convert.ToInt32(txtCantidadProduccion.Value);
        }

        private void txtDemasia_ValueChanged_1(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.CantidadDemasia = Convert.ToInt32(txtDemasia.Value);
            CalcularProduccionItem(ItemOrdenProduccion);
            MostrarItem(utOrdenProduccion.ActiveNode);
        }

        private void txtNroPiezasImpresion_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.NroPiezasImpresion = Convert.ToInt32(txtNroPiezasImpresion.Value);
            CalcularProduccionItem(ItemOrdenProduccion);
            MostrarItem(utOrdenProduccion.ActiveNode);
        }

        private void checkGraficoImpresionManual_CheckedChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.GraficoImpresionManual = checkGraficoImpresionManual.Checked;
            CalcularProduccionItem(ItemOrdenProduccion);
            MostrarItem(utOrdenProduccion.ActiveNode);
        }

        private void comboMedida_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemOrdenProduccion.UnidadMedidaAbierta = comboMedida.Text;
            CalcularProduccionItem(ItemOrdenProduccion);
            MostrarItem(utOrdenProduccion.ActiveNode);
        }

        private void btnTodosM2Servicios_Click(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }



        }


        private void ActuallizarMetrosCuadrados()
        {

            foreach (ItemOrdenProduccion item in OrdenProduccion.Items)
            {
                foreach (ItemOrdenProduccionServicio itemservicio in item.Servicios)
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
                if ((OrdenProduccion.LineaProduccion == null))
                {

                    OrdenProduccion.LineaProduccion = LineaProduccion;
                }
                Mostrar();
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void ssCotizador_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
                OrdenProduccion.Cotizador = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }  
        }

        private void udtFechaTentativaEntrega_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.FechaTentativaEntrega = Convert.ToDateTime(udtFechaTentativaEntrega.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void cboPrioridad_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.Prioridad = cboPrioridad.Text;
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }






        public override void Aceptar()
        {
            try
            {
                GenerarImagenes();
                base.Aceptar();
            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }
        }


        public void MostrarItem(ItemOrdenProduccion Item)
        {
            ActualizandoIU = true;
            //ItemOrdenProduccion Item = (ItemOrdenProduccion)Node.Tag;
            ItemOrdenProduccion = Item;
            GrupoMedidaAbierta.Visible = Item.TieneMedidaAbierta;
            GrupoMedidaCerrada.Visible = Item.TieneMedidaCerrada;
            GruposTiras.Visible = Item.TieneTiraRetira;
            ssMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
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



            comboMedida.Text = Item.UnidadMedidaAbierta;
            lblTipoUnidad.Visible = Item.TieneTipoUnidad;
            txtCantidadItem.Visible = Item.TieneTipoUnidad;
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
            ubeMetodo.Text = Item.MetodoImpresion;
            //}


            checkGraficoImpresionManual.Checked = Item.GraficoImpresionManual;

            utcItemOrdenProduccion.Tabs["Graficos"].Visible = Item.TieneGraficos;
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


            MostrarServicios(Item);
            ActualizandoIU = false;
        }

        private void GenerarImagenPreCorte(ItemOrdenProduccion itemGrafico)
        {
            try
            {
                if (itemGrafico == null) { return; }
                if (ActualizandoIU) { return; }
                Bitmap b = new Bitmap((Image)upbPrecorte.Image);
                String RutaGrafico = String.Format("{0}Grafico_Precorte_{1}.png", FrmMain.ObtenerValorKey("CarpetaOrdenesProduccion"), itemGrafico.ID);
                b.Save(RutaGrafico);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }


        private void GenerarImagenes()
        {
            try
            {
                foreach (ItemOrdenProduccion itemPROD in OrdenProduccion.Items)
                {
                    if (itemPROD.TieneGraficos)
                    {
                        //utOrdenProduccion.ActiveNode(itemPROD);
                        MostrarItem(itemPROD);
                        GenerarImagenPreCorte(itemPROD);
                        GenerarImagenImpresion(itemPROD);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }



        private void GenerarImagenImpresion(ItemOrdenProduccion itemGrafico)
        {

            try
            {
                if (itemGrafico == null) { return; }
                if (ActualizandoIU) { return; }

                Bitmap b = new Bitmap((Image)upbImpresion.Image);
                String RutaGrafico = String.Format("{0}Grafico_Impresion_{1}.png", FrmMain.ObtenerValorKey("CarpetaOrdenesProduccion"), itemGrafico.ID);
                b.Save(RutaGrafico);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }



    }
}
