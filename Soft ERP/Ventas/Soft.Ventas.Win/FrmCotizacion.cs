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

        const String colServicio = "Servicio";
        const String colServicioCosto = "Costo Servicio";

        const String colMaterial = "Material";
        const String colMaterialCosto = "Costo Material";

        
        const String colMaquina = "Máquina";
        const String colMaquinaCosto = "Costo Maquina";


        private Boolean ActualizandoIU = false;

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colServicio);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colServicioCosto);
            column.DataType = typeof(Decimal);
            
            column = columns.Columns.Add(colMaterial);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colMaterialCosto);
            column.DataType = typeof(Decimal);


            column = columns.Columns.Add(colMaquina);
            column.DataType = typeof(String);

             column = columns.Columns.Add(colMaquinaCosto);
            column.DataType = typeof(Decimal);


            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colServicio].Width = 100;
            ugServicios.DisplayLayout.Bands[0].Columns[colServicioCosto].Width = 50;
            
            ugServicios.DisplayLayout.Bands[0].Columns[colMaterial].Width = 100;
            ugServicios.DisplayLayout.Bands[0].Columns[colMaterialCosto].Width = 50;

            ugServicios.DisplayLayout.Bands[0].Columns[colMaquina].Width = 100;
            ugServicios.DisplayLayout.Bands[0].Columns[colMaquinaCosto].Width = 50;


            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
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
            uneSubTotal.Value = Cotizacion.SubTotal;
            uneImpuesto.Value = Cotizacion.Impuesto;
            uneTotal.Value = Cotizacion.Total;


            busListaCostoMaquina.Text = (Cotizacion.ListaCostosMaquina != null) ? Cotizacion.ListaCostosMaquina.Nombre : "";
            busListaPrecioMaterial.Text = (Cotizacion.ListaPreciosExistencia != null) ? Cotizacion.ListaPreciosExistencia.Nombre : "";
            busListaPreciosTransporte.Text = (Cotizacion.ListaPreciosTransporte != null) ? Cotizacion.ListaPreciosTransporte.Nombre : "";
            

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
                MostrarItem(Node);
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
            ItemCotizacion Item = (ItemCotizacion)Node.Tag;
            GrupoMedidaAbierta.Visible = Item.TieneMedidaAbierta;
            GrupoMedidaCerrada.Visible = Item.TieneMedidaCerrada;
            GruposTiras.Visible = Item.TieneTiraRetira;
            ssMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Nombre : "";
            lblTipoUnidad.Text = Item.TipoUnidad;
            txtObservacionItem.Text = Item.Observacion;
            txtCantidadItem.Value = Item.Cantidad;
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
            MostrarServicios(Item);
        }

        public void MostrarServicios(ItemCotizacion ItemCotizacion)
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemCotizacionServicio Item in ItemCotizacion.Servicios)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarServicio(Row);
            }
        }

        public void MostrarServicio(UltraGridRow Row)
        {
            ItemCotizacionServicio Item = (ItemCotizacionServicio)Row.Tag;
            Row.Cells[colServicio].Activation = (Item.Servicio != null)?Activation.NoEdit: Activation.AllowEdit;
            Row.Cells[colServicio].Value = (Item.Servicio != null)?Item.Servicio.Nombre:"";

            Row.Cells[colServicioCosto].Activation = (Item.Servicio != null)?Activation.NoEdit: Activation.AllowEdit;
            Row.Cells[colServicioCosto].Value = (Item.Servicio != null)?Item.CostoServicio:0;

            Row.Cells[colMaterial].Activation = (Item.Material != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaterial].Value = (Item.Material != null) ? Item.Material.Nombre : "";
            
            Row.Cells[colMaterialCosto].Activation = (Item.Material != null)?Activation.NoEdit: Activation.AllowEdit;
            Row.Cells[colMaterialCosto].Value = (Item.Material != null) ? Item.CostoMaterial : 0;

            Row.Cells[colMaquina].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquina].Value = (Item.Maquina != null) ? Item.Maquina.Nombre : "";

            Row.Cells[colMaquinaCosto].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquinaCosto].Value = (Item.Maquina != null) ? Item.CostoMaquina: 0;
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
                    ItemCotizacionServicio ItemNuevo = ItemCotizacion.AddServicio();
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

            if ((Cotizacion.TipoDocumento == null) || (Cotizacion.TipoDocumento.Codigo != TipoDocumento.Codigo))
            {
                Cotizacion.TipoDocumento = (TipoCotizacion)HelperNHibernate.GetEntityByID("TipoCotizacion", TipoDocumento.ID);
                Cotizacion.GenerarNumCp();
                Cotizacion.AsignarListadeCostosDesdeTipoDocumento();
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
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            if (ActualizandoIU) { return; }
            ItemCotizacion.MedidaAbiertaAlto = Convert.ToDecimal(txtMedidaAbiertoAlto.Value);
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
            ItemCotizacion.Cantidad = Convert.ToInt32(txtCantidadItem.Value);
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
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacion.Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Máquina");
            if (ItemCotizacion.Maquina != null) {
                ItemCotizacion.Maquina = (Maquina)HelperNHibernate.GetEntityByID("Maquina", ItemCotizacion.Maquina.ID);
            }
            ssMaquina.Text = (ItemCotizacion.Maquina != null) ? ItemCotizacion.Maquina.Nombre : "";
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacion.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", " EsInventariable = 1");
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
            ItemCotizacionServicio item = AgregarServicio.ObtenerServicio(Cotizacion);
            if (item != null) { 
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = item;
                Row.Cells[colServicio].Activate();
                ugServicios.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                MostrarServicio(Row);
            }

        }

        private void ubEliminarServicio_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null) { return; }
            ItemCotizacion.Servicios.Remove((ItemCotizacionServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
        }

        public void ugServicios_CellKeyEnter(UltraGridCell Cell)
        {
            try
            {
                if (Cell == null || ItemCotizacion == null) { return; }
                ItemCotizacionServicio Item = (ItemCotizacionServicio)Cell.Row.Tag;
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
            Cotizacion.Observacion = txtObservacion.Text;
        }

        private void unePorcentajeUtilidad_ValueChanged(object sender, EventArgs e)
        {
            Cotizacion.PorcentajeUtilidad = Convert.ToDecimal(unePorcentajeUtilidad.Value);
        }

        ListaCostosMaquina lcm = null;
        ListaPreciosExistencia lpe = null;
        ListaPreciosTransporte lpt = null;
        private void Costeo()
        {
            lcm = (ListaCostosMaquina)HelperNHibernate.GetEntityByID("ListaCostosMaquina", Cotizacion.ListaCostosMaquina.ID);
            lpe = (ListaPreciosExistencia)HelperNHibernate.GetEntityByID("ListaPreciosExistencia", Cotizacion.ListaPreciosExistencia.ID);
            lpt = (ListaPreciosTransporte)HelperNHibernate.GetEntityByID("ListaPreciosTransporte", Cotizacion.ListaPreciosTransporte.ID);

            foreach (ItemCotizacion itemcotizacion in Cotizacion.Items) {
                CosteoElemento(itemcotizacion);
                
            }
            Cotizacion.SubTotal = Cotizacion.SubTotal;
        }

        private void CosteoElemento(ItemCotizacion itemCotizacion) {
            if (ItemCotizacion.Maquina != null)
            {
                ItemCotizacion.CostoMaquina = obtenerItemListaCostosMaquina(itemCotizacion);
            }
            else {
                ItemCotizacion.CostoMaquina = 0;
            }


            if (ItemCotizacion.Material != null)
            {
                ItemCotizacion.CostoMaterial = obtenerItemListaCostosMaterial(itemCotizacion);
            }
            else
            {
                ItemCotizacion.CostoMaterial = 0;
            }

           
            ItemCotizacion.Costo = ItemCotizacion.CostoMaquina + ItemCotizacion.CostoMaterial;
            ItemCotizacion.Precio = ItemCotizacion.Costo;
            
        }

        private Decimal obtenerItemListaCostosMaterial(ItemCotizacion itemCotizacion)
        {
            Decimal resultado = 0;
            try
            {
                resultado = itemCotizacion.Material.CostoUltimaCompra * itemCotizacion.Cantidad;
            
            }
            catch (Exception)
            {
            }
            
            
            return resultado;

        }



        private Decimal obtenerItemListaCostosMaquina(ItemCotizacion itemCotizacion)
        {
            Decimal resultado = 0;
            try
            {
            ItemListaCostosMaquina ilcm = obtenerItemListaCostosMaquina(itemCotizacion.Maquina);
            UnidadListaCostosMaquina Uilcm = obtenerUnidadLCM(ilcm);
            EscalaListaCostosMaquina Elcm = obtenerEscalaLCM(Uilcm, itemCotizacion);
            

                resultado = Elcm.Costo * ((itemCotizacion.MedidaAbiertaAlto * itemCotizacion.MedidaAbiertaLargo) * ItemCotizacion.Cantidad);
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
        private UnidadListaCostosMaquina obtenerUnidadLCM(ItemListaCostosMaquina ilcm) {
            UnidadListaCostosMaquina Uilcm = null;

            if (ilcm == null) {
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

            if (Uilcm == null) {
                return eUilcm;
            }

            Decimal metroscuadrado = itemcotizacion.MedidaAbiertaAlto * itemcotizacion.MedidaAbiertaLargo * itemcotizacion.Cantidad;

            foreach (EscalaListaCostosMaquina escala in Uilcm.Escalas)
            {
                if ((escala.Desde == 0) && (escala.Hasta == 0))
                {
                    eUilcm = escala;
                    break;
                }
                else if ((escala.Desde <= metroscuadrado) && (escala.Hasta >= metroscuadrado))
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
        

        private void ubRecalcular_Click(object sender, EventArgs e)
        {
            Costeo();
            Mostrar();
        }

        public void GenerarGraficoImpresionNormal()
        {
            if (ItemCotizacion.MedidaAbiertaLargo == 0) { throw new Exception("El largo de la  medida abierta debe se mayor a 0."); }
            if (ItemCotizacion.MedidaAbiertaAlto == 0) { throw new Exception("El alto de la  medida abierta debe se mayor a 0."); }

            upbImpresion.Width = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo);
            upbImpresion.Height = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo);

            Bitmap b;
            b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
            upbImpresion.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo - 1), Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo - 1)));
            int CantidadPiezas = 0;
            for (int x = Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo); x <= upbImpresion.Width; x += Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo))
            {
                for (int y = Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto); y <= upbImpresion.Height; y += Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto))
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo), y - Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto), Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo), Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto)));
                    CantidadPiezas += 1;
                    y += ItemCotizacion.SeparacionY;
                }
                x += ItemCotizacion.SeparacionX;
            }
            txtNroPiezas.Value = CantidadPiezas;
        }

        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width
            int sourceWidth = imgToResize.Width;
            //Get the image current height
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        public void GenerarGraficoImpresionRotado()
        {
            if (ItemCotizacion.MedidaAbiertaLargo == 0) { throw new Exception("El largo de la  medida abierta debe se mayor a 0."); }
            if (ItemCotizacion.MedidaAbiertaAlto == 0) { throw new Exception("El alto de la  medida abierta debe se mayor a 0."); }
            
            upbImpresion.Width = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo);
            upbImpresion.Height = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo);
            Bitmap b;
            b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
            upbImpresion.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo - 1), Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo - 1)));
            int CantidadPiezas = 0;
            for (int x = Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto); x <= upbImpresion.Width; x += Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto))
            {
                for (int y = Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo); y <= upbImpresion.Height; y += Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo))
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto), y - Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo), Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto), Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo)));
                    CantidadPiezas += 1;
                    y += ItemCotizacion.SeparacionY;
                }
                x += ItemCotizacion.SeparacionX;
            }
            txtNroPiezas.Value = CantidadPiezas;
        }

        public void GenerarGraficoPrecorteNormal()
        {

            if (ItemCotizacion.Maquina.PliegoAnchoMaximo == 0) { throw new Exception("El ancho del pliego de la máquina debe se mayor a 0."); }
            if (ItemCotizacion.Maquina.PliegoAltoMaximo == 0) { throw new Exception("El alto del pliego de la máquina debe se mayor a 0."); }

            upbPrecorte.Width = Convert.ToInt32(ItemCotizacion.Material.Largo);
            upbPrecorte.Height = Convert.ToInt32(ItemCotizacion.Material.Alto);

            Bitmap b;
            b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            upbPrecorte.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Material.Largo - 1), Convert.ToInt32(ItemCotizacion.Material.Alto - 1)));

            for (int x = ItemCotizacion.Maquina.PliegoAnchoMaximo; x <= upbPrecorte.Width; x += ItemCotizacion.Maquina.PliegoAnchoMaximo)
            {
                for (int y = ItemCotizacion.Maquina.PliegoAltoMaximo; y <= upbPrecorte.Height; y += ItemCotizacion.Maquina.PliegoAltoMaximo)
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - ItemCotizacion.Maquina.PliegoAnchoMaximo, y - ItemCotizacion.Maquina.PliegoAltoMaximo, ItemCotizacion.Maquina.PliegoAnchoMaximo, ItemCotizacion.Maquina.PliegoAltoMaximo));
                }
            }
        }

        public void GenerarGraficoPrecorteRotado() {
            if (ItemCotizacion.Maquina.PliegoAnchoMaximo == 0) { throw new Exception("El ancho del pliego de la máquina debe se mayor a 0."); }
            if (ItemCotizacion.Maquina.PliegoAltoMaximo == 0) { throw new Exception("El alto del pliego de la máquina debe se mayor a 0."); }
            
            upbPrecorte.Width = Convert.ToInt32(ItemCotizacion.Material.Largo);
            upbPrecorte.Height = Convert.ToInt32(ItemCotizacion.Material.Alto);
            Bitmap b;
            b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            upbPrecorte.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(myPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Material.Largo - 1), Convert.ToInt32(ItemCotizacion.Material.Alto - 1)));
            
            for (int x = ItemCotizacion.Maquina.PliegoAltoMaximo; x <= upbPrecorte.Width; x += ItemCotizacion.Maquina.PliegoAltoMaximo)
            {
                for (int y = ItemCotizacion.Maquina.PliegoAnchoMaximo; y <= upbPrecorte.Height; y += ItemCotizacion.Maquina.PliegoAnchoMaximo)
                {
                    g.DrawRectangle(myPen, new Rectangle(x - ItemCotizacion.Maquina.PliegoAltoMaximo, y - ItemCotizacion.Maquina.PliegoAnchoMaximo, ItemCotizacion.Maquina.PliegoAltoMaximo, ItemCotizacion.Maquina.PliegoAnchoMaximo));
                }
            }
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
                        filtro = "IDMoneda='" + Cotizacion.Moneda.ID + "' and Fecha='" + Cotizacion.FechaCreacion.Date + "'";
                        FrmSelectedEntity FrmSelectedMoneda = new FrmSelectedEntity();
                        TipoCambio tc = (TipoCambio)FrmSelectedMoneda.GetSelectedEntity(typeof(TipoCambio), "Tipo de Cambio", filtro);
                        Cotizacion.TipoCambioFecha = tc.TipoCambioVenta;
                    }
                    else {
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
            if(Direccion != null){
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
                GenerarGraficoPrecorteNormal();
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
                GenerarGraficoPrecorteRotado();
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
            //Bitmap b = new Bitmap((Image)upbPrecorte.Image);
            Bitmap b = new Bitmap((Image)upbImpresion.Image);
            String PathImagenCorte = String.Format("{0}Grafico-{1}.png", FrmMain.CarpetaImagenes, ItemCotizacion.ID);
            b.Save(PathImagenCorte);
            Soft.Reporte.Entidades.Reporte Reporte = (Soft.Reporte.Entidades.Reporte)HelperNHibernate.GetEntityByID("Reporte", "01F1035F-77F6-4188-B75F-7B9436FAB7DD");
            foreach (ParametroReporte Parametro in Reporte.ParametrosCrystal)
            {
                if (Parametro.Nombre.Equals("PathImagenPrecorte"))
                {
                    Parametro.Valor = PathImagenCorte;
                }
                else if (Parametro.Nombre.Equals("PathImagenImpresion"))
                {
                    Parametro.Valor = PathImagenCorte;
                }
                else if (Parametro.Nombre.Equals("DimensionesPapelPrecorte"))
                {
                    Parametro.Valor = String.Format("{0} x {1} cm", ItemCotizacion.Material.Largo, ItemCotizacion.Material.Alto);
                }
                else if (Parametro.Nombre.Equals("DimensionesImpresoraPrecorte"))
                {
                    Parametro.Valor = String.Format("{0} x {1} cm", ItemCotizacion.Maquina.PliegoAnchoMaximo, ItemCotizacion.Maquina.PliegoAltoMaximo);
                }
            }
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
                ItemCotizacion.SeparacionX = Convert.ToInt32(uneSeparacionX.Value);
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
                ItemCotizacion.SeparacionY = Convert.ToInt32(uneSeparacionY.Value);
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
                if (ItemCotizacion == null) { return; }
                GenerarGraficoImpresionNormal();
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
                GenerarGraficoImpresionRotado();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubImprimirGraficoImpresion_Click(object sender, EventArgs e)
        {

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarServicio();
        }

        private void ModificarServicio() {
            if (ugServicios.ActiveRow != null) {
                ItemCotizacionServicio itemCotizacion = (ItemCotizacionServicio)ugServicios.ActiveRow.Tag;
                FrmCotizaciondeServicio AgregarServicio = new FrmCotizaciondeServicio(itemCotizacion);
                ItemCotizacionServicio item = AgregarServicio.ObtenerServicio(Cotizacion);
                if (item != null) {
                    ugServicios.ActiveRow.Tag = item;
                    ugServicios.ActiveRow.Cells[colServicio].Activate();
                    MostrarServicio(ugServicios.ActiveRow);
                }
            }else{
                Exception ex= new Exception("Debe seleccionar un servicio para poder modificar");
                Soft.Exceptions.SoftException.ShowException(ex);
            }

            
        }


        private void ugServicios_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            ModificarServicio();
        }

        private void ultraTabPageControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
