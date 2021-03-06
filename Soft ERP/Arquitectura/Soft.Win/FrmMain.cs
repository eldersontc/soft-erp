﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Soft.Win;
using Soft.Configuracion.Entidades;
using Infragistics.Win.UltraWinExplorerBar;
using Infragistics.Win.UltraWinTree;
using Microsoft.SqlServer.MessageBox;
using Infragistics.Win.UltraWinToolbars;
using Soft.Entities;
using Microsoft.VisualBasic;
using Infragistics.Win;
using Soft.DataAccess;
using Soft.Seguridad.Entidades;
using System.Xml;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using Soft.Exceptions;
using System.Configuration;
using Infragistics.Win.Misc;

namespace Soft.Win
{
    public partial class FrmMain : Form
    {

        private static Contenedor m_Contenedor;
        private static IList<ItemContenedorAccion> m_Acciones;
        private static IList<Alerta> m_Alertas;
        private static ItemContenedor m_ItemContenedor;
        private static Accion m_AccionActual;
        private static Usuario m_Usuario;
        
        private static Form m_ActiveForm;
        private static Form m_Main;

        public FrmMain()
        {
            InitializeComponent();
            
        }

        public static Usuario Usuario { get { return m_Usuario; } }
        
        public void IniciarAplicacion(Usuario Usuario) {
            m_Usuario = Usuario;
            m_Main = this;
            PersonalizarControles();
            ConstruirContenedores();
            ConstruirTab();
            //IniciarAlertas();
            Show();
        }
        /*
        public void IniciarAlertas() 
        {
            try
            {
                ItemContenedor itemContenedor = m_Contenedor.Items.Where(i => i.Nombre.Equals("Alertas")).First();
                itemContenedor.Filtro = string.Format(" WHERE IDUsuario = '{0}'", Usuario.ID);
            }
            catch (Exception)
            {
                SoftException.Control(new Exception("El Panel de Alerta no existe."));
            }

            m_Alertas = HelperNHibernate.GetObjects<Alerta>(new List<object[]>() { new object[] { TypeEnum.CEnumCondition.LIKE, "IDUsuario", Usuario.ID } });
            IList<object> lista = 
                HelperNHibernate.GetNObjects(string.Format(
                                             @" SELECT 
                                                    COUNT(Entidad + '(S) PENDIENTE(S) POR ' + Estado), Entidad + '(S) PENDIENTE(S) POR ' + Estado 
                                                FROM 
                                                    Alerta
                                                WHERE 
                                                    IDUsuario = '{0}' 
                                                GROUP BY 
            
                                        Entidad + '(S) PENDIENTE(S) POR ' + Estado", Usuario.ID));

            UltraDesktopAlert alert = new UltraDesktopAlert();
            alert.MultipleWindowDisplayStyle = MultipleWindowDisplayStyle.Tiled;
            alert.DesktopAlertLinkClicked += OnDesktopAlertLinkClicked;

            foreach (object[] item in lista)
            {
                UltraDesktopAlertShowWindowInfo info;
                info = new UltraDesktopAlertShowWindowInfo("ALERTA!", string.Format("TIENES {0} {1}", item[0], item[1]));
                alert.Show(info);
            }
            utAlert.Start();
        }
        */
        public void PersonalizarControles()
        {
            Infragistics.Shared.ResourceCustomizer rc = Infragistics.Win.UltraWinExplorerBar.Resources.Customizer;
            rc.SetCustomizedString("NavigationQuickCustomizeMenu_ShowMoreButtons", "Mostrar más botones");
            rc.SetCustomizedString("NavigationQuickCustomizeMenu_ShowFewerButtons", "Mostrar menos botones");
            rc.SetCustomizedString("NavigationQuickCustomizeMenu_NavigationPaneOptions", "Opciones del panel de navegación");
            rc.SetCustomizedString("NavigationQuickCustomizeMenu_AddOrRemoveButtons", "Agregar o quitar botones");
            rc = Infragistics.Win.UltraWinToolbars.Resources.Customizer;
            rc.SetCustomizedString("MinimizeRibbon", "Minimizar Panel");
            rc.SetCustomizedString("QuickAccessToolbarAboveRibbonCustomize", "Barra de herramientas de acceso rápido encima de la cinta Personalizar");
            rc.SetCustomizedString("QuickAccessToolbarBelowRibbonCustomize", "Barra de herramientas de acceso rápido debajo de la cinta Personalizar");
            rc = Infragistics.Win.UltraWinTabbedMdi.Resources.Customizer;
            rc.SetCustomizedString("MenuItemClose", "Cerrar");
            rc.SetCustomizedString("MenuItemNewHorizontalGroup", "Nuevo grupo de perstañas horizontal");
            rc.SetCustomizedString("MenuItemNewVerticalGroup", "Nuevo grupo de perstañas vertical");
            rc.SetCustomizedString("MenuItemMoveToNextGroup", "Mover al grupo de fichas siguiente");
            rc.SetCustomizedString("MenuItemMoveToPreviousGroup", "Mover al grupo de fichas anterior");

            Text = String.Format(":: {0} ::",m_Usuario.Empresa.RazonSocial);
            usbMain.Panels[0].Text = String.Format("USUARIO ACTUAL : {0}", m_Usuario.NombreUsuario);
        }

