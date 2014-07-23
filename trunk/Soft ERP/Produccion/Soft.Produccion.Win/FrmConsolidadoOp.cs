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
using System.Xml;
using Infragistics.Win.UltraWinGrid;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.Exceptions;

namespace Soft.Produccion.Win
{
    public partial class FrmConsolidadoOp : FrmParent
    {
        public FrmConsolidadoOp()
        {
            InitializeComponent();
        }

        public ConsolidadoOp ConsolidadoOp { get { return (ConsolidadoOp)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        const String colNumero = "Número";
        const String colDescripcion = "Descripcion";
        const String colCantidad = "Cantidad";

        const String colPresupuesto = "Presupuesto";
        const String colCotizacion = "Cotizacion";
        const String colSolicitudCotizacion = "Sol. Cotizacion";


        const String colEstadoEntrega = "E. Entrega";
        const String colEstadoFacturacion = "E. Facturacion";


        const String colFecha = "Fecha";


        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNumero);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colDescripcion);
            column.DataType = typeof(String);
            column.ReadOnly = true;


            column = columns.Columns.Add(colCantidad);
            column.DataType = typeof(Int64);
            column.ReadOnly = true;



            column = columns.Columns.Add(colFecha);
            column.DataType = typeof(DateTime);
            column.ReadOnly = true;



