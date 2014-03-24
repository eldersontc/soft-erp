namespace Soft.Facturacion.Win
{
    partial class FrmFacturacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturacion));
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            this.ssMoneda = new Soft.Controls.SoftSearch();
            this.lblMoneda = new Infragistics.Win.Misc.UltraLabel();
            this.udtFechaCreacion = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.txtNumeracion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.utcOrdenesProducion = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.tabItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ugOrdenesProduccion = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ubQuitarOP = new Infragistics.Win.Misc.UltraButton();
            this.ubAgregarOP = new Infragistics.Win.Misc.UltraButton();
            this.ssResponsable = new Soft.Controls.SoftSearch();
            this.ssCliente = new Soft.Controls.SoftSearch();
            this.ssTipoDocumento = new Soft.Controls.SoftSearch();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.lblCliente = new Infragistics.Win.Misc.UltraLabel();
            this.Codigo = new Infragistics.Win.Misc.UltraLabel();
            this.txtObservacion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
            this.ugbCosto = new Infragistics.Win.Misc.UltraGroupBox();
            this.uneTotal = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.uneImpuesto = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.uneSubTotal = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.LabelTotal = new Infragistics.Win.Misc.UltraLabel();
            this.LabelImpuesto = new Infragistics.Win.Misc.UltraLabel();
            this.LabelSubtotal = new Infragistics.Win.Misc.UltraLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udtFechaCreacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcOrdenesProducion)).BeginInit();
            this.utcOrdenesProducion.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugOrdenesProduccion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ugbCosto)).BeginInit();
            this.ugbCosto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uneTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneImpuesto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneSubTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.txtObservacion);
            this.ugbParent.Controls.Add(this.ultraLabel11);
            this.ugbParent.Controls.Add(this.ugbCosto);
            this.ugbParent.Controls.Add(this.ssMoneda);
            this.ugbParent.Controls.Add(this.lblMoneda);
            this.ugbParent.Controls.Add(this.udtFechaCreacion);
            this.ugbParent.Controls.Add(this.txtNumeracion);
            this.ugbParent.Controls.Add(this.ultraLabel7);
            this.ugbParent.Controls.Add(this.ultraLabel4);
            this.ugbParent.Controls.Add(this.utcOrdenesProducion);
            this.ugbParent.Controls.Add(this.ssResponsable);
            this.ugbParent.Controls.Add(this.ssCliente);
            this.ugbParent.Controls.Add(this.ssTipoDocumento);
            this.ugbParent.Controls.Add(this.ultraLabel3);
            this.ugbParent.Controls.Add(this.lblCliente);
            this.ugbParent.Controls.Add(this.Codigo);
            this.ugbParent.Size = new System.Drawing.Size(694, 544);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.Codigo, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblCliente, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel3, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssTipoDocumento, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssCliente, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssResponsable, 0);
            this.ugbParent.Controls.SetChildIndex(this.utcOrdenesProducion, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel4, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel7, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtNumeracion, 0);
            this.ugbParent.Controls.SetChildIndex(this.udtFechaCreacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblMoneda, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssMoneda, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbCosto, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel11, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtObservacion, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Location = new System.Drawing.Point(600, 505);
            // 
            // ubAceptar
            // 
            this.ubAceptar.Location = new System.Drawing.Point(519, 505);
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
            // ssMoneda
            // 
            this.ssMoneda.BackColor = System.Drawing.Color.Transparent;
            this.ssMoneda.Location = new System.Drawing.Point(153, 104);
            this.ssMoneda.Name = "ssMoneda";
            this.ssMoneda.Size = new System.Drawing.Size(106, 28);
            this.ssMoneda.TabIndex = 74;
            this.ssMoneda.Search += new System.EventHandler(this.ssMoneda_Search);
            // 
            // lblMoneda
            // 
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.lblMoneda.Appearance = appearance7;
            this.lblMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblMoneda.Location = new System.Drawing.Point(17, 108);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(130, 23);
            this.lblMoneda.TabIndex = 73;
            this.lblMoneda.Text = "Moneda";
            // 
            // udtFechaCreacion
            // 
            this.udtFechaCreacion.Location = new System.Drawing.Point(559, 45);
            this.udtFechaCreacion.Name = "udtFechaCreacion";
            this.udtFechaCreacion.Size = new System.Drawing.Size(119, 21);
            this.udtFechaCreacion.TabIndex = 72;
            this.udtFechaCreacion.ValueChanged += new System.EventHandler(this.udtFechaCreacion_ValueChanged);
            // 
            // txtNumeracion
            // 
            this.txtNumeracion.Location = new System.Drawing.Point(559, 16);
            this.txtNumeracion.Name = "txtNumeracion";
            this.txtNumeracion.Size = new System.Drawing.Size(119, 21);
            this.txtNumeracion.TabIndex = 57;
            this.txtNumeracion.TextChanged += new System.EventHandler(this.txtNumeracion_TextChanged);
            // 
            // ultraLabel7
            // 
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel7.Appearance = appearance8;
            this.ultraLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ultraLabel7.Location = new System.Drawing.Point(423, 49);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(130, 23);
            this.ultraLabel7.TabIndex = 69;
            this.ultraLabel7.Text = "Fecha de Creación";
            // 
            // ultraLabel4
            // 
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel4.Appearance = appearance9;
            this.ultraLabel4.Location = new System.Drawing.Point(423, 21);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(130, 23);
            this.ultraLabel4.TabIndex = 66;
            this.ultraLabel4.Text = "Numeración";
            // 
            // utcOrdenesProducion
            // 
            appearance10.BackColor = System.Drawing.Color.Transparent;
            this.utcOrdenesProducion.Appearance = appearance10;
            this.utcOrdenesProducion.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utcOrdenesProducion.Controls.Add(this.tabItems);
            this.utcOrdenesProducion.Location = new System.Drawing.Point(16, 134);
            this.utcOrdenesProducion.Name = "utcOrdenesProducion";
            this.utcOrdenesProducion.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utcOrdenesProducion.Size = new System.Drawing.Size(662, 250);
            appearance11.BackColor = System.Drawing.Color.Transparent;
            this.utcOrdenesProducion.TabHeaderAreaAppearance = appearance11;
            this.utcOrdenesProducion.TabIndex = 61;
            ultraTab2.TabPage = this.tabItems;
            ultraTab2.Text = ":: Ordenes de Producción ::";
            this.utcOrdenesProducion.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(658, 224);
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.ugOrdenesProduccion);
            this.tabItems.Controls.Add(this.ubQuitarOP);
            this.tabItems.Controls.Add(this.ubAgregarOP);
            this.tabItems.Location = new System.Drawing.Point(1, 23);
            this.tabItems.Name = "tabItems";
            this.tabItems.Size = new System.Drawing.Size(658, 224);
            // 
            // ugOrdenesProduccion
            // 
            this.ugOrdenesProduccion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugOrdenesProduccion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugOrdenesProduccion.DisplayLayout.Override.RowSelectorNumberStyle = Infragistics.Win.UltraWinGrid.RowSelectorNumberStyle.ListIndex;
            this.ugOrdenesProduccion.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ugOrdenesProduccion.Dock = System.Windows.Forms.DockStyle.Top;
            this.ugOrdenesProduccion.Location = new System.Drawing.Point(0, 0);
            this.ugOrdenesProduccion.Name = "ugOrdenesProduccion";
            this.ugOrdenesProduccion.Size = new System.Drawing.Size(658, 187);
            this.ugOrdenesProduccion.TabIndex = 9;
            this.ugOrdenesProduccion.Text = "ultraGrid1";
            // 
            // ubQuitarOP
            // 
            this.ubQuitarOP.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
            this.ubQuitarOP.Location = new System.Drawing.Point(542, 193);
            this.ubQuitarOP.Name = "ubQuitarOP";
            this.ubQuitarOP.Size = new System.Drawing.Size(111, 23);
            this.ubQuitarOP.TabIndex = 8;
            this.ubQuitarOP.Text = "&Quitar OP";
            this.ubQuitarOP.Click += new System.EventHandler(this.ubQuitarOP_Click);
            // 
            // ubAgregarOP
            // 
            this.ubAgregarOP.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
            this.ubAgregarOP.Location = new System.Drawing.Point(432, 193);
            this.ubAgregarOP.Name = "ubAgregarOP";
            this.ubAgregarOP.Size = new System.Drawing.Size(104, 23);
            this.ubAgregarOP.TabIndex = 7;
            this.ubAgregarOP.Text = "&Agregar OP";
            this.ubAgregarOP.Click += new System.EventHandler(this.ubAgregarOP_Click);
            // 
            // ssResponsable
            // 
            this.ssResponsable.BackColor = System.Drawing.Color.Transparent;
            this.ssResponsable.Location = new System.Drawing.Point(153, 76);
            this.ssResponsable.Name = "ssResponsable";
            this.ssResponsable.Size = new System.Drawing.Size(250, 28);
            this.ssResponsable.TabIndex = 60;
            this.ssResponsable.Search += new System.EventHandler(this.ssResponsable_Search);
            // 
            // ssCliente
            // 
            this.ssCliente.BackColor = System.Drawing.Color.Transparent;
            this.ssCliente.Location = new System.Drawing.Point(152, 47);
            this.ssCliente.Name = "ssCliente";
            this.ssCliente.Size = new System.Drawing.Size(250, 28);
            this.ssCliente.TabIndex = 58;
            this.ssCliente.Search += new System.EventHandler(this.ssCliente_Search);
            // 
            // ssTipoDocumento
            // 
            this.ssTipoDocumento.BackColor = System.Drawing.Color.Transparent;
            this.ssTipoDocumento.Location = new System.Drawing.Point(152, 16);
            this.ssTipoDocumento.Name = "ssTipoDocumento";
            this.ssTipoDocumento.Size = new System.Drawing.Size(250, 28);
            this.ssTipoDocumento.TabIndex = 56;
            this.ssTipoDocumento.Search += new System.EventHandler(this.ssTipoDocumento_Search);
            // 
            // ultraLabel3
            // 
            appearance12.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel3.Appearance = appearance12;
            this.ultraLabel3.Location = new System.Drawing.Point(17, 78);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(130, 23);
            this.ultraLabel3.TabIndex = 65;
            this.ultraLabel3.Text = "Creado Por";
            // 
            // lblCliente
            // 
            appearance13.BackColor = System.Drawing.Color.Transparent;
            this.lblCliente.Appearance = appearance13;
            this.lblCliente.Location = new System.Drawing.Point(16, 49);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(130, 23);
            this.lblCliente.TabIndex = 63;
            this.lblCliente.Text = "Cliente";
            // 
            // Codigo
            // 
            appearance14.BackColor = System.Drawing.Color.Transparent;
            this.Codigo.Appearance = appearance14;
            this.Codigo.Location = new System.Drawing.Point(16, 20);
            this.Codigo.Name = "Codigo";
            this.Codigo.Size = new System.Drawing.Size(130, 23);
            this.Codigo.TabIndex = 62;
            this.Codigo.Text = "Tipo de Documento";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(99, 399);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(311, 49);
            this.txtObservacion.TabIndex = 75;
            this.txtObservacion.TextChanged += new System.EventHandler(this.txtObservacion_TextChanged);
            // 
            // ultraLabel11
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel11.Appearance = appearance1;
            this.ultraLabel11.Location = new System.Drawing.Point(14, 402);
            this.ultraLabel11.Name = "ultraLabel11";
            this.ultraLabel11.Size = new System.Drawing.Size(130, 23);
            this.ultraLabel11.TabIndex = 77;
            this.ultraLabel11.Text = "Observación";
            // 
            // ugbCosto
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ugbCosto.Appearance = appearance2;
            this.ugbCosto.Controls.Add(this.uneTotal);
            this.ugbCosto.Controls.Add(this.uneImpuesto);
            this.ugbCosto.Controls.Add(this.uneSubTotal);
            this.ugbCosto.Controls.Add(this.LabelTotal);
            this.ugbCosto.Controls.Add(this.LabelImpuesto);
            this.ugbCosto.Controls.Add(this.LabelSubtotal);
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.ugbCosto.HeaderAppearance = appearance6;
            this.ugbCosto.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbCosto.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ugbCosto.Location = new System.Drawing.Point(447, 389);
            this.ugbCosto.Name = "ugbCosto";
            this.ugbCosto.Size = new System.Drawing.Size(229, 110);
            this.ugbCosto.TabIndex = 76;
            this.ugbCosto.Text = "Costo";
            // 
            // uneTotal
            // 
            this.uneTotal.Enabled = false;
            this.uneTotal.Location = new System.Drawing.Point(116, 80);
            this.uneTotal.Name = "uneTotal";
            this.uneTotal.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.uneTotal.Size = new System.Drawing.Size(100, 21);
            this.uneTotal.TabIndex = 58;
            // 
            // uneImpuesto
            // 
            this.uneImpuesto.Enabled = false;
            this.uneImpuesto.Location = new System.Drawing.Point(116, 55);
            this.uneImpuesto.Name = "uneImpuesto";
            this.uneImpuesto.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.uneImpuesto.Size = new System.Drawing.Size(100, 21);
            this.uneImpuesto.TabIndex = 57;
            // 
            // uneSubTotal
            // 
            this.uneSubTotal.Enabled = false;
            this.uneSubTotal.Location = new System.Drawing.Point(116, 30);
            this.uneSubTotal.Name = "uneSubTotal";
            this.uneSubTotal.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.uneSubTotal.Size = new System.Drawing.Size(100, 21);
            this.uneSubTotal.TabIndex = 56;
            // 
            // LabelTotal
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.LabelTotal.Appearance = appearance3;
            this.LabelTotal.Location = new System.Drawing.Point(14, 84);
            this.LabelTotal.Name = "LabelTotal";
            this.LabelTotal.Size = new System.Drawing.Size(96, 23);
            this.LabelTotal.TabIndex = 55;
            this.LabelTotal.Text = "Total";
            // 
            // LabelImpuesto
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.LabelImpuesto.Appearance = appearance4;
            this.LabelImpuesto.Location = new System.Drawing.Point(14, 59);
            this.LabelImpuesto.Name = "LabelImpuesto";
            this.LabelImpuesto.Size = new System.Drawing.Size(96, 23);
            this.LabelImpuesto.TabIndex = 54;
            this.LabelImpuesto.Text = "Impuesto";
            // 
            // LabelSubtotal
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.LabelSubtotal.Appearance = appearance5;
            this.LabelSubtotal.Location = new System.Drawing.Point(14, 30);
            this.LabelSubtotal.Name = "LabelSubtotal";
            this.LabelSubtotal.Size = new System.Drawing.Size(96, 23);
            this.LabelSubtotal.TabIndex = 53;
            this.LabelSubtotal.Text = "Sub Total";
            // 
            // FrmFacturacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 582);
            this.Name = "FrmFacturacion";
            this.Text = "Facturación";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            this.ugbParent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udtFechaCreacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcOrdenesProducion)).EndInit();
            this.utcOrdenesProducion.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugOrdenesProduccion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ugbCosto)).EndInit();
            this.ugbCosto.ResumeLayout(false);
            this.ugbCosto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uneTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneImpuesto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneSubTotal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtObservacion;
        private Infragistics.Win.Misc.UltraLabel ultraLabel11;
        private Infragistics.Win.Misc.UltraGroupBox ugbCosto;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneTotal;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneImpuesto;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneSubTotal;
        private Infragistics.Win.Misc.UltraLabel LabelTotal;
        private Infragistics.Win.Misc.UltraLabel LabelImpuesto;
        private Infragistics.Win.Misc.UltraLabel LabelSubtotal;
        private Controls.SoftSearch ssMoneda;
        private Infragistics.Win.Misc.UltraLabel lblMoneda;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor udtFechaCreacion;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNumeracion;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcOrdenesProducion;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabItems;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugOrdenesProduccion;
        private Infragistics.Win.Misc.UltraButton ubQuitarOP;
        private Infragistics.Win.Misc.UltraButton ubAgregarOP;
        private Controls.SoftSearch ssResponsable;
        private Controls.SoftSearch ssCliente;
        private Controls.SoftSearch ssTipoDocumento;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraLabel lblCliente;
        private Infragistics.Win.Misc.UltraLabel Codigo;
    }
}