        public void ObtenerContenedor() {
            XmlDocument XML = HelperNHibernate.ExecuteView("vSF_ContenedorxPerfil", String.Format(" IDPerfil = '{0}'", m_Usuario.Perfil.ID));
            if (XML.HasChildNodes) {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    m_Contenedor = (Contenedor)HelperNHibernate.GetEntityByID("Contenedor", NodoItem.SelectSingleNode("@IDContenedor").Value);    
                }
            }
        }

        public void ConstruirContenedores()
        {
            ObtenerContenedor();
            if (m_Contenedor != null)
            {
                uebMain.Groups.Clear();
                foreach (ItemContenedor Item in m_Contenedor.Items)
                {
                    if (Item.EsContenedor)
                    {
                        UltraExplorerBarGroup Group = new UltraExplorerBarGroup();
                        UltraExplorerBarContainerControl Container = new UltraExplorerBarContainerControl();
                        UltraTree Tree = new UltraTree();
                        uebMain.Controls.Add(Container);
                        Tree.Dock = DockStyle.Fill;
                        Tree.NodeConnectorStyle = NodeConnectorStyle.None;
                        Tree.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.Standard;
                        Tree.DisplayStyle = UltraTreeDisplayStyle.WindowsVista;
                        Tree.Override.ActiveNodeAppearance.FontData.Bold = DefaultableBoolean.True;
                        Tree.AfterSelect += Tree_AfterSelect;
                        Tree.Click += Tree_Click;
                        Container.Controls.Add(Tree);
                        Group.Text = Item.Nombre;
                        Group.Settings.NavigationPaneCollapsedGroupAreaText = Item.Nombre;
                        Group.Settings.AppearancesLarge.HeaderAppearance.Image = ilMain.Images[Item.Imagen];
                        Group.Tag = Item;
                        Group.Container = Container;
                        Group.Settings.Style = GroupStyle.ControlContainer;
                        uebMain.Groups.Add(Group);
                        AddNodesToTree(Tree.Nodes, Item);
                        Tree.ExpandAll();
                    }
                }
            }
        }

        public void MostrarPanel()
        {
            try
            {
                m_ActiveForm = MdiChildren.First(a => a.Text.Equals(String.Format(":: {0} ::", m_ItemContenedor.Nombre)));
                m_ActiveForm.Activate();
            }
            catch (Exception)
            {
                m_ActiveForm = new FrmDetails(m_Main, m_ItemContenedor);
            }
        }

