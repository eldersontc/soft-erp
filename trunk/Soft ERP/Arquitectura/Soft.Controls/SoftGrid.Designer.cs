namespace Soft.Controls
{
    partial class SoftGrid
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.UltraGridx = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridx)).BeginInit();
            this.SuspendLayout();
            // 
            // UltraGridx
            // 
            this.UltraGridx.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.UltraGridx.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            this.UltraGridx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UltraGridx.Location = new System.Drawing.Point(0, 0);
            this.UltraGridx.Name = "UltraGridx";
            this.UltraGridx.Size = new System.Drawing.Size(392, 237);
            this.UltraGridx.TabIndex = 0;
            this.UltraGridx.Text = "ultraGrid1";
            // 
            // SoftGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UltraGridx);
            this.Name = "SoftGrid";
            this.Size = new System.Drawing.Size(392, 237);
            ((System.ComponentModel.ISupportInitialize)(this.UltraGridx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid UltraGridx;
    }
}
