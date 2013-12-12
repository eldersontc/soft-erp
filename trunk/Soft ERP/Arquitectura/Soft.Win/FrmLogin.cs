using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Soft.DataAccess;
using Soft.Seguridad.Entidades;
using Soft.Exceptions;

namespace Soft.Win
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        public void ValidarUsuario(String Usuario, String Clave)
        {
            try
            {
                XmlDocument XML = HelperNHibernate.ExecuteView("vSF_Usuario", String.Format(" UserID = '{0}' AND Contrasena = '{1}'", Usuario, Clave));
                if (XML.HasChildNodes)
                {
                    foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                    {
                        Usuario User = (Usuario)HelperNHibernate.GetEntityByID("Usuario", NodoItem.SelectSingleNode("@ID").Value);
                        CargarSyn(User.Skyn);
                        FrmMain FrmMain = new FrmMain();
                        FrmMain.IniciarAplicacion(User);
                        Hide();
                    }
                }
                else
                {
                    throw new Exception("Usuario o Clave Incorrectos ...");
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, MessageBoxIcon.Information);
            }
        }

        public void CargarSyn(String Skyn)
        {
            if (Skyn.Equals(""))
            {
                Infragistics.Win.AppStyling.StyleManager.Load("Styles/SOFTSMART_SILVER.isl");
            }
            else
            {
                Infragistics.Win.AppStyling.StyleManager.Load(String.Format("Styles/{0}.isl", Skyn));
            }
        }

        private void ubAcept_Click(object sender, EventArgs e)
        {
            ValidarUsuario(txtUsuario.Text, txtContraseña.Text);
        }

        private void ubCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            Infragistics.Win.AppStyling.StyleManager.Load("Styles/SOFTSMART_SILVER.isl");
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValidarUsuario(txtUsuario.Text, txtContraseña.Text);
            }
        }
    }
}