        public static void MostrarReporte(String NameReport,ReportDocument Report) {
            try
            {
                m_ActiveForm = new FrmReportView(NameReport,Report, m_Main);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConstruirTab()
        {
            RibbonTab Tab = new RibbonTab("Inicio", "Inicio");
            RibbonGroup Group = new RibbonGroup("Estandar", "Opciones Estándar");
            
            ButtonTool Tool = new ButtonTool("Nuevo");
            Tool.SharedProps.Caption = "Nuevo";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["add_page.png"];
            Tool.SharedProps.Shortcut = Shortcut.CtrlN;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("Modificar");
            Tool.SharedProps.Caption = "Modificar";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["edit_page.png"];
            Tool.SharedProps.Shortcut = Shortcut.CtrlM;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("Eliminar");
            Tool.SharedProps.Caption = "Eliminar";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["delete_page.png"];
            Tool.SharedProps.Shortcut = Shortcut.CtrlD;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("Copiar");
            Tool.SharedProps.Caption = "Copiar";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["attachment.png"];
            Tool.SharedProps.Shortcut = Shortcut.CtrlC;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("Auditar");
            Tool.SharedProps.Caption = "Auditar";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["search.png"];
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("Actualizar");
            Tool.SharedProps.Caption = "Actualizar";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["refresh.png"];
            Tool.SharedProps.Shortcut = Shortcut.F5;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("ImportarXML");
            Tool.SharedProps.Caption = "Importar XML";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["orange_arrow_down.png"];
            //Tool.SharedProps.Shortcut = Shortcut.F5;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tool = new ButtonTool("ExportarXML");
            Tool.SharedProps.Caption = "Exportar XML";
            Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images["orange_arrow_up.png"];
            //Tool.SharedProps.Shortcut = Shortcut.F5;
            Tool.SharedProps.Enabled = false;
            Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });

            Tab.Groups.Add(Group);
            utbmMain.Ribbon.Tabs.Add(Tab);
        }

