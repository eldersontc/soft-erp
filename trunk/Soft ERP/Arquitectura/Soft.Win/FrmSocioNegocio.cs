using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Entities;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using Soft.Seguridad.Entidades;

namespace Soft.Win
{
    public partial class FrmSocioNegocio : FrmParent
    {
        public FrmSocioNegocio()
        {
            InitializeComponent();
        }

        public SocioNegocio SocioNegocio { get { return (SocioNegocio)base.m_ObjectFlow; } }

        public override void Init()
        {
            InitGridDirecciones();
            InitGridContactos();
            InitGridBancos();
            Mostrar();
        }

        Boolean mUIMoficiado = false;

        //Constantes Direcciones
        const String colDepartamento = "Departamento";
        const String colProvincia = "Provincia";
        const String colDistrito = "Distrito";
        const String colDireccion = "Dirección";
        const String colDirEntrega = "Entrega";
        const String colDirFactura = "Factura";

        //Constantes Contactos
        const String colContactoNombre = "Nombre";
        const String colContactoCargo = "Cargo";
        const String colContactoTelefono = "Teléfono";
        const String colContactoCorreo = "Correo";

        //Constantes Bancos
        const String colBanco = "Banco";
        const String colMoneda = "Moneda";
        const String colBancoDescripcion = "Descripción";

