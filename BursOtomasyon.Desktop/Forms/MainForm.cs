using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using BursOtomasyon.Desktop.Services;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class MainForm : RibbonForm
    {
        private Form? currentModuleForm = null;
        private ModuleType selectedModuleType = ModuleType.Unknown;
        private readonly OgrenciService _ogrenciService;
        private readonly BasvuruService _basvuruService;
        private readonly BagisciService _bagisciService;

        // Dashboard verileri
        private List<Ogrenci> _ogrenciler = new();
        private List<Basvuru> _basvurular = new();

        public MainForm()
        {
            InitializeComponent();
            
            // Servisleri oluştur
            _ogrenciService = new OgrenciService();
            _basvuruService = new BasvuruService();
            _bagisciService = new BagisciService();
            
            // Ribbon ayarları
            ribbonControl.ApplicationButtonDropDownControl = null;
            ribbonControl.SelectedPage = ribbonPageHome;
            ribbonControl.SelectedPageChanging += RibbonControl_SelectedPageChanging;
            ribbonControl.MinimizedChanged += Ribbon_MinimizedChanged;
            ribbonControl.Manager.HideBarsWhenMerging = false;
            ribbonStatusBar1.HideWhenMerging = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl.ForceInitialize();
            
            // Ribbon ayarları
            Ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
            IconOptions.ShowIcon = false;
            
            // Navigasyon menüsünü oluştur
            RegisterNavigationMenuItems();
            RegisterAccordionControlElements();
            
            // Layout visibility binding
            BindFolderPaneVisibility();
            
            // Dashboard'u göster
            selectedModuleType = ModuleType.Dashboard;
            LoadDashboardData();
            ShowDashboard();
        }

        private void LoadDashboardData()
        {
            try
            {
                _ogrenciler = _ogrenciService.GetAll();
                _basvurular = _basvuruService.GetAll();
            }
            catch { }
        }

        private void ShowDashboard()
        {
            // Ana ScrollableControl
            var scrollPanel = new XtraScrollableControl();
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Appearance.BackColor = Color.FromArgb(245, 247, 250);
            scrollPanel.Appearance.Options.UseBackColor = true;

            // İçerik Panel - Dinamik genişlik
            var contentPanel = new PanelControl();
            contentPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            contentPanel.Appearance.BackColor = Color.FromArgb(245, 247, 250);
            contentPanel.Appearance.Options.UseBackColor = true;
            contentPanel.Location = new Point(0, 0);
            
            // Form genişliğine göre dinamik genişlik hesapla (padding dahil)
            int padding = 16;
            int contentWidth = Math.Max(600, modulesContainer.Width - padding * 2);
            contentPanel.Size = new Size(contentWidth, 750);
            contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Resize eventi ekle
            modulesContainer.Resize += (s, e) => {
                if (contentPanel != null && !contentPanel.IsDisposed)
                {
                    int newWidth = Math.Max(600, modulesContainer.Width - padding * 2);
                    contentPanel.Width = newWidth;
                    UpdateDashboardLayout(contentPanel);
                }
            };

            BuildDashboardContent(contentPanel, contentWidth, padding);

            scrollPanel.Controls.Add(contentPanel);
            
            modulesContainer.Controls.Clear();
            modulesContainer.Controls.Add(scrollPanel);
        }

        private void UpdateDashboardLayout(PanelControl contentPanel)
        {
            int padding = 16;
            int contentWidth = contentPanel.Width;
            int availableWidth = contentWidth - (padding * 2);

            // Tüm panelleri güncelle
            foreach (Control ctrl in contentPanel.Controls)
            {
                if (ctrl is PanelControl panel)
                {
                    // Header panel
                    if (panel.Controls.Count > 0 && panel.Controls[0] is LabelControl lbl && lbl.Text.Contains("Öğrenci Burs Yönetim Sistemi"))
                    {
                        panel.Width = availableWidth;
                    }
                    // Stats panel
                    else if (panel.Controls.Count >= 4 && panel.Controls[0] is PanelControl statCard)
                    {
                        panel.Width = availableWidth;
                        UpdateStatsPanelLayout(panel, availableWidth);
                    }
                    // Middle section
                    else if (panel.Controls.Count == 2)
                    {
                        panel.Width = availableWidth;
                        UpdateMiddleSectionLayout(panel, availableWidth);
                    }
                    // Bottom section
                    else if (panel.Controls.Count == 2 && panel.Location.Y > 300)
                    {
                        panel.Width = availableWidth;
                        UpdateBottomSectionLayout(panel, availableWidth);
                    }
                }
            }
        }

        private void BuildDashboardContent(PanelControl contentPanel, int contentWidth, int padding)
        {
            int availableWidth = contentWidth - (padding * 2);
            int yPos = padding;

            // 1. BAŞLIK PANELİ
            var headerPanel = CreateHeaderSection();
            headerPanel.Location = new Point(padding, yPos);
            headerPanel.Size = new Size(availableWidth, 65);
            contentPanel.Controls.Add(headerPanel);
            yPos += 72;

            // 2. İSTATİSTİK KARTLARI
            var statsPanel = CreateStatsSection(availableWidth);
            statsPanel.Location = new Point(padding, yPos);
            statsPanel.Size = new Size(availableWidth, 90);
            contentPanel.Controls.Add(statsPanel);
            yPos += 95;

            // 3. ORTA BÖLÜM - Sol: Son Olaylar, Sağ: Sistem Önerisi
            var middlePanel = CreateMiddleSection(availableWidth);
            middlePanel.Location = new Point(padding, yPos);
            middlePanel.Size = new Size(availableWidth, 175);
            contentPanel.Controls.Add(middlePanel);
            yPos += 180;

            // 4. ALT BÖLÜM - Sol: Grafik, Sağ: Riskli Başvurular
            var bottomPanel = CreateBottomSection(availableWidth);
            bottomPanel.Location = new Point(padding, yPos);
            bottomPanel.Size = new Size(availableWidth, 175);
            contentPanel.Controls.Add(bottomPanel);
        }

        #region Dashboard Sections

        private PanelControl CreateHeaderSection()
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            // İkon
            var iconLabel = new LabelControl();
            iconLabel.ImageOptions.ImageUri.Uri = "Business/Chart;Size32x32";
            iconLabel.ImageOptions.SvgImageSize = new Size(32, 32);
            iconLabel.Location = new Point(10, 15);
            iconLabel.Size = new Size(35, 35);
            iconLabel.AutoSizeMode = LabelAutoSizeMode.None;

            // Başlık
            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(50, 10);
            titleLabel.Text = "Öğrenci Burs Yönetim Sistemi";

            // Alt başlık
            var subtitleLabel = new LabelControl();
            subtitleLabel.Appearance.Font = new Font("Segoe UI", 9F);
            subtitleLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            subtitleLabel.Location = new Point(50, 35);
            subtitleLabel.Text = "Burs başvurularını yönetin • " + DateTime.Now.ToString("dd MMM yyyy");

            panel.Controls.Add(iconLabel);
            panel.Controls.Add(titleLabel);
            panel.Controls.Add(subtitleLabel);

            return panel;
        }

        private PanelControl CreateStatsSection(int availableWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            int toplamOgrenci = _ogrenciler.Count;
            int toplamBasvuru = _basvurular.Count;
            int onaylananBurs = _basvurular.Count(b => b.BasvuruDurumu == "Onaylandı");
            int incelemede = _basvurular.Count(b => b.BasvuruDurumu == "Beklemede" || b.BasvuruDurumu == "İnceleniyor");

            int spacing = 7;
            int cardWidth = Math.Max(135, (availableWidth - (spacing * 3)) / 4);

            var card1 = CreateStatCard("Toplam Öğrenci", toplamOgrenci.ToString(), Color.FromArgb(0, 123, 255), "People;Size32x32", cardWidth);
            card1.Location = new Point(0, 0);
            card1.Size = new Size(cardWidth, 82);

            var card2 = CreateStatCard("Toplam Başvuru", toplamBasvuru.ToString(), Color.FromArgb(40, 167, 69), "TaskList;Size32x32", cardWidth);
            card2.Location = new Point(cardWidth + spacing, 0);
            card2.Size = new Size(cardWidth, 82);

            var card3 = CreateStatCard("İncelemede", incelemede.ToString(), Color.FromArgb(255, 193, 7), "Time;Size32x32", cardWidth);
            card3.Location = new Point((cardWidth + spacing) * 2, 0);
            card3.Size = new Size(cardWidth, 82);

            var card4 = CreateStatCard("Onaylanan Burs", onaylananBurs.ToString(), Color.FromArgb(40, 167, 69), "Apply;Size32x32", cardWidth);
            card4.Location = new Point((cardWidth + spacing) * 3, 0);
            card4.Size = new Size(cardWidth, 82);

            panel.Controls.Add(card1);
            panel.Controls.Add(card2);
            panel.Controls.Add(card3);
            panel.Controls.Add(card4);

            return panel;
        }

        private void UpdateStatsPanelLayout(PanelControl panel, int availableWidth)
        {
            int spacing = 7;
            int cardWidth = Math.Max(135, (availableWidth - (spacing * 3)) / 4);

            for (int i = 0; i < panel.Controls.Count; i++)
            {
                if (panel.Controls[i] is PanelControl card)
                {
                    card.Width = cardWidth;
                    card.Location = new Point((cardWidth + spacing) * i, 0);
                    UpdateStatCardLayout(card, cardWidth);
                }
            }
        }

        private void UpdateStatCardLayout(PanelControl card, int cardWidth)
        {
            // Üst renk çizgisini güncelle
            if (card.Controls.Count > 0 && card.Controls[0] is PanelControl colorBar)
            {
                colorBar.Width = cardWidth;
            }
        }

        private PanelControl CreateStatCard(string title, string value, Color accentColor, string iconUri, int cardWidth)
        {
            var card = new PanelControl();
            card.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            card.Appearance.BackColor = Color.White;
            card.Appearance.Options.UseBackColor = true;
            card.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            card.Appearance.Options.UseBorderColor = true;

            // Üst renk çizgisi
            var colorBar = new PanelControl();
            colorBar.Location = new Point(0, 0);
            colorBar.Size = new Size(cardWidth, 3);
            colorBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            colorBar.Appearance.BackColor = accentColor;
            colorBar.Appearance.Options.UseBackColor = true;

            // İkon
            var iconLabel = new LabelControl();
            iconLabel.ImageOptions.ImageUri.Uri = iconUri;
            iconLabel.ImageOptions.SvgImageSize = new Size(20, 20);
            iconLabel.Location = new Point(8, 10);
            iconLabel.Size = new Size(22, 22);
            iconLabel.AutoSizeMode = LabelAutoSizeMode.None;

            // Değer
            var valueLabel = new LabelControl();
            valueLabel.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            valueLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            valueLabel.Location = new Point(8, 35);
            valueLabel.Text = value;

            // Başlık
            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 8F);
            titleLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            titleLabel.Location = new Point(8, 62);
            titleLabel.Text = title;

            card.Controls.Add(colorBar);
            card.Controls.Add(iconLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(titleLabel);

            // Hover efekti
            card.MouseEnter += (s, e) => card.Appearance.BackColor = Color.FromArgb(248, 249, 250);
            card.MouseLeave += (s, e) => card.Appearance.BackColor = Color.White;

            return card;
        }

        private PanelControl CreateMiddleSection(int availableWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            int spacing = 5;
            int panelWidth = (availableWidth - spacing) / 2;

            // Sol Panel - Son Olaylar
            var leftPanel = CreateRecentEventsPanel(panelWidth);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Size = new Size(panelWidth, 175);

            // Sağ Panel - Sistem Önerisi
            var rightPanel = CreateAIRecommendationPanel(panelWidth);
            rightPanel.Location = new Point(panelWidth + spacing, 0);
            rightPanel.Size = new Size(panelWidth, 175);

            panel.Controls.Add(leftPanel);
            panel.Controls.Add(rightPanel);

            return panel;
        }

        private void UpdateMiddleSectionLayout(PanelControl panel, int availableWidth)
        {
            int spacing = 5;
            int panelWidth = (availableWidth - spacing) / 2;

            if (panel.Controls.Count >= 2)
            {
                if (panel.Controls[0] is PanelControl leftPanel)
                {
                    leftPanel.Width = panelWidth;
                }
                if (panel.Controls[1] is PanelControl rightPanel)
                {
                    rightPanel.Width = panelWidth;
                    rightPanel.Location = new Point(panelWidth + spacing, 0);
                }
            }
        }

        private PanelControl CreateRecentEventsPanel(int panelWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            // Başlık
            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "📋 Son Olaylar";
            panel.Controls.Add(titleLabel);

            // Olaylar listesi
            int yPos = 32;
            int itemWidth = panelWidth - 12;
            var events = GetRecentEvents();
            foreach (var evt in events.Take(4))
            {
                var eventPanel = CreateEventItem(evt.Icon, evt.Text, evt.Time, evt.Color, itemWidth);
                eventPanel.Location = new Point(6, yPos);
                eventPanel.Size = new Size(itemWidth, 28);
                panel.Controls.Add(eventPanel);
                yPos += 30;
            }

            return panel;
        }

        private List<(string Icon, string Text, string Time, Color Color)> GetRecentEvents()
        {
            var events = new List<(string Icon, string Text, string Time, Color Color)>();

            // Son başvurular
            var sonBasvurular = _basvurular.OrderByDescending(b => b.BasvuruTarihi).Take(3);
            foreach (var b in sonBasvurular)
            {
                string icon = b.BasvuruDurumu == "Onaylandı" ? "✅" : b.BasvuruDurumu == "Reddedildi" ? "❌" : "⏳";
                Color color = b.BasvuruDurumu == "Onaylandı" ? Color.FromArgb(40, 167, 69) : 
                              b.BasvuruDurumu == "Reddedildi" ? Color.FromArgb(220, 53, 69) : Color.FromArgb(255, 193, 7);
                events.Add((icon, $"{b.OgrenciAdSoyad} - {b.BasvuruDurumu}", b.BasvuruTarihi.ToString("dd.MM HH:mm"), color));
            }

            // Varsayılan olaylar ekle
            if (events.Count < 3)
            {
                events.Add(("📝", "Yeni başvuru bekleniyor", "Bugün", Color.FromArgb(108, 117, 125)));
                events.Add(("🔔", "Sistem hazır", DateTime.Now.ToString("HH:mm"), Color.FromArgb(0, 123, 255)));
            }

            return events;
        }

        private PanelControl CreateEventItem(string icon, string text, string time, Color color, int itemWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            // İkon
            var iconLabel = new LabelControl();
            iconLabel.Appearance.Font = new Font("Segoe UI", 12F);
            iconLabel.Location = new Point(5, 5);
            iconLabel.Text = icon;

            // Metin - Dinamik genişlik
            int textWidth = itemWidth - 100; // İkon ve zaman için yer bırak
            var textLabel = new LabelControl();
            textLabel.Appearance.Font = new Font("Segoe UI", 9F);
            textLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            textLabel.Location = new Point(30, 7);
            textLabel.AutoSizeMode = LabelAutoSizeMode.None;
            textLabel.Size = new Size(textWidth, 20);
            textLabel.Text = text.Length > (textWidth / 7) ? text.Substring(0, (textWidth / 7) - 3) + "..." : text;

            // Zaman
            var timeLabel = new LabelControl();
            timeLabel.Appearance.Font = new Font("Segoe UI", 8F);
            timeLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            timeLabel.Location = new Point(itemWidth - 60, 8);
            timeLabel.Text = time;

            panel.Controls.Add(iconLabel);
            panel.Controls.Add(textLabel);
            panel.Controls.Add(timeLabel);

            return panel;
        }

        private PanelControl CreateAIRecommendationPanel(int panelWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            // Başlık
            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "🤖 Akıllı Değerlendirme";
            panel.Controls.Add(titleLabel);

            // Örnek öğrenci bul
            var ornekOgrenci = _ogrenciler.OrderByDescending(o => o.BursPuani).FirstOrDefault();
            
            int contentWidth = panelWidth - 20;
            
            if (ornekOgrenci != null)
            {
                // Öğrenci adı
                var nameLabel = new LabelControl();
                nameLabel.Appearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                nameLabel.Appearance.ForeColor = Color.FromArgb(0, 123, 255);
                nameLabel.Location = new Point(10, 35);
                nameLabel.Text = $"📚 {ornekOgrenci.Ad} {ornekOgrenci.Soyad}";
                panel.Controls.Add(nameLabel);

                // Uygunluk yüzdesi
                int uygunluk = Math.Min(100, (int)(ornekOgrenci.BursPuani));
                var percentPanel = CreatePercentageBar(uygunluk, contentWidth);
                percentPanel.Location = new Point(10, 58);
                percentPanel.Size = new Size(contentWidth, 30);
                panel.Controls.Add(percentPanel);

                // AI Yorumu
                string yorum = GenerateAIComment(ornekOgrenci, uygunluk);
                var commentLabel = new LabelControl();
                commentLabel.Appearance.Font = new Font("Segoe UI", 8F);
                commentLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
                commentLabel.AutoSizeMode = LabelAutoSizeMode.None;
                commentLabel.Size = new Size(contentWidth, 70);
                commentLabel.Location = new Point(10, 92);
                commentLabel.Text = yorum;
                commentLabel.AutoEllipsis = true;
                panel.Controls.Add(commentLabel);
            }
            else
            {
                var noDataLabel = new LabelControl();
                noDataLabel.Appearance.Font = new Font("Segoe UI", 10F);
                noDataLabel.Appearance.ForeColor = Color.Gray;
                noDataLabel.Location = new Point(12, 80);
                noDataLabel.Text = "Henüz öğrenci verisi bulunmuyor.\nÖğrenci ekleyerek sistemi kullanmaya başlayın.";
                panel.Controls.Add(noDataLabel);
            }

            return panel;
        }

        private PanelControl CreatePercentageBar(int percentage, int contentWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            // Yüzde metni
            var percentLabel = new LabelControl();
            percentLabel.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            percentLabel.Appearance.ForeColor = percentage >= 70 ? Color.FromArgb(40, 167, 69) : 
                                                 percentage >= 50 ? Color.FromArgb(255, 193, 7) : Color.FromArgb(220, 53, 69);
            percentLabel.Location = new Point(0, 5);
            percentLabel.Text = $"%{percentage} Uygunluk";

            // Progress bar arka planı - Dinamik genişlik
            int barWidth = Math.Max(100, contentWidth - 130);
            var bgBar = new PanelControl();
            bgBar.Location = new Point(130, 10);
            bgBar.Size = new Size(barWidth, 16);
            bgBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            bgBar.Appearance.BackColor = Color.FromArgb(230, 230, 230);
            bgBar.Appearance.Options.UseBackColor = true;

            // Progress bar dolgu
            var fillBar = new PanelControl();
            fillBar.Location = new Point(0, 0);
            fillBar.Size = new Size((int)(barWidth * percentage / 100.0), 16);
            fillBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            fillBar.Appearance.BackColor = percentage >= 70 ? Color.FromArgb(40, 167, 69) : 
                                            percentage >= 50 ? Color.FromArgb(255, 193, 7) : Color.FromArgb(220, 53, 69);
            fillBar.Appearance.Options.UseBackColor = true;
            bgBar.Controls.Add(fillBar);

            panel.Controls.Add(percentLabel);
            panel.Controls.Add(bgBar);

            return panel;
        }

        private string GenerateAIComment(Ogrenci ogrenci, int uygunluk)
        {
            if (uygunluk >= 80)
                return $"✨ Yüksek öncelikli aday. GNO: {ogrenci.NotOrtalamasi:F2}, " +
                       $"Aile geliri düşük ({ogrenci.AileGeliri:N0} TL). Burs için önerilir.";
            else if (uygunluk >= 60)
                return $"📊 Orta öncelikli aday. Akademik başarı iyi (GNO: {ogrenci.NotOrtalamasi:F2}). " +
                       $"Detaylı inceleme önerilir.";
            else
                return $"📝 Değerlendirme gerekli. GNO: {ogrenci.NotOrtalamasi:F2}. " +
                       $"Ek belgeler talep edilebilir.";
        }

        private PanelControl CreateBottomSection(int availableWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            int spacing = 5;
            int panelWidth = (availableWidth - spacing) / 2;

            // Sol Panel - Başvuru Durumu Grafiği
            var leftPanel = CreateChartPanel(panelWidth);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Size = new Size(panelWidth, 175);

            // Sağ Panel - Riskli Başvurular
            var rightPanel = CreateRiskyApplicationsPanel(panelWidth);
            rightPanel.Location = new Point(panelWidth + spacing, 0);
            rightPanel.Size = new Size(panelWidth, 175);

            panel.Controls.Add(leftPanel);
            panel.Controls.Add(rightPanel);

            return panel;
        }

        private void UpdateBottomSectionLayout(PanelControl panel, int availableWidth)
        {
            int spacing = 5;
            int panelWidth = (availableWidth - spacing) / 2;

            if (panel.Controls.Count >= 2)
            {
                if (panel.Controls[0] is PanelControl leftPanel)
                {
                    leftPanel.Width = panelWidth;
                }
                if (panel.Controls[1] is PanelControl rightPanel)
                {
                    rightPanel.Width = panelWidth;
                    rightPanel.Location = new Point(panelWidth + spacing, 0);
                }
            }
        }

        private PanelControl CreateChartPanel(int panelWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            // Başlık
            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "📊 Başvuru Dağılımı";
            panel.Controls.Add(titleLabel);

            // Grafik yerine basit bar chart simülasyonu
            int onaylanan = _basvurular.Count(b => b.BasvuruDurumu == "Onaylandı");
            int beklemede = _basvurular.Count(b => b.BasvuruDurumu == "Beklemede" || b.BasvuruDurumu == "İnceleniyor");
            int reddedilen = _basvurular.Count(b => b.BasvuruDurumu == "Reddedildi");
            int toplam = Math.Max(1, onaylanan + beklemede + reddedilen);

            int barWidth = panelWidth - 20;
            int yPos = 35;
            
            // Onaylandı bar
            var bar1 = CreateChartBar("Onaylandı", onaylanan, toplam, Color.FromArgb(40, 167, 69), barWidth);
            bar1.Location = new Point(10, yPos);
            bar1.Size = new Size(barWidth, 40);
            panel.Controls.Add(bar1);
            yPos += 43;

            // İncelemede bar
            var bar2 = CreateChartBar("İncelemede", beklemede, toplam, Color.FromArgb(255, 193, 7), barWidth);
            bar2.Location = new Point(10, yPos);
            bar2.Size = new Size(barWidth, 40);
            panel.Controls.Add(bar2);
            yPos += 43;

            // Reddedildi bar
            var bar3 = CreateChartBar("Reddedildi", reddedilen, toplam, Color.FromArgb(220, 53, 69), barWidth);
            bar3.Location = new Point(10, yPos);
            bar3.Size = new Size(barWidth, 40);
            panel.Controls.Add(bar3);

            return panel;
        }

        private PanelControl CreateChartBar(string label, int value, int total, Color color, int barWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            // Label
            var labelCtrl = new LabelControl();
            labelCtrl.Appearance.Font = new Font("Segoe UI", 9F);
            labelCtrl.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            labelCtrl.Location = new Point(0, 3);
            labelCtrl.Size = new Size(80, 18);
            labelCtrl.Text = label;

            // Sayı - Dinamik konum
            var valueCtrl = new LabelControl();
            valueCtrl.Appearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            valueCtrl.Appearance.ForeColor = color;
            valueCtrl.Location = new Point(barWidth - 30, 3);
            valueCtrl.Text = value.ToString();

            // Bar arka plan - Dinamik genişlik
            var bgBar = new PanelControl();
            bgBar.Location = new Point(0, 20);
            bgBar.Size = new Size(barWidth, 16);
            bgBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            bgBar.Appearance.BackColor = Color.FromArgb(240, 240, 240);
            bgBar.Appearance.Options.UseBackColor = true;

            // Bar dolgu - Dinamik genişlik
            int fillWidth = total > 0 ? (int)((barWidth - 8) * value / (double)total) : 0;
            fillWidth = Math.Max(fillWidth, value > 0 ? 8 : 0);
            var fillBar = new PanelControl();
            fillBar.Location = new Point(0, 0);
            fillBar.Size = new Size(fillWidth, 16);
            fillBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            fillBar.Appearance.BackColor = color;
            fillBar.Appearance.Options.UseBackColor = true;
            bgBar.Controls.Add(fillBar);

            panel.Controls.Add(labelCtrl);
            panel.Controls.Add(valueCtrl);
            panel.Controls.Add(bgBar);

            return panel;
        }

        private PanelControl CreateRiskyApplicationsPanel(int panelWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            // Başlık
            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(220, 53, 69);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "⚠️ Riskli Başvurular";
            panel.Controls.Add(titleLabel);

            // Riskli öğrencileri bul (düşük puan veya eksik bilgi)
            var riskliOgrenciler = _ogrenciler
                .Where(o => o.BursPuani < 50 || o.NotOrtalamasi < 2.0m)
                .OrderBy(o => o.BursPuani)
                .Take(3)
                .ToList();

            int itemWidth = panelWidth - 12;
            int yPos = 32;
            if (riskliOgrenciler.Any())
            {
                foreach (var ogr in riskliOgrenciler)
                {
                    var riskItem = CreateRiskItem(ogr, itemWidth);
                    riskItem.Location = new Point(6, yPos);
                    riskItem.Size = new Size(itemWidth, 38);
                    panel.Controls.Add(riskItem);
                    yPos += 40;
                }
            }
            else
            {
                var noRiskLabel = new LabelControl();
                noRiskLabel.Appearance.Font = new Font("Segoe UI", 10F);
                noRiskLabel.Appearance.ForeColor = Color.FromArgb(40, 167, 69);
                noRiskLabel.Location = new Point(12, 80);
                noRiskLabel.Text = "✅ Şu anda riskli başvuru bulunmuyor.\nTüm başvurular normal durumda.";
                panel.Controls.Add(noRiskLabel);
            }

            return panel;
        }

        private PanelControl CreateRiskItem(Ogrenci ogrenci, int itemWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.FromArgb(255, 248, 248);
            panel.Appearance.Options.UseBackColor = true;

            // İkon
            var iconLabel = new LabelControl();
            iconLabel.Appearance.Font = new Font("Segoe UI", 12F);
            iconLabel.Location = new Point(8, 10);
            iconLabel.Text = "⚠️";

            // Ad - Dinamik genişlik
            var nameLabel = new LabelControl();
            nameLabel.Appearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nameLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            nameLabel.Location = new Point(35, 5);
            nameLabel.AutoSizeMode = LabelAutoSizeMode.None;
            nameLabel.Size = new Size(itemWidth - 40, 18);
            nameLabel.Text = $"{ogrenci.Ad} {ogrenci.Soyad}";

            // Neden
            string neden = ogrenci.BursPuani < 50 ? $"Düşük puan: {ogrenci.BursPuani:F0}" :
                          ogrenci.NotOrtalamasi < 2.0m ? $"Düşük GNO: {ogrenci.NotOrtalamasi:F2}" : "İnceleme gerekli";
            var reasonLabel = new LabelControl();
            reasonLabel.Appearance.Font = new Font("Segoe UI", 8F);
            reasonLabel.Appearance.ForeColor = Color.FromArgb(220, 53, 69);
            reasonLabel.Location = new Point(35, 22);
            reasonLabel.AutoSizeMode = LabelAutoSizeMode.None;
            reasonLabel.Size = new Size(itemWidth - 40, 15);
            reasonLabel.Text = neden;

            panel.Controls.Add(iconLabel);
            panel.Controls.Add(nameLabel);
            panel.Controls.Add(reasonLabel);

            return panel;
        }

        #endregion

        #region Ribbon Events
        private void RibbonControl_SelectedPageChanging(object? sender, RibbonPageChangingEventArgs e)
        {
            // Sayfa değişikliği işlemleri
        }

        private void Ribbon_MinimizedChanged(object? sender, EventArgs e) { }
        #endregion

        #region Navigation Menu
        private void RegisterNavigationMenuItems()
        {
            var moduleTypes = new ModuleType[] { 
                ModuleType.Dashboard, ModuleType.OgrenciEkle, ModuleType.OgrenciListele, 
                ModuleType.BasvuruListele, ModuleType.AIAnaliz, ModuleType.BursPuaniHesapla, ModuleType.Bagiscilar
            };

            foreach (var type in moduleTypes)
            {
                BarCheckItem biModule = new BarCheckItem();
                biModule.Caption = GetModuleCaption(type);
                biModule.Name = "biModule" + type.ToString();
                biModule.ImageOptions.ImageUri.Uri = GetModuleImageUri(type);
                biModule.ImageOptions.SvgImageSize = new Size(32, 32);
                biModule.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
                biModule.GroupIndex = 1;
                biModule.ItemClick += (s, e) => SelectModule(type);
                barNavigationItem.AddItem(biModule);
            }
        }
        #endregion

        #region Accordion Control
        private void RegisterAccordionControlElements()
        {
            accordionControl1.Elements.Clear();

            // Dashboard - Grup olarak değil, direkt item olarak ekle
            var dashboardItem = CreateAccordionElement(ModuleType.Dashboard);
            dashboardItem.ImageOptions.SvgImageSize = new Size(20, 20);
            accordionControl1.Elements.Add(dashboardItem);

            // Öğrenci İşlemleri grubu
            var ogrenciGroup = new AccordionControlElement { Name = "groupOgrenci", Text = "Öğrenci", Style = ElementStyle.Group, Expanded = true };
            ogrenciGroup.ImageOptions.ImageUri.Uri = "People;Size32x32";
            ogrenciGroup.ImageOptions.SvgImageSize = new Size(20, 20);
            ogrenciGroup.Elements.Add(CreateAccordionElement(ModuleType.OgrenciEkle));
            ogrenciGroup.Elements.Add(CreateAccordionElement(ModuleType.OgrenciGuncelle));
            accordionControl1.Elements.Add(ogrenciGroup);

            // Başvuru İşlemleri grubu
            var basvuruGroup = new AccordionControlElement { Name = "groupBasvuru", Text = "Başvuru", Style = ElementStyle.Group, Expanded = true };
            basvuruGroup.ImageOptions.ImageUri.Uri = "TaskList;Size32x32";
            basvuruGroup.ImageOptions.SvgImageSize = new Size(20, 20);
            basvuruGroup.Elements.Add(CreateAccordionElement(ModuleType.BasvuruListele));
            accordionControl1.Elements.Add(basvuruGroup);

            // Analiz & Puanlama grubu
            var analizGroup = new AccordionControlElement { Name = "groupAnaliz", Text = "Analiz", Style = ElementStyle.Group, Expanded = true };
            analizGroup.ImageOptions.ImageUri.Uri = "Chart;Size32x32";
            analizGroup.ImageOptions.SvgImageSize = new Size(20, 20);
            analizGroup.Elements.Add(CreateAccordionElement(ModuleType.AIAnaliz));
            analizGroup.Elements.Add(CreateAccordionElement(ModuleType.BursPuaniHesapla));
            accordionControl1.Elements.Add(analizGroup);

            // Finans grubu
            var bagisciGroup = new AccordionControlElement { Name = "groupBagisci", Text = "Finans", Style = ElementStyle.Group, Expanded = true };
            bagisciGroup.ImageOptions.ImageUri.Uri = "Currency/TRY;Size32x32";
            bagisciGroup.ImageOptions.SvgImageSize = new Size(20, 20);
            bagisciGroup.Elements.Add(CreateAccordionElement(ModuleType.Bagiscilar));
            accordionControl1.Elements.Add(bagisciGroup);
        }

        private AccordionControlElement CreateAccordionElement(ModuleType type)
        {
            var element = new AccordionControlElement();
            element.Tag = type;
            element.Name = "accordionElement" + type.ToString();
            element.Text = GetModuleCaption(type);
            element.SuperTip = new DevExpress.Utils.SuperToolTip();
            element.SuperTip.Items.AddTitle(element.Text);
            element.ImageOptions.ImageUri.Uri = GetModuleImageUri(type);
            element.ImageOptions.SvgImageSize = new Size(18, 18);
            element.Style = ElementStyle.Item;
            element.Height = 32;
            element.Click += (s, e) => SelectModule(type);
            return element;
        }

        private string GetModuleCaption(ModuleType type) => type switch
        {
            ModuleType.Dashboard => "Dashboard",
            ModuleType.OgrenciEkle => "Öğrenci Ekle",
            ModuleType.OgrenciListele => "Öğrenci Listele",
            ModuleType.OgrenciGuncelle => "Öğrenci Güncelle",
            ModuleType.BasvuruListele => "Başvuru Listele",
            ModuleType.AIAnaliz => "AI Analiz Yap",
            ModuleType.BursPuaniHesapla => "Burs Puanı Hesapla",
            ModuleType.Bagiscilar => "Bağışçılar",
            _ => "Bilinmeyen"
        };

        private string GetModuleImageUri(ModuleType type) => type switch
        {
            ModuleType.Dashboard => "Home;Size32x32",
            ModuleType.OgrenciEkle => "Add;Size32x32",
            ModuleType.OgrenciListele => "People;Size32x32",
            ModuleType.OgrenciGuncelle => "Edit;Size32x32",
            ModuleType.BasvuruListele => "TaskList;Size32x32",
            ModuleType.AIAnaliz => "Chart;Size32x32",
            ModuleType.BursPuaniHesapla => "CustomizeGrid;Size32x32",
            ModuleType.Bagiscilar => "Currency/TRY;Size32x32",
            _ => "Info;Size32x32"
        };
        #endregion

        #region Folder Pane Visibility
        private void BindFolderPaneVisibility()
        {
            bmiFolderNormal.ItemClick += (s, e) => SetFolderPaneState(AccordionControlState.Normal);
            bmiFolderMinimized.ItemClick += (s, e) => SetFolderPaneState(AccordionControlState.Minimized);
            bmiFolderOff.ItemClick += (s, e) => accordionControl1.Visible = false;
        }

        private void SetFolderPaneState(AccordionControlState state)
        {
            accordionControl1.Visible = true;
            accordionControl1.OptionsMinimizing.State = state;
        }
        #endregion

        #region Module Management
        private void SelectModule(ModuleType type)
        {
            selectedModuleType = type;
            Text = type == ModuleType.Dashboard ? "Öğrenci Burs Yönetim Sistemi" : $"Öğrenci Burs Yönetim Sistemi - {GetModuleCaption(type)}";

            // Hamburger menüyü kapat (minimize et)
            accordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;

            if (type == ModuleType.Dashboard)
            {
                LoadDashboardData();
                ShowDashboard();
                return;
            }
            LoadModule(type);
        }

        private void LoadModule(ModuleType type)
        {
            transitionManager.StartTransition(modulesContainer);
            try
            {
                if (currentModuleForm != null && !currentModuleForm.IsDisposed)
                {
                    currentModuleForm.Close();
                    currentModuleForm.Dispose();
                }

                Form? newForm = type switch
                {
                    ModuleType.OgrenciEkle => new OgrenciEkleForm(),
                    ModuleType.OgrenciListele => new OgrenciListeForm(),
                    ModuleType.OgrenciGuncelle => new OgrenciListeForm(true),
                    ModuleType.BasvuruListele => new BasvuruListeForm(),
                    ModuleType.AIAnaliz => new AIAnalizForm(),
                    ModuleType.BursPuaniHesapla => new BursPuaniHesaplaForm(),
                    ModuleType.Bagiscilar => new BagisciListeForm(),
                    _ => null
                };

                if (newForm != null)
                {
                    currentModuleForm = newForm;
                    newForm.TopLevel = false;
                    newForm.FormBorderStyle = FormBorderStyle.None;
                    newForm.Dock = DockStyle.Fill;
                    modulesContainer.Controls.Clear();
                    modulesContainer.Controls.Add(newForm);
                    newForm.Show();
                }
            }
            finally { transitionManager.EndTransition(); }
        }
        #endregion
    }

    public enum ModuleType
    {
        Unknown, Dashboard, OgrenciEkle, OgrenciListele, OgrenciGuncelle,
        BasvuruListele, AIAnaliz, BursPuaniHesapla, Bagiscilar
    }
}
