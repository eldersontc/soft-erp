namespace Soft.Ventas.Win
{
    partial class FrmSolicitudCotizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSolicitudCotizacion));
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.txtNumeracion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblNumeracion = new Infragistics.Win.Misc.UltraLabel();
            this.ssCliente = new Soft.Controls.SoftSearch();
            this.ssTipoDocumento = new Soft.Controls.SoftSearch();
            this.lblCliente = new Infragistics.Win.Misc.UltraLabel();
            this.lblTipoDocumento = new Infragistics.Win.Misc.UltraLabel();
            this.udtFechaCreacion = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.lblFechaCreacion = new Infragistics.Win.Misc.UltraLabel();
            this.llbDescripcion = new Infragistics.Win.Misc.UltraLabel();
            this.txtDescripcion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblCantidad = new Infragistics.Win.Misc.UltraLabel();
            this.uneCantidad = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.lblLineaTrabajo = new Infragistics.Win.Misc.UltraLabel();
            this.ssLineaTrabajo = new Soft.Controls.SoftSearch();
            this.utcSubClasificacion = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
            this.ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
            this.tabItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
            this.ugProductos = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ubEliminarExistencia = new Infragistics.Win.Misc.UltraButton();
            this.ubNuevaExistencia = new Infragistics.Win.Misc.UltraButton();
            this.txtObservacion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel11 = new Infragistics.Win.Misc.UltraLabel();
            this.ssResponsable = new Soft.Controls.SoftSearch();
            this.lblResponsable = new Infragistics.Win.Misc.UltraLabel();
            this.txtTotal = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.lblTotal = new Infragistics.Win.Misc.UltraLabel();
            this.ubRecalcular = new Infragistics.Win.Misc.UltraButton();
            this.lblFormaPago = new Infragistics.Win.Misc.UltraLabel();
            this.ssFormaPago = new Soft.Controls.SoftSearch();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeracion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udtFechaCreacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcSubClasificacion)).BeginInit();
            this.utcSubClasificacion.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.ssFormaPago);
            this.ugbParent.Controls.Add(this.lblFormaPago);
            this.ugbParent.Controls.Add(this.ubRecalcular);
            this.ugbParent.Controls.Add(this.txtTotal);
            this.ugbParent.Controls.Add(this.lblTotal);
            this.ugbParent.Controls.Add(this.ssResponsable);
            this.ugbParent.Controls.Add(this.lblResponsable);
            this.ugbParent.Controls.Add(this.txtObservacion);
            this.ugbParent.Controls.Add(this.ultraLabel11);
            this.ugbParent.Controls.Add(this.utcSubClasificacion);
            this.ugbParent.Controls.Add(this.ssLineaTrabajo);
            this.ugbParent.Controls.Add(this.lblLineaTrabajo);
            this.ugbParent.Controls.Add(this.uneCantidad);
            this.ugbParent.Controls.Add(this.lblCantidad);
            this.ugbParent.Controls.Add(this.txtDescripcion);
            this.ugbParent.Controls.Add(this.llbDescripcion);
            this.ugbParent.Controls.Add(this.udtFechaCreacion);
            this.ugbParent.Controls.Add(this.lblFechaCreacion);
            this.ugbParent.Controls.Add(this.txtNumeracion);
            this.ugbParent.Controls.Add(this.lblNumeracion);
            this.ugbParent.Controls.Add(this.ssCliente);
            this.ugbParent.Controls.Add(this.ssTipoDocumento);
            this.ugbParent.Controls.Add(this.lblCliente);
            this.ugbParent.Controls.Add(this.lblTipoDocumento);
            this.ugbParent.Size = new System.Drawing.Size(700, 531);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblTipoDocumento, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblCliente, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssTipoDocumento, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssCliente, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblNumeracion, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtNumeracion, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblFechaCreacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.udtFechaCreacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.llbDescripcion, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtDescripcion, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblCantidad, 0);
            this.ugbParent.Controls.SetChildIndex(this.uneCantidad, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblLineaTrabajo, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssLineaTrabajo, 0);
            this.ugbParent.Controls.SetChildIndex(this.utcSubClasificacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel11, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtObservacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblResponsable, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssResponsable, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblTotal, 0);
            this.ugbParent.Controls.SetChildIndex(this.txtTotal, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubRecalcular, 0);
            this.ugbParent.Controls.SetChildIndex(this.lblFormaPago, 0);
            this.ugbParent.Controls.SetChildIndex(this.ssFormaPago, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Location = new System.Drawing.Point(606, 496);
            // 
            // ubAceptar
            // 
            this.ubAceptar.Location = new System.Drawing.Point(525, 496);
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
            // txtNumeracion
            // 
            this.txtNumeracion.Location = new System.Drawing.Point(561, 15);
            this.txtNumeracion.Name = "txtNumeracion";
            this.txtNumeracion.Size = new System.Drawing.Size(119, 21);
            this.txtNumeracion.TabIndex = 46;
            this.txtNumeracion.TextChanged += new System.EventHandler(this.txtNumeracion_TextChanged);
            // 
            // lblNumeracion
            // 
            appearance11.BackColor = System.Drawing.Color.Transparent;
            this.lblNumeracion.Appearance = appearance11;
            this.lblNumeracion.Location = new System.Drawing.Point(425, 20);
            this.lblNumeracion.Name = "lblNumeracion";
            this.lblNumeracion.Size = new System.Drawing.Size(130, 23);
            this.lblNumeracion.TabIndex = 50;
            this.lblNumeracion.Text = "Numeración";
            // 
            // ssCliente
            // 
            this.ssCliente.BackColor = System.Drawing.Color.Transparent;
            this.ssCliente.Location = new System.Drawing.Point(154, 46);
            this.ssCliente.Name = "ssCliente";
            this.ssCliente.Size = new System.Drawing.Size(250, 28);
            this.ssCliente.TabIndex = 47;
            this.ssCliente.Search += new System.EventHandler(this.ssCliente_Search);
            // 
            // ssTipoDocumento
            // 
            this.ssTipoDocumento.BackColor = System.Drawing.Color.Transparent;
            this.ssTipoDocumento.Location = new System.Drawing.Point(154, 15);
            this.ssTipoDocumento.Name = "ssTipoDocumento";
            this.ssTipoDocumento.Size = new System.Drawing.Size(250, 28);
            this.ssTipoDocumento.TabIndex = 45;
            this.ssTipoDocumento.Search += new System.EventHandler(this.ssTipoDocumento_Search);
            // 
            // lblCliente
            // 
            appearance12.BackColor = System.Drawing.Color.Transparent;
            this.lblCliente.Appearance = appearance12;
            this.lblCliente.Location = new System.Drawing.Point(18, 48);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(130, 23);
            this.lblCliente.TabIndex = 49;
            this.lblCliente.Text = "Cliente";
            // 
            // lblTipoDocumento
            // 
            appearance13.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoDocumento.Appearance = appearance13;
            this.lblTipoDocumento.Location = new System.Drawing.Point(18, 19);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(130, 23);
            this.lblTipoDocumento.TabIndex = 48;
            this.lblTipoDocumento.Text = "Tipo de Documento";
            // 
            // udtFechaCreacion
            // 
            this.udtFechaCreacion.DateTime = new System.DateTime(2013, 11, 21, 0, 0, 0, 0);
            this.udtFechaCreacion.Location = new System.Drawing.Point(561, 44);
            this.udtFechaCreacion.Name = "udtFechaCreacion";
            this.udtFechaCreacion.Size = new System.Drawing.Size(119, 21);
            this.udtFechaCreacion.TabIndex = 53;
            this.udtFechaCreacion.Value = new System.DateTime(2013, 11, 21, 0, 0, 0, 0);
            this.udtFechaCreacion.ValueChanged += new System.EventHandler(this.udtFechaCreacion_ValueChanged);
            // 
            // lblFechaCreacion
            // 
            appearance10.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaCreacion.Appearance = appearance10;
            this.lblFechaCreacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblFechaCreacion.Location = new System.Drawing.Point(425, 48);
            this.lblFechaCreacion.Name = "lblFechaCreacion";
            this.lblFechaCreacion.Size = new System.Drawing.Size(130, 23);
            this.lblFechaCreacion.TabIndex = 52;
            this.lblFechaCreacion.Text = "Fecha de Creación";
            // 
            // llbDescripcion
            // 
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.llbDescripcion.Appearance = appearance9;
            this.llbDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.llbDescripcion.Location = new System.Drawing.Point(18, 77);
            this.llbDescripcion.Name = "llbDescripcion";
            this.llbDescripcion.Size = new System.Drawing.Size(130, 23);
            this.llbDescripcion.TabIndex = 54;
            this.llbDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(154, 73);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(526, 21);
            this.txtDescripcion.TabIndex = 55;
            this.txtDescripcion.TextChanged += new System.EventHandler(this.txtDescripcion_TextChanged);
            // 
            // lblCantidad
            // 
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.lblCantidad.Appearance = appearance8;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCantidad.Location = new System.Drawing.Point(18, 134);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(130, 23);
            this.lblCantidad.TabIndex = 56;
            this.lblCantidad.Text = "Cantidad";
            // 
            // uneCantidad
            // 
            this.uneCantidad.Location = new System.Drawing.Point(155, 134);
            this.uneCantidad.Name = "uneCantidad";
            this.uneCantidad.Size = new System.Drawing.Size(100, 21);
            this.uneCantidad.TabIndex = 57;
            this.uneCantidad.ValueChanged += new System.EventHandler(this.uneCantidad_ValueChanged);
            // 
            // lblLineaTrabajo
            // 
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.lblLineaTrabajo.Appearance = appearance7;
            this.lblLineaTrabajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblLineaTrabajo.Location = new System.Drawing.Point(294, 138);
            this.lblLineaTrabajo.Name = "lblLineaTrabajo";
            this.lblLineaTrabajo.Size = new System.Drawing.Size(130, 23);
            this.lblLineaTrabajo.TabIndex = 58;
            this.lblLineaTrabajo.Text = "Linea de Trabajo";
            // 
            // ssLineaTrabajo
            // 
            this.ssLineaTrabajo.BackColor = System.Drawing.Color.Transparent;
            this.ssLineaTrabajo.Location = new System.Drawing.Point(430, 134);
            this.ssLineaTrabajo.Name = "ssLineaTrabajo";
            this.ssLineaTrabajo.Size = new System.Drawing.Size(250, 28);
            this.ssLineaTrabajo.TabIndex = 59;
            this.ssLineaTrabajo.Search += new System.EventHandler(this.ssLineaTrabajo_Search);
            // 
            // utcSubClasificacion
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.utcSubClasificacion.Appearance = appearance5;
            this.utcSubClasificacion.Controls.Add(this.ultraTabSharedControlsPage1);
            this.utcSubClasificacion.Controls.Add(this.tabItems);
            this.utcSubClasificacion.Location = new System.Drawing.Point(19, 171);
            this.utcSubClasificacion.Name = "utcSubClasificacion";
            this.utcSubClasificacion.SharedControlsPage = this.ultraTabSharedControlsPage1;
            this.utcSubClasificacion.Size = new System.Drawing.Size(662, 239);
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.utcSubClasificacion.TabHeaderAreaAppearance = appearance6;
            this.utcSubClasificacion.TabIndex = 60;
            ultraTab2.TabPage = this.tabItems;
            ultraTab2.Text = ":: Productos o Servicios ::";
            this.utcSubClasificacion.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[] {
            ultraTab2});
            // 
            // ultraTabSharedControlsPage1
            // 
            this.ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
            this.ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
            this.ultraTabSharedControlsPage1.Size = new System.Drawing.Size(658, 213);
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.ugProductos);
            this.tabItems.Controls.Add(this.ubEliminarExistencia);
            this.tabItems.Controls.Add(this.ubNuevaExistencia);
            this.tabItems.Location = new System.Drawing.Point(1, 23);
            this.tabItems.Name = "tabItems";
            this.tabItems.Size = new System.Drawing.Size(658, 213);
            // 
            // ugProductos
            // 
            this.ugProductos.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugProductos.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugProductos.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.ugProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.ugProductos.Location = new System.Drawing.Point(0, 0);
            this.ugProductos.Name = "ugProductos";
            this.ugProductos.Size = new System.Drawing.Size(658, 183);
            this.ugProductos.TabIndex = 9;
            this.ugProductos.Text = "ultraGrid1";
            this.ugProductos.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugProductos_CellChange);
            // 
            // ubEliminarExistencia
            // 
            this.ubEliminarExistencia.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
            this.ubEliminarExistencia.Location = new System.Drawing.Point(542, 187);
            this.ubEliminarExistencia.Name = "ubEliminarExistencia";
            this.ubEliminarExistencia.Size = new System.Drawing.Size(111, 23);
            this.ubEliminarExistencia.TabIndex = 8;
            this.ubEliminarExistencia.Text = "&Eliminar Existencia";
            this.ubEliminarExistencia.Click += new System.EventHandler(this.ubEliminarExistencia_Click);
            // 
            // ubNuevaExistencia
            // 
            this.ubNuevaExistencia.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Office2010Button;
            this.ubNuevaExistencia.Location = new System.Drawing.Point(432, 187);
            this.ubNuevaExistencia.Name = "ubNuevaExistencia";
            this.ubNuevaExistencia.Size = new System.Drawing.Size(104, 23);
            this.ubNuevaExistencia.TabIndex = 7;
            this.ubNuevaExistencia.Text = "&Nueva Existencia";
            this.ubNuevaExistencia.Click += new System.EventHandler(this.ubNuevaExistencia_Click);
            // 
            // txtObservacion
            // 
            this.txtObservacion.Location = new System.Drawing.Point(105, 431);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(349, 49);
            this.txtObservacion.TabIndex = 61;
            this.txtObservacion.TextChanged += new System.EventHandler(this.txtObservacion_TextChanged);
            // 
            // ultraLabel11
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel11.Appearance = appearance4;
            this.ultraLabel11.Location = new System.Drawing.Point(20, 434);
            this.ultraLabel11.Name = "ultraLabel11";
            this.ultraLabel11.Size = new System.Drawing.Size(130, 23);
            this.ultraLabel11.TabIndex = 62;
            this.ultraLabel11.Text = "Observación";
            // 
            // ssResponsable
            // 
            this.ssResponsable.BackColor = System.Drawing.Color.Transparent;
            this.ssResponsable.Location = new System.Drawing.Point(154, 104);
            this.ssResponsable.Name = "ssResponsable";
            this.ssResponsable.Size = new System.Drawing.Size(250, 28);
            this.ssResponsable.TabIndex = 63;
            this.ssResponsable.Search += new System.EventHandler(this.ssResponsable_Search);
            // 
            // lblResponsable
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.lblResponsable.Appearance = appearance3;
            this.lblResponsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblResponsable.Location = new System.Drawing.Point(18, 106);
            this.lblResponsable.Name = "lblResponsable";
            this.lblResponsable.Size = new System.Drawing.Size(130, 23);
            this.lblResponsable.TabIndex = 64;
            this.lblResponsable.Text = "Responsable";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(580, 459);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.txtTotal.Size = new System.Drawing.Size(100, 21);
            this.txtTotal.TabIndex = 66;
            this.txtTotal.ValueChanged += new System.EventHandler(this.txtTotal_ValueChanged);
            // 
            // lblTotal
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.Appearance = appearance2;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblTotal.Location = new System.Drawing.Point(510, 463);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(73, 24);
            this.lblTotal.TabIndex = 65;
            this.lblTotal.Text = "Total";
            // 
            // ubRecalcular
            // 
            this.ubRecalcular.Location = new System.Drawing.Point(510, 429);
            this.ubRecalcular.Name = "ubRecalcular";
            this.ubRecalcular.Size = new System.Drawing.Size(170, 23);
            this.ubRecalcular.TabIndex = 67;
            this.ubRecalcular.Text = "Recalcular";
            this.ubRecalcular.Click += new System.EventHandler(this.ubRecalcular_Click);
            // 
            // lblFormaPago
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.lblFormaPago.Appearance = appearance1;
            this.lblFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblFormaPago.Location = new System.Drawing.Point(425, 106);
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.Size = new System.Drawing.Size(130, 23);
            this.lblFormaPago.TabIndex = 68;
            this.lblFormaPago.Text = "Forma Pago";
            // 
            // ssFormaPago
            // 
            this.ssFormaPago.BackColor = System.Drawing.Color.Transparent;
            this.ssFormaPago.Location = new System.Drawing.Point(510, 104);
            this.ssFormaPago.Name = "ssFormaPago";
            this.ssFormaPago.Size = new System.Drawing.Size(171, 28);
            this.ssFormaPago.TabIndex = 69;
            this.ssFormaPago.Search += new System.EventHandler(this.ssFormaPago_Search);
            // 
            // FrmSolicitudCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 569);
            this.Name = "FrmSolicitudCotizacion";
            this.Text = "Solicitud de Cotización";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            this.ugbParent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeracion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udtFechaCreacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripcion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.utcSubClasificacion)).EndInit();
            this.utcSubClasificacion.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor udtFechaCreacion;
        private Infragistics.Win.Misc.UltraLabel lblFechaCreacion;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNumeracion;
        private Infragistics.Win.Misc.UltraLabel lblNumeracion;
        private Controls.SoftSearch ssCliente;
        private Controls.SoftSearch ssTipoDocumento;
        private Infragistics.Win.Misc.UltraLabel lblCliente;
        private Infragistics.Win.Misc.UltraLabel lblTipoDocumento;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtDescripcion;
        private Infragistics.Win.Misc.UltraLabel llbDescripcion;
        private Controls.SoftSearch ssLineaTrabajo;
        private Infragistics.Win.Misc.UltraLabel lblLineaTrabajo;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneCantidad;
        private Infragistics.Win.Misc.UltraLabel lblCantidad;
        private Infragistics.Win.UltraWinTabControl.UltraTabControl utcSubClasificacion;
        private Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage ultraTabSharedControlsPage1;
        private Infragistics.Win.UltraWinTabControl.UltraTabPageControl tabItems;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugProductos;
        private Infragistics.Win.Misc.UltraButton ubEliminarExistencia;
        private Infragistics.Win.Misc.UltraButton ubNuevaExistencia;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtObservacion;
        private Infragistics.Win.Misc.UltraLabel ultraLabel11;
        private Controls.SoftSearch ssResponsable;
        private Infragistics.Win.Misc.UltraLabel lblResponsable;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor txtTotal;
        private Infragistics.Win.Misc.UltraLabel lblTotal;
        private Infragistics.Win.Misc.UltraButton ubRecalcular;
        private Controls.SoftSearch ssFormaPago;
        private Infragistics.Win.Misc.UltraLabel lblFormaPago;
    }
}