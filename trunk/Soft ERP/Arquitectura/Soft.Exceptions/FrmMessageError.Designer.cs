namespace Soft.Exceptions
{
    partial class FrmMessageError
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.ufmError = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.FrmMessageError_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.ugbError = new Infragistics.Win.Misc.UltraGroupBox();
            this.upbIcon = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
            this.lblMessage = new Infragistics.Win.Misc.UltraLabel();
            this.ugbDetails = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            this.ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            this.txtDetails = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this._FrmMessageError_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._FrmMessageError_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._FrmMessageError_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            ((System.ComponentModel.ISupportInitialize)(this.ufmError)).BeginInit();
            this.FrmMessageError_Fill_Panel.ClientArea.SuspendLayout();
            this.FrmMessageError_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbError)).BeginInit();
            this.ugbError.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbDetails)).BeginInit();
            this.ugbDetails.SuspendLayout();
            this.ultraExpandableGroupBoxPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // ufmError
            // 
            this.ufmError.Form = this;
            // 
            // FrmMessageError_Fill_Panel
            // 
            // 
            // FrmMessageError_Fill_Panel.ClientArea
            // 
            this.FrmMessageError_Fill_Panel.ClientArea.Controls.Add(this.ugbError);
            this.FrmMessageError_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.FrmMessageError_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrmMessageError_Fill_Panel.Location = new System.Drawing.Point(8, 31);
            this.FrmMessageError_Fill_Panel.Name = "FrmMessageError_Fill_Panel";
            this.FrmMessageError_Fill_Panel.Size = new System.Drawing.Size(329, 89);
            this.FrmMessageError_Fill_Panel.TabIndex = 0;
            // 
            // ugbError
            // 
            this.ugbError.Controls.Add(this.upbIcon);
            this.ugbError.Controls.Add(this.lblMessage);
            this.ugbError.Controls.Add(this.ugbDetails);
            this.ugbError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugbError.Location = new System.Drawing.Point(0, 0);
            this.ugbError.Name = "ugbError";
            this.ugbError.Size = new System.Drawing.Size(329, 89);
            this.ugbError.TabIndex = 0;
            // 
            // upbIcon
            // 
            this.upbIcon.BackColor = System.Drawing.Color.Transparent;
            this.upbIcon.BorderShadowColor = System.Drawing.Color.Empty;
            this.upbIcon.Location = new System.Drawing.Point(15, 9);
            this.upbIcon.Name = "upbIcon";
            this.upbIcon.Size = new System.Drawing.Size(36, 37);
            this.upbIcon.TabIndex = 2;
            this.upbIcon.UseAppStyling = false;
            // 
            // lblMessage
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            appearance3.TextHAlignAsString = "Center";
            appearance3.TextVAlignAsString = "Middle";
            this.lblMessage.Appearance = appearance3;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(61, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(249, 23);
            this.lblMessage.TabIndex = 1;
            // 
            // ugbDetails
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ugbDetails.Appearance = appearance4;
            this.ugbDetails.Controls.Add(this.ultraExpandableGroupBoxPanel1);
            this.ugbDetails.Expanded = false;
            this.ugbDetails.ExpandedSize = new System.Drawing.Size(308, 76);
            this.ugbDetails.ExpansionIndicator = Infragistics.Win.Misc.GroupBoxExpansionIndicator.Far;
            this.ugbDetails.Location = new System.Drawing.Point(15, 41);
            this.ugbDetails.Name = "ugbDetails";
            this.ugbDetails.Size = new System.Drawing.Size(308, 21);
            this.ugbDetails.TabIndex = 0;
            this.ugbDetails.UseAppStyling = false;
            // 
            // ultraExpandableGroupBoxPanel1
            // 
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.txtDetails);
            this.ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
            this.ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(200, 100);
            this.ultraExpandableGroupBoxPanel1.TabIndex = 0;
            this.ultraExpandableGroupBoxPanel1.Visible = false;
            // 
            // txtDetails
            // 
            this.txtDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetails.Location = new System.Drawing.Point(0, 0);
            this.txtDetails.Multiline = true;
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(200, 100);
            this.txtDetails.TabIndex = 0;
            // 
            // _FrmMessageError_UltraFormManager_Dock_Area_Left
            // 
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.FormManager = this.ufmError;
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 8;
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 31);
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.Name = "_FrmMessageError_UltraFormManager_Dock_Area_Left";
            this._FrmMessageError_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(8, 89);
            // 
            // _FrmMessageError_UltraFormManager_Dock_Area_Right
            // 
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.FormManager = this.ufmError;
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 8;
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(337, 31);
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.Name = "_FrmMessageError_UltraFormManager_Dock_Area_Right";
            this._FrmMessageError_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(8, 89);
            // 
            // _FrmMessageError_UltraFormManager_Dock_Area_Top
            // 
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.FormManager = this.ufmError;
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.Name = "_FrmMessageError_UltraFormManager_Dock_Area_Top";
            this._FrmMessageError_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(345, 31);
            // 
            // _FrmMessageError_UltraFormManager_Dock_Area_Bottom
            // 
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.FormManager = this.ufmError;
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 8;
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 120);
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.Name = "_FrmMessageError_UltraFormManager_Dock_Area_Bottom";
            this._FrmMessageError_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(345, 8);
            // 
            // FrmMessageError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 128);
            this.Controls.Add(this.FrmMessageError_Fill_Panel);
            this.Controls.Add(this._FrmMessageError_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._FrmMessageError_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._FrmMessageError_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._FrmMessageError_UltraFormManager_Dock_Area_Bottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMessageError";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.ufmError)).EndInit();
            this.FrmMessageError_Fill_Panel.ClientArea.ResumeLayout(false);
            this.FrmMessageError_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugbError)).EndInit();
            this.ugbError.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugbDetails)).EndInit();
            this.ugbDetails.ResumeLayout(false);
            this.ultraExpandableGroupBoxPanel1.ResumeLayout(false);
            this.ultraExpandableGroupBoxPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinForm.UltraFormManager ufmError;
        private Infragistics.Win.Misc.UltraPanel FrmMessageError_Fill_Panel;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _FrmMessageError_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _FrmMessageError_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _FrmMessageError_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _FrmMessageError_UltraFormManager_Dock_Area_Bottom;
        private Infragistics.Win.Misc.UltraGroupBox ugbError;
        private Infragistics.Win.Misc.UltraExpandableGroupBox ugbDetails;
        private Infragistics.Win.Misc.UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtDetails;
        private Infragistics.Win.Misc.UltraLabel lblMessage;
        private Infragistics.Win.UltraWinEditors.UltraPictureBox upbIcon;
    }
}