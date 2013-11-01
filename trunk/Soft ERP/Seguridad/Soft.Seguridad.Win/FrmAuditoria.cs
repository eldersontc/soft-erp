using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Entities;
using Soft.DataAccess;
using Soft.Seguridad.Entidades;
using System.Collections;
using Infragistics.Win.UltraWinGrid;
using Soft.Configuracion.Entidades;

namespace Soft.Seguridad.Win
{
    public partial class FrmAuditoria : FrmParent
    {
        public FrmAuditoria()
        {
            InitializeComponent();
        }

        const String colFecha = "Fecha";
        const String colHora = "Hora";
        const String colUsuario = "Usuario";
        const String colAccion = "Acción";
        const String colMostrar = "Mostrar";
        //
        public Parent ObjectFlow { get { return (Parent)base.m_ObjectFlow; } }

        public override void Init()
        {
            base.Init();
            this.InitGrid();
            this.MostrarItems();
        }

        public void InitGrid()
        {

            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colFecha);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colHora);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colUsuario);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colAccion);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colMostrar);
            column.DataType = typeof(String);

            ugHistorial.DataSource = columns;
            ugHistorial.DisplayLayout.Bands[0].Columns[colMostrar].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            ugHistorial.DisplayLayout.Bands[0].Columns[colMostrar].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;

        }

        public void MostrarItems() {
            IList Auditorias = HelperNHibernate.GetEntitiesByField("Auditoria", "IDObjectFlow", ObjectFlow.ID);
            foreach (Auditoria Auditoria in Auditorias)
            {
                UltraGridRow Row = ugHistorial.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Auditoria;
                this.MostrarItem(Row);
            }
        }

        public void MostrarItem(UltraGridRow Row)
        {
            Auditoria Auditoria = (Auditoria)Row.Tag;
            Row.Cells[colFecha].Value = Auditoria.Fecha;
            Row.Cells[colHora].Value = Auditoria.Hora;
            Row.Cells[colUsuario].Value = Auditoria.Usuario;
            Row.Cells[colAccion].Value = Auditoria.Accion;
        }

        private void ugHistorial_ClickCellButton(object sender, CellEventArgs e)
        {
            Auditoria Auditoria = (Auditoria)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colMostrar:
                    MostrarFormulario(Auditoria.XML);
                    break;
                default:
                    break;
            }
        }

        public void MostrarFormulario(String XML) {
            try
            {
                Parent ObjectFlow = (Parent)Factory.ToObject(XML, base.m_EntidadSF.EnsambladoClase.Ensamblado_);
                ControllerApp FrmObjectFlow = (ControllerApp)Factory.InstanceObject(base.m_EntidadSF.EnsambladoFormulario.Ensamblado_, base.m_EntidadSF.NombreFormulario);
                FrmObjectFlow.m_ObjectFlow = ObjectFlow;
                FrmObjectFlow.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
