namespace Soft.Win
{
    partial class FrmEntitySF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEntitySF));
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.ugAtributos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ssTabla = new Soft.Controls.SoftSearch();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ssEnsambladoClase = new Soft.Controls.SoftSearch();
            this.txtNombreClase = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.txtNombreFormulario = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ssEnsambladoFormulario = new Soft.Controls.SoftSearch();
            this.ugbGeneral = new Infragistics.Win.Misc.UltraGroupBox();
            this.ugbClase = new Infragistics.Win.Misc.UltraGroupBox();
            this.ugbFormulario = new Infragistics.Win.Misc.UltraGroupBox();
            this.ugbAtributos = new Infragistics.Win.Misc.UltraGroupBox();
            this.ubEliminar = new Infragistics.Win.Misc.UltraButton();
            this.ubNuevo = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugAtributos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreClase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreFormulario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ugbGeneral)).BeginInit();
            this.ugbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbClase)).BeginInit();
            this.ugbClase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbFormulario)).BeginInit();
            this.ugbFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbAtributos)).BeginInit();
            this.ugbAtributos.SuspendLayout();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.ugbAtributos);
            this.ugbParent.Controls.Add(this.ugbFormulario);
            this.ugbParent.Controls.Add(this.ugbClase);
            this.ugbParent.Controls.Add(this.ugbGeneral);
            this.ugbParent.Size = new System.Drawing.Size(540, 552);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbGeneral, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbClase, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbFormulario, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbAtributos, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Location = new System.Drawing.Point(446, 514);
            // 
            // ubAceptar
            // 
            this.ubAceptar.Location = new System.Drawing.Point(365, 514);
            // 
            // ilMain
            // 
            this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
            this.ilMain.Images.SetKeyName(0, "accept.png");
            this.ilMain.Images.SetKeyName(1, "accept_page.png");
            this.ilMain.Images.SetKeyName(2, "add.png");
            this.ilMain.Images.SetKeyName(3, "add_page.png");
            this.ilMain.Images.SetKeyName(4, "add_to_folder.png");
            this.ilMain.Images.SetKeyName(5, "attachment.png");
            this.ilMain.Images.SetKeyName(6, "back.png");
            this.ilMain.Images.SetKeyName(7, "block.png");
            this.ilMain.Images.SetKeyName(8, "calendar.png");
            this.ilMain.Images.SetKeyName(9, "calendar_empty.png");
            this.ilMain.Images.SetKeyName(10, "chart.png");
            this.ilMain.Images.SetKeyName(11, "chart_pie.png");
            this.ilMain.Images.SetKeyName(12, "clock.png");
            this.ilMain.Images.SetKeyName(13, "comment.png");
            this.ilMain.Images.SetKeyName(14, "comments.png");
            this.ilMain.Images.SetKeyName(15, "delete.png");
            this.ilMain.Images.SetKeyName(16, "delete_folder.png");
            this.ilMain.Images.SetKeyName(17, "delete_page.png");
            this.ilMain.Images.SetKeyName(18, "download.png");
            this.ilMain.Images.SetKeyName(19, "edit.png");
            this.ilMain.Images.SetKeyName(20, "edit_page.png");
            this.ilMain.Images.SetKeyName(21, "favorite.png");
            this.ilMain.Images.SetKeyName(22, "folder.png");
            this.ilMain.Images.SetKeyName(23, "folder_accept.png");
            this.ilMain.Images.SetKeyName(24, "folder_full.png");
            this.ilMain.Images.SetKeyName(25, "full_page.png");
            this.ilMain.Images.SetKeyName(26, "heart.png");
            this.ilMain.Images.SetKeyName(27, "help.png");
            this.ilMain.Images.SetKeyName(28, "info.png");
            this.ilMain.Images.SetKeyName(29, "lock.png");
            this.ilMain.Images.SetKeyName(30, "mail.png");
            this.ilMain.Images.SetKeyName(31, "mail_lock.png");
            this.ilMain.Images.SetKeyName(32, "mail_receive.png");
            this.ilMain.Images.SetKeyName(33, "mail_search.png");
            this.ilMain.Images.SetKeyName(34, "mail_send.png");
            this.ilMain.Images.SetKeyName(35, "new_page.png");
            this.ilMain.Images.SetKeyName(36, "next.png");
            this.ilMain.Images.SetKeyName(37, "page_process.png");
            this.ilMain.Images.SetKeyName(38, "process.png");
            this.ilMain.Images.SetKeyName(39, "promotion.png");
            this.ilMain.Images.SetKeyName(40, "protection.png");
            this.ilMain.Images.SetKeyName(41, "refresh.png");
            this.ilMain.Images.SetKeyName(42, "rss.png");
            this.ilMain.Images.SetKeyName(43, "search.png");
            this.ilMain.Images.SetKeyName(44, "search_page.png");
            this.ilMain.Images.SetKeyName(45, "tag_blue.png");
            this.ilMain.Images.SetKeyName(46, "tag_green.png");
            this.ilMain.Images.SetKeyName(47, "text_page.png");
            this.ilMain.Images.SetKeyName(48, "unlock.png");
            this.ilMain.Images.SetKeyName(49, "user.png");
            this.ilMain.Images.SetKeyName(50, "users.png");
            this.ilMain.Images.SetKeyName(51, "warning.png");
            // 
            // ugAtributos
            // 
            this.ugAtributos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugAtributos.Dock = System.Windows.Forms.DockStyle.Top;
            this.ugAtributos.Location = new System.Drawing.Point(3, 25);
            this.ugAtributos.Name = "ugAtributos";
            this.ugAtributos.Size = new System.Drawing.Size(499, 233);
            this.ugAtributos.TabIndex = 0;
            this.ugAtributos.Text = "ultraGrid1";
            this.ugAtributos.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugAtributos_CellChange);
            // 
            // ssTabla
            // 
            this.ssTabla.BackColor = System.Drawing.Color.Transparent;
            this.ssTabla.Location = new System.Drawing.Point(91, 32);
            this.ssTabla.Name = "ssTabla";
            this.ssTabla.Size = new System.Drawing.Size(180, 30);
            this.ssTabla.TabIndex = 20;
            this.ssTabla.Search += new System.EventHandler(this.ssTabla_Search);
            // 
            // ultraLabel2
            // 
            appearance12.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel2.Appearance = appearance12;
            this.ultraLabel2.Location = new System.Drawing.Point(11, 32);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(84, 23);
            this.ultraLabel2.TabIndex = 17;
            this.ultraLabel2.Text = "Tabla ";
            // 
            // ultraLabel3
            // 
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel3.Appearance = appearance8;
            this.ultraLabel3.Location = new System.Drawing.Point(11, 32);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(74, 23);
            this.ultraLabel3.TabIndex = 8;
            this.ultraLabel3.Text = "Ensamblado";
            // 
            // ultraLabel1
            // 
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel1.Appearance = appearance9;
            this.ultraLabel1.Location = new System.Drawing.Point(285, 34);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(54, 23);
            this.ultraLabel1.TabIndex = 6;
            this.ultraLabel1.Text = "Nombre";
            // 
            // ssEnsambladoClase
            // 
            this.ssEnsambladoClase.BackColor = System.Drawing.Color.Transparent;
            this.ssEnsambladoClase.Location = new System.Drawing.Point(91, 30);
            this.ssEnsambladoClase.Name = "ssEnsambladoClase";
            this.ssEnsambladoClase.Size = new System.Drawing.Size(180, 30);
            this.ssEnsambladoClase.TabIndex = 9;
            this.ssEnsambladoClase.Search += new System.EventHandler(this.ssEnsambladoClase_Search);
            // 
            // txtNombreClase
            // 
            this.txtNombreClase.Location = new System.Drawing.Point(342, 31);
            this.txtNombreClase.Name = "txtNombreClase";
            this.txtNombreClase.Size = new System.Drawing.Size(149, 21);
            this.txtNombreClase.TabIndex = 10;
            this.txtNombreClase.TextChanged += new System.EventHandler(this.txtNombreClase_TextChanged);
            // 
            // ultraLabel5
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel5.Appearance = appearance5;
            this.ultraLabel5.Location = new System.Drawing.Point(11, 32);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(71, 23);
            this.ultraLabel5.TabIndex = 16;
            this.ultraLabel5.Text = "Ensamblado";
            // 
            // ultraLabel6
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel6.Appearance = appearance4;
            this.ultraLabel6.Location = new System.Drawing.Point(285, 33);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(52, 23);
            this.ultraLabel6.TabIndex = 15;
            this.ultraLabel6.Text = "Nombre";
            // 
            // txtNombreFormulario
            // 
            this.txtNombreFormulario.Location = new System.Drawing.Point(342, 30);
            this.txtNombreFormulario.Name = "txtNombreFormulario";
            this.txtNombreFormulario.Size = new System.Drawing.Size(149, 21);
            this.txtNombreFormulario.TabIndex = 18;
            this.txtNombreFormulario.TextChanged += new System.EventHandler(this.txtNombreFormulario_TextChanged);
            // 
            // ssEnsambladoFormulario
            // 
            this.ssEnsambladoFormulario.BackColor = System.Drawing.Color.Transparent;
            this.ssEnsambladoFormulario.Location = new System.Drawing.Point(91, 30);
            this.ssEnsambladoFormulario.Name = "ssEnsambladoFormulario";
            this.ssEnsambladoFormulario.Size = new System.Drawing.Size(180, 30);
            this.ssEnsambladoFormulario.TabIndex = 17;
            this.ssEnsambladoFormulario.Search += new System.EventHandler(this.ssEnsambladoFormulario_Search);
            // 
            // ugbGeneral
            // 
            appearance11.BackColor = System.Drawing.Color.Transparent;
            this.ugbGeneral.Appearance = appearance11;
            this.ugbGeneral.Controls.Add(this.ssTabla);
            this.ugbGeneral.Controls.Add(this.ultraLabel2);
            appearance13.BackColor = System.Drawing.Color.Transparent;
            this.ugbGeneral.HeaderAppearance = appearance13;
            this.ugbGeneral.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbGeneral.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ugbGeneral.Location = new System.Drawing.Point(16, 15);
            this.ugbGeneral.Name = "ugbGeneral";
            this.ugbGeneral.Size = new System.Drawing.Size(505, 60);
            this.ugbGeneral.TabIndex = 15;
            this.ugbGeneral.Text = "Base de Datos";
            // 
            // ugbClase
            // 
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.ugbClase.Appearance = appearance7;
            this.ugbClase.Controls.Add(this.txtNombreClase);
            this.ugbClase.Controls.Add(this.ultraLabel3);
            this.ugbClase.Controls.Add(this.ssEnsambladoClase);
            this.ugbClase.Controls.Add(this.ultraLabel1);
            appearance10.BackColor = System.Drawing.Color.Transparent;
            this.ugbClase.HeaderAppearance = appearance10;
            this.ugbClase.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbClase.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ugbClase.Location = new System.Drawing.Point(16, 80);
            this.ugbClase.Name = "ugbClase";
            this.ugbClase.Size = new System.Drawing.Size(505, 60);
            this.ugbClase.TabIndex = 21;
            this.ugbClase.Text = "Clase";
            // 
            // ugbFormulario
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.ugbFormulario.Appearance = appearance3;
            this.ugbFormulario.Controls.Add(this.txtNombreFormulario);
            this.ugbFormulario.Controls.Add(this.ultraLabel6);
            this.ugbFormulario.Controls.Add(this.ultraLabel5);
            this.ugbFormulario.Controls.Add(this.ssEnsambladoFormulario);
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.ugbFormulario.HeaderAppearance = appearance6;
            this.ugbFormulario.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbFormulario.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ugbFormulario.Location = new System.Drawing.Point(16, 144);
            this.ugbFormulario.Name = "ugbFormulario";
            this.ugbFormulario.Size = new System.Drawing.Size(505, 60);
            this.ugbFormulario.TabIndex = 22;
            this.ugbFormulario.Text = "Formulario";
            // 
            // ugbAtributos
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.ugbAtributos.Appearance = appearance1;
            this.ugbAtributos.Controls.Add(this.ubEliminar);
            this.ugbAtributos.Controls.Add(this.ubNuevo);
            this.ugbAtributos.Controls.Add(this.ugAtributos);
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ugbAtributos.HeaderAppearance = appearance2;
            this.ugbAtributos.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbAtributos.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ugbAtributos.Location = new System.Drawing.Point(16, 210);
            this.ugbAtributos.Name = "ugbAtributos";
            this.ugbAtributos.Size = new System.Drawing.Size(505, 298);
            this.ugbAtributos.TabIndex = 23;
            this.ugbAtributos.Text = "Atributos";
            // 
            // ubEliminar
            // 
            this.ubEliminar.Location = new System.Drawing.Point(424, 264);
            this.ubEliminar.Name = "ubEliminar";
            this.ubEliminar.Size = new System.Drawing.Size(75, 23);
            this.ubEliminar.TabIndex = 2;
            this.ubEliminar.Text = "Eliminar";
            this.ubEliminar.Click += new System.EventHandler(this.ubEliminar_Click);
            // 
            // ubNuevo
            // 
            this.ubNuevo.Location = new System.Drawing.Point(343, 264);
            this.ubNuevo.Name = "ubNuevo";
            this.ubNuevo.Size = new System.Drawing.Size(75, 23);
            this.ubNuevo.TabIndex = 1;
            this.ubNuevo.Text = "Nuevo";
            this.ubNuevo.Click += new System.EventHandler(this.ubNuevo_Click);
            // 
            // FrmEntitySF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 590);
            this.Name = "FrmEntitySF";
            this.Text = "Entidad SF";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugAtributos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreClase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombreFormulario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ugbGeneral)).EndInit();
            this.ugbGeneral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugbClase)).EndInit();
            this.ugbClase.ResumeLayout(false);
            this.ugbClase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbFormulario)).EndInit();
            this.ugbFormulario.ResumeLayout(false);
            this.ugbFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbAtributos)).EndInit();
            this.ugbAtributos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Controls.SoftSearch ssEnsambladoClase;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNombreClase;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNombreFormulario;
        private Controls.SoftSearch ssEnsambladoFormulario;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Controls.SoftSearch ssTabla;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugAtributos;
        private Infragistics.Win.Misc.UltraGroupBox ugbGeneral;
        private Infragistics.Win.Misc.UltraGroupBox ugbClase;
        private Infragistics.Win.Misc.UltraGroupBox ugbAtributos;
        private Infragistics.Win.Misc.UltraGroupBox ugbFormulario;
        private Infragistics.Win.Misc.UltraButton ubEliminar;
        private Infragistics.Win.Misc.UltraButton ubNuevo;
    }
}