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
        const String colMaterial = "Material";
        const String colMaquina = "Máquina";

        private Boolean ActualizandoIU = false;

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colServicio);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colMaterial);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colMaquina);
            column.DataType = typeof(String);

            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colServicio].Width = 250;
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
            txtNumeracion.Text = Cotizacion.Numeracion;
            udtFechaCreacion.Value = Cotizacion.FechaCreacion;
            txtObservacion.Text = Cotizacion.Observacion;
            txtDescripcion.Text = Cotizacion.Descripcion;
            uneCantidad.Value = Cotizacion.Cantidad;
            txtNumeracion.Text = Cotizacion.Numeracion;
            unePorcentajeUtilidad.Value = Cotizacion.PorcentajeUtilidad;

            MostrarItems();
            ActualizandoIU = false;
        }

        public void MostrarItems()
        {
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
            uneCosto.Value = Item.Costo;
            unePrecio.Value = Item.Precio;
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
            Row.Cells[colMaterial].Activation = (Item.Material != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaterial].Value = (Item.Material != null) ? Item.Material.Nombre : "";
            Row.Cells[colMaquina].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquina].Value = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
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
                //Item.CantidadFinal = 1;
                MostrarServicio(Row);
                for (int i = 2; i <= Productos.Count; i++)
                {
                    UltraGridRow RowNuevo = ugServicios.DisplayLayout.Bands[0].AddNew();
                    ItemCotizacionServicio ItemNuevo = ItemCotizacion.AddServicio();
                    Existencia ProductoNuevo = (Existencia)Productos[i];
                    ItemNuevo.Servicio = (Existencia)HelperNHibernate.GetEntityByID("Existencia", ProductoNuevo.ID);
                    //ItemNuevo.CantidadFinal = 1;
                    RowNuevo.Tag = ItemNuevo;
                    MostrarServicio(RowNuevo);
                }
            }
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
            TipoCotizacion TipoDocumento = (TipoCotizacion)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoCotizacion), "Tipo de Cotización");
            Cotizacion.TipoDocumento = (TipoCotizacion)HelperNHibernate.GetEntityByID("TipoCotizacion", TipoDocumento.ID);
            ssTipoDocumento.Text = (Cotizacion.TipoDocumento != null) ? Cotizacion.TipoDocumento.Nombre : "";
            Cotizacion.GenerarNumCp();
            txtNumeracion.Text = Cotizacion.Numeracion;
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
            ItemCotizacion.MedidaAbiertaLargo = Convert.ToInt32(txtMedidaAbiertoLargo.Value);
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.MedidaAbiertaAlto = Convert.ToInt32(txtMedidaAbiertoAlto.Value);
        }

        private void txtMedidaCerradaLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.MedidaCerradaLargo = Convert.ToInt32(txtMedidaCerradaLargo.Value);
        }

        private void txtMedidaCerradaAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.MedidaCerradaAlto = Convert.ToInt32(txtMedidaCerradaAlto.Value);
        }

        private void txtImpresoTiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.ImpresoTiraColor = Convert.ToInt32(txtImpresoTiraColor.Value);
        }

        private void txtCantidadItem_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.Cantidad = Convert.ToInt32(txtCantidadItem.Value);
        }

        private void txtImpresoRetiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.ImpresoRetiraColor = Convert.ToInt32(txtImpresoRetiraColor.Value);
        }

        private void ssMaquina_Search(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacion.Maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Máquina");
            ssMaquina.Text = (ItemCotizacion.Maquina != null) ? ItemCotizacion.Maquina.Nombre : "";
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            ItemCotizacion.Material = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", " EsInventariable = 1");
            ssMaterial.Text = (ItemCotizacion.Material != null) ? ItemCotizacion.Material.Nombre : "";
        }

        private void txtObservacionItem_TextChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.Observacion = txtObservacionItem.Text;
        }

        private void ubNuevoServicio_Click(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
            Row.Tag = ItemCotizacion.AddServicio();
            Row.Cells[colServicio].Activate();
            ugServicios.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
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

        private void ubRecalcular_Click(object sender, EventArgs e)
        {
            GenerarGraficosNormal();
        }

        public void GenerarGraficosNormal() {
            if (ItemCotizacion.Material == null) { return; }
            GenerarGraficoPrecorteNormal();
            GenerarGraficoImpresionNormal();
        }

        public void GenerarGraficosRotado() {
            if (ItemCotizacion.Material == null) { return; }
            GenerarGraficoPrecorteRotado();
            GenerarGraficoImpresionRotado();
        }

        public void GenerarGraficoPrecorteNormal() {
            upbPrecorte.Width = Convert.ToInt32(ItemCotizacion.Material.Largo);
            upbPrecorte.Height = Convert.ToInt32(ItemCotizacion.Material.Alto);
            Bitmap b;
            b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            upbPrecorte.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Material.Largo - 1), Convert.ToInt32(ItemCotizacion.Material.Alto - 1)));
            int CantidadPiezas = 0;
            for (int x = ItemCotizacion.Maquina.PliegoAnchoMaximo; x <= upbPrecorte.Width; x += ItemCotizacion.Maquina.PliegoAnchoMaximo)
            {
                for (int y = ItemCotizacion.Maquina.PliegoAltoMaximo; y <= upbPrecorte.Height; y += ItemCotizacion.Maquina.PliegoAltoMaximo)
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - ItemCotizacion.Maquina.PliegoAnchoMaximo, y - ItemCotizacion.Maquina.PliegoAltoMaximo, ItemCotizacion.Maquina.PliegoAnchoMaximo, ItemCotizacion.Maquina.PliegoAltoMaximo));
                    CantidadPiezas += 1;
                }
            }
            txtObservacion.Text = String.Format("Cantidad Total de Piezas : {0}", CantidadPiezas);
        }

        public void GenerarGraficoImpresionNormal()
        {
            upbImpresion.Width = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo);
            upbImpresion.Height = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo);
            Bitmap b;
            b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
            upbImpresion.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo - 1), Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo - 1)));

            for (int x = Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo); x <= upbImpresion.Width; x += Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo))
            {
                for (int y = Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto); y <= upbImpresion.Height; y += Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto))
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo), y - Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto), Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo), Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto)));
                }
            }
        }

        public void GenerarGraficoImpresionRotado()
        {
            upbImpresion.Width = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo);
            upbImpresion.Height = Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo);
            Bitmap b;
            b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
            upbImpresion.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen MyPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(MyPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Maquina.PliegoAnchoMaximo - 1), Convert.ToInt32(ItemCotizacion.Maquina.PliegoAltoMaximo - 1)));

            for (int x = Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto); x <= upbImpresion.Width; x += Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto))
            {
                for (int y = Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo); y <= upbImpresion.Height; y += Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo))
                {
                    g.DrawRectangle(MyPen, new Rectangle(x - Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto), y - Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo), Convert.ToInt32(ItemCotizacion.MedidaAbiertaAlto), Convert.ToInt32(ItemCotizacion.MedidaAbiertaLargo)));
                }
            }
        }

        public void GenerarGraficoPrecorteRotado() {
            upbPrecorte.Width = Convert.ToInt32(ItemCotizacion.Material.Largo);
            upbPrecorte.Height = Convert.ToInt32(ItemCotizacion.Material.Alto);
            Bitmap b;
            b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            upbPrecorte.Image = (Image)b;
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.White);
            Pen myPen = new Pen(System.Drawing.Color.Black, 1);
            g.DrawRectangle(myPen, new Rectangle(0, 0, Convert.ToInt32(ItemCotizacion.Material.Largo - 1), Convert.ToInt32(ItemCotizacion.Material.Alto - 1)));
            int CantidadPiezas = 0;
            for (int x = ItemCotizacion.Maquina.PliegoAltoMaximo; x <= upbPrecorte.Width; x += ItemCotizacion.Maquina.PliegoAltoMaximo)
            {
                for (int y = ItemCotizacion.Maquina.PliegoAnchoMaximo; y <= upbPrecorte.Height; y += ItemCotizacion.Maquina.PliegoAnchoMaximo)
                {
                    g.DrawRectangle(myPen, new Rectangle(x - ItemCotizacion.Maquina.PliegoAltoMaximo, y - ItemCotizacion.Maquina.PliegoAnchoMaximo, ItemCotizacion.Maquina.PliegoAltoMaximo, ItemCotizacion.Maquina.PliegoAnchoMaximo));

                    CantidadPiezas += 1;
                }
            }
            txtObservacion.Text = String.Format("Cantidad Total de Piezas : {0}", CantidadPiezas);
        }

        private void uneCosto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.Costo = Convert.ToDecimal(uneCosto.Value);
        }

        private void unePrecio_ValueChanged(object sender, EventArgs e)
        {
            if (ItemCotizacion == null) { return; }
            ItemCotizacion.Precio = Convert.ToDecimal(unePrecio.Value);
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            Cotizacion.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void ubCambiarPosicion_Click(object sender, EventArgs e)
        {
            GenerarGraficosRotado();
        }

        private void ubImprimirGraficos_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap((Image)upbPrecorte.Image, new Size(390, 405));
            String PathImagenCorte = String.Format("Recursos\\Gráficos\\Grafico-{0}.png", ItemCotizacion.ID);
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
                    Parametro.Valor = "";
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

    }
}
