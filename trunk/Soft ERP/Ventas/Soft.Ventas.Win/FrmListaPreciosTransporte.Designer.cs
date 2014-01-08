namespace Soft.Ventas.Win
{
    partial class FrmListaPreciosTransporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaPreciosTransporte));
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab1 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            this.ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ubEliminar = new Infragistics.Win.Misc.UltraButton();
            this.ubNuevo = new Infragistics.Win.Misc.UltraButton();
            this.ugDistritos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ubElminarEscala = new Infragistics.Win.Misc.UltraButton();
            this.ubNuevaEscala = new Infragistics.Win.Misc.UltraButton();
            this.ugEscalas = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.uceActivo = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.txtNombre = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtCodigo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblNombre = new Infragistics.Win.Misc.UltraLabel();
            this.lblCodigo = new Infragistics.Win.Misc.UltraLabel();
            this.utcOrigenDestino = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.utcEscalas = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage3 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            this.ultraTabPageControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugDistritos)).BeginInit();
            this.ultraTabPageControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugEscalas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uceActivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcOrigenDestino)).BeginInit();
            this.utcOrigenDestino.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utcEscalas)).BeginInit();
            this.utcEscalas.SuspendLayout();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.utcEscalas);
            this.ugbParent.Controls.Add(this.utcOrigenDestino);
            this.ugbParent.Controls.Add(this.uceActivo);
            this.ugbParent.Controls.Add(this.txtNombre);
            this.ugbParent.Controls.Add(this.txtCodigo);
            this.ugbParent.Controls.Add(this.lblNombre);
            this.ugbParent.Controls.Add(this.lblCodigo);
            this.ugbParent.Size = new System.Drawing.Size(495, 519);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblCodigo, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblNombre, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtCodigo, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtNombre, 0);
            this.ugbParent.Controls.SetChildIndex(this.uceActivo, 0);
            this.ugbParent.Controls.SetChildIndex(this.utcOrigenDestino, 0);
            this.ugbParent.Controls.SetChildIndex(this.utcEscalas, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Location = new System.Drawing.Point(395, 483);
            // 
            // ubAceptar
            // 
            this.ubAceptar.Location = new System.Drawing.Point(314, 483);
            // 
            // ilMain
            // 
            this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
            this.ilMain.Images.SetKeyName(0, "accept.png");
            this.ilMain.Images.SetKeyName(1, "accept_database.png");
            this.ilMain.Images.SetKeyName(2, "accept_page.png");
            this.ilMain.Images.SetKeyName(3, "add.png");
            this.ilMain.Images.SetKeyName(4, "add_comment.png");
            this.ilMain.Images.SetKeyName(5, "add_home.png");
            this.ilMain.Images.SetKeyName(6, "add_image.png");
            this.ilMain.Images.SetKeyName(7, "add_page.png");
            this.ilMain.Images.SetKeyName(8, "add_pages.png");
            this.ilMain.Images.SetKeyName(9, "add_printer.png");
            this.ilMain.Images.SetKeyName(10, "add_to_database.png");
            this.ilMain.Images.SetKeyName(11, "add_to_favorites.png");
            this.ilMain.Images.SetKeyName(12, "add_to_folder.png");
            this.ilMain.Images.SetKeyName(13, "add_to_shopping_cart.png");
            this.ilMain.Images.SetKeyName(14, "add_user.png");
            this.ilMain.Images.SetKeyName(15, "artwork.png");
            this.ilMain.Images.SetKeyName(16, "attach_image.png");
            this.ilMain.Images.SetKeyName(17, "attachment.png");
            this.ilMain.Images.SetKeyName(18, "back.png");
            this.ilMain.Images.SetKeyName(19, "block.png");
            this.ilMain.Images.SetKeyName(20, "blue_arrow_down.png");
            this.ilMain.Images.SetKeyName(21, "blue_arrow_up.png");
            this.ilMain.Images.SetKeyName(22, "blue_energy.png");
            this.ilMain.Images.SetKeyName(23, "bmp_file.png");
            this.ilMain.Images.SetKeyName(24, "book.png");
            this.ilMain.Images.SetKeyName(25, "book_accept.png");
            this.ilMain.Images.SetKeyName(26, "book_download.png");
            this.ilMain.Images.SetKeyName(27, "book_search.png");
            this.ilMain.Images.SetKeyName(28, "book_warning.png");
            this.ilMain.Images.SetKeyName(29, "calculator.png");
            this.ilMain.Images.SetKeyName(30, "calendar.png");
            this.ilMain.Images.SetKeyName(31, "calendar_empty.png");
            this.ilMain.Images.SetKeyName(32, "canvas_holder.png");
            this.ilMain.Images.SetKeyName(33, "card.png");
            this.ilMain.Images.SetKeyName(34, "cd.png");
            this.ilMain.Images.SetKeyName(35, "chalk_board.png");
            this.ilMain.Images.SetKeyName(36, "chart.png");
            this.ilMain.Images.SetKeyName(37, "chart_down.png");
            this.ilMain.Images.SetKeyName(38, "chart_pie.png");
            this.ilMain.Images.SetKeyName(39, "chart_up.png");
            this.ilMain.Images.SetKeyName(40, "clock.png");
            this.ilMain.Images.SetKeyName(41, "cloud_comment.png");
            this.ilMain.Images.SetKeyName(42, "coffee_cup.png");
            this.ilMain.Images.SetKeyName(43, "comment.png");
            this.ilMain.Images.SetKeyName(44, "comments.png");
            this.ilMain.Images.SetKeyName(45, "computer.png");
            this.ilMain.Images.SetKeyName(46, "computer_accept.png");
            this.ilMain.Images.SetKeyName(47, "computer_add.png");
            this.ilMain.Images.SetKeyName(48, "computer_help.png");
            this.ilMain.Images.SetKeyName(49, "computer_info.png");
            this.ilMain.Images.SetKeyName(50, "computer_process.png");
            this.ilMain.Images.SetKeyName(51, "computer_warning.png");
            this.ilMain.Images.SetKeyName(52, "copy_paste.png");
            this.ilMain.Images.SetKeyName(53, "credit_cart.png");
            this.ilMain.Images.SetKeyName(54, "credit_cart_cancelled.png");
            this.ilMain.Images.SetKeyName(55, "css_file.png");
            this.ilMain.Images.SetKeyName(56, "csv_file.png");
            this.ilMain.Images.SetKeyName(57, "cut.png");
            this.ilMain.Images.SetKeyName(58, "cut_from_page.png");
            this.ilMain.Images.SetKeyName(59, "database.png");
            this.ilMain.Images.SetKeyName(60, "delete.png");
            this.ilMain.Images.SetKeyName(61, "delete_comment.png");
            this.ilMain.Images.SetKeyName(62, "delete_computer.png");
            this.ilMain.Images.SetKeyName(63, "delete_folder.png");
            this.ilMain.Images.SetKeyName(64, "delete_home.png");
            this.ilMain.Images.SetKeyName(65, "delete_image.png");
            this.ilMain.Images.SetKeyName(66, "delete_page.png");
            this.ilMain.Images.SetKeyName(67, "delete_user.png");
            this.ilMain.Images.SetKeyName(68, "dollar_currency_sign.png");
            this.ilMain.Images.SetKeyName(69, "download.png");
            this.ilMain.Images.SetKeyName(70, "download_database.png");
            this.ilMain.Images.SetKeyName(71, "download_image.png");
            this.ilMain.Images.SetKeyName(72, "download_to_computer.png");
            this.ilMain.Images.SetKeyName(73, "dvd.png");
            this.ilMain.Images.SetKeyName(74, "edit.png");
            this.ilMain.Images.SetKeyName(75, "edit_page.png");
            this.ilMain.Images.SetKeyName(76, "edit_profile.png");
            this.ilMain.Images.SetKeyName(77, "eps_file.png");
            this.ilMain.Images.SetKeyName(78, "equalizer.png");
            this.ilMain.Images.SetKeyName(79, "euro_currency_sign.png");
            this.ilMain.Images.SetKeyName(80, "favorite.png");
            this.ilMain.Images.SetKeyName(81, "favorite_film.png");
            this.ilMain.Images.SetKeyName(82, "film.png");
            this.ilMain.Images.SetKeyName(83, "firewall.png");
            this.ilMain.Images.SetKeyName(84, "folder.png");
            this.ilMain.Images.SetKeyName(85, "folder_accept.png");
            this.ilMain.Images.SetKeyName(86, "folder_conflicted.png");
            this.ilMain.Images.SetKeyName(87, "folder_full.png");
            this.ilMain.Images.SetKeyName(88, "folder_modified.png");
            this.ilMain.Images.SetKeyName(89, "full_page.png");
            this.ilMain.Images.SetKeyName(90, "games.png");
            this.ilMain.Images.SetKeyName(91, "gif_file.png");
            this.ilMain.Images.SetKeyName(92, "globe.png");
            this.ilMain.Images.SetKeyName(93, "globe_download.png");
            this.ilMain.Images.SetKeyName(94, "globe_process.png");
            this.ilMain.Images.SetKeyName(95, "globe_warning.png");
            this.ilMain.Images.SetKeyName(96, "green_arrow_down.png");
            this.ilMain.Images.SetKeyName(97, "green_arrow_up.png");
            this.ilMain.Images.SetKeyName(98, "green_button.png");
            this.ilMain.Images.SetKeyName(99, "green_energy.png");
            this.ilMain.Images.SetKeyName(100, "green_flag.png");
            this.ilMain.Images.SetKeyName(101, "heart.png");
            this.ilMain.Images.SetKeyName(102, "help.png");
            this.ilMain.Images.SetKeyName(103, "help_balloon.png");
            this.ilMain.Images.SetKeyName(104, "home.png");
            this.ilMain.Images.SetKeyName(105, "home_accept.png");
            this.ilMain.Images.SetKeyName(106, "html_file.png");
            this.ilMain.Images.SetKeyName(107, "ico_file.png");
            this.ilMain.Images.SetKeyName(108, "id_card.png");
            this.ilMain.Images.SetKeyName(109, "image.png");
            this.ilMain.Images.SetKeyName(110, "image_accept.png");
            this.ilMain.Images.SetKeyName(111, "info.png");
            this.ilMain.Images.SetKeyName(112, "insert_to_shopping_cart.png");
            this.ilMain.Images.SetKeyName(113, "jpg_file.png");
            this.ilMain.Images.SetKeyName(114, "js_file.png");
            this.ilMain.Images.SetKeyName(115, "json_file.png");
            this.ilMain.Images.SetKeyName(116, "key.png");
            this.ilMain.Images.SetKeyName(117, "light_bulb.png");
            this.ilMain.Images.SetKeyName(118, "link.png");
            this.ilMain.Images.SetKeyName(119, "lock.png");
            this.ilMain.Images.SetKeyName(120, "magnet.png");
            this.ilMain.Images.SetKeyName(121, "mail.png");
            this.ilMain.Images.SetKeyName(122, "mail_lock.png");
            this.ilMain.Images.SetKeyName(123, "mail_receive.png");
            this.ilMain.Images.SetKeyName(124, "mail_search.png");
            this.ilMain.Images.SetKeyName(125, "mail_send.png");
            this.ilMain.Images.SetKeyName(126, "mobile_phone.png");
            this.ilMain.Images.SetKeyName(127, "mouse.png");
            this.ilMain.Images.SetKeyName(128, "music.png");
            this.ilMain.Images.SetKeyName(129, "new.png");
            this.ilMain.Images.SetKeyName(130, "new_page.png");
            this.ilMain.Images.SetKeyName(131, "news.png");
            this.ilMain.Images.SetKeyName(132, "next.png");
            this.ilMain.Images.SetKeyName(133, "note.png");
            this.ilMain.Images.SetKeyName(134, "note_accept.png");
            this.ilMain.Images.SetKeyName(135, "note_book.png");
            this.ilMain.Images.SetKeyName(136, "office_folders.png");
            this.ilMain.Images.SetKeyName(137, "old_clock.png");
            this.ilMain.Images.SetKeyName(138, "open_store.png");
            this.ilMain.Images.SetKeyName(139, "orange_arrow_down.png");
            this.ilMain.Images.SetKeyName(140, "orange_arrow_up.png");
            this.ilMain.Images.SetKeyName(141, "orange_button.png");
            this.ilMain.Images.SetKeyName(142, "package.png");
            this.ilMain.Images.SetKeyName(143, "package_accept.png");
            this.ilMain.Images.SetKeyName(144, "package_add.png");
            this.ilMain.Images.SetKeyName(145, "package_download.png");
            this.ilMain.Images.SetKeyName(146, "package_warning.png");
            this.ilMain.Images.SetKeyName(147, "page_down.png");
            this.ilMain.Images.SetKeyName(148, "page_process.png");
            this.ilMain.Images.SetKeyName(149, "page_up.png");
            this.ilMain.Images.SetKeyName(150, "pages.png");
            this.ilMain.Images.SetKeyName(151, "pages_warning.png");
            this.ilMain.Images.SetKeyName(152, "paint.png");
            this.ilMain.Images.SetKeyName(153, "paint_brush.png");
            this.ilMain.Images.SetKeyName(154, "palette.png");
            this.ilMain.Images.SetKeyName(155, "palette_brush.png");
            this.ilMain.Images.SetKeyName(156, "pastel_colors.png");
            this.ilMain.Images.SetKeyName(157, "pdf_file.png");
            this.ilMain.Images.SetKeyName(158, "phone_book.png");
            this.ilMain.Images.SetKeyName(159, "phone_book_edit.png");
            this.ilMain.Images.SetKeyName(160, "photo_camera.png");
            this.ilMain.Images.SetKeyName(161, "photo_camera_accept.png");
            this.ilMain.Images.SetKeyName(162, "php_file.png");
            this.ilMain.Images.SetKeyName(163, "pin.png");
            this.ilMain.Images.SetKeyName(164, "png_file.png");
            this.ilMain.Images.SetKeyName(165, "ppt_file.png");
            this.ilMain.Images.SetKeyName(166, "printer.png");
            this.ilMain.Images.SetKeyName(167, "printer_accept.png");
            this.ilMain.Images.SetKeyName(168, "printer_warning.png");
            this.ilMain.Images.SetKeyName(169, "prize_winner.png");
            this.ilMain.Images.SetKeyName(170, "process.png");
            this.ilMain.Images.SetKeyName(171, "process_accept.png");
            this.ilMain.Images.SetKeyName(172, "process_info.png");
            this.ilMain.Images.SetKeyName(173, "process_warning.png");
            this.ilMain.Images.SetKeyName(174, "promotion.png");
            this.ilMain.Images.SetKeyName(175, "protection.png");
            this.ilMain.Images.SetKeyName(176, "psd_file.png");
            this.ilMain.Images.SetKeyName(177, "puzzle.png");
            this.ilMain.Images.SetKeyName(178, "recycle.png");
            this.ilMain.Images.SetKeyName(179, "red_button.png");
            this.ilMain.Images.SetKeyName(180, "red_flag.png");
            this.ilMain.Images.SetKeyName(181, "refresh.png");
            this.ilMain.Images.SetKeyName(182, "refresh_page.png");
            this.ilMain.Images.SetKeyName(183, "remote_desktop.png");
            this.ilMain.Images.SetKeyName(184, "remove_from_database.png");
            this.ilMain.Images.SetKeyName(185, "remove_from_shopping_cart.png");
            this.ilMain.Images.SetKeyName(186, "report.png");
            this.ilMain.Images.SetKeyName(187, "rss.png");
            this.ilMain.Images.SetKeyName(188, "ruler.png");
            this.ilMain.Images.SetKeyName(189, "ruler_pencil.png");
            this.ilMain.Images.SetKeyName(190, "sale.png");
            this.ilMain.Images.SetKeyName(191, "save.png");
            this.ilMain.Images.SetKeyName(192, "search.png");
            this.ilMain.Images.SetKeyName(193, "search_computer.png");
            this.ilMain.Images.SetKeyName(194, "search_database.png");
            this.ilMain.Images.SetKeyName(195, "search_globe.png");
            this.ilMain.Images.SetKeyName(196, "search_home.png");
            this.ilMain.Images.SetKeyName(197, "search_image.png");
            this.ilMain.Images.SetKeyName(198, "search_page.png");
            this.ilMain.Images.SetKeyName(199, "search_printer.png");
            this.ilMain.Images.SetKeyName(200, "search_user.png");
            this.ilMain.Images.SetKeyName(201, "security.png");
            this.ilMain.Images.SetKeyName(202, "send_sms.png");
            this.ilMain.Images.SetKeyName(203, "shopping_cart.png");
            this.ilMain.Images.SetKeyName(204, "shopping_cart_accept.png");
            this.ilMain.Images.SetKeyName(205, "sms.png");
            this.ilMain.Images.SetKeyName(206, "sound.png");
            this.ilMain.Images.SetKeyName(207, "sound_muted.png");
            this.ilMain.Images.SetKeyName(208, "star_empty.png");
            this.ilMain.Images.SetKeyName(209, "star_full.png");
            this.ilMain.Images.SetKeyName(210, "star_half_full.png");
            this.ilMain.Images.SetKeyName(211, "sterling_pound_currency_sign.png");
            this.ilMain.Images.SetKeyName(212, "support.png");
            this.ilMain.Images.SetKeyName(213, "svg_file.png");
            this.ilMain.Images.SetKeyName(214, "swf_file.png");
            this.ilMain.Images.SetKeyName(215, "tablet.png");
            this.ilMain.Images.SetKeyName(216, "tag_blue.png");
            this.ilMain.Images.SetKeyName(217, "tag_green.png");
            this.ilMain.Images.SetKeyName(218, "target.png");
            this.ilMain.Images.SetKeyName(219, "television.png");
            this.ilMain.Images.SetKeyName(220, "text_page.png");
            this.ilMain.Images.SetKeyName(221, "tiff_file.png");
            this.ilMain.Images.SetKeyName(222, "tools.png");
            this.ilMain.Images.SetKeyName(223, "trash_can.png");
            this.ilMain.Images.SetKeyName(224, "turquoise_button.png");
            this.ilMain.Images.SetKeyName(225, "twitter.png");
            this.ilMain.Images.SetKeyName(226, "txt_file.png");
            this.ilMain.Images.SetKeyName(227, "unlock.png");
            this.ilMain.Images.SetKeyName(228, "up.png");
            this.ilMain.Images.SetKeyName(229, "user.png");
            this.ilMain.Images.SetKeyName(230, "user_accept.png");
            this.ilMain.Images.SetKeyName(231, "user_comment.png");
            this.ilMain.Images.SetKeyName(232, "users.png");
            this.ilMain.Images.SetKeyName(233, "users_comments.png");
            this.ilMain.Images.SetKeyName(234, "violet_button.png");
            this.ilMain.Images.SetKeyName(235, "warning.png");
            this.ilMain.Images.SetKeyName(236, "white_flag.png");
            this.ilMain.Images.SetKeyName(237, "windows_terminal.png");
            this.ilMain.Images.SetKeyName(238, "xml_file.png");
            this.ilMain.Images.SetKeyName(239, "yen_currency_sign.png");
            this.ilMain.Images.SetKeyName(240, "zip_file.png");
            this.ilMain.Images.SetKeyName(241, "zip_file_accept.png");
            this.ilMain.Images.SetKeyName(242, "zip_file_download.png");
            this.ilMain.Images.SetKeyName(243, "zip_file_info.png");
            this.ilMain.Images.SetKeyName(244, "zip_file_search.png");
            this.ilMain.Images.SetKeyName(245, "zoom_in.png");
            this.ilMain.Images.SetKeyName(246, "zoom_out.png");
            // 
            // ultraTabPageControl1
            // 
            this.ultraTabPageControl1.Controls.Add(this.ubEliminar);
            this.ultraTabPageControl1.Controls.Add(this.ubNuevo);
            this.ultraTabPageControl1.Controls.Add(this.ugDistritos);
            this.ultraTabPageControl1.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl1.Name = "ultraTabPageControl1";
            this.ultraTabPageControl1.Size = new System.Drawing.Size(445, 186);
            // 
            // ubEliminar
            // 
            this.ubEliminar.Location = new System.Drawing.Point(328, 157);
            this.ubEliminar.Name = "ubEliminar";
            this.ubEliminar.Size = new System.Drawing.Size(113, 23);
            this.ubEliminar.TabIndex = 2;
            this.ubEliminar.Text = "Eliminar";
            this.ubEliminar.Click += new System.EventHandler(this.ubEliminar_Click);
            // 
            // ubNuevo
            // 
            this.ubNuevo.Location = new System.Drawing.Point(211, 157);
            this.ubNuevo.Name = "ubNuevo";
            this.ubNuevo.Size = new System.Drawing.Size(111, 23);
            this.ubNuevo.TabIndex = 1;
            this.ubNuevo.Text = "Nuevo";
            this.ubNuevo.Click += new System.EventHandler(this.ubNuevo_Click);
            // 
            // ugDistritos
            // 
            this.ugDistritos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugDistritos.Dock = System.Windows.Forms.DockStyle.Top;
            this.ugDistritos.Location = new System.Drawing.Point(0, 0);
            this.ugDistritos.Name = "ugDistritos";
            this.ugDistritos.Size = new System.Drawing.Size(445, 151);
            this.ugDistritos.TabIndex = 0;
            this.ugDistritos.Text = "ultraGrid1";
            this.ugDistritos.ClickCellButton += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugDistritos_ClickCellButton);
            this.ugDistritos.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.ugDistritos_AfterSelectChange);
            this.ugDistritos.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.ugDistritos_ClickCell);
            // 
            // ultraTabPageControl3
            // 
            this.ultraTabPageControl3.Controls.Add(this.ubElminarEscala);
            this.ultraTabPageControl3.Controls.Add(this.ubNuevaEscala);
            this.ultraTabPageControl3.Controls.Add(this.ugEscalas);
            this.ultraTabPageControl3.Location = new System.Drawing.Point(1, 23);
            this.ultraTabPageControl3.Name = "ultraTabPageControl3";
            this.ultraTabPageControl3.Size = new System.Drawing.Size(443, 152);
            // 
            // ubElminarEscala
            // 
            this.ubElminarEscala.Location = new System.Drawing.Point(326, 125);
            this.ubElminarEscala.Name = "ubElminarEscala";
            this.ubElminarEscala.Size = new System.Drawing.Size(113, 23);
            this.ubElminarEscala.TabIndex = 2;
            this.ubElminarEscala.Text = "Eliminar Escala";
            this.ubElminarEscala.Click += new System.EventHandler(this.ubElminarEscala_Click);
            // 
            // ubNuevaEscala
            // 
            this.ubNuevaEscala.Location = new System.Drawing.Point(209, 125);
            this.ubNuevaEscala.Name = "ubNuevaEscala";
            this.ubNuevaEscala.Size = new System.Drawing.Size(111, 23);
            this.ubNuevaEscala.TabIndex = 1;
            this.ubNuevaEscala.Text = "Nueva Escala";
            this.ubNuevaEscala.Click += new System.EventHandler(this.ubNuevaEscala_Click);
            // 
            // ugEscalas
            // 
            this.ugEscalas.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugEscalas.Dock = System.Windows.Forms.DockStyle.Top;
            this.ugEscalas.Location = new System.Drawing.Point(0, 0);
            this.ugEscalas.Name = "ugEscalas";
            this.ugEscalas.Size = new System.Drawing.Size(443, 120);
            this.ugEscalas.TabIndex = 0;
            this.ugEscalas.Text = "ultraGrid1";
            this.ugEscalas.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugEscalas_CellChange);
            // 
            // uceActivo
            // 
            this.uceActivo.BackColor = System.Drawing.Color.Transparent;
            this.uceActivo.BackColorInternal = System.Drawing.Color.Transparent;
            this.uceActivo.Location = new System.Drawing.Point(415, 16);
            this.uceActivo.Name = "uceActivo";
            this.uceActivo.Size = new System.Drawing.Size(58, 20);
            this.uceActivo.TabIndex = 16;
            this.uceActivo.Text = "Activo";
            this.uceActivo.CheckedChanged += new System.EventHandler(this.uceActivo_CheckedChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(115, 44);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(358, 21);
            this.txtNombre.TabIndex = 15;
            this.txtNombre.TextChanged += new System.EventHandler(this.txtNombre_TextChanged);
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(115, 15);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(133, 21);
            this.txtCodigo.TabIndex = 14;
            this.txtCodigo.TextChanged += new System.EventHandler(this.txtCodigo_TextChanged);
            // 
            // lblNombre
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.lblNombre.Appearance = appearance5;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblNombre.Location = new System.Drawing.Point(24, 48);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(85, 23);
            this.lblNombre.TabIndex = 13;
            this.lblNombre.Text = "Nombre";
            // 
            // lblCodigo
            // 
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigo.Appearance = appearance6;
            this.lblCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCodigo.Location = new System.Drawing.Point(24, 19);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(85, 23);
            this.lblCodigo.TabIndex = 12;
            this.lblCodigo.Text = "Código";
            // 
            // utcOrigenDestino
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.utcOrigenDestino.Appearance = appearance3;
            this.utcOrigenDestino.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utcOrigenDestino.Controls.Add(this.ultraTabPageControl1);
            this.utcOrigenDestino.Location = new System.Drawing.Point(24, 81);
            this.utcOrigenDestino.Name = "utcOrigenDestino";
            this.utcOrigenDestino.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utcOrigenDestino.Size = new System.Drawing.Size(449, 212);
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.utcOrigenDestino.TabHeaderAreaAppearance = appearance4;
            this.utcOrigenDestino.TabIndex = 17;
            ultraTab2.TabPage = this.ultraTabPageControl1;
            ultraTab2.Text = ":: Distritos ::";
            this.utcOrigenDestino.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(445, 186);
            // 
            // utcEscalas
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.utcEscalas.Appearance = appearance1;
            this.utcEscalas.Controls.Add(this.ultraTabSharedControlsPage3);
            this.utcEscalas.Controls.Add(this.ultraTabPageControl3);
            this.utcEscalas.Location = new System.Drawing.Point(26, 299);
            this.utcEscalas.Name = "utcEscalas";
            this.utcEscalas.SharedControlsPage = this.ultraTabSharedControlsPage3;
            this.utcEscalas.Size = new System.Drawing.Size(447, 178);
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.utcEscalas.TabHeaderAreaAppearance = appearance2;
            this.utcEscalas.TabIndex = 18;
            ultraTab1.TabPage = this.ultraTabPageControl3;
            ultraTab1.Text = ":: Escalas ::";
            this.utcEscalas.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab1});
            // 
            // ultraTabSharedControlsPage3
            // 
            this.ultraTabSharedControlsPage3.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage3.Name = "ultraTabSharedControlsPage3";
            this.ultraTabSharedControlsPage3.Size = new System.Drawing.Size(443, 152);
            // 
            // FrmListaPreciosTransporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 557);
            this.Name = "FrmListaPreciosTransporte";
            this.Text = "Lista de Precios de Transporte";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            this.ugbParent.PerformLayout();
            this.ultraTabPageControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugDistritos)).EndInit();
            this.ultraTabPageControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugEscalas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uceActivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcOrigenDestino)).EndInit();
            this.utcOrigenDestino.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.utcEscalas)).EndInit();
            this.utcEscalas.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraCheckEditor uceActivo;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNombre;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtCodigo;
        private Infragistics.Win.Misc.UltraLabel lblNombre;
        private Infragistics.Win.Misc.UltraLabel lblCodigo;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcOrigenDestino;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl1;
        private Infragistics.Win.Misc.UltraButton ubEliminar;
        private Infragistics.Win.Misc.UltraButton ubNuevo;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugDistritos;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcEscalas;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage3;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl ultraTabPageControl3;
        private Infragistics.Win.Misc.UltraButton ubElminarEscala;
        private Infragistics.Win.Misc.UltraButton ubNuevaEscala;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugEscalas;
    }
}