using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.Utils.Animation;
using System.Drawing;
using System.Windows.Forms;

namespace BursOtomasyon.Desktop.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Transition transition1 = new Transition();
            SlideFadeTransition slideFadeTransition1 = new SlideFadeTransition();
            modulesContainer = new XtraUserControl();
            ribbonControl = new RibbonControl();
            barNavigationItem = new BarSubItem();
            biFolderPaneSubItem = new BarSubItem();
            bmiFolderNormal = new BarCheckItem();
            bmiFolderMinimized = new BarCheckItem();
            bmiFolderOff = new BarCheckItem();
            skinDropDownButtonItem1 = new SkinDropDownButtonItem();
            skinPaletteRibbonGalleryBarItem1 = new SkinPaletteRibbonGalleryBarItem();
            ribbonPageHome = new RibbonPage();
            ribbonPageGroupHome = new RibbonPageGroup();
            ribbonPageView = new RibbonPage();
            ribbonPageGroupModule = new RibbonPageGroup();
            ribbonPageGroupLayout = new RibbonPageGroup();
            rpgAppearance = new RibbonPageGroup();
            ribbonStatusBar1 = new RibbonStatusBar();
            dockManager = new DockManager(components);
            transitionManager = new TransitionManager(components);
            accordionControl1 = new AccordionControl();
            accordionControlElement1 = new AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)ribbonControl).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dockManager).BeginInit();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).BeginInit();
            SuspendLayout();
            // 
            // modulesContainer
            // 
            modulesContainer.Dock = DockStyle.Fill;
            modulesContainer.Location = new Point(62, 108);
            modulesContainer.Name = "modulesContainer";
            modulesContainer.Size = new Size(708, 562);
            modulesContainer.TabIndex = 2;
            // 
            // ribbonControl
            // 
            ribbonControl.CommandLayout = CommandLayout.Simplified;
            ribbonControl.ExpandCollapseItem.Id = 0;
            ribbonControl.Items.AddRange(new BarItem[] { ribbonControl.ExpandCollapseItem, barNavigationItem, biFolderPaneSubItem, bmiFolderNormal, bmiFolderMinimized, bmiFolderOff, skinDropDownButtonItem1, skinPaletteRibbonGalleryBarItem1 });
            ribbonControl.Location = new Point(0, 0);
            ribbonControl.MaxItemId = 10;
            ribbonControl.Name = "ribbonControl";
            ribbonControl.Pages.AddRange(new RibbonPage[] { ribbonPageHome, ribbonPageView });
            ribbonControl.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl.ShowToolbarCustomizeItem = false;
            ribbonControl.Size = new Size(770, 108);
            ribbonControl.StatusBar = ribbonStatusBar1;
            ribbonControl.Toolbar.ShowCustomizeItem = false;
            ribbonControl.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // barNavigationItem
            // 
            barNavigationItem.Caption = "Navigasyon";
            barNavigationItem.Id = 2;
            barNavigationItem.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            barNavigationItem.ImageOptions.ImageUri.Uri = "Navigation;Size32x32";
            barNavigationItem.Name = "barNavigationItem";
            barNavigationItem.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
            // 
            // biFolderPaneSubItem
            // 
            biFolderPaneSubItem.Caption = "Panel Görünümü";
            biFolderPaneSubItem.Id = 10;
            biFolderPaneSubItem.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            biFolderPaneSubItem.ImageOptions.ImageUri.Uri = "ListBox;Size32x32";
            biFolderPaneSubItem.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(bmiFolderNormal), new LinkPersistInfo(bmiFolderMinimized), new LinkPersistInfo(bmiFolderOff) });
            biFolderPaneSubItem.Name = "biFolderPaneSubItem";
            biFolderPaneSubItem.ShowNavigationHeader = DevExpress.Utils.DefaultBoolean.True;
            // 
            // bmiFolderNormal
            // 
            bmiFolderNormal.Caption = "Normal";
            bmiFolderNormal.GroupIndex = 4;
            bmiFolderNormal.Id = 6;
            bmiFolderNormal.ImageOptions.ImageUri.Uri = "CustomizeGrid;Size16x16";
            bmiFolderNormal.Name = "bmiFolderNormal";
            // 
            // bmiFolderMinimized
            // 
            bmiFolderMinimized.Caption = "Küçültülmüş";
            bmiFolderMinimized.GroupIndex = 4;
            bmiFolderMinimized.Id = 7;
            bmiFolderMinimized.ImageOptions.ImageUri.Uri = "Decrease;Size16x16";
            bmiFolderMinimized.Name = "bmiFolderMinimized";
            // 
            // bmiFolderOff
            // 
            bmiFolderOff.Caption = "Kapalı";
            bmiFolderOff.GroupIndex = 4;
            bmiFolderOff.Id = 8;
            bmiFolderOff.ImageOptions.ImageUri.Uri = "Cancel;Size16x16";
            bmiFolderOff.Name = "bmiFolderOff";
            // 
            // skinDropDownButtonItem1
            // 
            skinDropDownButtonItem1.Id = 13;
            skinDropDownButtonItem1.Name = "skinDropDownButtonItem1";
            // 
            // skinPaletteRibbonGalleryBarItem1
            // 
            skinPaletteRibbonGalleryBarItem1.Caption = "Paletler";
            skinPaletteRibbonGalleryBarItem1.Id = 15;
            skinPaletteRibbonGalleryBarItem1.Name = "skinPaletteRibbonGalleryBarItem1";
            skinPaletteRibbonGalleryBarItem1.RememberLastCommand = true;
            // 
            // ribbonPageHome
            // 
            ribbonPageHome.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroupHome });
            ribbonPageHome.Name = "ribbonPageHome";
            ribbonPageHome.Text = "Ana Sayfa";
            // 
            // ribbonPageGroupHome
            // 
            ribbonPageGroupHome.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            ribbonPageGroupHome.Name = "ribbonPageGroupHome";
            ribbonPageGroupHome.Text = "İşlemler";
            // 
            // ribbonPageView
            // 
            ribbonPageView.Groups.AddRange(new RibbonPageGroup[] { ribbonPageGroupModule, ribbonPageGroupLayout, rpgAppearance });
            ribbonPageView.Name = "ribbonPageView";
            ribbonPageView.Text = "Görünüm";
            // 
            // ribbonPageGroupModule
            // 
            ribbonPageGroupModule.AllowTextClipping = false;
            ribbonPageGroupModule.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            ribbonPageGroupModule.ItemLinks.Add(barNavigationItem);
            ribbonPageGroupModule.MergeOrder = 0;
            ribbonPageGroupModule.Name = "ribbonPageGroupModule";
            ribbonPageGroupModule.Text = "Modül";
            // 
            // ribbonPageGroupLayout
            // 
            ribbonPageGroupLayout.AllowTextClipping = false;
            ribbonPageGroupLayout.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            ribbonPageGroupLayout.ItemLinks.Add(biFolderPaneSubItem);
            ribbonPageGroupLayout.MergeOrder = 1;
            ribbonPageGroupLayout.Name = "ribbonPageGroupLayout";
            ribbonPageGroupLayout.Text = "Düzen";
            // 
            // rpgAppearance
            // 
            rpgAppearance.AllowTextClipping = false;
            rpgAppearance.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            rpgAppearance.ItemLinks.Add(skinDropDownButtonItem1);
            rpgAppearance.ItemLinks.Add(skinPaletteRibbonGalleryBarItem1);
            rpgAppearance.MergeOrder = 2;
            rpgAppearance.Name = "rpgAppearance";
            rpgAppearance.Text = "Görünüm";
            // 
            // ribbonStatusBar1
            // 
            ribbonStatusBar1.Location = new Point(0, 670);
            ribbonStatusBar1.Name = "ribbonStatusBar1";
            ribbonStatusBar1.Ribbon = ribbonControl;
            ribbonStatusBar1.Size = new Size(770, 30);
            // 
            // dockManager
            // 
            dockManager.DockingOptions.FloatOnDblClick = false;
            dockManager.DockingOptions.ShowAutoHideButton = false;
            dockManager.DockingOptions.ShowMaximizeButton = false;
            dockManager.Form = modulesContainer;
            dockManager.MenuManager = ribbonControl;
            dockManager.TopZIndexControls.AddRange(new string[] { "DevExpress.XtraBars.BarDockControl", "DevExpress.XtraBars.StandaloneBarDockControl", "System.Windows.Forms.StatusBar", "System.Windows.Forms.MenuStrip", "System.Windows.Forms.StatusStrip", "DevExpress.XtraBars.Ribbon.RibbonStatusBar", "XtraBars.Navigation.OfficeNavigationBar", "DevExpress.XtraBars.Ribbon.RibbonControl" });
            // 
            // transitionManager
            // 
            transitionManager.ShowWaitingIndicator = false;
            transition1.Control = modulesContainer;
            slideFadeTransition1.Parameters.EffectOptions = PushEffectOptions.FromRight;
            slideFadeTransition1.Parameters.FrameInterval = 5000;
            transition1.TransitionType = slideFadeTransition1;
            transitionManager.Transitions.Add(transition1);
            // 
            // accordionControl1
            // 
            accordionControl1.AllowItemSelection = true;
            accordionControl1.Appearance.AccordionControl.Font = new Font("Segoe UI", 9F);
            accordionControl1.Appearance.AccordionControl.Options.UseFont = true;
            accordionControl1.Dock = DockStyle.Left;
            accordionControl1.Elements.AddRange(new AccordionControlElement[] { accordionControlElement1 });
            accordionControl1.Location = new Point(0, 40);
            accordionControl1.MinimumSize = new Size(0, 630);
            accordionControl1.Name = "accordionControl1";
            accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.True;
            accordionControl1.OptionsMinimizing.MinimizedWidth = 50;
            accordionControl1.OptionsMinimizing.NormalWidth = 200;
            accordionControl1.OptionsMinimizing.State = AccordionControlState.Minimized;
            accordionControl1.Size = new Size(62, 630);
            accordionControl1.TabIndex = 9;
            accordionControl1.ViewType = AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            accordionControlElement1.ImageOptions.ImageUri.Uri = "Add;Size32x32";
            accordionControlElement1.ImageOptions.SvgImageSize = new Size(32, 32);
            accordionControlElement1.Name = "accordionControlElement1";
            accordionControlElement1.Style = ElementStyle.Item;
            accordionControlElement1.Text = "Element1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 800);
            Controls.Add(modulesContainer);
            Controls.Add(accordionControl1);
            Controls.Add(ribbonStatusBar1);
            Controls.Add(ribbonControl);
            FormBorderEffect = FormBorderEffect.Shadow;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(900, 600);
            Name = "MainForm";
            NavigationControl = accordionControl1;
            NavigationControlLayoutMode = RibbonFormNavigationControlLayoutMode.StretchToFormTitle;
            Ribbon = ribbonControl;
            StartPosition = FormStartPosition.CenterScreen;
            StatusBar = ribbonStatusBar1;
            Text = "Öğrenci Burs Yönetim Sistemi";
            WindowState = FormWindowState.Normal;
            ((System.ComponentModel.ISupportInitialize)ribbonControl).EndInit();
            ((System.ComponentModel.ISupportInitialize)dockManager).EndInit();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RibbonControl ribbonControl;
        private RibbonPage ribbonPageView;
        private RibbonPageGroup rpgAppearance;
        private RibbonStatusBar ribbonStatusBar1;
        private RibbonPageGroup ribbonPageGroupModule;
        private RibbonPageGroup ribbonPageGroupLayout;
        private RibbonPage ribbonPageHome;
        private RibbonPageGroup ribbonPageGroupHome;
        private DockManager dockManager;
        private TransitionManager transitionManager;
        private XtraUserControl modulesContainer;
        private BarSubItem barNavigationItem;
        private BarSubItem biFolderPaneSubItem;
        private BarCheckItem bmiFolderNormal;
        private BarCheckItem bmiFolderMinimized;
        private BarCheckItem bmiFolderOff;
        private SkinDropDownButtonItem skinDropDownButtonItem1;
        private SkinPaletteRibbonGalleryBarItem skinPaletteRibbonGalleryBarItem1;
        private AccordionControl accordionControl1;
        private AccordionControlElement accordionControlElement1;
    }
}
