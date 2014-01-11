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

namespace Soft.Ventas.Win
{
    public partial class FrmCotizaciondeServicio : FrmParent 
    {
        public FrmCotizaciondeServicio()
        {
            InitializeComponent();
        }

        private Cotizacion cotizacion = null;
        private ItemCotizacionServicio Item = new ItemCotizacionServicio();
        private Boolean SWAcept = false;

        public ItemCotizacionServicio ObtenerServicio(Cotizacion m_Cotizacion)
        {
            cotizacion = m_Cotizacion;
            ShowDialog();
            if (SWAcept) { return Item; }
            return null;
        }

    }
}
