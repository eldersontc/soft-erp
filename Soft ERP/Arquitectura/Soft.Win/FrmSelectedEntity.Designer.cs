namespace Soft.Win
{
    partial class FrmSelectedEntity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSelectedEntity));
            this.ugEntity = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.uceTodos = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.ubSelecionar = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugEntity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uceTodos)).BeginInit();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.ubSelecionar);
            this.ugbParent.Controls.Add(this.uceTodos);
            this.ugbParent.Controls.Add(this.ugEntity);
            this.ugbParent.Size = new System.Drawing.Size(789, 491);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugEntity, 0);
            this.ugbParent.Controls.SetChildIndex(this.uceTodos, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubSelecionar, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ubCancelar.Location = new System.Drawing.Point(701, 456);
            // 
            // ubAceptar
            // 
            this.ubAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ubAceptar.Location = new System.Drawing.Point(497, 456);
            this.ubAceptar.Size = new System.Drawing.Size(92, 23);
            this.ubAceptar.Visible = false;
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
            // ugEntity
            // 
            this.ugEntity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ugEntity.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugEntity.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.ugEntity.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugEntity.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ugEntity.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.ugEntity.DisplayLayout.Override.FilterOperandStyle = Infragistics.Win.UltraWinGrid.FilterOperandStyle.UseColumnEditor;
            this.ugEntity.DisplayLayout.Override.FilterOperatorDefaultValue = Infragistics.Win.UltraWinGrid.FilterOperatorDefaultValue.Contains;
            this.ugEntity.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            this.ugEntity.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.ugEntity.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsVista;
            this.ugEntity.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.ListIndex;
            this.ugEntity.Location = new System.Drawing.Point(3, 3);
            this.ugEntity.Name = "ugEntity";
            this.ugEntity.Size = new System.Drawing.Size(786, 445);
            this.ugEntity.TabIndex = 5;
            this.ugEntity.Text = "ultraGrid1";
            this.ugEntity.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ugEntity_ClickCell);
            this.ugEntity.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.ugEntity_DoubleClickCell);
            this.ugEntity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ugEntity_KeyDown);
            // 
            // uceTodos
            // 
            this.uceTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uceTodos.BackColor = System.Drawing.Color.Transparent;
            this.uceTodos.BackColorInternal = System.Drawing.Color.Transparent;
            this.uceTodos.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
            this.uceTodos.Location = new System.Drawing.Point(12, 459);
            this.uceTodos.Name = "uceTodos";
            this.uceTodos.Size = new System.Drawing.Size(81, 20);
            this.uceTodos.TabIndex = 6;
            this.uceTodos.Text = "Todos";
            this.uceTodos.Visible = false;
            this.uceTodos.CheckedChanged += new System.EventHandler(this.uceTodos_CheckedChanged);
            // 
            // ubSelecionar
            // 
            this.ubSelecionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ubSelecionar.Location = new System.Drawing.Point(600, 456);
            this.ubSelecionar.Name = "ubSelecionar";
            this.ubSelecionar.Size = new System.Drawing.Size(95, 23);
            this.ubSelecionar.TabIndex = 7;
            this.ubSelecionar.Text = "Seleccionar";
            this.ubSelecionar.Click += new System.EventHandler(this.ubSelecionar_Click);
            // 
            // FrmSelectedEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 529);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "FrmSelectedEntity";
            this.Text = "Seleccionar ";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugEntity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uceTodos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid ugEntity;
        private Infragistics.Win.UltraWinEditors.UltraCheckEditor uceTodos;
        private Infragistics.Win.Misc.UltraButton ubSelecionar;

    }
}