        public void InitGridDirecciones()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colDepartamento);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colProvincia);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colDistrito);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colDireccion);
            column.DataType = typeof(String);


            column = columns.Columns.Add(colDirEntrega);
            column.DataType = typeof(Boolean);


            column = columns.Columns.Add(colDirFactura);
            column.DataType = typeof(Boolean);



            GrillaDirecciones.DataSource = columns;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDepartamento].Width = 70;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colProvincia].Width = 70;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDistrito].Width = 70;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDireccion].Width = 200;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDirEntrega].Width = 50;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDirFactura].Width = 50;


            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDepartamento].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colProvincia].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDistrito].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;


            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDireccion].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDirEntrega].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            GrillaDirecciones.DisplayLayout.Bands[0].Columns[colDirFactura].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;

            //MapKeys(ref GrillaDirecciones);
        }

        public void InitGridContactos()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colContactoNombre);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colContactoCargo);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colContactoTelefono);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colContactoCorreo);
            column.DataType = typeof(String);

            GrillaContactos.DataSource = columns;
            GrillaContactos.DisplayLayout.Bands[0].Columns[colContactoNombre].Width = 120;
            GrillaContactos.DisplayLayout.Bands[0].Columns[colContactoCargo].Width = 70;
            GrillaContactos.DisplayLayout.Bands[0].Columns[colContactoTelefono].Width = 70;
            GrillaContactos.DisplayLayout.Bands[0].Columns[colContactoCorreo].Width = 100;

            //MapKeys(ref GrillaContactos);
        }

        public void InitGridBancos()
        {
            DataTable columns = new DataTable();
            DataColumn column = new DataColumn();

            column = columns.Columns.Add(colBanco);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colMoneda);
            column.DataType = typeof(String);

            column = columns.Columns.Add(colBancoDescripcion);
            column.DataType = typeof(String);


            GrillaBancos.DataSource = columns;
            GrillaBancos.DisplayLayout.Bands[0].Columns[colBanco].Width = 150;
            GrillaBancos.DisplayLayout.Bands[0].Columns[colMoneda].Width = 70;
            GrillaBancos.DisplayLayout.Bands[0].Columns[colBancoDescripcion].Width = 150;

            GrillaBancos.DisplayLayout.Bands[0].Columns[colBanco].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
            GrillaBancos.DisplayLayout.Bands[0].Columns[colMoneda].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;



            //MapKeys(ref GrillaContactos);
        }

        public void Mostrar()
        {
            mUIMoficiado = true;
            if (SocioNegocio.TipoSocioNegocio != null) { ssTipoSocio.Text = SocioNegocio.TipoSocioNegocio.Descripcion; }
            txtCodigo.Text = SocioNegocio.Codigo;
            txtNombre.Text = SocioNegocio.Nombre;
            udtAniversario.Value = SocioNegocio.Aniversario;
            uceActivo.Checked = SocioNegocio.Activo;
            uceCliente.Checked = SocioNegocio.Cliente;
            uceProveedor.Checked = SocioNegocio.Proveedor;
            uceEmpleado.Checked = SocioNegocio.Empleado;
            utSocioNegocio.Tabs["Cliente"].Enabled = SocioNegocio.Cliente;
            utSocioNegocio.Tabs["Proveedor"].Enabled = SocioNegocio.Proveedor;
            utSocioNegocio.Tabs["Empleado"].Enabled = SocioNegocio.Empleado;

            txtApellidoPaterno.Text = SocioNegocio.ApellidoPaterno;
            txtApellidoMaterno.Text = SocioNegocio.ApellidoMaterno;

            txtNombre1.Text = SocioNegocio.Nombre1;
            txtNombre2.Text = SocioNegocio.Nombre2;

            txtCorreo.Text = SocioNegocio.Correo;
            txtPaginaWeb.Text = SocioNegocio.PaginaWeb;


            try
            {
                if (!(SocioNegocio.Firma == null))
                {
                    if ((!SocioNegocio.Firma.Equals("")))
                    {
                        pbFirma.Image = Image.FromFile(String.Format("{0}{1}", FrmMain.CarpetaFirmas, SocioNegocio.Firma));
                    }
                }
            }
            catch (Exception)
            {
                
                
            }

            

            MostrarDirecciones();
            MostrarContactos();
            MostrarBancos();

            MostrarEmpleado();
            MostrarCliente();

            txtNombreComercial.Text = SocioNegocio.NombreComercial;

            mUIMoficiado = false;
        }

        private void ssTipoSocio_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SocioNegocio.TipoSocioNegocio = (TipoSocioNegocio)FrmSeleccionar.GetSelectedEntity(typeof(TipoSocioNegocio), "Tipo de Socio");
            Mostrar();
        }

        private void uceActivo_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocio.Activo = uceActivo.Checked;
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            SocioNegocio.Codigo = txtCodigo.Text;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            SocioNegocio.Nombre = txtNombre.Text;
        }

        private void udtAniversario_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.Aniversario = (DateTime)udtAniversario.Value;
        }

        private void uceCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (mUIMoficiado)
            {
                return;
            }

            SocioNegocio.Cliente = uceCliente.Checked;
            if (SocioNegocio.Cliente)
            {
                this.SocioNegocio.AddItemCliente();
            }
            else
            {
                SocioNegocio.Clientes.Clear();
            }
            Mostrar();
        }

        private void uceProveedor_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocio.Proveedor = uceProveedor.Checked;
            Mostrar();
        }

        private void uceEmpleado_CheckedChanged(object sender, EventArgs e)
        {
            if (mUIMoficiado)
            {
                return;
            }

            SocioNegocio.Empleado = uceEmpleado.Checked;
            if (SocioNegocio.Empleado)
            {
                this.SocioNegocio.AddItemEmpleado();
            }
            else
            {
                SocioNegocio.Empleados.Clear();
            }
            Mostrar();
        }

        public void MostrarDireccion(UltraGridRow Row)
        {
            ItemSocioNegocioDireccion item = (ItemSocioNegocioDireccion)Row.Tag;

            Row.Cells[colDepartamento].Value = item.Departamento.Nombre;
            Row.Cells[colProvincia].Value = item.Provincia.Nombre;
            Row.Cells[colDistrito].Value = item.Distrito.Nombre;
            Row.Cells[colDireccion].Value = item.Direccion;
            Row.Cells[colDirEntrega].Value = item.EsDireccionEntrega;
            Row.Cells[colDirFactura].Value = item.EsDireccionFacturacion;
        }

        public void MostrarContacto(UltraGridRow Row)
        {
            ItemSocioNegocioContacto item = (ItemSocioNegocioContacto)Row.Tag;
            Row.Cells[colContactoNombre].Value = item.Nombre;
            Row.Cells[colContactoCargo].Value = item.Cargo;
            Row.Cells[colContactoCorreo].Value = item.Correo;
            Row.Cells[colContactoTelefono].Value = item.Telefono;
        }

        public void MostrarBanco(UltraGridRow Row)
        {
            ItemSocioNegocioBanco item = (ItemSocioNegocioBanco)Row.Tag;
            if (item.Banco != null)
            {
                Row.Cells[colBanco].Value = item.Banco.Nombre;
            }

            if (item.Moneda != null)
            {
                Row.Cells[colMoneda].Value = item.Moneda.Simbolo;
            }
            Row.Cells[colBancoDescripcion].Value = item.Descripcion;
        }

        public void MostrarDirecciones()
        {
            base.ClearAllRows(ref GrillaDirecciones);
            foreach (ItemSocioNegocioDireccion Item in this.SocioNegocio.Direcciones)
            {
                UltraGridRow Row = GrillaDirecciones.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarDireccion(Row);
            }
        }

        public void MostrarContactos()
        {
            base.ClearAllRows(ref GrillaContactos);
            foreach (ItemSocioNegocioContacto Item in this.SocioNegocio.Contactos)
            {
                UltraGridRow Row = GrillaContactos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarContacto(Row);
            }
        }

        public void MostrarBancos()
        {
            base.ClearAllRows(ref GrillaBancos);
            foreach (ItemSocioNegocioBanco Item in this.SocioNegocio.Bancos)
            {
                UltraGridRow Row = GrillaBancos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = Item;
                this.MostrarBanco(Row);
            }
        }

        private void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            FrmSeleccionarDireccion SeleccionarDireccion = new FrmSeleccionarDireccion();
            ItemSocioNegocioDireccion ItemDireccion = SeleccionarDireccion.ObtenerDireccion();
            if (ItemDireccion != null)
            {
                SocioNegocio.Direcciones.Add(ItemDireccion);
                UltraGridRow Row = GrillaDirecciones.DisplayLayout.Bands[0].AddNew();
                Row.Tag = ItemDireccion;
                MostrarDireccion(Row);
            }
        }

        private void GrillaDirecciones_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            ModificarDireccion(e.Cell.Row);
        }

        private void GrillaDirecciones_CellChange(object sender, CellEventArgs e)
        {
            ItemSocioNegocioDireccion Item = (ItemSocioNegocioDireccion)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colDireccion:
                    Item.Direccion = Convert.ToString(e.Cell.Text);
                    break;
                case colDirEntrega:
                    Item.EsDireccionEntrega = Convert.ToBoolean(e.Cell.Text);
                    break;
                case colDirFactura:
                    Item.EsDireccionFacturacion = Convert.ToBoolean(e.Cell.Text);
                    break;
                default:
                    break;
            }
            this.MostrarDireccion(e.Cell.Row);
        }

        private void ModificarDireccion(UltraGridRow Row)
        {
            ItemSocioNegocioDireccion ItemDireccion = (ItemSocioNegocioDireccion)Row.Tag;
            FrmSeleccionarDireccion SeleccionarDireccion = new FrmSeleccionarDireccion();
            ItemDireccion = SeleccionarDireccion.ModificarDireccion(ItemDireccion);
            MostrarDireccion(Row);
        }

        private void btnModificarDireccion_Click(object sender, EventArgs e)
        {
            if (GrillaDirecciones.ActiveRow != null)
            {
                ModificarDireccion(GrillaDirecciones.ActiveRow);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (GrillaDirecciones.ActiveRow == null) { return; }
            this.SocioNegocio.Direcciones.Remove((ItemSocioNegocioDireccion)this.GrillaDirecciones.ActiveRow.Tag);
            this.GrillaDirecciones.ActiveRow.Delete(false);
        }

        private void btnAgregarContacto_Click(object sender, EventArgs e)
        {
            UltraGridRow Row = GrillaContactos.DisplayLayout.Bands[0].AddNew();
            Row.Tag = this.SocioNegocio.AddItemContacto();
        }

        private void btnEliminarContacto_Click(object sender, EventArgs e)
        {
            if (GrillaContactos.ActiveRow == null) { return; }
            this.SocioNegocio.Contactos.Remove((ItemSocioNegocioContacto)this.GrillaContactos.ActiveRow.Tag);
            this.GrillaContactos.ActiveRow.Delete(false);
        }

        private void GrillaContactos_CellChange(object sender, CellEventArgs e)
        {
            ItemSocioNegocioContacto Item = (ItemSocioNegocioContacto)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colContactoNombre:
                    Item.Nombre = Convert.ToString(e.Cell.Text);
                    break;
                case colContactoCargo:
                    Item.Cargo = Convert.ToString(e.Cell.Text);
                    break;
                case colContactoTelefono:
                    Item.Telefono = Convert.ToString(e.Cell.Text);
                    break;
                case colContactoCorreo:
                    Item.Correo = Convert.ToString(e.Cell.Text);
                    break;
                default:
                    break;
            }
            this.MostrarContacto(e.Cell.Row);
        }

        private void btnAgregarBancos_Click(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
            String Filtro = "ID NOT IN (";
            String IDs = "";
            foreach (ItemSocioNegocioBanco Item in SocioNegocio.Bancos) { IDs = IDs + "'" + Item.Banco.ID + "',"; }
            if (IDs.Length > 0) { Filtro = Filtro + IDs.Substring(0, IDs.Length - 1) + ")"; }
            else { Filtro = ""; }
            Banco Banco = (Banco)FrmSeleccionarPanel.GetSelectedEntity(typeof(Banco), "Banco", Filtro);
            if (Banco != null)
            {
                UltraGridRow Row = GrillaBancos.DisplayLayout.Bands[0].AddNew();
                Row.Tag = this.SocioNegocio.AddItemBanco();
                ItemSocioNegocioBanco Item = (ItemSocioNegocioBanco)Row.Tag;
                Item.Banco = Banco;
                MostrarBanco(Row);
            }
        }

        private void btnEliminarBancos_Click(object sender, EventArgs e)
        {
            if (GrillaBancos.ActiveRow == null) { return; }
            this.SocioNegocio.Bancos.Remove((ItemSocioNegocioBanco)this.GrillaBancos.ActiveRow.Tag);
            this.GrillaBancos.ActiveRow.Delete(false);
        }

        private void GrillaBancos_ClickCellButton(object sender, CellEventArgs e)
        {
            ItemSocioNegocioBanco Item = (ItemSocioNegocioBanco)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colMoneda:
                    FrmSelectedEntity FrmSeleccionarPanel = new FrmSelectedEntity();
                    Moneda moneda = (Moneda)FrmSeleccionarPanel.GetSelectedEntity(typeof(Moneda), "Moneda");
                    Item.Moneda = moneda;
                    break;
                default:
                    break;
            }
            this.MostrarBanco(e.Cell.Row);
        }

        private void GrillaBancos_CellChange(object sender, CellEventArgs e)
        {
            ItemSocioNegocioBanco Item = (ItemSocioNegocioBanco)e.Cell.Row.Tag;
            switch (e.Cell.Column.Key)
            {
                case colBancoDescripcion:
                    Item.Descripcion = Convert.ToString(e.Cell.Text);
                    break;
                default:
                    break;
            }
            this.MostrarBanco(e.Cell.Row);
        }

        private void txtApellidoPaterno_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.ApellidoPaterno = txtApellidoPaterno.Text;
            SocioNegocio.ConcatenarNombres();
            txtNombre.Text = SocioNegocio.Nombre;
        }

        private void txtApellidoMaterno_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.ApellidoMaterno = txtApellidoMaterno.Text;
            SocioNegocio.ConcatenarNombres();
            txtNombre.Text = SocioNegocio.Nombre;
        }

        private void txtNombre1_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.Nombre1 = txtNombre1.Text;
            SocioNegocio.ConcatenarNombres();
            txtNombre.Text = SocioNegocio.Nombre;
        }

        private void txtNombre2_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.Nombre2 = txtNombre2.Text;
            SocioNegocio.ConcatenarNombres();
            txtNombre.Text = SocioNegocio.Nombre;
        }

        private void txtPaginaWeb_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.PaginaWeb = txtPaginaWeb.Text;
        }

        private void txtCorreo_ValueChanged(object sender, EventArgs e)
        {
            SocioNegocio.Correo = txtCorreo.Text;
        }

        private void busArea_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SocioNegocioEmpleado empleado = SocioNegocio.Empleados.First();
            empleado.Area = (Area)FrmSeleccionar.GetSelectedEntity(typeof(Area), "Area");
            MostrarEmpleado();
        }

        private void MostrarEmpleado()
        {
            mUIMoficiado = true;
            if (SocioNegocio.Empleados.Count > 0)
            {
                SocioNegocioEmpleado empleado = SocioNegocio.Empleados.First();
                if (empleado != null)
                {
                    if (empleado.Area != null)
                    {
                        busArea.Text = empleado.Area.Nombre;
                    }
                    else
                    {
                        busArea.Text = null;
                    }
                    if (empleado.Usuario != null)
                    {
                        busNombreUsuario.Text = empleado.Usuario.NombreUsuario;
                        busCodigoUsuario.Text = empleado.Usuario.UserID;
                    }
                    else
                    {
                        busNombreUsuario.Text = null;
                        busCodigoUsuario.Text = null;
                    }
                    checkActivoEmpleado.Checked = empleado.Activo;
                }
            }
            mUIMoficiado = false;
        }

        private void checkActivoEmpleado_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocioEmpleado empleado = SocioNegocio.Empleados.First();
            empleado.Activo = checkActivoEmpleado.Checked;
        }

        private void MostrarCliente()
        {
            mUIMoficiado = true;
            if (SocioNegocio.Clientes.Count > 0)
            {
                SocioNegocioCliente cliente = SocioNegocio.Clientes.First();
                if (cliente != null)
                {
                    if (cliente.SocioNegocioEmpleado != null)
                    {
                        FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
                        String Filtro = "ID='" + cliente.SocioNegocioEmpleado.ID + "'";
                        cliente.SocioNegocioEmpleado = (SocioNegocioEmpleado)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocioEmpleado), "Vendedor", Filtro);
                        busVendedor.Text = cliente.SocioNegocioEmpleado.Nombre;
                    }
                    else
                    {
                        busVendedor.Text = null;
                    }
                    checkActivoClliente.Checked = cliente.Activo;
                }
            }
            mUIMoficiado = false;
        }

        private void busVendedor_Search(object sender, EventArgs e)
        {
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SocioNegocioCliente clliente = SocioNegocio.Clientes.First();
            clliente.SocioNegocioEmpleado = (SocioNegocioEmpleado)FrmSeleccionar.GetSelectedEntity(typeof(SocioNegocioEmpleado), "Vendedor");
            MostrarCliente();
        }

        private void checkActivoClliente_CheckedChanged(object sender, EventArgs e)
        {
            SocioNegocioCliente cliente = SocioNegocio.Clientes.First();
            cliente.Activo = checkActivoClliente.Checked;
        }

        private void busNombreUsuario_Search(object sender, EventArgs e)
        {
            String Filtro = "";
            if (busNombreUsuario.Text.Length > 0)
            {
                Filtro = "NombreUsuario like '" + busNombreUsuario.Text + "%'";
            }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SocioNegocioEmpleado Empleado = SocioNegocio.Empleados.First();
            Empleado.Usuario = (Usuario)FrmSeleccionar.GetSelectedEntity(typeof(Usuario), "Usuario", Filtro);
            MostrarEmpleado();
        }

        private void busCodigoUsuario_Search(object sender, EventArgs e)
        {
            String Filtro = "";
            if (busCodigoUsuario.Text.Length > 0)
            {
                Filtro = "UserID like '" + busCodigoUsuario.Text + "%'";
            }
            FrmSelectedEntity FrmSeleccionar = new FrmSelectedEntity();
            SocioNegocioEmpleado Empleado = SocioNegocio.Empleados.First();
            Empleado.Usuario = (Usuario)FrmSeleccionar.GetSelectedEntity(typeof(Usuario), "Usuario", Filtro);
            MostrarEmpleado();
        }

        private void ubFirma_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.InitialDirectory = FrmMain.CarpetaFirmas;
            OpenFile.Filter = "[JPG,JPEG]|*.jpg|[PNG]|*.png";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                SocioNegocio.Firma = OpenFile.SafeFileName;
                pbFirma.Image = Image.FromFile(OpenFile.FileName);
            }
        }

        private void txtNombreComercial_TextChanged(object sender, EventArgs e)
        {
            SocioNegocio.NombreComercial = txtNombreComercial.Text;
            txtNombreComercial.Text = SocioNegocio.NombreComercial;
        }

    }
}
