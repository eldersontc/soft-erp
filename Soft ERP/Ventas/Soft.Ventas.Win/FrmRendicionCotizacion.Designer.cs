using Soft.Win;
namespace Soft.Ventas.Win
{
    partial class FrmRendicionCotizacion: FrmParent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRendicionCotizacion));
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.udteFechaCreacion = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.uteNumeroCotizacion = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ugbItems = new Infragistics.Win.Misc.UltraGroupBox();
            this.ugItems = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.uneTotalReal = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            this.uneTotalCotizacion = new Infragistics.Win.UltraWinEditors.UltraNumericEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udteFechaCreacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uteNumeroCotizacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ugbItems)).BeginInit();
            this.ugbItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneTotalReal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneTotalCotizacion)).BeginInit();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.uneTotalCotizacion);
            this.ugbParent.Controls.Add(this.uneTotalReal);
            this.ugbParent.Controls.Add(this.ultraLabel4);
            this.ugbParent.Controls.Add(this.ultraLabel3);
            this.ugbParent.Controls.Add(this.ugbItems);
            this.ugbParent.Controls.Add(this.uteNumeroCotizacion);
            this.ugbParent.Controls.Add(this.udteFechaCreacion);
            this.ugbParent.Controls.Add(this.ultraLabel2);
            this.ugbParent.Controls.Add(this.ultraLabel1);
            this.ugbParent.Size = new System.Drawing.Size(1032, 516);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel1, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel2, 0);
            this.ugbParent.Controls.SetChildIndex(this.udteFechaCreacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.uteNumeroCotizacion, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbItems, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel3, 0);
            this.ugbParent.Controls.SetChildIndex(this.ultraLabel4, 0);
            this.ugbParent.Controls.SetChildIndex(this.uneTotalReal, 0);
            this.ugbParent.Controls.SetChildIndex(this.uneTotalCotizacion, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ubCancelar.Location = new System.Drawing.Point(940, 479);
            // 
            // ubAceptar
            // 
            this.ubAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ubAceptar.Location = new System.Drawing.Point(859, 479);
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
            // ultraLabel1
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel1.Appearance = appearance5;
            this.ultraLabel1.Location = new System.Drawing.Point(16, 18);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel1.TabIndex = 6;
            this.ultraLabel1.Text = "FECHA DE CREACIÓN";
            // 
            // ultraLabel2
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel2.Appearance = appearance4;
            this.ultraLabel2.Location = new System.Drawing.Point(318, 18);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(134, 23);
            this.ultraLabel2.TabIndex = 7;
            this.ultraLabel2.Text = "Nº COTIZACIÓN";
            // 
            // udteFechaCreacion
            // 
            this.udteFechaCreacion.DateTime = new System.DateTime(2015, 3, 9, 0, 0, 0, 0);
            this.udteFechaCreacion.Location = new System.Drawing.Point(150, 14);
            this.udteFechaCreacion.Name = "udteFechaCreacion";
            this.udteFechaCreacion.Size = new System.Drawing.Size(144, 21);
            this.udteFechaCreacion.TabIndex = 8;
            this.udteFechaCreacion.Value = new System.DateTime(2015, 3, 9, 0, 0, 0, 0);
            this.udteFechaCreacion.ValueChanged += new System.EventHandler(this.udteFechaCreacion_ValueChanged);
            // 
            // uteNumeroCotizacion
            // 
            this.uteNumeroCotizacion.Location = new System.Drawing.Point(440, 14);
            this.uteNumeroCotizacion.Name = "uteNumeroCotizacion";
            this.uteNumeroCotizacion.ReadOnly = true;
            this.uteNumeroCotizacion.Size = new System.Drawing.Size(159, 21);
            this.uteNumeroCotizacion.TabIndex = 9;
            // 
            // ugbItems
            // 
            this.ugbItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.ugbItems.Appearance = appearance3;
            this.ugbItems.Controls.Add(this.ugItems);
            this.ugbItems.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbItems.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopInsideBorder;
            this.ugbItems.Location = new System.Drawing.Point(16, 47);
            this.ugbItems.Name = "ugbItems";
            this.ugbItems.Size = new System.Drawing.Size(999, 396);
            this.ugbItems.TabIndex = 12;
            this.ugbItems.Text = "ITEMS";
            // 
            // ugItems
            // 
            this.ugItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.ugItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ugItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ugItems.Location = new System.Drawing.Point(3, 25);
            this.ugItems.Name = "ugItems";
            this.ugItems.Size = new System.Drawing.Size(993, 368);
            this.ugItems.TabIndex = 0;
            this.ugItems.Text = "ultraGrid1";
            this.ugItems.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.ugItems_CellChange);
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel3.Appearance = appearance2;
            this.ultraLabel3.Location = new System.Drawing.Point(598, 453);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(122, 23);
            this.ultraLabel3.TabIndex = 13;
            this.ultraLabel3.Text = "TOTAL COTIZACIÓN";
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel4.Appearance = appearance1;
            this.ultraLabel4.Location = new System.Drawing.Point(823, 453);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(80, 23);
            this.ultraLabel4.TabIndex = 14;
            this.ultraLabel4.Text = "TOTAL REAL";
            // 
            // uneTotalReal
            // 
            this.uneTotalReal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uneTotalReal.Location = new System.Drawing.Point(915, 449);
            this.uneTotalReal.Name = "uneTotalReal";
            this.uneTotalReal.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.uneTotalReal.ReadOnly = true;
            this.uneTotalReal.Size = new System.Drawing.Size(100, 21);
            this.uneTotalReal.TabIndex = 15;
            // 
            // uneTotalCotizacion
            // 
            this.uneTotalCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uneTotalCotizacion.Location = new System.Drawing.Point(717, 449);
            this.uneTotalCotizacion.Name = "uneTotalCotizacion";
            this.uneTotalCotizacion.NumericType = Infragistics.Win.UltraWinEditors.NumericType.Double;
            this.uneTotalCotizacion.ReadOnly = true;
            this.uneTotalCotizacion.Size = new System.Drawing.Size(100, 21);
            this.uneTotalCotizacion.TabIndex = 16;
            // 
            // FrmRendicionCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 554);
            this.Name = "FrmRendicionCotizacion";
            this.Text = "RENDICIÓN DE COTIZACIÓN";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            this.ugbParent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udteFechaCreacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uteNumeroCotizacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ugbItems)).EndInit();
            this.ugbItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneTotalReal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uneTotalCotizacion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor uteNumeroCotizacion;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor udteFechaCreacion;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraGroupBox ugbItems;
        private Infragistics.Win.UltraWinGrid.UltraGrid ugItems;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneTotalCotizacion;
        private Infragistics.Win.UltraWinEditors.UltraNumericEditor uneTotalReal;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
    }
}