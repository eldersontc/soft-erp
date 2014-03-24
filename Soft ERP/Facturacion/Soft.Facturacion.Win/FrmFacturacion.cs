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

namespace Soft.Facturacion.Win
{
    public partial class FrmFacturacion : FrmParent
    {
        public FrmFacturacion()
        {
            InitializeComponent();
        }

        #region "Propiedades"

        const String colNroOP = "Nº OP";
        const String colDescripcion = "Descripción";
        const String colCantidad = "Cantidad";
        const String colObservacion = "Observación";
        const String colTotal = "Total";

        public Soft.Facturacion.Entidades.Facturacion Facturacion { get { return (Soft.Facturacion.Entidades.Facturacion)base.m_ObjectFlow; } }

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
            ssTipoDocumento.Text = (Facturacion.TipoFacturacion != null) ? Facturacion.TipoFacturacion.Descripcion : "";
            ssCliente.Text  = (Facturacion.Cliente != null) ? Facturacion.Cliente.Nombre : "";
            ssResponsable.Text = (Facturacion.Responsable != null) ? Facturacion.Responsable.Nombre : "";
            ssMoneda.Text = (Facturacion.Moneda != null) ? Facturacion.Moneda.Simbolo : "";
            txtNumeracion.Text = Facturacion.Numeracion;
            udtFechaCreacion.Value = Facturacion.FechaCreacion;
            txtObservacion.Text = Facturacion.Observacion;
            MostrarItems();
            MostrarTotales();
            if (!Facturacion.NewInstance) { DeshabilitarControles(); }
        }

        public void MostrarItems()
        {
            if (Facturacion.Items.Count == 0)
            {
                Facturacion.Total = 0;
                uneTotal.Value = 0;
                return;
            }
            base.ClearAllRows(ref ugOrdenesProduccion);
            String Filtro = String.Format(" IDOP IN ({0})", Facturacion.ObtenerFiltroOps());
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_OrdenProduccionxID", Filtro);
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    ItemFacturacion Item = Facturacion.ObtenerItem(NodoItem.SelectSingleNode("@IDOP").Value);
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
            Facturacion.CalcularTotales();
            uneSubTotal.Value = Facturacion.SubTotal;
            uneImpuesto.Value = Facturacion.Impuesto;
            uneTotal.Value = Facturacion.Total;
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
                Facturacion.TipoFacturacion = (TipoFacturacion)FrmSeleccionar.GetSelectedEntity(typeof(TipoFacturacion), "Tipo Facturación", All:true);
                if (Facturacion.TipoFacturacion != null)
                {
                    ssTipoDocumento.Text = Facturacion.TipoFacturacion.Descripcion;
                    txtNumeracion.Enabled = !Facturacion.TipoFacturacion.GeneraNumeracionAlFinal;
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
                Facturacion.Numeracion = txtNumeracion.Text;
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
                Facturacion.Cliente = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Cliente");
                if (Facturacion.Cliente != null)
                {
                    ssCliente.Text = Facturacion.Cliente.Nombre;
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
                Facturacion.FechaCreacion = Convert.ToDateTime(udtFechaCreacion.Value);
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
                Facturacion.Responsable = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                if (Facturacion.Responsable != null)
                {
                    ssResponsable.Text = Facturacion.Responsable.Nombre;
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
                Facturacion.Moneda = (Moneda)FrmSeleccionar.GetSelectedEntity(typeof(Moneda), "Moneda");
                if (Facturacion.Moneda != null)
                {
                    ssMoneda.Text = Facturacion.Moneda.Simbolo;
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
                String Filtro = Facturacion.ObtenerFiltroOps();
                Filtro = (Filtro.Length > 0) ? String.Format(" ID NOT IN ({0}) AND IDCliente = '{1}' AND EstadoFacturacion = 'PENDIENTE'", Filtro, Facturacion.Cliente.ID) : String.Format(" IDCliente = '{0}' AND EstadoFacturacion = 'PENDIENTE'", Facturacion.Cliente.ID);
                Ops = FrmSeleccionar.GetSelectedsEntities(typeof(OrdenProduccion), "Selección de Ordenes de Producción", Filtro);
                foreach (OrdenProduccion Item in Ops)
                {
                    Facturacion.AddItem(Item.ID);
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
                Facturacion.Items.Remove((ItemFacturacion)ugOrdenesProduccion.ActiveRow.Tag);
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
                Facturacion.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        #endregion
    }
}
