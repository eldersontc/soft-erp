namespace Soft.Win
{
    partial class FrmDetails
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ugDetails = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.ugDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // ugDetails
            // 
            this.ugDetails.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugDetails.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
            this.ugDetails.DisplayLayout.DefaultSelectedForeColor = System.Drawing.Color.Empty;
            this.ugDetails.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
            this.ugDetails.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ugDetails.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugDetails.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugDetails.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ugDetails.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            this.ugDetails.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.UseColumnEditor;
            this.ugDetails.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.ugDetails.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.ugDetails.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            appearance1.AlphaLevel = ((short)(180));
            appearance1.BackColorAlpha = Infragistics.Win.Alpha.Transparent;
            this.ugDetails.DisplayLayout.Override.RowAppearance = appearance1;
            this.ugDetails.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.ListIndex;
            this.ugDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugDetails.Location = new System.Drawing.Point(0, 0);
            this.ugDetails.Name = "ugDetails";
            this.ugDetails.Size = new System.Drawing.Size(284, 262);
            this.ugDetails.TabIndex = 0;
            this.ugDetails.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.ugDetails_DoubleClickRow);
            this.ugDetails.FilterCellValueChanged += new Infragistics.Win.UltraWinGrid.FilterCellValueChangedEventHandler(this.ugDetails_FilterCellValueChanged);
            this.ugDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ugDetails_KeyDown);
            // 
            // FrmDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.ugDetails);
            this.Name = "FrmDetails";
            this.Text = "FrmDetails";
            ((System.ComponentModel.ISupportInitialize)(this.ugDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid ugDetails;
    }
}