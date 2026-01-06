using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using BursOtomasyon.Desktop.Services;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class BagisciListeForm : XtraForm
    {
        private readonly BagisciService _bagisciService;
        private Bagisci? _selectedBagisci;
        
        // UI Bileşenleri
        private XtraScrollableControl mainScroll = null!;
        private PanelControl contentPanel = null!;

        public BagisciListeForm()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _bagisciService = new BagisciService();
            CreateModernUI();
        }

        private void CreateModernUI()
        {
            this.Controls.Clear();
            this.Appearance.BackColor = Color.FromArgb(245, 247, 250);
            this.Appearance.Options.UseBackColor = true;

            mainScroll = new XtraScrollableControl();
            mainScroll.Dock = DockStyle.Fill;
            mainScroll.Appearance.BackColor = Color.FromArgb(245, 247, 250);
            mainScroll.Appearance.Options.UseBackColor = true;

            // Dinamik genişlik hesapla
            int padding = 16;
            int contentWidth = Math.Max(600, this.ClientSize.Width - padding * 2);
            
            contentPanel = new PanelControl();
            contentPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            contentPanel.Appearance.BackColor = Color.FromArgb(245, 247, 250);
            contentPanel.Appearance.Options.UseBackColor = true;
            contentPanel.Location = new Point(0, 0);
            contentPanel.Size = new Size(contentWidth, 900);
            contentPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Resize eventi ekle
            this.Resize += (s, e) => {
                if (contentPanel != null && !contentPanel.IsDisposed)
                {
                    int newWidth = Math.Max(600, this.ClientSize.Width - padding * 2);
                    contentPanel.Width = newWidth;
                    UpdateLayout(contentPanel, newWidth, padding);
                }
            };

            BuildContent(contentPanel, contentWidth, padding);

            mainScroll.Controls.Add(contentPanel);
            this.Controls.Add(mainScroll);
        }

        private void BuildContent(PanelControl contentPanel, int contentWidth, int padding)
        {
            int availableWidth = contentWidth - (padding * 2);
            int yPos = padding;

            // 1. BAŞLIK
            var headerPanel = CreateHeaderSection();
            headerPanel.Location = new Point(padding, yPos);
            headerPanel.Size = new Size(availableWidth, 50);
            contentPanel.Controls.Add(headerPanel);
            yPos += 55;

            // 2. ÖZET KARTLARI
            var statsPanel = CreateStatsSection(availableWidth);
            statsPanel.Location = new Point(padding, yPos);
            statsPanel.Size = new Size(availableWidth, 75);
            contentPanel.Controls.Add(statsPanel);
            yPos += 82;

            // 3. ORTA BÖLÜM - Sol: Bağışçı Listesi, Sağ: Bağışçı Profili
            var middlePanel = CreateMiddleSection(availableWidth);
            middlePanel.Location = new Point(padding, yPos);
            middlePanel.Size = new Size(availableWidth, 200);
            contentPanel.Controls.Add(middlePanel);
            yPos += 207;

            // 4. SON BAĞIŞLAR
            var recentPanel = CreateRecentDonationsSection(availableWidth);
            recentPanel.Location = new Point(padding, yPos);
            recentPanel.Size = new Size(availableWidth, 150);
            contentPanel.Controls.Add(recentPanel);
            yPos += 157;

            // 5. AYLIK GRAFİK
            var chartPanel = CreateChartSection(availableWidth);
            chartPanel.Location = new Point(padding, yPos);
            chartPanel.Size = new Size(availableWidth, 140);
            contentPanel.Controls.Add(chartPanel);
            yPos += 147;

            // 6. BAĞIŞ YAP BUTONU
            var actionPanel = CreateActionSection(availableWidth);
            actionPanel.Location = new Point(padding, yPos);
            actionPanel.Size = new Size(availableWidth, 50);
            contentPanel.Controls.Add(actionPanel);
        }

        private void UpdateLayout(PanelControl contentPanel, int contentWidth, int padding)
        {
            int availableWidth = contentWidth - (padding * 2);
            
            foreach (Control ctrl in contentPanel.Controls)
            {
                if (ctrl is PanelControl panel)
                {
                    // Header panel
                    if (panel.Controls.Count > 0 && panel.Controls[0] is LabelControl lbl && lbl.Text.Contains("Bağışçı Yönetimi"))
                    {
                        panel.Width = availableWidth;
                    }
                    // Stats panel
                    else if (panel.Controls.Count >= 4)
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
                    // Recent donations
                    else if (panel.Controls.Count > 0 && panel.Controls[0] is LabelControl title && title.Text.Contains("Son Bağışlar"))
                    {
                        panel.Width = availableWidth;
                        UpdateRecentDonationsLayout(panel, availableWidth);
                    }
                    // Chart section
                    else if (panel.Controls.Count > 0 && panel.Controls[0] is LabelControl chartTitle && chartTitle.Text.Contains("Aylık Bağış"))
                    {
                        panel.Width = availableWidth;
                        UpdateChartLayout(panel, availableWidth);
                    }
                    // Action section
                    else if (panel.Controls.Count > 0 && panel.Controls[0] is SimpleButton)
                    {
                        panel.Width = availableWidth;
                    }
                }
            }
        }

        #region UI Sections

        private PanelControl CreateHeaderSection()
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            var iconLabel = new LabelControl();
            iconLabel.Text = "💰";
            iconLabel.Appearance.Font = new Font("Segoe UI", 20F);
            iconLabel.Location = new Point(12, 10);

            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(50, 8);
            titleLabel.Text = "Bağışçı Yönetimi";

            var subtitleLabel = new LabelControl();
            subtitleLabel.Appearance.Font = new Font("Segoe UI", 9F);
            subtitleLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            subtitleLabel.Location = new Point(50, 30);
            subtitleLabel.Text = "Bağışları takip edin, bağışçıları yönetin";

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

            var kasaOzeti = _bagisciService.GetKasaOzeti();
            int bagisciSayisi = _bagisciService.GetToplamBagisciSayisi();
            int desteklenenOgrenci = _bagisciService.GetDesteklenenOgrenciSayisi();
            decimal buAyBagis = _bagisciService.GetBuAyBagis();

            int spacing = 6;
            int cardWidth = Math.Max(138, (availableWidth - (spacing * 3)) / 4);

            // Toplam bağış = tüm gelen bağışlar
            var card1 = CreateStatCard("Toplam Bağış", kasaOzeti.ToplamGelenBagis.ToString("N0") + " ₺", Color.FromArgb(40, 167, 69), "💵", cardWidth);
            card1.Location = new Point(0, 0);
            card1.Size = new Size(cardWidth, 70);

            var card2 = CreateStatCard("Bağışçı Sayısı", bagisciSayisi.ToString(), Color.FromArgb(0, 123, 255), "👥", cardWidth);
            card2.Location = new Point(cardWidth + spacing, 0);
            card2.Size = new Size(cardWidth, 70);

            var card3 = CreateStatCard("Desteklenen Öğr.", desteklenenOgrenci.ToString(), Color.FromArgb(255, 193, 7), "🎓", cardWidth);
            card3.Location = new Point((cardWidth + spacing) * 2, 0);
            card3.Size = new Size(cardWidth, 70);

            var card4 = CreateStatCard("Bu Ay", buAyBagis.ToString("N0") + " ₺", Color.FromArgb(111, 66, 193), "📅", cardWidth);
            card4.Location = new Point((cardWidth + spacing) * 3, 0);
            card4.Size = new Size(cardWidth, 70);

            panel.Controls.Add(card1);
            panel.Controls.Add(card2);
            panel.Controls.Add(card3);
            panel.Controls.Add(card4);

            return panel;
        }

        private void UpdateStatsPanelLayout(PanelControl panel, int availableWidth)
        {
            int spacing = 6;
            int cardWidth = Math.Max(138, (availableWidth - (spacing * 3)) / 4);

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
            if (card.Controls.Count > 0 && card.Controls[0] is PanelControl colorBar)
            {
                colorBar.Width = cardWidth;
            }
        }

        private PanelControl CreateStatCard(string title, string value, Color accentColor, string emoji, int cardWidth)
        {
            var card = new PanelControl();
            card.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            card.Appearance.BackColor = Color.White;
            card.Appearance.Options.UseBackColor = true;
            card.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            card.Appearance.Options.UseBorderColor = true;

            var colorBar = new PanelControl();
            colorBar.Location = new Point(0, 0);
            colorBar.Size = new Size(cardWidth, 3);
            colorBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            colorBar.Appearance.BackColor = accentColor;
            colorBar.Appearance.Options.UseBackColor = true;

            var emojiLabel = new LabelControl();
            emojiLabel.Text = emoji;
            emojiLabel.Appearance.Font = new Font("Segoe UI", 14F);
            emojiLabel.Location = new Point(8, 10);

            var valueLabel = new LabelControl();
            valueLabel.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            valueLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            valueLabel.Location = new Point(8, 35);
            valueLabel.Text = value;

            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 7.5F);
            titleLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            titleLabel.Location = new Point(8, 55);
            titleLabel.Text = title;

            card.Controls.Add(colorBar);
            card.Controls.Add(emojiLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(titleLabel);

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

            int spacing = 8;
            int leftWidth = (int)(availableWidth * 0.6); // %60 sol
            int rightWidth = availableWidth - leftWidth - spacing; // Kalan sağ

            // Sol - Bağışçı Listesi
            var leftPanel = CreateDonorListPanel(leftWidth);
            leftPanel.Location = new Point(0, 0);
            leftPanel.Size = new Size(leftWidth, 200);

            // Sağ - Bağışçı Profili
            var rightPanel = CreateDonorProfilePanel(rightWidth);
            rightPanel.Location = new Point(leftWidth + spacing, 0);
            rightPanel.Size = new Size(rightWidth, 200);

            panel.Controls.Add(leftPanel);
            panel.Controls.Add(rightPanel);

            return panel;
        }

        private void UpdateMiddleSectionLayout(PanelControl panel, int availableWidth)
        {
            int spacing = 8;
            int leftWidth = (int)(availableWidth * 0.6);
            int rightWidth = availableWidth - leftWidth - spacing;

            if (panel.Controls.Count >= 2)
            {
                if (panel.Controls[0] is PanelControl leftPanel)
                {
                    leftPanel.Width = leftWidth;
                }
                if (panel.Controls[1] is PanelControl rightPanel)
                {
                    rightPanel.Width = rightWidth;
                    rightPanel.Location = new Point(leftWidth + spacing, 0);
                }
            }
        }

        private PanelControl CreateDonorListPanel(int panelWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "👥 Bağışçı Listesi";
            panel.Controls.Add(titleLabel);

            var grid = new GridControl();
            grid.Location = new Point(5, 32);
            grid.Size = new Size(panelWidth - 10, 160);
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            
            var view = new GridView(grid);
            grid.MainView = view;
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.OptionsSelection.MultiSelect = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsView.ColumnAutoWidth = true;

            var bagiscilar = _bagisciService.GetAll();
            grid.DataSource = bagiscilar;

            view.FocusedRowChanged += (s, e) => {
                if (view.FocusedRowHandle >= 0)
                {
                    var bagisci = view.GetRow(view.FocusedRowHandle) as Bagisci;
                    if (bagisci != null)
                    {
                        _selectedBagisci = bagisci;
                        UpdateDonorProfile(bagisci.BagisciID);
                    }
                }
            };

            panel.Controls.Add(grid);
            return panel;
        }

        private PanelControl _profilePanel = null!;
        private LabelControl _lblProfileName = null!;
        private LabelControl _lblProfileRozet = null!;
        private LabelControl _lblProfileToplamBagis = null!;
        private LabelControl _lblProfileBagisSayisi = null!;
        private LabelControl _lblProfileSonBagis = null!;

        private PanelControl CreateDonorProfilePanel(int panelWidth)
        {
            _profilePanel = new PanelControl();
            _profilePanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            _profilePanel.Appearance.BackColor = Color.White;
            _profilePanel.Appearance.Options.UseBackColor = true;
            _profilePanel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            _profilePanel.Appearance.Options.UseBorderColor = true;

            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "👤 Bağışçı Profili";
            _profilePanel.Controls.Add(titleLabel);

            _lblProfileName = new LabelControl();
            _lblProfileName.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _lblProfileName.Appearance.ForeColor = Color.FromArgb(0, 123, 255);
            _lblProfileName.Location = new Point(10, 38);
            _lblProfileName.Text = "Bağışçı seçiniz...";
            _profilePanel.Controls.Add(_lblProfileName);

            _lblProfileRozet = new LabelControl();
            _lblProfileRozet.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _lblProfileRozet.Location = new Point(10, 62);
            _lblProfileRozet.Text = "";
            _profilePanel.Controls.Add(_lblProfileRozet);

            _lblProfileToplamBagis = new LabelControl();
            _lblProfileToplamBagis.Appearance.Font = new Font("Segoe UI", 9F);
            _lblProfileToplamBagis.Appearance.ForeColor = Color.FromArgb(40, 167, 69);
            _lblProfileToplamBagis.Location = new Point(10, 90);
            _lblProfileToplamBagis.Text = "💰 Toplam: -";
            _profilePanel.Controls.Add(_lblProfileToplamBagis);

            _lblProfileBagisSayisi = new LabelControl();
            _lblProfileBagisSayisi.Appearance.Font = new Font("Segoe UI", 9F);
            _lblProfileBagisSayisi.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            _lblProfileBagisSayisi.Location = new Point(10, 112);
            _lblProfileBagisSayisi.Text = "📊 Bağış Sayısı: -";
            _profilePanel.Controls.Add(_lblProfileBagisSayisi);

            _lblProfileSonBagis = new LabelControl();
            _lblProfileSonBagis.Appearance.Font = new Font("Segoe UI", 9F);
            _lblProfileSonBagis.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            _lblProfileSonBagis.Location = new Point(10, 134);
            _lblProfileSonBagis.Text = "📅 Son Bağış: -";
            _profilePanel.Controls.Add(_lblProfileSonBagis);

            return _profilePanel;
        }

        private void UpdateDonorProfile(int bagisciId)
        {
            var profil = _bagisciService.GetBagisciProfil(bagisciId);
            if (profil != null)
            {
                _lblProfileName.Text = profil.AdSoyad;
                _lblProfileRozet.Text = profil.Rozet;
                _lblProfileRozet.Appearance.ForeColor = profil.ToplamBagis >= 50000 ? Color.FromArgb(255, 193, 7) :
                                                         profil.ToplamBagis >= 20000 ? Color.FromArgb(192, 192, 192) :
                                                         profil.ToplamBagis >= 5000 ? Color.FromArgb(205, 127, 50) :
                                                         Color.FromArgb(108, 117, 125);
                _lblProfileToplamBagis.Text = $"💰 Toplam: {profil.ToplamBagis:N0} ₺";
                _lblProfileBagisSayisi.Text = $"📊 Bağış Sayısı: {profil.BagisSayisi}";
                _lblProfileSonBagis.Text = profil.SonBagisTarihi.HasValue 
                    ? $"📅 Son Bağış: {profil.SonBagisTarihi:dd.MM.yyyy}" 
                    : "📅 Son Bağış: Henüz yok";
            }
        }

        private PanelControl CreateRecentDonationsSection(int availableWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "🕐 Son Bağışlar";
            panel.Controls.Add(titleLabel);

            var sonBagislar = _bagisciService.GetSonBagislar(5);
            int itemWidth = availableWidth - 16;
            int yPos = 32;

            foreach (var bagis in sonBagislar)
            {
                var itemPanel = CreateDonationItem(bagis, itemWidth);
                itemPanel.Location = new Point(8, yPos);
                itemPanel.Size = new Size(itemWidth, 22);
                panel.Controls.Add(itemPanel);
                yPos += 24;
            }

            if (!sonBagislar.Any())
            {
                var noDataLabel = new LabelControl();
                noDataLabel.Appearance.Font = new Font("Segoe UI", 9F);
                noDataLabel.Appearance.ForeColor = Color.Gray;
                noDataLabel.Location = new Point(10, 60);
                noDataLabel.Text = "Henüz bağış bulunmuyor";
                panel.Controls.Add(noDataLabel);
            }

            return panel;
        }

        private PanelControl CreateDonationItem(Bagis bagis, int itemWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            int nameWidth = (int)(itemWidth * 0.35);
            int amountWidth = 100;

            var nameLabel = new LabelControl();
            nameLabel.Appearance.Font = new Font("Segoe UI", 8.5F);
            nameLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            nameLabel.Location = new Point(5, 3);
            nameLabel.AutoSizeMode = LabelAutoSizeMode.None;
            nameLabel.Size = new Size(nameWidth, 18);
            nameLabel.Text = bagis.BagisciAdSoyad.Length > (nameWidth / 7) ? bagis.BagisciAdSoyad.Substring(0, (nameWidth / 7) - 3) + "..." : bagis.BagisciAdSoyad;

            var amountLabel = new LabelControl();
            amountLabel.Appearance.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            amountLabel.Appearance.ForeColor = Color.FromArgb(40, 167, 69);
            amountLabel.Location = new Point(nameWidth + 10, 3);
            amountLabel.Text = $"{bagis.Tutar:N0} ₺";

            var dateLabel = new LabelControl();
            dateLabel.Appearance.Font = new Font("Segoe UI", 8F);
            dateLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
            dateLabel.Location = new Point(nameWidth + amountWidth + 15, 3);
            dateLabel.Text = bagis.BagisTarihi.ToString("dd.MM.yy HH:mm");

            var statusLabel = new LabelControl();
            statusLabel.Appearance.Font = new Font("Segoe UI", 8F);
            statusLabel.Location = new Point(itemWidth - 35, 3);
            statusLabel.Text = bagis.Durum == "Onaylandi" ? "✅" : bagis.Durum == "Beklemede" ? "⏳" : "❌";

            panel.Controls.Add(nameLabel);
            panel.Controls.Add(amountLabel);
            panel.Controls.Add(dateLabel);
            panel.Controls.Add(statusLabel);

            return panel;
        }

        private void UpdateRecentDonationsLayout(PanelControl panel, int availableWidth)
        {
            int itemWidth = availableWidth - 16;
            int yPos = 32;

            foreach (Control ctrl in panel.Controls)
            {
                if (ctrl is PanelControl itemPanel && ctrl != panel.Controls[0]) // İlk kontrol başlık
                {
                    itemPanel.Width = itemWidth;
                    itemPanel.Location = new Point(8, yPos);
                    yPos += 24;
                }
            }
        }

        private PanelControl CreateChartSection(int availableWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panel.Appearance.BackColor = Color.White;
            panel.Appearance.Options.UseBackColor = true;
            panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            panel.Appearance.Options.UseBorderColor = true;

            var titleLabel = new LabelControl();
            titleLabel.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            titleLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
            titleLabel.Location = new Point(10, 8);
            titleLabel.Text = "📊 Aylık Bağış Grafiği";
            panel.Controls.Add(titleLabel);

            var aylikBagislar = _bagisciService.GetAylikBagislar();
            
            // Her zaman grafik çiz (veri 0 olsa bile)
            decimal maxBagis = aylikBagislar.Values.Any() ? aylikBagislar.Values.Max() : 1;
            if (maxBagis == 0) maxBagis = 1; // Sıfıra bölme hatası engelle
            
            int chartWidth = availableWidth - 30;
            int barCount = aylikBagislar.Count;
            int barWidth = barCount > 0 ? Math.Max(60, chartWidth / barCount) : 90;
            int xPos = 15;
            int chartBottom = 115;
            int maxBarHeight = 65;

            // Ay isimleri
            var ayIsimleri = new Dictionary<string, string> {
                {"01", "Oca"}, {"02", "Şub"}, {"03", "Mar"}, {"04", "Nis"},
                {"05", "May"}, {"06", "Haz"}, {"07", "Tem"}, {"08", "Ağu"},
                {"09", "Eyl"}, {"10", "Eki"}, {"11", "Kas"}, {"12", "Ara"}
            };

            foreach (var item in aylikBagislar.OrderBy(x => x.Key))
            {
                var barHeight = (int)(maxBarHeight * (double)item.Value / (double)maxBagis);
                barHeight = Math.Max(barHeight, 5); // Minimum bar yüksekliği

                // Bar
                var bar = new PanelControl();
                bar.Location = new Point(xPos, chartBottom - barHeight);
                bar.Size = new Size(barWidth - 15, barHeight);
                bar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                bar.Appearance.BackColor = item.Value > 0 ? Color.FromArgb(40, 167, 69) : Color.FromArgb(200, 200, 200);
                bar.Appearance.Options.UseBackColor = true;
                panel.Controls.Add(bar);

                // Ay etiketi
                var monthKey = item.Key.Substring(5); // MM
                var monthLabel = new LabelControl();
                monthLabel.Appearance.Font = new Font("Segoe UI", 7.5F);
                monthLabel.Appearance.ForeColor = Color.FromArgb(108, 117, 125);
                monthLabel.Location = new Point(xPos + 10, chartBottom + 5);
                monthLabel.Text = ayIsimleri.ContainsKey(monthKey) ? ayIsimleri[monthKey] : monthKey;
                panel.Controls.Add(monthLabel);

                // Değer etiketi
                var valueLabel = new LabelControl();
                valueLabel.Appearance.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
                valueLabel.Appearance.ForeColor = Color.FromArgb(33, 37, 41);
                valueLabel.Location = new Point(xPos + 5, chartBottom - barHeight - 15);
                if (item.Value >= 1000)
                    valueLabel.Text = (item.Value / 1000).ToString("N1") + "K";
                else
                    valueLabel.Text = item.Value.ToString("N0");
                panel.Controls.Add(valueLabel);

                xPos += barWidth;
            }

            return panel;
        }

        private void UpdateChartLayout(PanelControl panel, int availableWidth)
        {
            // Chart section'ı yeniden oluşturmak daha kolay
            // Bu metod sadece placeholder, gerçek güncelleme CreateChartSection'da yapılıyor
        }

        private PanelControl CreateActionSection(int availableWidth)
        {
            var panel = new PanelControl();
            panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panel.Appearance.BackColor = Color.Transparent;
            panel.Appearance.Options.UseBackColor = true;

            var btnBagisYap = new SimpleButton();
            btnBagisYap.Text = "💳 Yeni Bağış Kaydet";
            btnBagisYap.Location = new Point(0, 5);
            btnBagisYap.Size = new Size(180, 40);
            btnBagisYap.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBagisYap.Appearance.BackColor = Color.FromArgb(40, 167, 69);
            btnBagisYap.Appearance.ForeColor = Color.White;
            btnBagisYap.Appearance.Options.UseBackColor = true;
            btnBagisYap.Appearance.Options.UseForeColor = true;
            btnBagisYap.Appearance.Options.UseFont = true;
            btnBagisYap.Click += BtnBagisYap_Click;
            panel.Controls.Add(btnBagisYap);

            var btnYenile = new SimpleButton();
            btnYenile.Text = "🔄 Yenile";
            btnYenile.Location = new Point(190, 5);
            btnYenile.Size = new Size(100, 40);
            btnYenile.Appearance.Font = new Font("Segoe UI", 9F);
            btnYenile.Click += (s, e) => CreateModernUI();
            panel.Controls.Add(btnYenile);

            return panel;
        }

        #endregion

        #region Events

        private void BtnBagisYap_Click(object? sender, EventArgs e)
        {
            using (var dialog = new XtraForm())
            {
                dialog.Text = "Yeni Bağış Kaydet";
                dialog.Size = new Size(400, 350);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                var lblBagisci = new LabelControl { Text = "Bağışçı:", Location = new Point(20, 25) };
                var cmbBagisci = new ComboBoxEdit { Location = new Point(120, 22), Size = new Size(240, 24) };
                cmbBagisci.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                var bagiscilar = _bagisciService.GetAll();
                foreach (var b in bagiscilar)
                    cmbBagisci.Properties.Items.Add($"{b.BagisciID}|{b.Ad} {b.Soyad}");

                var lblTutar = new LabelControl { Text = "Tutar (₺):", Location = new Point(20, 60) };
                var txtTutar = new TextEdit { Location = new Point(120, 57), Size = new Size(240, 24) };

                var lblAmac = new LabelControl { Text = "Bağış Amacı:", Location = new Point(20, 95) };
                var cmbAmac = new ComboBoxEdit { Location = new Point(120, 92), Size = new Size(240, 24) };
                cmbAmac.Properties.Items.AddRange(new[] { "Genel Burs Fonu", "Başarılı Öğrenciler", "İhtiyaç Sahibi Öğrenciler" });
                cmbAmac.SelectedIndex = 0;

                var lblOdeme = new LabelControl { Text = "Ödeme Yöntemi:", Location = new Point(20, 130) };
                var cmbOdeme = new ComboBoxEdit { Location = new Point(120, 127), Size = new Size(240, 24) };
                cmbOdeme.Properties.Items.AddRange(new[] { "Nakit", "Kredi Kartı", "Banka Havalesi" });
                cmbOdeme.SelectedIndex = 0;

                var chkAnonim = new CheckEdit { Text = "Anonim Bağış", Location = new Point(120, 165), Size = new Size(200, 24) };

                var lblNot = new LabelControl { Text = "Açıklama:", Location = new Point(20, 200) };
                var txtNot = new MemoEdit { Location = new Point(120, 197), Size = new Size(240, 60) };

                var btnKaydet = new SimpleButton { 
                    Text = "✅ Kaydet", 
                    Location = new Point(120, 270), 
                    Size = new Size(110, 35),
                    DialogResult = DialogResult.OK
                };
                btnKaydet.Appearance.BackColor = Color.FromArgb(40, 167, 69);
                btnKaydet.Appearance.ForeColor = Color.White;
                btnKaydet.Appearance.Options.UseBackColor = true;
                btnKaydet.Appearance.Options.UseForeColor = true;

                var btnIptal = new SimpleButton { 
                    Text = "❌ İptal", 
                    Location = new Point(240, 270), 
                    Size = new Size(110, 35),
                    DialogResult = DialogResult.Cancel
                };

                dialog.Controls.AddRange(new Control[] { 
                    lblBagisci, cmbBagisci, lblTutar, txtTutar, lblAmac, cmbAmac, 
                    lblOdeme, cmbOdeme, chkAnonim, lblNot, txtNot, btnKaydet, btnIptal 
                });

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (cmbBagisci.SelectedItem == null || string.IsNullOrEmpty(txtTutar.Text))
                        {
                            XtraMessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var bagisciIdStr = cmbBagisci.SelectedItem.ToString()?.Split('|')[0];
                        int bagisciId = int.Parse(bagisciIdStr ?? "0");
                        decimal tutar = decimal.Parse(txtTutar.Text.Replace(".", "").Replace(",", "."));
                        string aciklama = $"{cmbAmac.SelectedItem} - {txtNot.Text}";
                        string odemeYontemi = cmbOdeme.SelectedItem?.ToString()?.Replace(" ", "") ?? "Nakit";

                        _bagisciService.AddBagis(bagisciId, tutar, aciklama, odemeYontemi, chkAnonim.Checked);

                        // Etki mesajı hesapla
                        int desteklenenOgrenci = (int)(tutar / 2500); // Örnek: 2500 TL = 1 öğrenci 1 ay
                        string etkiMesaji = desteklenenOgrenci > 0 
                            ? $"Bu bağış ile {desteklenenOgrenci} öğrencinin 1 aylık bursu karşılandı! 🎓"
                            : "Bağışınız için teşekkür ederiz! 💚";

                        XtraMessageBox.Show(
                            $"✅ Bağış başarıyla kaydedildi!\n\n{etkiMesaji}\n\nBağış Tutarı: {tutar:N0} ₺",
                            "Teşekkürler!",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        CreateModernUI(); // Yenile
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion
    }
}