            column = columns.Columns.Add(colPresupuesto);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colCotizacion);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colSolicitudCotizacion);
            column.DataType = typeof(String);
            column.ReadOnly = true;


            column = columns.Columns.Add(colEstadoEntrega);
            column.DataType = typeof(String);
            column.ReadOnly = true;

            column = columns.Columns.Add(colEstadoFacturacion);
            column.DataType = typeof(String);
            column.ReadOnly = true;



            ugConsolidadosOp.DataSource = columns;


            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colNumero].CellActivation = Activation.ActivateOnly;
            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colDescripcion].CellActivation = Activation.ActivateOnly;
            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colCantidad].CellActivation = Activation.ActivateOnly;
            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colPresupuesto].CellActivation = Activation.ActivateOnly;
            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colCotizacion].CellActivation = Activation.ActivateOnly;
            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colSolicitudCotizacion].CellActivation = Activation.ActivateOnly;

            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colEstadoEntrega].CellActivation = Activation.ActivateOnly;
            ugConsolidadosOp.DisplayLayout.Bands[0].Columns[colEstadoFacturacion].CellActivation = Activation.ActivateOnly;


            MapKeys(ref ugConsolidadosOp);
        }

        public void Mostrar()
        {
            ssTipoConsolidadosOp.Text = (ConsolidadoOp.TipoDocumento != null) ? ConsolidadoOp.TipoDocumento.Nombre : "";
            ssCliente.Text = (ConsolidadoOp.Cliente != null) ? ConsolidadoOp.Cliente.Nombre : "";
            busResponsable.Text = (ConsolidadoOp.Responsable != null) ? ConsolidadoOp.Responsable.Nombre : "";
            txtNumeracion.Text = ConsolidadoOp.Numeracion;
            udtFechaCreacion.Value = ConsolidadoOp.FechaCreacion;
            MostrarItems(false);
        }

        public void MostrarItems(Boolean Nuevo)
        {
            if (ConsolidadoOp.Items.Count == 0)
            {
                base.ClearAllRows(ref ugConsolidadosOp);

                return;
            }
            base.ClearAllRows(ref ugConsolidadosOp);
            String Filtro = String.Format(" IDOrdenProduccion IN ({0})", ConsolidadoOp.ObtenerFiltroOPs());
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_ItemConsolidadoOpxOrdenProduccion", Filtro);
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    ItemConsolidadoOp Item = ConsolidadoOp.ObtenerItem(NodoItem.SelectSingleNode("@IDOrdenProduccion").Value);
                    UltraGridRow Row = ugConsolidadosOp.DisplayLayout.Bands[0].AddNew();
                    Row.Tag = Item;
                    Row.Cells[colNumero].Value = NodoItem.SelectSingleNode("@Numeracion").Value;
                    Row.Cells[colDescripcion].Value = NodoItem.SelectSingleNode("@Descripcion").Value;
                    Row.Cells[colCantidad].Value = Convert.ToInt64(NodoItem.SelectSingleNode("@Cantidad").Value);
                    Row.Cells[colFecha].Value = NodoItem.SelectSingleNode("@Fecha").Value;

                    Row.Cells[colPresupuesto].Value = NodoItem.SelectSingleNode("@Presupuesto").Value;
                    Row.Cells[colCotizacion].Value = NodoItem.SelectSingleNode("@Cotizacion").Value;
                    Row.Cells[colSolicitudCotizacion].Value = NodoItem.SelectSingleNode("@SolicitudCotizacion").Value;

                    Row.Cells[colEstadoEntrega].Value = NodoItem.SelectSingleNode("@EstadoEntrega").Value;
                    Row.Cells[colEstadoFacturacion].Value = NodoItem.SelectSingleNode("@EstadoFacturacion").Value;

                }
            }
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarCliente = new FrmSelectedEntity();

                SocioNegocio SocioNegocio = (SocioNegocio)FrmSeleccionarCliente.GetSelectedEntity(typeof(SocioNegocio), "Cliente Pendiente de Consolidados de Ops");

                if (SocioNegocio != null) {
                    ConsolidadoOp.Cliente = SocioNegocio;
                    ConsolidadoOp.Items.Clear();
                }

                Mostrar();
            }
            catch (Exception)
            {
                
                throw;
            }

            
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            ConsolidadoOp.Numeracion = txtNumeracion.Text;
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            ConsolidadoOp.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
        }

        private void ubAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                if (ConsolidadoOp.TipoDocumento == null)
                {
                    throw new Exception("Debe elegir un tipo de Documento");
                }


                if (ConsolidadoOp.Cliente == null)
                {
                    throw new Exception("Debe seleccionar un Cliente");
                }


                Collection OrdenesProduccion = new Collection();
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                String Filtro = ConsolidadoOp.ObtenerFiltroOPs();
                Filtro = (Filtro.Length > 0) ? String.Format(" ID NOT IN ({0}) AND IDCliente = '{1}'", Filtro, ConsolidadoOp.Cliente.ID) : String.Format(" IDCliente = '{0}'", ConsolidadoOp.Cliente.ID);
                OrdenesProduccion = FrmSeleccionar.GetSelectedsEntities(typeof(OrdenProduccion), "Selección de Ordenes de Produccion Consolidado", Filtro);
                foreach (OrdenProduccion Item in OrdenesProduccion)
                {
                    ConsolidadoOp.AddItem(Item);
                }
                MostrarItems(true);
            }
            catch (Exception ex)
            {

                SoftException.Control(ex);
            }

        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugConsolidadosOp.ActiveRow == null) { return; }
            ConsolidadoOp.Items.Remove((ItemConsolidadoOp)ugConsolidadosOp.ActiveRow.Tag);
            ugConsolidadosOp.ActiveRow.Delete(false);
            MostrarItems(false);
        }

        private void ssTipoPresupuesto_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionarTipoDocumento = new FrmSelectedEntity();
                TipoDocumentoConsolidadoOp TipoDocumento = (TipoDocumentoConsolidadoOp)FrmSeleccionarTipoDocumento.GetSelectedEntity(typeof(TipoDocumentoConsolidadoOp), "Tipo Documento Consolidado Op");
                if (TipoDocumento != null)
                {
                    ConsolidadoOp.TipoDocumento = (TipoDocumentoConsolidadoOp)HelperNHibernate.GetEntityByID("TipoDocumentoConsolidadoOp", TipoDocumento.ID);
                    ssTipoConsolidadosOp.Text = (ConsolidadoOp.TipoDocumento != null) ? ConsolidadoOp.TipoDocumento.Nombre : "";

                    try
                    {
                        FrmSelectedEntity FrmSeleccionarEmpleado = new FrmSelectedEntity();
                        String filtro = "IDUsuario='" + FrmMain.Usuario.ID + "'";
                        SocioNegocio sn = (SocioNegocio)FrmSeleccionarEmpleado.GetSelectedEntity(typeof(SocioNegocio), "Empleado", filtro);

                        ConsolidadoOp.Responsable = (SocioNegocio)HelperNHibernate.GetEntityByID("SocioNegocio", sn.ID);
                        busResponsable.Text = ConsolidadoOp.Responsable.Nombre;
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    ssTipoConsolidadosOp.Text = "";
                    busResponsable.Text = "";

                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void busResponsable_Search(object sender, EventArgs e)
        {
            try
            {

            FrmSelectedEntity FrmSeleccionarResponsable = new FrmSelectedEntity();
            ConsolidadoOp.Responsable = (SocioNegocio)FrmSeleccionarResponsable.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
            busResponsable.Text = (ConsolidadoOp.Responsable != null) ? ConsolidadoOp.Responsable.Nombre : "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void busResponsable_Clear(object sender, EventArgs e)
        {
            try
            {   
                ConsolidadoOp.Responsable = null;
                busResponsable.Text = "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssTipoConsolidadosOp_Clear(object sender, EventArgs e)
        {
            try
            {
                ConsolidadoOp.TipoDocumento = null;
                ssTipoConsolidadosOp.Text = "";
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCliente_Clear(object sender, EventArgs e)
        {
            try
            {
                ConsolidadoOp.Cliente = null;
                ConsolidadoOp.Items.Clear();
                Mostrar();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }


    }
}
