using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Seguridad.Entidades;
using System.IO;

namespace Soft.Seguridad.Win
{
    public partial class FrmUsuario : FrmParent
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private Boolean UIActualizando; 
        public Usuario Usuario { get { return (Usuario)base.m_ObjectFlow; } }
        
        public override void Init()
        {
            base.Init();
            this.Mostrar();
        }

        public void Mostrar()
        {
            UIActualizando = true;
            ssEmpresa.Text = (Usuario.Empresa!=null)?Usuario.Empresa.RazonSocial:"";
            ssPerfil.Text = (Usuario.Perfil != null) ? Usuario.Perfil.Nombre : "";
            txtNombreUsuario.Text = Usuario.NombreUsuario;
            txtIDUsuario.Text = Usuario.UserID;
            txtContrasena.Text = Usuario.Contrasena;
            uceSkyn.Text = Usuario.Skyn;
            upbImagen.Image = (Usuario.Imagen != null) ? Usuario.ObtenerImagen : null;
            UIActualizando = false;
        }

        private void txtNombreUsuario_TextChanged(object sender, EventArgs e)
        {
            if (UIActualizando) { return; }
            Usuario.NombreUsuario = txtNombreUsuario.Text;
        }

        private void ssEmpresa_Search(object sender, EventArgs e)
        {
            if (UIActualizando) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Usuario.Empresa = (Empresa)FrmSeleccionar.GetSelectedEntity(typeof(Empresa), "Empresa");
            Mostrar();
        }

        private void txtIDUsuario_TextChanged(object sender, EventArgs e)
        {
            if (UIActualizando) { return; }
            Usuario.UserID = txtIDUsuario.Text;
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            if (UIActualizando) { return; }
            Usuario.Contrasena = txtContrasena.Text;
        }

        private void ssPerfil_Search(object sender, EventArgs e)
        {
            if (UIActualizando) { return; }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            Usuario.Perfil = (Perfil)FrmSeleccionar.GetSelectedEntity(typeof(Perfil), "Perfil");
            Mostrar();
        }

        private void uceSkyn_ValueChanged(object sender, EventArgs e)
        {
            if (UIActualizando) { return; }
            Usuario.Skyn = uceSkyn.Text;
            Infragistics.Win.AppStyling.StyleManager.Load(String.Format("Styles/{0}.isl", Usuario.Skyn));
        }

        public byte[] ToBytes(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void ubBuscarImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fop = new OpenFileDialog();
            fop.InitialDirectory = @"C:\";
            fop.Filter = "[JPG,JPEG]|*.jpg|[PNG]|*.png";
            if (fop.ShowDialog() == DialogResult.OK)
            {
                upbImagen.Image = Image.FromFile(fop.FileName);
                Usuario.Imagen = ToBytes(fop.FileName);
            }
        }

    }
}
