using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;
using Soft.DataAccess;
using Soft.Win;
using Soft.Ventas.Entidades;
using Soft.Inventario.Entidades;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;



namespace Soft.Ventas.Win
{
    public partial class FrmMaquina : FrmParent
    {
        public FrmMaquina()
        {
            InitializeComponent();
        }

        public Maquina Maquina { get { return (Maquina)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            InitGrid();
            Mostrar();
        }
        private Boolean ActualizandoIU = false;

        //Constantes
        const String colNobre = "Nombre";
        const String colUnidad = "Unidad";

        public void InitGrid()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colNobre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colUnidad);
            column.DataType = typeof(String);


            ugServicios.DataSource = columns;
            ugServicios.DisplayLayout.Bands[0].Columns[colNobre].Width = 200;
            MapKeys(ref ugServicios);
        }

        public void Mostrar()
        {
            ActualizandoIU = true;
            ssTipoMaquina.Text = (Maquina.TipoMaquina != null)?Maquina.TipoMaquina.Descripcion:"";
            ssMarca.Text = (Maquina.Marca != null) ? Maquina.Marca.Nombre : "";
            ssProveedor.Text = (Maquina.Proveedor != null) ? Maquina.Proveedor.Nombre : "";
            txtCodigo.Text = Maquina.Codigo;
            txtNombre.Text = Maquina.Nombre;
            uneCantidadCuerpos.Value = Maquina.CantidadCuerpos;
            uneGramajeMinimo.Value = Maquina.MinimoGramaje;
            uneGramajeMaximo.Value = Maquina.MaximoGramaje;
            uneAnchoMinimo.Value = Maquina.PliegoAnchoMimino;
            uneAnchoMaximo.Value = Maquina.PliegoAnchoMaximo;
            uneAltoMinimo.Value = Maquina.PliegoAltoMinimo;
            uneAltoMaximo.Value = Maquina.PliegoAltoMaximo;
            unePinza.Value = Maquina.MargenPinza;
            uneSalida.Value = Maquina.MargenSalida;
            uneEscuadra.Value = Maquina.MargenEscuadra;
            uneContraEscuadra.Value = Maquina.MargenContraEscuadra;
            uneCalle.Value = Maquina.MargenCalle;
            uceActivo.Checked = Maquina.Activo;
            txtResolucionMinimo.Value = Maquina.ResolucionMinimo;
            txtResolucionMaximo.Value = Maquina.ResolucionMaximo;
            txtDescripcción.Text = Maquina.Descripcion;

            MostrarItems();

            ActualizandoIU = false;

        }



        private void MostrarItems()
        {
            base.ClearAllRows(ref ugServicios);
            foreach (ItemMaquinaServicio Item in Maquina.ItemsServicio)
            {
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                MostrarServicio(Row);
            }
        }


        private void MostrarServicio(UltraGridRow Row)
        {
            ItemMaquinaServicio Item = (ItemMaquinaServicio)Row.Tag;
            if (Item.Servicio != null)
            {
                AgregarUnidades(Row);
                Row.Cells[colNobre].Value = Item.Servicio.Nombre;
                Row.Cells[colUnidad].Value = Item.Unidad.Unidad.Codigo;
            }
        }


