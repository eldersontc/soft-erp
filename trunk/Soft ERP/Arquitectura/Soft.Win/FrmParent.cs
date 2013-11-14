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
using Infragistics.Win.SupportDialogs.FilterUIProvider;
using Microsoft.VisualBasic;
using Soft.DataAccess;
using System.Reflection;
using Infragistics.Win.UltraWinGrid;

namespace Soft.Win
{
    public partial class FrmParent : ControllerApp
    {

        public FrmParent()
        {
            InitializeComponent();
            
        }

        public FrmParent(Object ObjectFlow) {
            base.m_ObjectFlow = ObjectFlow;
        }

        public override void Start()
        {
            Init();
            Show();
        }

        public virtual void Init() {}

        public Image GetImage(String Key) {
            return ilMain.Images[Key];
        }

        private void ubAceptar_Click(object sender, EventArgs e)
        {
            Aceptar();   
        }

        public void Aceptar() {
            base.m_ResultProcess = EnumResult.SUCESS;
            if (!EsValido()) { return; }
            this.Close();
            base.Start();
        }

        public Boolean EsValido() {
            //Collection PropiedadesInvalidas = new Collection();
            String PropiedadesInvalidas = "";
            foreach (AtributoSF Attr in m_EntidadSF.Atributos)
            {
                if (Attr.Obligatorio) {
                    PropertyInfo pInfo = m_ObjectFlow.GetType().GetProperty(Attr.Propiedad);
                    if (pInfo == null) { MessageBox.Show(String.Format("La propiedad {0} no existe.", Attr.Propiedad)); return false; }
                    Object value = pInfo.GetValue(m_ObjectFlow, null);
                    switch (pInfo.PropertyType.Name)
                    {
                        case "String":
                            if (Convert.ToString(value).Equals("")) { PropiedadesInvalidas += "● " + Attr.Nombre + System.Environment.NewLine; }
                            //if (Convert.ToString(value).Equals("")) { PropiedadesInvalidas.Add(Attr); }
                            break;
                        case "Decimal":
                            if (Convert.ToDecimal(value) == 0) { PropiedadesInvalidas += "● " + Attr.Nombre + System.Environment.NewLine; }
                            //if (Convert.ToDecimal(value) == 0) { PropiedadesInvalidas.Add(Attr); }
                            break;
                        case "Double":
                            if (Convert.ToDouble(value) == 0) { PropiedadesInvalidas += "● " + Attr.Nombre + System.Environment.NewLine; }
                            //if (Convert.ToDouble(value) == 0) { PropiedadesInvalidas.Add(Attr); }
                            break;
                        case "Int32":
                            if (Convert.ToInt32(value) == 0) { PropiedadesInvalidas += "● " + Attr.Nombre + System.Environment.NewLine; }
                            //if (Convert.ToInt32(value) == 0) { PropiedadesInvalidas.Add(Attr); }
                            break;
                        case "Boolean":
                        case "DateTime":
                            break;
                        default:
                            if (value == null) { PropiedadesInvalidas += "● " + Attr.Nombre + System.Environment.NewLine; }
                            break;
                    }
                }
            }
            if (PropiedadesInvalidas.Length > 0) { MessageBox.Show("=====================" + System.Environment.NewLine + "=== Propiedades Inválidas ===" + System.Environment.NewLine + "=====================" + System.Environment.NewLine + PropiedadesInvalidas); return false; }
            return true;
        }

        public void MapKeys(ref UltraGrid Grid) {
            Grid.KeyDown += Grid_KeyDown;
        }

        public static event EventHandler show;

        public void Grid_KeyDown(Object sender, KeyEventArgs e)
        {
            UltraGrid Grid = (UltraGrid)sender;
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Grid.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    Grid.PerformAction(UltraGridAction.AboveCell, false, false);
                    e.Handled = true;
                    Grid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case Keys.Down:
                    Grid.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    Grid.PerformAction(UltraGridAction.BelowCell, false, false);
                    e.Handled = true;
                    Grid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case Keys.Right:
                    Grid.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    Grid.PerformAction(UltraGridAction.NextCell, false, false);
                    e.Handled = true;
                    Grid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case Keys.Left:
                    Grid.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    Grid.PerformAction(UltraGridAction.PrevCell, false, false);
                    e.Handled = true;
                    Grid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case Keys.Enter:
                    Type TypeInfo = this.GetType();
                    Object[] Args = { Grid.ActiveCell};
                    MethodInfo MethodInfo = TypeInfo.GetMethod(String.Format("{0}_CellKeyEnter", Grid.Name));
                    if (MethodInfo != null) { MethodInfo.Invoke(this, Args); }
                    Grid.PerformAction(UltraGridAction.ExitEditMode, false, false);
                    Grid.PerformAction(UltraGridAction.NextCell, false, false);
                    e.Handled = true;
                    Grid.PerformAction(UltraGridAction.EnterEditMode, false, false);
                    break;
                case Keys.Escape:
                    break;
            }
        }

        public void ClearAllRows(ref UltraGrid Grid) {
            Grid.Selected.Rows.AddRange((UltraGridRow[])Grid.Rows.All);
            Grid.DeleteSelectedRows(false);
        }

        private void FrmParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (base.m_ResultProcess == EnumResult.SUCESS) { return; }
            FrmMessageBox MsBox = new FrmMessageBox();
            MsBox.Show(MessageBoxIcon.Question);
            MsBox.ShowDialog();
            if (MsBox.m_ResultProcess == EnumResult.SUCESS)
            {
                base.m_ResultProcess = EnumResult.CANCEL;
                base.Start();
            }
            else { e.Cancel = true; }
        }

        private void ubCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        public virtual void Cancelar()
        {
            base.m_ResultProcess = EnumResult.CANCEL;
            this.Close();
        }

    }
}
