using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Produccion.Entidades;
using Soft.DataAccess;
using Soft.Entities;
using Soft.Ventas.Entidades;
using Infragistics.Win.UltraWinTree;
using Infragistics.Win.UltraWinGrid;
using Soft.Exceptions;
using Soft.Inventario.Entidades;
using Soft.Reporte.Entidades;
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
            base.Init();
            InitGrids();
            Mostrar();
        }

        Boolean ActualizandoIU = false;

        const String colServicio = "Servicio";
        const String colServicioCosto = "Costo Servicio";
        const String colMaterial = "Material";
        const String colMaterialCosto = "Costo Material";
        const String colMaquina = "Máquina";
        const String colMaquinaCosto = "Costo Maquina";

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colServicio);
            column.DataType = typeof(String);


            /*column = columns.Columns.Add(colServicioCosto);
            column.DataType = typeof(Decimal);*/

            column = columns.Columns.Add(colMaterial);
            column.DataType = typeof(String);

            /*
            column = columns.Columns.Add(colMaterialCosto);
            column.DataType = typeof(Decimal);
            */

            column = columns.Columns.Add(colMaquina);
            column.DataType = typeof(String);

            /*column = columns.Columns.Add(colMaquinaCosto);
            column.DataType = typeof(Decimal);
            */

            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colServicio].Width = 100;
            //ugServicios.DisplayLayout.Bands[0].Columns[colServicioCosto].Width = 50;

            ugServicios.DisplayLayout.Bands[0].Columns[colMaterial].Width = 100;
            //ugServicios.DisplayLayout.Bands[0].Columns[colMaterialCosto].Width = 50;

            ugServicios.DisplayLayout.Bands[0].Columns[colMaquina].Width = 100;
            //ugServicios.DisplayLayout.Bands[0].Columns[colMaquinaCosto].Width = 50;


            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            try
            {
                if (OrdenProduccion.TipoDocumento != null)
                {
                    ssTipoDocumento.Text = OrdenProduccion.TipoDocumento.Descripcion;
                    txtNumeracion.Enabled = (OrdenProduccion.TipoDocumento.NumeracionAutomatica) ? false : true;
                }
                ssCliente.Text = (OrdenProduccion.Cliente != null) ? OrdenProduccion.Cliente.Nombre : "";
                ssCotizador.Text = (OrdenProduccion.Cotizador != null) ? OrdenProduccion.Cotizador.Nombre : "";
                ssResponsable.Text = (OrdenProduccion.Responsable != null) ? OrdenProduccion.Responsable.Nombre : "";
                ssVendedor.Text = (OrdenProduccion.Vendedor != null) ? OrdenProduccion.Vendedor.Nombre : "";
                udtFechaCreacion.Value = OrdenProduccion.FechaCreacion;
                udtFechaCreacion.ReadOnly = true;
                udtFechaTentativaEntrega.Value = OrdenProduccion.FechaTentativaEntrega;
                txtNumeracion.Text = OrdenProduccion.Numeracion;
                cboPrioridad.Text = OrdenProduccion.Prioridad;
                txtDescripcion.Text = OrdenProduccion.Descripcion;
                busListaPrecioMaterial.Text = (OrdenProduccion.ListaPreciosExistencia != null) ? OrdenProduccion.ListaPreciosExistencia.Nombre : "";
                busListaCostoMaquina.Text = (OrdenProduccion.ListaCostosMaquina != null) ? OrdenProduccion.ListaCostosMaquina.Nombre : "";
                busListaPreciosTransporte.Text = (OrdenProduccion.ListaPreciosTransporte != null) ? OrdenProduccion.ListaPreciosTransporte.Nombre : "";


                busLineaProduccion.Text = (OrdenProduccion.LineaProduccion != null) ? OrdenProduccion.LineaProduccion.Nombre : "";
          
                ssDireccionEntrega.Text = OrdenProduccion.DireccionEntrega;
                ssDireccionFactura.Text = OrdenProduccion.DireccionFacturacion;

                ssContacto.Text = (OrdenProduccion.Contacto != null) ? OrdenProduccion.Contacto.Nombre : "";

                uneCantidad.Value = OrdenProduccion.Cantidad;

                txtObservacion.Text = OrdenProduccion.Observacion;

                MostrarCotizacionPresupuesto();



                MostrarItems();

               

               


            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
            ActualizandoIU = false;
        }


        private void MostrarCotizacionPresupuesto(){
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
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Descripcion : "";
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
            //uneCostoMaquina.Value = Item.CostoMaquina;
            //uneCostoMaterial.Value = Item.CostoMaterial;
            //uneCosto.Value = Item.Costo;
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
            //txtCostoServicio.Value = Item.CostoServicio;
            //lblCostoMaquina.Visible = Item.TieneMaquina;
            //uneCostoMaquina.Visible = Item.TieneMaquina;
            //lblCostoMaterial.Visible = Item.TieneMaterial;
            //uneCostoMaterial.Visible = Item.TieneMaterial;


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
            LabelProduccion.Text = "";
            if (Item.NumeroPliegos > 0)
            {
                LabelProduccion.Text = Item.NumeroPliegos + " pliegos de : ";
            }
            LabelProduccion.Text += Math.Round(((Item.CantidadMaterial + Item.CantidadDemasiaMaterial) * Item.NroPiezasPrecorte), 0).ToString() + " Hjs/Maquina";
            txtPliegos.Value = Item.NumeroPliegos;


            // if (Item.MetodoImpresion != null) {
            ubeMetodo.Text = Item.MetodoImpresion;
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


            MostrarServicios(Item);
            ActualizandoIU = false;
        }

        public void MostrarItem(ItemOrdenProduccion ItemPROD)
        {
            ActualizandoIU = true;
            ItemOrdenProduccion Item = ItemPROD;
            ItemOrdenProduccion = Item;
            GrupoMedidaAbierta.Visible = Item.TieneMedidaAbierta;
            GrupoMedidaCerrada.Visible = Item.TieneMedidaCerrada;
            GruposTiras.Visible = Item.TieneTiraRetira;
            ssMaquina.Text = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            ssMaterial.Text = (Item.Material != null) ? Item.Material.Descripcion : "";
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
            //uneCostoMaquina.Value = Item.CostoMaquina;
            //uneCostoMaterial.Value = Item.CostoMaterial;
            //uneCosto.Value = Item.Costo;
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
            //txtCostoServicio.Value = Item.CostoServicio;
            //lblCostoMaquina.Visible = Item.TieneMaquina;
            //uneCostoMaquina.Visible = Item.TieneMaquina;
            //lblCostoMaterial.Visible = Item.TieneMaterial;
            //uneCostoMaterial.Visible = Item.TieneMaterial;


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
            LabelProduccion.Text = "";
            if (Item.NumeroPliegos > 0)
            {
                LabelProduccion.Text = Item.NumeroPliegos + " pliegos de : ";
            }
            LabelProduccion.Text += Math.Round(((Item.CantidadMaterial + Item.CantidadDemasiaMaterial) * Item.NroPiezasPrecorte), 0).ToString() + " Hjs/Maquina";
            txtPliegos.Value = Item.NumeroPliegos;


            // if (Item.MetodoImpresion != null) {
            ubeMetodo.Text = Item.MetodoImpresion;
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


            MostrarServicios(Item);
            ActualizandoIU = false;
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
            //b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            b = new Bitmap(120, 80);
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

        public void GenerarGraficoImpresionRotado()
        {
            try
            {
                if (ItemOrdenProduccion.MedidaAbiertaLargo == 0)
                { throw new Exception("No se ha asignado el largo de la medida abierta"); }
                if (ItemOrdenProduccion.MedidaAbiertaAlto == 0)
                { throw new Exception("No se ha asignado el alto de la medida abierta"); }
                if (ItemOrdenProduccion.Material == null)
                { throw new Exception("No se ha asignado ningún material."); }
                if (ItemOrdenProduccion.Maquina == null)
                { throw new Exception("No se ha asignado ninguna máquina."); }

                if (ItemOrdenProduccion.MetodoImpresion == null)
                {
                    throw new Exception("Debe elegir un metodo de Impresion.");
                }

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
                //b = new Bitmap(upbImpresion.Width, upbImpresion.Height);
                b = new Bitmap(100 * 10, 70 * 10);
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
                        y += ItemOrdenProduccion.SeparacionY;

                    }
                    //x += ItemCotizacion.SeparacionX / 10;
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

                //upbImpresion.Width = upbImpresion.Width / 5;
                //upbImpresion.Height = upbImpresion.Height / 5;
                upbImpresion.Width = 100;
                upbImpresion.Height = 70;

                ItemOrdenProduccion.NroPiezasImpresion = CantidadPiezas;
                txtNroPiezasImpresion.Value = CantidadPiezas;

            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }

            
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
            //b = new Bitmap(upbPrecorte.Width, upbPrecorte.Height);
            b = new Bitmap(120, 80);

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

        public void GenerarGraficoImpresionNormal()
        {

            try
            {
                if (ItemOrdenProduccion.MedidaAbiertaLargo == 0)
                { throw new Exception("No se ha asignado el largo de la medida abierta"); }
                if (ItemOrdenProduccion.MedidaAbiertaAlto == 0)
                { throw new Exception("No se ha asignado el alto de la medida abierta"); }
                if (ItemOrdenProduccion.Material == null)
                { throw new Exception("No se ha asignado ningún material."); }
                if (ItemOrdenProduccion.Maquina == null)
                { throw new Exception("No se ha asignado ninguna máquina."); }

                if (ItemOrdenProduccion.MetodoImpresion == null) {
                    throw new Exception("Debe elegir un metodo de Impresion.");
                }

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
                //b = new Bitmap(upbImpresion.Width, upbImpresion.Height);

                b = new Bitmap(100*10, 70*10);

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
                        y += ItemOrdenProduccion.SeparacionY;
                    }
                    //x += ItemCotizacion.SeparacionX / 10;
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

                upbImpresion.Width = 100;
                upbImpresion.Height = 70;



                ItemOrdenProduccion.NroPiezasImpresion = CantidadPiezas;
                txtNroPiezasImpresion.Value = CantidadPiezas;

            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }

            
            
        }

        public void MostrarServicios(ItemOrdenProduccion item)
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemOrdenProduccionServicio Item in item.Servicios)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarServicio(Row);
            }

            //MostrarTotalServicio(item);
        }

        public void MostrarServicio(UltraGridRow Row)
        {
            ItemOrdenProduccionServicio Item = (ItemOrdenProduccionServicio)Row.Tag;
            Row.Cells[colServicio].Activation = (Item.Servicio != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colServicio].Value = (Item.Servicio != null) ? Item.Servicio.Nombre : "";

            /*Row.Cells[colServicioCosto].Activation = (Item.Servicio != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colServicioCosto].Value = (Item.Servicio != null) ? Item.CostoServicio : 0;
            */

            Row.Cells[colMaterial].Activation = (Item.Material != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaterial].Value = (Item.Material != null) ? Item.Material.Nombre : "";

            /*Row.Cells[colMaterialCosto].Activation = (Item.Material != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaterialCosto].Value = (Item.Material != null) ? Item.CostoMaterial : 0;
            */

            Row.Cells[colMaquina].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquina].Value = (Item.Maquina != null) ? Item.Maquina.Nombre : "";
            /*
            Row.Cells[colMaquinaCosto].Activation = (Item.Maquina != null) ? Activation.NoEdit : Activation.AllowEdit;
            Row.Cells[colMaquinaCosto].Value = (Item.Maquina != null) ? Item.CostoMaquina : 0;*/
        }

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

                    Decimal mat = (itemcosteado.CantidadElemento / (itemcosteado.NroPiezasPrecorte * itemcosteado.NroPiezasImpresion));
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

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                OrdenProduccion.TipoDocumento = (TipoOrdenProduccion)FrmSeleccionar.GetSelectedEntity(typeof(TipoOrdenProduccion), "Tipo Orden de Producción", All: true);
                if (OrdenProduccion.TipoDocumento != null) {
                    OrdenProduccion.GenerarNumeracion();
                    OrdenProduccion.Responsable = FrmMain.ObtenerResponsable();
                    ssTipoDocumento.Text = (OrdenProduccion.TipoDocumento != null) ? OrdenProduccion.TipoDocumento.Descripcion : "";
                    txtNumeracion.Text = OrdenProduccion.Numeracion;
                    txtNumeracion.Enabled = (OrdenProduccion.TipoDocumento.NumeracionAutomatica) ? false : true;
                    ssResponsable.Text = (OrdenProduccion.Responsable != null) ? OrdenProduccion.Responsable.Nombre : "";
                }
            }
            catch (Exception ex) 
            {
                SoftException.Control(ex);
            }
        }

        private void ssTipoDocumento_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.TipoDocumento = null;
                OrdenProduccion.Numeracion = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
          
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarProveedor = new FrmSelectedEntity();
                OrdenProduccion.Cliente = (SocioNegocio)FrmSeleccionarProveedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Cliente = 1");
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }  

        }

        private void ssCliente_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.Cliente = null;
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
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

        private void ssCotizador_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.Cotizador = null;
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }  
        }

        private void ssVendedor_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarVendedor = new FrmSelectedEntity();
                OrdenProduccion.Vendedor = (SocioNegocio)FrmSeleccionarVendedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssVendedor_Clear(object sender, EventArgs e)
        {

            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarVendedor = new FrmSelectedEntity();
                OrdenProduccion.Vendedor =null;
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarVendedor = new FrmSelectedEntity();
                OrdenProduccion.Responsable = (SocioNegocio)FrmSeleccionarVendedor.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssResponsable_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionarVendedor = new FrmSelectedEntity();
                OrdenProduccion.Responsable =null;
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

        private void uneCantidad_ValueChanged(object sender, EventArgs e)
        {
            
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.Cantidad = Convert.ToDecimal(uneCantidad.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }

        }

        private void txtDescripcion_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.Descripcion = txtDescripcion.Text;
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void busListaPrecioMaterial_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                OrdenProduccion.ListaPreciosExistencia = (ListaPreciosExistencia)FrmSeleccionar.GetSelectedEntity(typeof(ListaPreciosExistencia), "Lista Precios Producto Servicio", " Activo = 1");
                busListaPrecioMaterial.Text = (OrdenProduccion.ListaPreciosExistencia != null) ? OrdenProduccion.ListaPreciosExistencia.Nombre : "";       
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void busListaPrecioMaterial_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.ListaPreciosExistencia = null;
                busListaPrecioMaterial.Text = (OrdenProduccion.ListaPreciosExistencia != null) ? OrdenProduccion.ListaPreciosExistencia.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void busListaCostoMaquina_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                OrdenProduccion.ListaCostosMaquina = (ListaCostosMaquina)FrmSeleccionar.GetSelectedEntity(typeof(ListaCostosMaquina), "Lista de Costos Máquina", " Activo = 1");
                busListaCostoMaquina.Text = (OrdenProduccion.ListaCostosMaquina != null) ? OrdenProduccion.ListaCostosMaquina.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void busListaCostoMaquina_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                OrdenProduccion.ListaCostosMaquina = null;                   
                busListaCostoMaquina.Text = (OrdenProduccion.ListaCostosMaquina != null) ? OrdenProduccion.ListaCostosMaquina.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void busListaPreciosTransporte_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                OrdenProduccion.ListaPreciosTransporte = (ListaPreciosTransporte)FrmSeleccionar.GetSelectedEntity(typeof(ListaPreciosTransporte), "Lista Precios Transporte", " Activo = 1");
                busListaPreciosTransporte.Text = (OrdenProduccion.ListaPreciosTransporte != null) ? OrdenProduccion.ListaPreciosTransporte.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void busListaPreciosTransporte_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                OrdenProduccion.ListaPreciosTransporte =null;
                busListaPreciosTransporte.Text = (OrdenProduccion.ListaPreciosTransporte != null) ? OrdenProduccion.ListaPreciosTransporte.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssContacto_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                if (OrdenProduccion.Cliente == null) {
                    throw new Exception("Debe elegir un cliente");                
                }

                FrmSelectedEntity FrmSeleccionarContacto = new FrmSelectedEntity();
                OrdenProduccion.Contacto = (ItemSocioNegocioContacto)FrmSeleccionarContacto.GetSelectedEntity(typeof(ItemSocioNegocioContacto), "Contacto", String.Format("IDSocioNegocio = '{0}'", OrdenProduccion.Cliente.ID));
                ssContacto.Text = (OrdenProduccion.Contacto != null) ? OrdenProduccion.Contacto.Nombre : "";
             }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssContacto_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.Contacto = null;
                ssContacto.Text = (OrdenProduccion.Contacto != null) ? OrdenProduccion.Contacto.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssDireccionEntrega_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                if (OrdenProduccion.Cliente == null)
                {
                    throw new Exception("Debe elegir un cliente");
                }
                FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
                ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionEntrega = 1", OrdenProduccion.Cliente.ID));
                if (Direccion != null)
                {
                    OrdenProduccion.DireccionEntrega = Direccion.Direccion;
                    ssDireccionEntrega.Text = Direccion.Direccion;
                }

            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }

        }
        
        private void ssDireccionEntrega_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.DireccionEntrega = null;
                ssDireccionEntrega.Text = "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }

        }

        private void ssDireccionFactura_Search(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                if (OrdenProduccion.Cliente == null)
                {
                    throw new Exception("Debe elegir un cliente");
                }
                FrmSelectedEntity FrmSeleccionarDireccion = new FrmSelectedEntity();
                ItemSocioNegocioDireccion Direccion = (ItemSocioNegocioDireccion)FrmSeleccionarDireccion.GetSelectedEntity(typeof(ItemSocioNegocioDireccion), "Dirección", String.Format("IDSocioNegocio = '{0}' AND EsDireccionFacturacion = 1", OrdenProduccion.Cliente.ID));
                if (Direccion != null)
                {
                    OrdenProduccion.DireccionFacturacion = Direccion.Direccion;
                    ssDireccionFactura.Text = Direccion.Direccion;
                }
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
            
        }

        private void ssDireccionFactura_Clear(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }
            try
            {
                OrdenProduccion.DireccionFacturacion = null;
                ssDireccionFactura.Text = "";
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtMedidaAbiertoLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }

            try
            {
                ItemOrdenProduccion.MedidaAbiertaLargo = Convert.ToDecimal(txtMedidaAbiertoLargo.Value);
                txtImpresionLargo.Value = ItemOrdenProduccion.MedidaAbiertaLargo;
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtMedidaAbiertoAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.MedidaAbiertaAlto = Convert.ToDecimal(txtMedidaAbiertoAlto.Value);
                txtImpresionLargo.Value = ItemOrdenProduccion.MedidaAbiertaAlto;
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void comboMedida_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.UnidadMedidaAbierta = comboMedida.Text;
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtMedidaCerradaLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.MedidaCerradaLargo = Convert.ToDecimal(txtMedidaCerradaLargo.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtMedidaCerradaAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.MedidaCerradaAlto = Convert.ToDecimal(txtMedidaCerradaAlto.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtCantidadProduccion_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.CantidadElemento = Convert.ToDecimal(txtCantidadProduccion.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtCantidadItem_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.CantidadUnidad = Convert.ToInt32(txtCantidadItem.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtImpresoTiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.ImpresoTiraColor = Convert.ToInt32(txtImpresoTiraColor.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtImpresoRetiraColor_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.ImpresoRetiraColor = Convert.ToInt32(txtImpresoRetiraColor.Value);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssMaquina_Search(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                if (ItemOrdenProduccion.Operacion == null)
                {
                   throw new Exception("Debe de seleccionar una operacion");
                }
                String filtro = "IDOperacion='" + ItemOrdenProduccion.Operacion.ID + "'";
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                Maquina maquina = (Maquina)FrmSeleccionar.GetSelectedEntity(typeof(Maquina), "Maquina Operacion", filtro);
                if (maquina != null)
                {
                    ItemOrdenProduccion.Maquina = (Maquina)HelperNHibernate.GetEntityByID("Maquina", maquina.ID);
                }

                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssMaquina_Clear(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.Maquina = null;
               MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssMaterial_Search(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                string filtro = "EsInventariable = 1";
                if (ssMaterial.Text.Length > 0)
                {
                    filtro += " and Nombre like '%" + ssMaterial.Text + "%'";
                }

                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                Existencia material= (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", filtro);
                if (material != null)
                {
                    ItemOrdenProduccion.Material = (Existencia)HelperNHibernate.GetEntityByID("Existencia", material.ID);
                }
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ssMaterial_Clear(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.Material = null;
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void btnNuevoElemento_Click(object sender, EventArgs e)
        {

            ItemOrdenProduccion item = OrdenProduccion.AddItem();
            item.TieneGraficos = true;
            item.TieneMaquina = true;
            item.TieneMaterial = true;
            item.TieneMedidaAbierta = true;
            item.TieneTiraRetira = true;
            item.Nombre = "ELEMENTO PRUEBA";
            item.Operacion = (Existencia)HelperNHibernate.GetEntityByID("Existencia", "2EE2172A-F8BE-440D-96FD-735930D33104");
            Mostrar();

        }

        private void utCotizacion_AfterSelect(object sender, SelectEventArgs e)
        {
            
            try
            {
                UltraTreeNode Node = utOrdenProduccion.ActiveNode;
                if (Node != null)
                {
                    ItemOrdenProduccion = (ItemOrdenProduccion)Node.Tag;
                    utcItemCotizacion.Tabs[0].Text = Node.Text;
                    MostrarItem(Node);
                }
            }
            catch (Exception ex)
            {
               SoftException.ShowException(ex);
            }
            
        }

        private void txtFormatoImpresionLargo_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.FormatoImpresionLargo = Convert.ToDecimal(txtFormatoImpresionLargo.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtFormatoImpresionAlto_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.FormatoImpresionAlto = Convert.ToDecimal(txtFormatoImpresionAlto.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }

        }

        private void uneSeparacionX_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.SeparacionX = Convert.ToInt32(uneSeparacionX.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void uneSeparacionY_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.SeparacionY = Convert.ToInt32(uneSeparacionY.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtNroPiezasImpresion_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.NroPiezasImpresion = Convert.ToInt32(txtNroPiezasImpresion.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void txtDemasia_ValueChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.CantidadDemasia = Convert.ToInt32(txtDemasia.Value);
                CalcularProduccionItem(ItemOrdenProduccion);
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void checkGraficoImpresionManual_CheckedChanged(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                ItemOrdenProduccion.GraficoImpresionManual = checkGraficoImpresionManual.Checked;
                MostrarItem(utOrdenProduccion.ActiveNode);
            }
            catch (Exception ex)
            {
                SoftException.ShowException(ex);
            }
        }

        private void ubMostrarGraficoPrecorte_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }
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
                if (ActualizandoIU) { return; }
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
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }
                Bitmap b = new Bitmap((Image)upbPrecorte.Image);
                String RutaGrafico = String.Format("{0}Grafico_Precorte_{1}.png", FrmMain.ObtenerValorKey("CarpetaOrdenesProduccion"), ItemOrdenProduccion.ID);
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

        private void GenerarImagenPreCorte(ItemOrdenProduccion itemGrafico) {
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

        private void GenerarImagenImpresion(ItemOrdenProduccion itemGrafico) {

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

        private void GenerarImagenes() {

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

        private void ubMostrarGraficoImpresion_Click(object sender, EventArgs e)
        {
            try
            {
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }

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
                if (ActualizandoIU) { return; }

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
                if (ItemOrdenProduccion == null) { return; }
                if (ActualizandoIU) { return; }

                Bitmap b = new Bitmap((Image)upbImpresion.Image);
                String RutaGrafico = String.Format("{0}Grafico_Impresion_{1}.png", FrmMain.ObtenerValorKey("CarpetaOrdenesProduccion"), ItemOrdenProduccion.ID);
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

        private void ubNuevoServicio_Click(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
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
                    //MostrarTotalServicio(ItemCotizacion);
                    MostrarServicio(Row);
                }

            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

            
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (ItemOrdenProduccion == null) { return; }
            if (ActualizandoIU) { return; }
            try
            {
                 ModificarServicio();
                //MostrarTotalServicio(ItemCotizacion);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void ModificarServicio()
        {
            if (ugServicios.ActiveRow != null)
            {
                ItemOrdenProduccionServicio itemOrdenProduccionServicio = (ItemOrdenProduccionServicio)ugServicios.ActiveRow.Tag;
                FrmOrdenProducciondeServicio AgregarServicio = new FrmOrdenProducciondeServicio(OrdenProduccion, itemOrdenProduccionServicio, ItemOrdenProduccion);
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

        private void ubEliminarServicio_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null) { return; }
            ItemOrdenProduccion.Servicios.Remove((ItemOrdenProduccionServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
            //MostrarTotalServicio(ItemOrdenProduccion);
        }

        public override void Aceptar()
        {
            try
            {
                Costeo();
                GenerarImagenes();
                base.Aceptar();
            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }
            
            
        }

        Boolean acatualizalistas = true;
        ListaCostosMaquina lcm = null;
        ListaPreciosExistencia lpe = null;
        ListaPreciosTransporte lpt = null;

        private void Costeo()
        {
            try
            {
                if (OrdenProduccion.ListaCostosMaquina == null) {
                    throw new Exception("Debe Eligir una lista de costos de maquina");
                }
                if (OrdenProduccion.ListaPreciosExistencia == null)
                {
                    throw new Exception("Debe Eligir una lista de costos de Existencia");
                }
                if (OrdenProduccion.ListaPreciosTransporte == null)
                {
                    throw new Exception("Debe Eligir una lista de costos de Transporte");
                }

                if (acatualizalistas)
                {
                    lcm = (ListaCostosMaquina)HelperNHibernate.GetEntityByID("ListaCostosMaquina", OrdenProduccion.ListaCostosMaquina.ID);
                    lpe = (ListaPreciosExistencia)HelperNHibernate.GetEntityByID("ListaPreciosExistencia", OrdenProduccion.ListaPreciosExistencia.ID);
                    lpt = (ListaPreciosTransporte)HelperNHibernate.GetEntityByID("ListaPreciosTransporte", OrdenProduccion.ListaPreciosTransporte.ID);
                    acatualizalistas = false;
                }

                foreach (ItemOrdenProduccion itemOp in OrdenProduccion.Items)
                {
                    CosteoElemento(itemOp);
                }
            }
            catch (Exception ex)
            {

                SoftException.ShowException(ex);
            }
        }

        private void CosteoElemento(ItemOrdenProduccion item)
        {
            if (item.Maquina != null)
            {
                item.CostoMaquina = obtenerItemListaCostosMaquina(item);
            }
            else
            {
                item.CostoMaquina = 0;
            }


            if (item.Material != null)
            {
                item.CostoMaterial = obtenerItemListaCostosMaterial(item);
            }
            else
            {
                item.CostoMaterial = 0;
            }

            Decimal totalservicio = 0;
            foreach (ItemOrdenProduccionServicio itemServicio in item.Servicios)
            {
                totalservicio += itemServicio.CostoTotalServicio;
            }
            item.CostoServicio = totalservicio;
            item.Cantidad = 1;
            item.Costo = item.CostoMaquina + item.CostoMaterial + item.CostoServicio;
            item.Precio = item.Costo;
        }

        private Decimal obtenerItemListaCostosMaquina(ItemOrdenProduccion itemOP)
        {
            Decimal resultado = 0;
            try
            {
                ItemListaCostosMaquina ilcm = obtenerItemListaCostosMaquina(itemOP.Maquina);
                UnidadListaCostosMaquina Uilcm = obtenerUnidadLCM(ilcm);
                EscalaListaCostosMaquina Elcm = obtenerEscalaLCM(Uilcm, itemOP);

                Int32 multiplicador = 1;
                if (Uilcm.Unidad.Nombre.Equals("MILLAR"))
                {
                    multiplicador = 1000;
                    resultado = (itemOP.CantidadProduccion / itemOP.NumerodePases) / multiplicador;
                    Int32 entero = Convert.ToInt32(resultado);
                    Decimal residuo = (resultado - entero) * 100;
                    if (residuo >= 20 && residuo <= 100)
                    {
                        resultado = (entero + 1) * Elcm.Costo * itemOP.NumerodePases * itemOP.NumeroPliegos;
                    }
                    else if (entero == 0 && residuo > 0)
                    {
                        resultado = (1) * Elcm.Costo * itemOP.NumerodePases * itemOP.NumeroPliegos;
                    }
                    else
                    {
                        resultado = (entero) * Elcm.Costo * itemOP.NumerodePases * itemOP.NumeroPliegos;
                    }
                }

                else
                {
                    multiplicador = 1;
                    resultado = itemOP.CantidadProduccion / multiplicador;
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

        private EscalaListaCostosMaquina obtenerEscalaLCM(UnidadListaCostosMaquina Uilcm, ItemOrdenProduccion itemOP)
        {
            EscalaListaCostosMaquina eUilcm = null;

            if (Uilcm == null)
            {
                return eUilcm;
            }

            CalcularProduccionItem(itemOP);


            Decimal cantidadProduccion = 0;
            if (Uilcm.Unidad.Nombre.Equals("MILLAR"))
            {

                cantidadProduccion = itemOP.CantidadProduccion / 1000;

                Decimal entero = Convert.ToInt32(cantidadProduccion);

                Decimal dif = (cantidadProduccion - entero) * 100;
                if (dif < 20)
                {

                    cantidadProduccion = entero;
                }



            }
            else
            {
                cantidadProduccion = itemOP.CantidadProduccion;

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

        private Decimal obtenerItemListaCostosMaterial(ItemOrdenProduccion itemOP)
        {
            Decimal resultado = 0;
            try
            {
                if (itemOP.NumeroPliegos == 0)
                {
                    itemOP.NumeroPliegos = 1;
                }

                resultado = itemOP.Material.CostoUltimaCompra * (itemOP.CantidadMaterial + itemOP.CantidadDemasiaMaterial) * itemOP.NumeroPliegos;

            }
            catch (Exception)
            {
            }


            return resultado;

        }

        private void ubAceptar_Click(object sender, EventArgs e)
        {

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

    }
}
