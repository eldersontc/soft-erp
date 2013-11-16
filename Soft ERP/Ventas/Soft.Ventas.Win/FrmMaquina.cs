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
            Mostrar();
        }

        public void Mostrar()
        {
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

    }
}
