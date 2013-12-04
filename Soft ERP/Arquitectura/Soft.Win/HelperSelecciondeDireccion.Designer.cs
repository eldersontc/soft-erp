namespace Soft.Win
{
    partial class HelperSelecciondeDireccion
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
            this.busDepartamento = new Soft.Controls.SoftSearch();
            this.busProvincia = new Soft.Controls.SoftSearch();
            this.busDistrito = new Soft.Controls.SoftSearch();
            this.lblRelacion = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.txtDireccion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraButton1 = new Infragistics.Win.Misc.UltraButton();
            this.ultraButton2 = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion)).BeginInit();
            this.SuspendLayout();
            // 
            // busDepartamento
            // 
            this.busDepartamento.BackColor = System.Drawing.Color.Transparent;
            this.busDepartamento.Location = new System.Drawing.Point(127, 12);
            this.busDepartamento.Name = "busDepartamento";
            this.busDepartamento.Size = new System.Drawing.Size(247, 30);
            this.busDepartamento.TabIndex = 8;
            this.busDepartamento.Search += new System.EventHandler(this.busDepartamento_Search);
            // 
            // busProvincia
            // 
            this.busProvincia.BackColor = System.Drawing.Color.Transparent;
            this.busProvincia.Location = new System.Drawing.Point(127, 48);
            this.busProvincia.Name = "busProvincia";
            this.busProvincia.Size = new System.Drawing.Size(247, 30);
            this.busProvincia.TabIndex = 9;
            this.busProvincia.Search += new System.EventHandler(this.busProvincia_Search);
            // 
            // busDistrito
            // 
            this.busDistrito.BackColor = System.Drawing.Color.Transparent;
            this.busDistrito.Location = new System.Drawing.Point(127, 84);
            this.busDistrito.Name = "busDistrito";
            this.busDistrito.Size = new System.Drawing.Size(247, 27);
            this.busDistrito.TabIndex = 10;
            this.busDistrito.Search += new System.EventHandler(this.busDistrito_Search);
            // 
            // lblRelacion
            // 
            this.lblRelacion.Location = new System.Drawing.Point(21, 12);
            this.lblRelacion.Name = "lblRelacion";
            this.lblRelacion.Size = new System.Drawing.Size(100, 21);
            this.lblRelacion.TabIndex = 11;
            this.lblRelacion.Text = "Departamento";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(21, 48);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(100, 21);
            this.ultraLabel1.TabIndex = 12;
            this.ultraLabel1.Text = "Provincia";
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(21, 84);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(100, 21);
            this.ultraLabel2.TabIndex = 13;
            this.ultraLabel2.Text = "Distrito";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Location = new System.Drawing.Point(21, 115);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(100, 21);
            this.ultraLabel3.TabIndex = 14;
            this.ultraLabel3.Text = "Direccion:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(21, 136);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(353, 46);
            this.txtDireccion.TabIndex = 15;
            this.txtDireccion.ValueChanged += new System.EventHandler(this.txtDireccion_ValueChanged);
            // 
            // ultraButton1
            // 
            this.ultraButton1.Location = new System.Drawing.Point(203, 200);
            this.ultraButton1.Name = "ultraButton1";
            this.ultraButton1.Size = new System.Drawing.Size(82, 31);
            this.ultraButton1.TabIndex = 16;
            this.ultraButton1.Text = "OK";
            this.ultraButton1.Click += new System.EventHandler(this.ultraButton1_Click);
            // 
            // ultraButton2
            // 
            this.ultraButton2.Location = new System.Drawing.Point(291, 200);
            this.ultraButton2.Name = "ultraButton2";
            this.ultraButton2.Size = new System.Drawing.Size(82, 31);
            this.ultraButton2.TabIndex = 17;
            this.ultraButton2.Text = "CANCELAR";
            this.ultraButton2.Click += new System.EventHandler(this.ultraButton2_Click);
            // 
            // HelperSelecciondeDireccion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 234);
            this.Controls.Add(this.ultraButton2);
            this.Controls.Add(this.ultraButton1);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.ultraLabel3);
            this.Controls.Add(this.ultraLabel2);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.lblRelacion);
            this.Controls.Add(this.busDistrito);
            this.Controls.Add(this.busProvincia);
            this.Controls.Add(this.busDepartamento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HelperSelecciondeDireccion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seleccion de Direcciones";
            ((System.ComponentModel.ISupportInitialize)(this.txtDireccion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.SoftSearch busDepartamento;
        private Controls.SoftSearch busProvincia;
        private Controls.SoftSearch busDistrito;
        private Infragistics.Win.Misc.UltraLabel lblRelacion;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtDireccion;
        private Infragistics.Win.Misc.UltraButton ultraButton1;
        private Infragistics.Win.Misc.UltraButton ultraButton2;
    }
}