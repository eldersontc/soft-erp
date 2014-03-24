using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Facturacion.Entidades;
using Soft.Exceptions;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.Produccion.Entidades;
using System.Xml;
using Soft.DataAccess;
using Infragistics.Win.UltraWinGrid;
using Soft.Facturacion.Entidades;

namespace Soft.Facturacion.Win
{
    public partial class FrmNotaDebito : FrmParent
    {
        public FrmNotaDebito()
        {
            InitializeComponent();
        }

        #region "Propiedades"

        const String colNroOP = "Nº OP";
        const String colDescripcion = "Descripción";
        const String colCantidad = "Cantidad";
        const String colObservacion = "Observación";
        const String colTotal = "Total";

        public NotaDebito NotaDebito { get { return (NotaDebito)base.m_ObjectFlow; } }

        #endregion

        #region "Métodos"

        public override void Init()
        {
            InitGrids();
            Mostrar();
        }

        public void InitGrids()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNroOP);
            column.DataType = typeof(string);

            column = columns.Columns.Add(colDescripcion);
            column.DataType = typeof(string);

            column = columns.Columns.Add(colCantidad);
            column.DataType = typeof(decimal);

            column = columns.Columns.Add(colObservacion);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colTotal);
            column.DataType = typeof(decimal);

            ugOrdenesProduccion.DataSource = columns;
            MapKeys(ref ugOrdenesProduccion);
        }

        public void Mostrar()
        {
            ssTipoDocumento.Text = (NotaDebito.TipoNotaDebito != null) ? NotaDebito.TipoNotaDebito.Descripcion : "";
            ssCliente.Text  = (NotaDebito.Cliente != null) ? NotaDebito.Cliente.Nombre : "";
            ssResponsable.Text = (NotaDebito.Responsable != null) ? NotaDebito.Responsable.Nombre : "";
            ssMoneda.Text = (NotaDebito.Moneda != null) ? NotaDebito.Moneda.Simbolo : "";
            txtNumeracion.Text = NotaDebito.Numeracion;
            udtFechaCreacion.Value = NotaDebito.FechaCreacion;
            txtObservacion.Text = NotaDebito.Observacion;
            txtNroFactura.Text = NotaDebito.NroFactura;
            MostrarItems();
            MostrarTotales();
            if (!NotaDebito.NewInstance) { DeshabilitarControles(); }
        }

        public void MostrarItems()
        {
            if (NotaDebito.Items.Count == 0)
            {
                NotaDebito.Total = 0;
                uneTotal.Value = 0;
                return;
            }
            base.ClearAllRows(ref ugOrdenesProduccion);
            String Filtro = String.Format(" IDOP IN ({0})", NotaDebito.ObtenerFiltroOps());
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_OrdenProduccionxID", Filtro);
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    ItemNotaDebito Item = NotaDebito.ObtenerItem(NodoItem.SelectSingleNode("@IDOP").Value);
                    Item.Total = Convert.ToDecimal(NodoItem.SelectSingleNode("@Total").Value);
                    UltraGridRow Row = ugOrdenesProduccion.DisplayLayout.Bands[0].AddNew();
                    Row.Tag = Item;
                    Row.Cells[colNroOP].Value = NodoItem.SelectSingleNode("@Numeracion").Value;
                    Row.Cells[colDescripcion].Value = NodoItem.SelectSingleNode("@Descripcion").Value;
                    Row.Cells[colCantidad].Value = NodoItem.SelectSingleNode("@Cantidad").Value;
                    Row.Cells[colObservacion].Value = NodoItem.SelectSingleNode("@Observacion").Value;
                    Row.Cells[colTotal].Value = Convert.ToDecimal(NodoItem.SelectSingleNode("@Total").Value);
                }
            }
        }

        public void MostrarTotales() {
            NotaDebito.CalcularTotales();
            uneSubTotal.Value = NotaDebito.SubTotal;
            uneImpuesto.Value = NotaDebito.Impuesto;
            uneTotal.Value = NotaDebito.Total;
        }

        public void DeshabilitarControles() {
            ssTipoDocumento.Enabled = false;
            ssCliente.Enabled = false;
            ssResponsable.Enabled = false;
            ssMoneda.Enabled = false;
            txtNumeracion.Enabled = false;
            udtFechaCreacion.Enabled = false;
            ubAgregarOP.Enabled = false;
            ubQuitarOP.Enabled = false;
            txtObservacion.Enabled = false;
        }

        #endregion

        #region "Eventos"

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                NotaDebito.TipoNotaDebito = (TipoNotaDebito)FrmSeleccionar.GetSelectedEntity(typeof(TipoNotaDebito), "Tipo Nota Débito", All:true);
                if (NotaDebito.TipoNotaDebito != null)
                {
                    ssTipoDocumento.Text = NotaDebito.TipoNotaDebito.Descripcion;
                    txtNumeracion.Enabled = !NotaDebito.TipoNotaDebito.GeneraNumeracionAlFinal;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtNumeracion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NotaDebito.Numeracion = txtNumeracion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssCliente_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                NotaDebito.Cliente = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Cliente");
                if (NotaDebito.Cliente != null)
                {
                    ssCliente.Text = NotaDebito.Cliente.Nombre;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void udtFechaCreacion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NotaDebito.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssResponsable_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                NotaDebito.Responsable = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                if (NotaDebito.Responsable != null)
                {
                    ssResponsable.Text = NotaDebito.Responsable.Nombre;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssMoneda_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                NotaDebito.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
                if (NotaDebito.Moneda != null)
                {
                    ssMoneda.Text = NotaDebito.Moneda.Simbolo;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubAgregarOP_Click(object sender, EventArgs e)
        {
            try
            {
                Collection Ops = new Collection();
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                String Filtro = NotaDebito.ObtenerFiltroOps();
                Filtro = (Filtro.Length > 0) ? String.Format(" ID NOT IN ({0}) AND IDCliente = '{1}' AND EstadoFacturacion = 'PENDIENTE'", Filtro, NotaDebito.Cliente.ID) : String.Format(" IDCliente = '{0}' AND EstadoFacturacion = 'PENDIENTE'", NotaDebito.Cliente.ID);
                Ops = FrmSeleccionar.GetSelectedsEntities(typeof(OrdenProduccion), "Selección de Ordenes de Producción", Filtro);
                foreach (OrdenProduccion Item in Ops)
                {
                    NotaDebito.AddItem(Item.ID);
                }
                MostrarItems();
                MostrarTotales();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ubQuitarOP_Click(object sender, EventArgs e)
        {
            try
            {
                if (ugOrdenesProduccion.ActiveRow == null) { return; }
                NotaDebito.Items.Remove((ItemNotaDebito)ugOrdenesProduccion.ActiveRow.Tag);
                ugOrdenesProduccion.ActiveRow.Delete(false);
                MostrarItems();
                MostrarTotales();
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void txtObservacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                NotaDebito.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        #endregion
    }
}