        public void ConstruirGrupoPersonalizado()
        {
            RibbonTab Tab = utbmMain.Ribbon.Tabs["Inicio"];
            if (m_Acciones.Count > 0)
            {
                RibbonGroup Group = null;
                if (Tab.Groups.Exists("Personalizadas")) { Tab.Groups.Remove("Personalizadas"); }
                Group = new RibbonGroup("Personalizadas", "Opciones Personalizadas"); 
                foreach (ItemContenedorAccion Item in m_Acciones)
                {
                    ButtonTool Tool = new ButtonTool(Item.ID);
                    Tool.SharedProps.Caption = Item.Accion.Descripcion;
                    Tool.InstanceProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
                    Tool.SharedProps.AppearancesLarge.Appearance.Image = ilMain.Images[Item.Accion.Imagen];
                    Group.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] { Tool });
                }
                Tab.Groups.Add(Group);
            }
            else { if (Tab.Groups.Exists("Personalizadas")) { Tab.Groups.Remove("Personalizadas"); } }
        }

        public void HabilitarOpcionesEstandar()
        {
            RibbonTab Tab = utbmMain.Ribbon.Tabs["Inicio"];
            ButtonTool ToolBase = null;
            if (m_ItemContenedor.Crear || m_ItemContenedor.AccionCrear != null)
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Nuevo"];
                ToolBase.SharedProps.Enabled = true;
            }
            else 
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Nuevo"];
                ToolBase.SharedProps.Enabled = false;  
            }
            if (m_ItemContenedor.Modificar || m_ItemContenedor.AccionModificar != null)
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Modificar"];
                ToolBase.SharedProps.Enabled = true;
            }
            else 
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Modificar"];
                ToolBase.SharedProps.Enabled = false;
            }
            if (m_ItemContenedor.Eliminar || m_ItemContenedor.AccionEliminar != null)
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Eliminar"];
                ToolBase.SharedProps.Enabled = true;
            }
            else 
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Eliminar"];
                ToolBase.SharedProps.Enabled = false;  
            }
            if (m_ItemContenedor.Copiar)
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Copiar"];
                ToolBase.SharedProps.Enabled = true;
            }
            else 
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Copiar"];
                ToolBase.SharedProps.Enabled = false;
            }
            if (m_ItemContenedor.Auditar)
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Auditar"];
                ToolBase.SharedProps.Enabled = true;
            }
            else
            {
                ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Auditar"];
                ToolBase.SharedProps.Enabled = false;
            }
            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Actualizar"];
            ToolBase.SharedProps.Enabled = true;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["ExportarXML"];
            ToolBase.SharedProps.Enabled = true;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["ImportarXML"];
            ToolBase.SharedProps.Enabled = true;
        }

        public void DeshabilitarOpcionesEstandar()
        {
            RibbonTab Tab = utbmMain.Ribbon.Tabs["Inicio"];

            ButtonTool ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Nuevo"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Modificar"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Eliminar"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Copiar"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Auditar"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["Actualizar"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["ExportarXML"];
            ToolBase.SharedProps.Enabled = false;

            ToolBase = (ButtonTool)Tab.Groups["Estandar"].Tools["ImportarXML"];
            ToolBase.SharedProps.Enabled = false;
        }

        public void AddNodesToTree(TreeNodesCollection Nodes, ItemContenedor ItemPadre)
        {
            foreach (ItemContenedor Item in m_Contenedor.GetItemsByItemParent(ItemPadre.ID))
            {
                UltraTreeNode Node = new UltraTreeNode(Item.ID, Item.Nombre);
                Image Imagen = ilMain.Images[Item.Imagen];
                Node.Tag = Item;
                if (Imagen != null) { Node.LeftImages.Add(Imagen); };
                Nodes.Add(Node);
                AddNodesToTree(Node.Nodes, Item);
            }
        }

        public static void IniciarFlujo()
        {
            if (m_AccionActual != null)
            {
                ItemAccion ItemAccion = m_AccionActual.Items[0];
                ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(ItemAccion.Ensamblado.Ensamblado_, ItemAccion.Clase);
                if (m_AccionActual.FilaSeleccionada) { Controlador.m_ItemsSelecteds = ((FrmDetails)m_ActiveForm).GetIDs(); }
                Controlador.m_Parameter = ItemAccion.Parametro;
                Controlador.m_AccionActual = m_AccionActual;
                Controlador.m_ItemAccionAcual = ItemAccion;
                Controlador.m_ItemContenedor = m_ItemContenedor;
                Controlador.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF",m_ItemContenedor.Panel.EntidadSF.ID);
                Controlador.Start();
            }
        }

        public static void RefreshView() {
            ((FrmDetails)m_ActiveForm).RefreshView();
        }

        public static void CrearPersonalizado(Object Object,Accion Accion) {
            ItemAccion Item = Accion.ItemByName("Crear");
            if(Item != null){
                ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(Item.Ensamblado.Ensamblado_, Item.Clase);
                Controlador.m_ObjectFlow = Object;
                Controlador.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", m_ItemContenedor.Panel.EntidadSF.ID);
                Controlador.Start();
                RefreshView();     
            }
        }

        public static void CrearEstandar(Object Object)
        {
            Accion Accion = (Accion)HelperNHibernate.GetEntityByID("Accion", "7E35D25A-E84A-4689-8297-9E392D0EC187");
            ItemAccion Item = Accion.ItemByName("Crear");
            ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(Item.Ensamblado.Ensamblado_, Item.Clase);
            Controlador.m_ObjectFlow = Object;
            Controlador.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", m_ItemContenedor.Panel.EntidadSF.ID);
            Controlador.Start();
            RefreshView();
        }

        public static void ModificarPersonalizado(Object Object, Accion Accion)
        {
            ItemAccion Item = Accion.ItemByName("Modificar");
            if (Item != null) {
                ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(Item.Ensamblado.Ensamblado_, Item.Clase);
                Controlador.m_ObjectFlow = Object;
                Controlador.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", m_ItemContenedor.Panel.EntidadSF.ID);
                Controlador.Start();
                RefreshView();    
            }
        }

        public static void ModificarEstandar(Object Object)
        {
            Accion Accion = (Accion)HelperNHibernate.GetEntityByID("Accion", "93C47EBE-51D9-4509-9CCE-76D2540B15FB");
            ItemAccion Item = Accion.ItemByName("Modificar");
            ControllerApp Controlador = (ControllerApp)Factory.InstanceObject(Item.Ensamblado.Ensamblado_, Item.Clase);
            Controlador.m_ObjectFlow = Object;
            Controlador.m_EntidadSF = (EntidadSF)HelperNHibernate.GetEntityByID("EntidadSF", m_ItemContenedor.Panel.EntidadSF.ID);
            Controlador.Start();
            RefreshView();
        }

        public void CrearEntidad() {
            if (m_ItemContenedor.Crear)
            {
                m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "7E35D25A-E84A-4689-8297-9E392D0EC187");
                m_AccionActual.AsignarEnsamblado(m_ItemContenedor.Panel.EntidadSF, "Formulario");
            }
            else {
                m_AccionActual = m_ItemContenedor.AccionCrear;
            }
            IniciarFlujo();
        }

        public static void ModificarEntidad() {
            if (m_ItemContenedor.Modificar) {
                m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "93C47EBE-51D9-4509-9CCE-76D2540B15FB");
                m_AccionActual.AsignarEnsamblado(m_ItemContenedor.Panel.EntidadSF, "Formulario");
            }
            else {
                m_AccionActual = m_ItemContenedor.AccionModificar;
            }
            IniciarFlujo();    
        }

        public void EliminarEntidad() {
            if (m_ItemContenedor.Eliminar)
            {
                m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "D820B486-63F5-40AA-8D41-0AD1E95080EF");
            }
            else {
                m_AccionActual = m_ItemContenedor.AccionEliminar;
            }
            IniciarFlujo();
        }

        public void CopiarEntidad() {
            if (m_ItemContenedor.Copiar)
            {
                m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "D8DCF2FE-D69F-4008-803B-64169D326887");
                m_AccionActual.AsignarEnsamblado(m_ItemContenedor.Panel.EntidadSF, "Formulario");
            }
            else {
                m_AccionActual = m_ItemContenedor.AccionCopiar;
            }
            IniciarFlujo();
        }

        public void AuditarEntidad() {
            m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "F2AA7339-35D6-44B0-AB96-04A97BF30F01");
            IniciarFlujo();
        }

        public void ImportarXML()
        {
            m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "58AA8CC5-16F2-490A-924A-4C52CCF35E58");
            IniciarFlujo();
        }

        public void ExportarXML()
        {
            m_AccionActual = (Accion)HelperNHibernate.GetEntityByID("Accion", "5F4621A7-3641-4BD6-8FB5-050FA13046CE");
            IniciarFlujo();
        }

        public Shortcut ObtenerShortCut(String Teclas)
        {
            Shortcut Abreviacion = Shortcut.None;
            switch (Teclas)
            {
                case "CTRL + A":
                    Abreviacion = Shortcut.CtrlA;
                    break;
                case "CTRL + B":
                    Abreviacion = Shortcut.CtrlB;
                    break;
                case "CTRL + C":
                    Abreviacion = Shortcut.CtrlC;
                    break;
                case "CTRL + D":
                    Abreviacion = Shortcut.CtrlD;
                    break;
                case "CTRL + E":
                    Abreviacion = Shortcut.CtrlE;
                    break;
                case "CTRL + F":
                    Abreviacion = Shortcut.CtrlF;
                    break;
                case "CTRL + G":
                    Abreviacion = Shortcut.CtrlG;
                    break;
                case "CTRL + H":
                    Abreviacion = Shortcut.CtrlH;
                    break;
                case "CTRL + I":
                    Abreviacion = Shortcut.CtrlI;
                    break;
                case "CTRL + J":
                    Abreviacion = Shortcut.CtrlJ;
                    break;
                case "CTRL + K":
                    Abreviacion = Shortcut.CtrlK;
                    break;
                case "CTRL + L":
                    Abreviacion = Shortcut.CtrlL;
                    break;
                case "CTRL + M":
                    Abreviacion = Shortcut.CtrlM;
                    break;
                case "CTRL + N":
                    Abreviacion = Shortcut.CtrlN;
                    break;
                case "CTRL + O":
                    Abreviacion = Shortcut.CtrlO;
                    break;
                case "CTRL + P":
                    Abreviacion = Shortcut.CtrlP;
                    break;
                case "CTRL + Q":
                    Abreviacion = Shortcut.CtrlQ;
                    break;
                case "CTRL + R":
                    Abreviacion = Shortcut.CtrlR;
                    break;
                case "CTRL + S":
                    Abreviacion = Shortcut.CtrlS;
                    break;
                case "CTRL + T":
                    Abreviacion = Shortcut.CtrlT;
                    break;
                case "CTRL + U":
                    Abreviacion = Shortcut.CtrlU;
                    break;
                case "CTRL + V":
                    Abreviacion = Shortcut.CtrlV;
                    break;
                case "CTRL + W":
                    Abreviacion = Shortcut.CtrlW;
                    break;
                case "CTRL + X":
                    Abreviacion = Shortcut.CtrlX;
                    break;
                case "CTRL + Y":
                    Abreviacion = Shortcut.CtrlY;
                    break;
                case "CTRL + Z":
                    Abreviacion = Shortcut.CtrlZ;
                    break;
                default:
                    break;
            }
            return Abreviacion;
        }

        public static SocioNegocio ObtenerResponsable()
        {
            XmlDocument XML = HelperNHibernate.ExecuteSQL("SELECT TOP (1) IDSocioNegocio FROM SocioNegocioEmpleado ", String.Format(" IDUsuario = '{0}'", FrmMain.Usuario.ID));
            SocioNegocio Responsable = null;
            if (XML.HasChildNodes)
            {
                foreach (XmlNode NodoItem in XML.DocumentElement.ChildNodes)
                {
                    Responsable = (SocioNegocio)HelperNHibernate.GetEntityByID("SocioNegocio", NodoItem.SelectSingleNode("@IDSocioNegocio").Value);
                }
            }
            return Responsable;
        }

        public static string CarpetaReportes { get { return ConfigurationManager.AppSettings["CarpetaReportes"]; } }

        public static string CarpetaImagenes { get { return ConfigurationManager.AppSettings["CarpetaImagenes"]; } }

        public static string CarpetaExportacion { get { return ConfigurationManager.AppSettings["CarpetaExportacion"]; } }

        public static string ObtenerValorKey(string key) {
            return ConfigurationManager.AppSettings[key];
        }

        public void Tree_Click(object sender, EventArgs e)
        {
            try
            {
                ItemContenedor Item = (ItemContenedor)((UltraTree)sender).ActiveNode.Tag;
                if (Item != null)
                {
                    if (Item.EsPanel)
                    {
                        m_ItemContenedor = Item;
                        m_Acciones = Item.Acciones;
                        MostrarPanel();
                    }
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());
            }
        }

        public void Tree_AfterSelect(Object sender, Infragistics.Win.UltraWinTree.SelectEventArgs e)
        {
            try
            {
                ItemContenedor Item = (ItemContenedor)((UltraTree)sender).ActiveNode.Tag;
                if (Item != null)
                {
                    if (Item.EsPanel)
                    {
                        m_ItemContenedor = Item;
                        m_Acciones = Item.Acciones;
                        MostrarPanel();
                    }
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());  
            }
        }

        public void HabilitarOpciones() {
            HabilitarOpcionesEstandar();
            ConstruirGrupoPersonalizado();
        }

        public void DeshabilitarOpciones() {
            m_Acciones = new List<ItemContenedorAccion>();
            DeshabilitarOpcionesEstandar();
            ConstruirGrupoPersonalizado();
        }

        private void utbmMain_ToolClick(object sender, ToolClickEventArgs e)
        {
            try
            {
                switch (e.Tool.Key)
                {
                    case "Nuevo":
                        CrearEntidad();
                        break;
                    case "Modificar":
                        ModificarEntidad();
                        break;
                    case "Eliminar":
                        EliminarEntidad();
                        break;
                    case "Copiar":
                        CopiarEntidad();
                        break;
                    case "Auditar":
                        AuditarEntidad();
                        break;
                    case "Actualizar":
                        RefreshView();
                        break;
                    case "ImportarXML":
                        ImportarXML();
                        break;
                    case "ExportarXML":
                        ExportarXML();
                        break;
                    default:
                        m_AccionActual = ((ItemContenedorAccion)m_Acciones.First(a => a.ID == e.Tool.Key)).Accion;
                        IniciarFlujo();
                        break;
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());   
            }
            
        }

        private void utbmMain_BeforeToolbarListDropdown(object sender, BeforeToolbarListDropdownEventArgs e)
        {
            e.Cancel = true;
        }

        private void utmmMain_TabClosed(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {
            try
            {
                if (MdiChildren.Length == 0)
                {
                    RibbonTab Tab = utbmMain.Ribbon.Tabs["Inicio"];
                    if (Tab.Groups.Exists("Personalizadas")) { Tab.Groups.Remove("Personalizadas"); }
                    DeshabilitarOpcionesEstandar();
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void utmmMain_TabActivated(object sender, Infragistics.Win.UltraWinTabbedMdi.MdiTabEventArgs e)
        {
            try
            {
                m_ItemContenedor = (ItemContenedor)e.Tab.Form.Tag;
                m_ActiveForm = (Form)e.Tab.Form;
                if (m_ItemContenedor != null)
                {
                    m_Acciones = m_ItemContenedor.Acciones;
                    HabilitarOpciones();
                }
                else
                {
                    DeshabilitarOpciones();
                }
            }
            catch (Exception ex)
            {
                SoftException.Control(ex, SystemIcons.Warning.ToBitmap());
            }
        }
        /*
        private string ObtenerFiltroSQLAlertas(IList<Alerta> alertas) 
        {
            string filtro = string.Empty;
            if (alertas.Count > 0)
            {
                filtro = "AND ID IN (";
                foreach (var item in alertas)
                {
                    filtro += string.Format("'{0}',", item.ID);
                }
                filtro += ")";
                filtro = filtro.Replace(",)", ")");
            }
            return filtro;
        }

        private bool EsNuevaAlerta(string ID) 
        {
            foreach (var item in m_Alertas)
                if (item.ID == ID)
                    return false;
            return true;
        }

        public IList<Alerta> ObtenerAlertasNuevas(IList<Alerta> alertas) 
        {
            IList<Alerta> alertasNuevas = new List<Alerta>();
            foreach (var item in alertas)
                if (EsNuevaAlerta(item.ID))
                    alertasNuevas.Add(item);
            return alertasNuevas;
        }

        private void utAlert_Tick(object sender, EventArgs e)
        {
            IList<Alerta> alertas = HelperNHibernate.GetObjects<Alerta>(new List<object[]>() { new object[] { TypeEnum.CEnumCondition.LIKE, "IDUsuario", Usuario.ID } });
            IList<Alerta> alertasNuevas = ObtenerAlertasNuevas(alertas);
            if (alertasNuevas.Count > 0) 
            {
                IList<object> lista =
                HelperNHibernate.GetNObjects(string.Format(
                                                @"  SELECT 
                                                        COUNT(Entidad + '(S) PENDIENTE(S) POR ' + Estado), Entidad + '(S) PENDIENTE(S) POR ' + Estado 
                                                    FROM 
                                                        Alerta
                                                    WHERE 
                                                        IDUsuario = '{0}' {1}
                                                    GROUP BY 
                                                        Entidad + '(S) PENDIENTE(S) POR ' + Estado ", Usuario.ID, ObtenerFiltroSQLAlertas(alertasNuevas)));
                //.
                UltraDesktopAlert alert = new UltraDesktopAlert();
                alert.MultipleWindowDisplayStyle = MultipleWindowDisplayStyle.Tiled;
                alert.DesktopAlertLinkClicked += OnDesktopAlertLinkClicked;
                foreach (object[] item in lista)
                {
                    UltraDesktopAlertShowWindowInfo info;
                    info = new UltraDesktopAlertShowWindowInfo("ALERTA!", string.Format("TIENES {0} {1}", item[0], item[1]));
                    alert.Show(info);
                }
            }
            m_Alertas = alertas;
        }

        private void OnDesktopAlertLinkClicked(object sender, DesktopAlertLinkClickedEventArgs e)
        {
            try
            {
                m_ItemContenedor = m_Contenedor.Items.Where(i => i.Nombre.Equals("Alertas")).First();
                m_ActiveForm = new FrmDetails(m_Main, m_ItemContenedor);
            }
            catch (Exception)
            {
                SoftException.Control(new Exception("El Panel de Alerta no existe."));
            }
        }
        */
    }
}
