﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Facturacion.Entidades;
using System.Xml;
using Soft.DataAccess;
using Infragistics.Win.UltraWinGrid;
using Soft.Exceptions;
using Soft.Entities;
using Microsoft.VisualBasic;
using Soft.Produccion.Entidades;

namespace Soft.Facturacion.Win
{
    public partial class FrmGuiaRemision : FrmParent
    {
        public FrmGuiaRemision()
        {
            InitializeComponent();
        }



        #region "Propiedades"

        const String colNroOP = "Nº OP";
        const String colDescripcion = "Descripción";
        const String colCantidad = "Cantidad";
        const String colObservacion = "Observación";

        public Soft.Facturacion.Entidades.GuiaRemision GuiaRemision { get { return (GuiaRemision)base.m_ObjectFlow; } }

        #endregion


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


            ugOrdenesProduccion.DataSource = columns;
            MapKeys(ref ugOrdenesProduccion);
        }



        public void Mostrar()
        {
            ssTipoDocumento.Text = (GuiaRemision.TipoEntrega != null) ? GuiaRemision.TipoEntrega.Descripcion : "";
            ssCliente.Text = (GuiaRemision.Cliente != null) ? GuiaRemision.Cliente.Nombre : "";
            ssResponsable.Text = (GuiaRemision.Responsable != null) ? GuiaRemision.Responsable.Nombre : "";
            txtNumeracion.Text = GuiaRemision.Numeracion;
            udtFechaCreacion.Value = GuiaRemision.FechaCreacion;
            txtObservacion.Text = GuiaRemision.Observacion;
            MostrarItems();
            if (!GuiaRemision.NewInstance) { DeshabilitarControles(); }
        }

        public void DeshabilitarControles()
        {
            ssTipoDocumento.Enabled = false;
            ssCliente.Enabled = false;
            ssResponsable.Enabled = false;
            txtNumeracion.Enabled = false;
            udtFechaCreacion.Enabled = false;
            ubAgregarOP.Enabled = false;
            ubQuitarOP.Enabled = false;
            txtObservacion.Enabled = false;
        }


        public void MostrarItems()
        {
            if (GuiaRemision.Items.Count == 0)
            {
                GuiaRemision.Total = 0;
                return;
            }
            base.ClearAllRows(ref ugOrdenesProduccion);
            String Filtro = String.Format(" IDOP IN ({0})", GuiaRemision.ObtenerFiltroOps());
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_OrdenProduccionxID", Filtro);
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    ItemGuiaRemision Item = GuiaRemision.ObtenerItem(NodoItem.SelectSingleNode("@IDOP").Value);
                    Item.Total = Convert.ToDecimal(NodoItem.SelectSingleNode("@Total").Value);
                    UltraGridRow Row = ugOrdenesProduccion.DisplayLayout.Bands[0].AddNew();
                    Row.Tag = Item;
                    Row.Cells[colNroOP].Value = NodoItem.SelectSingleNode("@Numeracion").Value;
                    Row.Cells[colDescripcion].Value = NodoItem.SelectSingleNode("@Descripcion").Value;
                    Row.Cells[colCantidad].Value = NodoItem.SelectSingleNode("@Cantidad").Value;
                    Row.Cells[colObservacion].Value = NodoItem.SelectSingleNode("@Observacion").Value;
                }
            }
        }

        private void ssTipoDocumento_Search(object sender, EventArgs e)
        {
            try
            {
                FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                GuiaRemision.TipoEntrega = (TipoEntrega)FrmSeleccionar.GetSelectedEntity(typeof(TipoEntrega), "Tipo de Entrega", All: true);
                if (GuiaRemision.TipoEntrega != null)
                {
                    ssTipoDocumento.Text = GuiaRemision.TipoEntrega.Descripcion;
                    txtNumeracion.Enabled = !GuiaRemision.TipoEntrega.GeneraNumeracionAlFinal;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

        private void ssTipoDocumento_Clear(object sender, EventArgs e)
        {
            try
            {
                GuiaRemision.TipoEntrega = null;
                Mostrar();
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
                GuiaRemision.Cliente = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Cliente");
                if (GuiaRemision.Cliente != null)
                {
                    ssCliente.Text = GuiaRemision.Cliente.Nombre;
                }
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
                GuiaRemision.Cliente = null;
                Mostrar();
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
                GuiaRemision.Responsable = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio", " Empleado = 1");
                if (GuiaRemision.Responsable != null)
                {
                    ssResponsable.Text = GuiaRemision.Responsable.Nombre;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }

        }

        private void ssResponsable_Clear(object sender, EventArgs e)
        {

            try
            {
                GuiaRemision.Responsable = null;
                Mostrar();
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
                String Filtro = GuiaRemision.ObtenerFiltroOps();
                Filtro = (Filtro.Length > 0) ? String.Format(" ID NOT IN ({0}) AND IDCliente = '{1}' AND EstadoEntrega = 'PENDIENTE'", Filtro, GuiaRemision.Cliente.ID) : String.Format(" IDCliente = '{0}' AND EstadoEntrega = 'PENDIENTE'", GuiaRemision.Cliente.ID);
                Ops = FrmSeleccionar.GetSelectedsEntities(typeof(OrdenProduccion), "Selección de Ordenes de Producción", Filtro);
                foreach (OrdenProduccion Item in Ops)
                {
                    GuiaRemision.AddItem(Item.ID);
                }
                MostrarItems();
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
                GuiaRemision.Items.Remove((ItemGuiaRemision)ugOrdenesProduccion.ActiveRow.Tag);
                ugOrdenesProduccion.ActiveRow.Delete(false);
                MostrarItems();
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
                GuiaRemision.Observacion = txtObservacion.Text;
            }
            catch (Exception ex)
            {
                SoftException.Control(ex);
            }
        }

     

     


    }
}