        public void AgregarUnidades(UltraGridRow Row)
        {
            ItemMaquinaServicio Item = (ItemMaquinaServicio)Row.Tag;
            ValueList List = new ValueList();
            if (Item.Servicio != null)
            {
                Item.Unidad = Item.Servicio.UnidadBase;
            }

            foreach (ExistenciaUnidad Unidad in Item.Servicio.Unidades)
            {
                if (Item.Unidad == null & Unidad.EsUnidadBase)
                {
                    Item.Unidad = Unidad;
                }
                List.ValueListItems.Add(Unidad, Unidad.Unidad.Codigo);
            }

            Row.Cells[colUnidad].ValueList = List;
        }


        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            Maquina.Codigo = txtCodigo.Text;
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            Maquina.Activo = uceActivo.Checked;
        }

        private void ssTipoMaquina_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Maquina.TipoMaquina = (TipoMaquina)FrmSeleccionar.GetSelectedEntity(typeof(TipoMaquina), "Tipo Máquina");
            ssTipoMaquina.Text = (Maquina.TipoMaquina != null) ? Maquina.TipoMaquina.Descripcion : "";
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            Maquina.Nombre = txtNombre.Text;
        }

        private void ssMarca_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Maquina.Marca = (Marca)FrmSeleccionar.GetSelectedEntity(typeof(Marca), "Marca");
            ssMarca.Text = (Maquina.Marca != null) ? Maquina.Marca.Nombre : "";
        }

        private void ssProveedor_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Maquina.Proveedor = (SocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocio), "Socio de Negocio");
            ssProveedor.Text = (Maquina.Proveedor != null) ? Maquina.Proveedor.Nombre : "";
        }

        private void uneCantidadCuerpos_ValueChanged(object sender, EventArgs e)
        {
            Maquina.CantidadCuerpos = Convert.ToInt32(uneCantidadCuerpos.Value);
        }

        private void uneGramajeMinimo_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MinimoGramaje = Convert.ToDecimal(uneGramajeMinimo.Value);
        }

        private void uneGramajeMaximo_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MaximoGramaje = Convert.ToDecimal(uneGramajeMaximo.Value);
        }

        private void uneAnchoMinimo_ValueChanged(object sender, EventArgs e)
        {
            Maquina.PliegoAnchoMimino = Convert.ToInt32(uneAnchoMinimo.Value);
        }

        private void uneAnchoMaximo_ValueChanged(object sender, EventArgs e)
        {
            Maquina.PliegoAnchoMaximo = Convert.ToInt32(uneAnchoMaximo.Value);
        }

        private void uneAltoMinimo_ValueChanged(object sender, EventArgs e)
        {
            Maquina.PliegoAltoMinimo = Convert.ToInt32(uneAltoMinimo.Value);
        }

        private void uneAltoMaximo_ValueChanged(object sender, EventArgs e)
        {
            Maquina.PliegoAltoMaximo = Convert.ToInt32(uneAltoMaximo.Value);
        }

        private void unePinza_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MargenPinza = Convert.ToInt32(unePinza.Value);
        }

        private void uneSalida_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MargenSalida = Convert.ToInt32(uneSalida.Value);
        }

        private void uneEscuadra_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MargenEscuadra = Convert.ToInt32(uneEscuadra.Value);
        }

        private void uneContraEscuadra_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MargenContraEscuadra = Convert.ToInt32(uneContraEscuadra.Value);
        }

        private void uneCalle_ValueChanged(object sender, EventArgs e)
        {
            Maquina.MargenCalle = Convert.ToInt32(uneCalle.Value);
        }

        private void txtResolucionMinimo_ValueChanged(object sender, EventArgs e)
        {
                Maquina.ResolucionMinimo = Convert.ToInt32(txtResolucionMinimo.Value);  
            
             
        }

        private void txtResolucionMaximo_ValueChanged(object sender, EventArgs e)
        {
                Maquina.ResolucionMaximo = Convert.ToInt32(txtResolucionMaximo.Value);
            
        }

        private void txtDescripcción_ValueChanged(object sender, EventArgs e)
        {
            if (ActualizandoIU) { return; }

            try
            {
                Maquina.Descripcion = txtDescripcción.Text;
            }
            catch (Exception ex)
            {

                Soft.Exceptions.SoftException.ShowException(ex);
            }

        }

        private void ubNuevo_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();

            String Filtro = "ID NOT IN (";
            String IDs = "";
            foreach (ItemMaquinaServicio Item in Maquina.ItemsServicio)
            {
                IDs = IDs + "'" + Item.Servicio.ID + "',";
            }
            Filtro = (IDs.Length > 0) ? Filtro + IDs.Substring(0, IDs.Length - 1) + ") AND EsServicio = 1" : " EsServicio = 1";

            Existencia Existencia = (Existencia)FrmSeleccionar.GetSelectedEntity(typeof(Existencia), "Existencia", Filtro);
            if (Existencia != null)
            {
                Existencia ExistenciaCompleta = (Existencia)HelperNHibernate.GetEntityByID("Existencia", Existencia.ID);
                UltraGridRow Row = ugServicios.DisplayLayout.Bands[0].AddNew();
                Row.Tag =Maquina.CrearServicio(ExistenciaCompleta);
                Mostrar();
            }
        }

        private void ubEliminar_Click(object sender, EventArgs e)
        {
            if (ugServicios.ActiveRow == null || Maquina == null) { return; }
            Maquina.ItemsServicio.Remove((ItemMaquinaServicio)ugServicios.ActiveRow.Tag);
            ugServicios.ActiveRow.Delete(false);
        }

  
    
     

    }
}
