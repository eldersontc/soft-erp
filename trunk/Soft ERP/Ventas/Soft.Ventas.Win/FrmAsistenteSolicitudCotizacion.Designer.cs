namespace Soft.Ventas.Win
{
    partial class FrmAsistenteSolicitudCotizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAsistenteSolicitudCotizacion));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            this.ugbGeneral = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtCodigoGrupo = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblCodigoGrupo = new Infragistics.Win.Misc.UltraLabel();
            this.ssPlantilla = new Soft.Controls.SoftSearch();
            this.udtFechaCreacion = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.lblPlantilla = new Infragistics.Win.Misc.UltraLabel();
            this.lblFechaCreacion = new Infragistics.Win.Misc.UltraLabel();
            this.ssMoneda = new Soft.Controls.SoftSearch();
            this.ssCliente = new Soft.Controls.SoftSearch();
            this.lblMoneda = new Infragistics.Win.Misc.UltraLabel();
            this.ssTipoDocumento = new Soft.Controls.SoftSearch();
            this.lblCliente = new Infragistics.Win.Misc.UltraLabel();
            this.lblTipoDocumento = new Infragistics.Win.Misc.UltraLabel();
            this.ubGenerarSolicitudCotizacion = new Infragistics.Win.Misc.UltraButton();
            this.lblContacto = new Infragistics.Win.Misc.UltraLabel();
            this.lblFormaPago = new Infragistics.Win.Misc.UltraLabel();
            this.ssContacto = new Soft.Controls.SoftSearch();
            this.ssFormaPago = new Soft.Controls.SoftSearch();
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).BeginInit();
            this.ugbParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ugbGeneral)).BeginInit();
            this.ugbGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udtFechaCreacion)).BeginInit();
            this.SuspendLayout();
            // 
            // ugbParent
            // 
            this.ugbParent.Controls.Add(this.ubGenerarSolicitudCotizacion);
            this.ugbParent.Controls.Add(this.ugbGeneral);
            this.ugbParent.Size = new System.Drawing.Size(612, 207);
            this.ugbParent.Controls.SetChildIndex(this.ubAceptar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubCancelar, 0);
            this.ugbParent.Controls.SetChildIndex(this.ugbGeneral, 0);
            this.ugbParent.Controls.SetChildIndex(this.ubGenerarSolicitudCotizacion, 0);
            // 
            // ubCancelar
            // 
            this.ubCancelar.Location = new System.Drawing.Point(523, 213);
            this.ubCancelar.Visible = false;
            // 
            // ubAceptar
            // 
            this.ubAceptar.Location = new System.Drawing.Point(442, 213);
            this.ubAceptar.Visible = false;
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
            // ugbGeneral
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.ugbGeneral.Appearance = appearance1;
            this.ugbGeneral.Controls.Add(this.ssFormaPago);
            this.ugbGeneral.Controls.Add(this.ssContacto);
            this.ugbGeneral.Controls.Add(this.lblFormaPago);
            this.ugbGeneral.Controls.Add(this.lblContacto);
            this.ugbGeneral.Controls.Add(this.txtCodigoGrupo);
            this.ugbGeneral.Controls.Add(this.lblCodigoGrupo);
            this.ugbGeneral.Controls.Add(this.ssPlantilla);
            this.ugbGeneral.Controls.Add(this.udtFechaCreacion);
            this.ugbGeneral.Controls.Add(this.lblPlantilla);
            this.ugbGeneral.Controls.Add(this.lblFechaCreacion);
            this.ugbGeneral.Controls.Add(this.ssMoneda);
            this.ugbGeneral.Controls.Add(this.ssCliente);
            this.ugbGeneral.Controls.Add(this.lblMoneda);
            this.ugbGeneral.Controls.Add(this.ssTipoDocumento);
            this.ugbGeneral.Controls.Add(this.lblCliente);
            this.ugbGeneral.Controls.Add(this.lblTipoDocumento);
            this.ugbGeneral.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded3;
            this.ugbGeneral.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ugbGeneral.Location = new System.Drawing.Point(18, 17);
            this.ugbGeneral.Name = "ugbGeneral";
            this.ugbGeneral.Size = new System.Drawing.Size(580, 149);
            this.ugbGeneral.TabIndex = 82;
            this.ugbGeneral.Text = "General";
            // 
            // txtCodigoGrupo
            // 
            this.txtCodigoGrupo.Enabled = false;
            this.txtCodigoGrupo.Location = new System.Drawing.Point(447, 87);
            this.txtCodigoGrupo.Name = "txtCodigoGrupo";
            this.txtCodigoGrupo.Size = new System.Drawing.Size(119, 21);
            this.txtCodigoGrupo.TabIndex = 88;
            // 
            // lblCodigoGrupo
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.lblCodigoGrupo.Appearance = appearance4;
            this.lblCodigoGrupo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCodigoGrupo.Location = new System.Drawing.Point(324, 91);
            this.lblCodigoGrupo.Name = "lblCodigoGrupo";
            this.lblCodigoGrupo.Size = new System.Drawing.Size(130, 23);
            this.lblCodigoGrupo.TabIndex = 87;
            this.lblCodigoGrupo.Text = "Código Grupo";
            // 
            // ssPlantilla
            // 
            this.ssPlantilla.BackColor = System.Drawing.Color.Transparent;
            this.ssPlantilla.Location = new System.Drawing.Point(117, 86);
            this.ssPlantilla.Name = "ssPlantilla";
            this.ssPlantilla.Size = new System.Drawing.Size(192, 28);
            this.ssPlantilla.TabIndex = 85;
            this.ssPlantilla.Search += new System.EventHandler(this.ssPlantilla_Search);
            // 
            // udtFechaCreacion
            // 
            this.udtFechaCreacion.DateTime = new System.DateTime(2013, 11, 21, 0, 0, 0, 0);
            this.udtFechaCreacion.Location = new System.Drawing.Point(447, 31);
            this.udtFechaCreacion.Name = "udtFechaCreacion";
            this.udtFechaCreacion.Size = new System.Drawing.Size(119, 21);
            this.udtFechaCreacion.TabIndex = 84;
            this.udtFechaCreacion.Value = new System.DateTime(2013, 11, 21, 0, 0, 0, 0);
            this.udtFechaCreacion.ValueChanged += new System.EventHandler(this.udtFechaCreacion_ValueChanged);
            // 
            // lblPlantilla
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.lblPlantilla.Appearance = appearance5;
            this.lblPlantilla.Location = new System.Drawing.Point(10, 91);
            this.lblPlantilla.Name = "lblPlantilla";
            this.lblPlantilla.Size = new System.Drawing.Size(130, 23);
            this.lblPlantilla.TabIndex = 86;
            this.lblPlantilla.Text = "Plantilla";
            // 
            // lblFechaCreacion
            // 
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.lblFechaCreacion.Appearance = appearance6;
            this.lblFechaCreacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblFechaCreacion.Location = new System.Drawing.Point(324, 33);
            this.lblFechaCreacion.Name = "lblFechaCreacion";
            this.lblFechaCreacion.Size = new System.Drawing.Size(117, 23);
            this.lblFechaCreacion.TabIndex = 83;
            this.lblFechaCreacion.Text = "Fecha de Creación";
            // 
            // ssMoneda
            // 
            this.ssMoneda.BackColor = System.Drawing.Color.Transparent;
            this.ssMoneda.Location = new System.Drawing.Point(447, 58);
            this.ssMoneda.Name = "ssMoneda";
            this.ssMoneda.Size = new System.Drawing.Size(119, 28);
            this.ssMoneda.TabIndex = 84;
            this.ssMoneda.Search += new System.EventHandler(this.ssMoneda_Search);
            // 
            // ssCliente
            // 
            this.ssCliente.BackColor = System.Drawing.Color.Transparent;
            this.ssCliente.Location = new System.Drawing.Point(117, 59);
            this.ssCliente.Name = "ssCliente";
            this.ssCliente.Size = new System.Drawing.Size(192, 28);
            this.ssCliente.TabIndex = 83;
            this.ssCliente.Search += new System.EventHandler(this.ssCliente_Search);
            // 
            // lblMoneda
            // 
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.lblMoneda.Appearance = appearance7;
            this.lblMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblMoneda.Location = new System.Drawing.Point(324, 62);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(130, 23);
            this.lblMoneda.TabIndex = 83;
            this.lblMoneda.Text = "Moneda";
            // 
            // ssTipoDocumento
            // 
            this.ssTipoDocumento.BackColor = System.Drawing.Color.Transparent;
            this.ssTipoDocumento.Location = new System.Drawing.Point(117, 32);
            this.ssTipoDocumento.Name = "ssTipoDocumento";
            this.ssTipoDocumento.Size = new System.Drawing.Size(192, 28);
            this.ssTipoDocumento.TabIndex = 83;
            this.ssTipoDocumento.Search += new System.EventHandler(this.ssTipoDocumento_Search);
            // 
            // lblCliente
            // 
            appearance8.BackColor = System.Drawing.Color.Transparent;
            this.lblCliente.Appearance = appearance8;
            this.lblCliente.Location = new System.Drawing.Point(10, 62);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(130, 23);
            this.lblCliente.TabIndex = 84;
            this.lblCliente.Text = "Cliente";
            // 
            // lblTipoDocumento
            // 
            appearance9.BackColor = System.Drawing.Color.Transparent;
            this.lblTipoDocumento.Appearance = appearance9;
            this.lblTipoDocumento.Location = new System.Drawing.Point(10, 35);
            this.lblTipoDocumento.Name = "lblTipoDocumento";
            this.lblTipoDocumento.Size = new System.Drawing.Size(130, 23);
            this.lblTipoDocumento.TabIndex = 84;
            this.lblTipoDocumento.Text = "Tipo de Documento";
            // 
            // ubGenerarSolicitudCotizacion
            // 
            this.ubGenerarSolicitudCotizacion.Location = new System.Drawing.Point(18, 172);
            this.ubGenerarSolicitudCotizacion.Name = "ubGenerarSolicitudCotizacion";
            this.ubGenerarSolicitudCotizacion.Size = new System.Drawing.Size(580, 23);
            this.ubGenerarSolicitudCotizacion.TabIndex = 83;
            this.ubGenerarSolicitudCotizacion.Text = "Generar Solicitud Cotización";
            this.ubGenerarSolicitudCotizacion.Click += new System.EventHandler(this.ubGenerarSolicitudCotizacion_Click);
            // 
            // lblContacto
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.lblContacto.Appearance = appearance3;
            this.lblContacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblContacto.Location = new System.Drawing.Point(10, 118);
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.Size = new System.Drawing.Size(130, 23);
            this.lblContacto.TabIndex = 89;
            this.lblContacto.Text = "Contacto";
            // 
            // lblFormaPago
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.lblFormaPago.Appearance = appearance2;
            this.lblFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblFormaPago.Location = new System.Drawing.Point(324, 118);
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.Size = new System.Drawing.Size(130, 23);
            this.lblFormaPago.TabIndex = 90;
            this.lblFormaPago.Text = "Forma de Pago";
            // 
            // ssContacto
            // 
            this.ssContacto.BackColor = System.Drawing.Color.Transparent;
            this.ssContacto.Location = new System.Drawing.Point(117, 115);
            this.ssContacto.Name = "ssContacto";
            this.ssContacto.Size = new System.Drawing.Size(192, 28);
            this.ssContacto.TabIndex = 91;
            this.ssContacto.Search += new System.EventHandler(this.ssContacto_Search);
            // 
            // ssFormaPago
            // 
            this.ssFormaPago.BackColor = System.Drawing.Color.Transparent;
            this.ssFormaPago.Location = new System.Drawing.Point(447, 115);
            this.ssFormaPago.Name = "ssFormaPago";
            this.ssFormaPago.Size = new System.Drawing.Size(119, 28);
            this.ssFormaPago.TabIndex = 92;
            this.ssFormaPago.Search += new System.EventHandler(this.ssFormaPago_Search);
            // 
            // FrmAsistenteSolicitudCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 245);
            this.Name = "FrmAsistenteSolicitudCotizacion";
            this.Text = "Asistente para la  Creación de Solicitudes de Cotización";
            ((System.ComponentModel.ISupportInitialize)(this.ugbParent)).EndInit();
            this.ugbParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ugbGeneral)).EndInit();
            this.ugbGeneral.ResumeLayout(false);
            this.ugbGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigoGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udtFechaCreacion)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ugbGeneral;
        private Controls.SoftSearch ssTipoDocumento;
        private Infragistics.Win.Misc.UltraLabel lblTipoDocumento;
        private Controls.SoftSearch ssCliente;
        private Infragistics.Win.Misc.UltraLabel lblCliente;
        private Controls.SoftSearch ssMoneda;
        private Infragistics.Win.Misc.UltraLabel lblMoneda;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor udtFechaCreacion;
        private Infragistics.Win.Misc.UltraLabel lblFechaCreacion;
        private Controls.SoftSearch ssPlantilla;
        private Infragistics.Win.Misc.UltraLabel lblPlantilla;
        private Infragistics.Win.Misc.UltraButton ubGenerarSolicitudCotizacion;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtCodigoGrupo;
        private Infragistics.Win.Misc.UltraLabel lblCodigoGrupo;
        private Controls.SoftSearch ssFormaPago;
        private Controls.SoftSearch ssContacto;
        private Infragistics.Win.Misc.UltraLabel lblFormaPago;
        private Infragistics.Win.Misc.UltraLabel lblContacto;
    }
}