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
using Soft.Inventario.Entidades;
using Soft.DataAccess;
using Soft.Produccion.Entidades;

namespace Soft.Produccion.Win
{
    public partial class FrmCalculoMetros : FrmParent 
    {
        public FrmCalculoMetros()
        {
            InitializeComponent();
        }

        private Boolean SWAcept = false;
        public  ItemOrdenProduccionServicio m_item = null;
        private ItemOrdenProduccion m_itemOrdenProduccion = null;

        private Decimal TotalCantidad;

        public FrmCalculoMetros(ItemOrdenProduccionServicio item, ItemOrdenProduccion itemOrdenProduccion)
        {
            InitializeComponent();
            m_item = item;
            m_itemOrdenProduccion = itemOrdenProduccion;
            Mostrar();
            ShowDialog();
        }


        public override void Aceptar()
        {
            base.m_ResultProcess = EnumResult.SUCESS;
            m_item.CantidadMaterial = Convert.ToDecimal(txtTotal.Value);
            SWAcept = true;
            Close();
        }

        public void Mostrar(){
            busUnidadMaterial.Text = m_item.UnidadMaterial.Nombre;
        }

        private Decimal CalcularMetro(Decimal medida){
            Decimal resultado = 0;
            ExistenciaUnidad eu=null;

            foreach (ExistenciaUnidad Itemunidad in m_item.Material.Unidades)
            {
                if (Itemunidad.Unidad.Nombre.Equals(m_item.UnidadMaterial.Nombre)) {
                    eu = Itemunidad;
                    break;
                }
            }

            Decimal dividido = medida/ eu.FactorConversion;
            Decimal entero = Math.Truncate(dividido);
            Decimal residuo = dividido - entero;
            if (residuo > 0)
            {
                resultado = entero+1;
            }
            else {
                resultado = entero;
            }



            return resultado * eu.FactorConversion * m_itemOrdenProduccion.CantidadElemento;
        }

        private void busUnidadMaterial_Search(object sender, EventArgs e)
        {

        }

        private void checkHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHorizontal.Checked == true)
            {
                Decimal largo = m_itemOrdenProduccion.MedidaAbiertaLargo;
                Decimal alto = m_itemOrdenProduccion.MedidaAbiertaAlto;

                if (m_itemOrdenProduccion.UnidadMedidaAbierta.Equals("CM."))
                {
                    largo = largo / 100;
                    alto = alto / 100;
                }

                txtHorizontal.Value = CalcularMetro(alto);
            }
            else {
                txtHorizontal.Value = 0;
            }
            Totales();       
        }

        private void checkVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHorizontal.Checked == true)
            {
                Decimal largo = m_itemOrdenProduccion.MedidaAbiertaLargo;
                Decimal alto = m_itemOrdenProduccion.MedidaAbiertaAlto;

                if (m_itemOrdenProduccion.UnidadMedidaAbierta.Equals("CM."))
                {
                    largo = largo / 100;
                    alto = alto / 100;
                }

                txtVertical.Value = CalcularMetro(largo);
            }
            else
            {
                txtVertical.Value = 0;
            }
            Totales();
        }

        private void Totales() {

            txtTotal.Value = Convert.ToDecimal(txtHorizontal.Value) + Convert.ToDecimal(txtVertical.Value);
        }

    }

}
