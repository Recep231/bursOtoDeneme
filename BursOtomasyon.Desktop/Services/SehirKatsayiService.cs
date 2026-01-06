using System;
using System.Collections.Generic;
using System.Linq;

namespace BursOtomasyon.Desktop.Services
{
    public class SehirKatsayiService
    {
        // Üniversite -> Şehir mapping
        private readonly Dictionary<string, string> _universiteSehirMap;
        
        // Şehir -> Yaşam Maliyeti Katsayısı mapping
        private readonly Dictionary<string, decimal> _sehirKatsayiMap;

        public SehirKatsayiService()
        {
            _universiteSehirMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _sehirKatsayiMap = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            // Üniversite -> Şehir Mapping
            _universiteSehirMap["İstanbul Üniversitesi"] = "İstanbul";
            _universiteSehirMap["İstanbul Teknik Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Boğaziçi Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Galatasaray Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Sabancı Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Koç Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Yeditepe Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Bahçeşehir Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Marmara Üniversitesi"] = "İstanbul";
            _universiteSehirMap["Yıldız Teknik Üniversitesi"] = "İstanbul";
            _universiteSehirMap["İstanbul"] = "İstanbul"; // Genel eşleşme
            
            _universiteSehirMap["Ankara Üniversitesi"] = "Ankara";
            _universiteSehirMap["Orta Doğu Teknik Üniversitesi"] = "Ankara";
            _universiteSehirMap["Hacettepe Üniversitesi"] = "Ankara";
            _universiteSehirMap["Gazi Üniversitesi"] = "Ankara";
            _universiteSehirMap["Bilkent Üniversitesi"] = "Ankara";
            _universiteSehirMap["Başkent Üniversitesi"] = "Ankara";
            _universiteSehirMap["Ankara"] = "Ankara";
            
            _universiteSehirMap["Ege Üniversitesi"] = "İzmir";
            _universiteSehirMap["Dokuz Eylül Üniversitesi"] = "İzmir";
            _universiteSehirMap["İzmir Yüksek Teknoloji Enstitüsü"] = "İzmir";
            _universiteSehirMap["İzmir"] = "İzmir";
            
            _universiteSehirMap["Fırat Üniversitesi"] = "Elazığ";
            _universiteSehirMap["Elazığ"] = "Elazığ";
            
            _universiteSehirMap["Atatürk Üniversitesi"] = "Erzurum";
            _universiteSehirMap["Erzurum"] = "Erzurum";
            
            _universiteSehirMap["Çukurova Üniversitesi"] = "Adana";
            _universiteSehirMap["Adana"] = "Adana";
            
            _universiteSehirMap["Akdeniz Üniversitesi"] = "Antalya";
            _universiteSehirMap["Antalya"] = "Antalya";
            
            _universiteSehirMap["Selçuk Üniversitesi"] = "Konya";
            _universiteSehirMap["Konya"] = "Konya";
            
            _universiteSehirMap["Ondokuz Mayıs Üniversitesi"] = "Samsun";
            _universiteSehirMap["Samsun"] = "Samsun";
            
            _universiteSehirMap["Karadeniz Teknik Üniversitesi"] = "Trabzon";
            _universiteSehirMap["Trabzon"] = "Trabzon";
            
            _universiteSehirMap["Dicle Üniversitesi"] = "Diyarbakır";
            _universiteSehirMap["Diyarbakır"] = "Diyarbakır";
            
            _universiteSehirMap["Gaziantep Üniversitesi"] = "Gaziantep";
            _universiteSehirMap["Gaziantep"] = "Gaziantep";
            
            _universiteSehirMap["Pamukkale Üniversitesi"] = "Denizli";
            _universiteSehirMap["Denizli"] = "Denizli";
            
            _universiteSehirMap["Erciyes Üniversitesi"] = "Kayseri";
            _universiteSehirMap["Kayseri"] = "Kayseri";
            
            _universiteSehirMap["Uludağ Üniversitesi"] = "Bursa";
            _universiteSehirMap["Bursa"] = "Bursa";
            
            _universiteSehirMap["Süleyman Demirel Üniversitesi"] = "Isparta";
            _universiteSehirMap["Isparta"] = "Isparta";
            
            _universiteSehirMap["Mersin Üniversitesi"] = "Mersin";
            _universiteSehirMap["Mersin"] = "Mersin";
            
            _universiteSehirMap["Kocaeli Üniversitesi"] = "Kocaeli";
            _universiteSehirMap["Kocaeli"] = "Kocaeli";
            
            _universiteSehirMap["Sakarya Üniversitesi"] = "Sakarya";
            _universiteSehirMap["Sakarya"] = "Sakarya";
            
            _universiteSehirMap["Eskişehir Osmangazi Üniversitesi"] = "Eskişehir";
            _universiteSehirMap["Anadolu Üniversitesi"] = "Eskişehir";
            _universiteSehirMap["Eskişehir"] = "Eskişehir";
            
            _universiteSehirMap["Çanakkale Onsekiz Mart Üniversitesi"] = "Çanakkale";
            _universiteSehirMap["Çanakkale"] = "Çanakkale";
            
            _universiteSehirMap["Muğla Sıtkı Koçman Üniversitesi"] = "Muğla";
            _universiteSehirMap["Muğla"] = "Muğla";
            
            _universiteSehirMap["Balıkesir Üniversitesi"] = "Balıkesir";
            _universiteSehirMap["Balıkesir"] = "Balıkesir";
            
            _universiteSehirMap["Trakya Üniversitesi"] = "Edirne";
            _universiteSehirMap["Edirne"] = "Edirne";
            
            _universiteSehirMap["Kırıkkale Üniversitesi"] = "Kırıkkale";
            _universiteSehirMap["Kırıkkale"] = "Kırıkkale";
            
            _universiteSehirMap["Afyon Kocatepe Üniversitesi"] = "Afyon";
            _universiteSehirMap["Afyon"] = "Afyon";
            
            _universiteSehirMap["Kütahya Dumlupınar Üniversitesi"] = "Kütahya";
            _universiteSehirMap["Kütahya"] = "Kütahya";
            
            _universiteSehirMap["Nevşehir Hacı Bektaş Veli Üniversitesi"] = "Nevşehir";
            _universiteSehirMap["Nevşehir"] = "Nevşehir";
            
            _universiteSehirMap["Aksaray Üniversitesi"] = "Aksaray";
            _universiteSehirMap["Aksaray"] = "Aksaray";
            
            _universiteSehirMap["Niğde Ömer Halisdemir Üniversitesi"] = "Niğde";
            _universiteSehirMap["Niğde"] = "Niğde";
            
            _universiteSehirMap["Kastamonu Üniversitesi"] = "Kastamonu";
            _universiteSehirMap["Kastamonu"] = "Kastamonu";
            
            _universiteSehirMap["Zonguldak Bülent Ecevit Üniversitesi"] = "Zonguldak";
            _universiteSehirMap["Zonguldak"] = "Zonguldak";
            
            _universiteSehirMap["Bartın Üniversitesi"] = "Bartın";
            _universiteSehirMap["Bartın"] = "Bartın";
            
            _universiteSehirMap["Sinop Üniversitesi"] = "Sinop";
            _universiteSehirMap["Sinop"] = "Sinop";
            
            _universiteSehirMap["Amasya Üniversitesi"] = "Amasya";
            _universiteSehirMap["Amasya"] = "Amasya";
            
            _universiteSehirMap["Tokat Gaziosmanpaşa Üniversitesi"] = "Tokat";
            _universiteSehirMap["Tokat"] = "Tokat";
            
            _universiteSehirMap["Sivas Cumhuriyet Üniversitesi"] = "Sivas";
            _universiteSehirMap["Sivas"] = "Sivas";
            
            _universiteSehirMap["Yozgat Bozok Üniversitesi"] = "Yozgat";
            _universiteSehirMap["Yozgat"] = "Yozgat";
            
            _universiteSehirMap["Kırşehir Ahi Evran Üniversitesi"] = "Kırşehir";
            _universiteSehirMap["Kırşehir"] = "Kırşehir";
            
            _universiteSehirMap["Nevşehir Hacı Bektaş Veli Üniversitesi"] = "Nevşehir";
            _universiteSehirMap["Nevşehir"] = "Nevşehir";
            
            _universiteSehirMap["Kayseri Erciyes Üniversitesi"] = "Kayseri";
            _universiteSehirMap["Kayseri"] = "Kayseri";
            
            _universiteSehirMap["Malatya İnönü Üniversitesi"] = "Malatya";
            _universiteSehirMap["Malatya"] = "Malatya";
            
            _universiteSehirMap["Kahramanmaraş Sütçü İmam Üniversitesi"] = "Kahramanmaraş";
            _universiteSehirMap["Kahramanmaraş"] = "Kahramanmaraş";
            
            _universiteSehirMap["Hatay Mustafa Kemal Üniversitesi"] = "Hatay";
            _universiteSehirMap["Hatay"] = "Hatay";
            
            _universiteSehirMap["Osmaniye Korkut Ata Üniversitesi"] = "Osmaniye";
            _universiteSehirMap["Osmaniye"] = "Osmaniye";
            
            _universiteSehirMap["Adıyaman Üniversitesi"] = "Adıyaman";
            _universiteSehirMap["Adıyaman"] = "Adıyaman";
            
            _universiteSehirMap["Şanlıurfa Harran Üniversitesi"] = "Şanlıurfa";
            _universiteSehirMap["Şanlıurfa"] = "Şanlıurfa";
            
            _universiteSehirMap["Mardin Artuklu Üniversitesi"] = "Mardin";
            _universiteSehirMap["Mardin"] = "Mardin";
            
            _universiteSehirMap["Batman Üniversitesi"] = "Batman";
            _universiteSehirMap["Batman"] = "Batman";
            
            _universiteSehirMap["Siirt Üniversitesi"] = "Siirt";
            _universiteSehirMap["Siirt"] = "Siirt";
            
            _universiteSehirMap["Şırnak Üniversitesi"] = "Şırnak";
            _universiteSehirMap["Şırnak"] = "Şırnak";
            
            _universiteSehirMap["Hakkari Üniversitesi"] = "Hakkari";
            _universiteSehirMap["Hakkari"] = "Hakkari";
            
            _universiteSehirMap["Van Yüzüncü Yıl Üniversitesi"] = "Van";
            _universiteSehirMap["Van"] = "Van";
            
            _universiteSehirMap["Muş Alparslan Üniversitesi"] = "Muş";
            _universiteSehirMap["Muş"] = "Muş";
            
            _universiteSehirMap["Bitlis Eren Üniversitesi"] = "Bitlis";
            _universiteSehirMap["Bitlis"] = "Bitlis";
            
            _universiteSehirMap["Ağrı İbrahim Çeçen Üniversitesi"] = "Ağrı";
            _universiteSehirMap["Ağrı"] = "Ağrı";
            
            _universiteSehirMap["Ardahan Üniversitesi"] = "Ardahan";
            _universiteSehirMap["Ardahan"] = "Ardahan";
            
            _universiteSehirMap["Artvin Çoruh Üniversitesi"] = "Artvin";
            _universiteSehirMap["Artvin"] = "Artvin";
            
            _universiteSehirMap["Rize Recep Tayyip Erdoğan Üniversitesi"] = "Rize";
            _universiteSehirMap["Rize"] = "Rize";
            
            _universiteSehirMap["Giresun Üniversitesi"] = "Giresun";
            _universiteSehirMap["Giresun"] = "Giresun";
            
            _universiteSehirMap["Ordu Üniversitesi"] = "Ordu";
            _universiteSehirMap["Ordu"] = "Ordu";
            
            _universiteSehirMap["Gümüşhane Üniversitesi"] = "Gümüşhane";
            _universiteSehirMap["Gümüşhane"] = "Gümüşhane";
            
            _universiteSehirMap["Bayburt Üniversitesi"] = "Bayburt";
            _universiteSehirMap["Bayburt"] = "Bayburt";
            
            _universiteSehirMap["Erzincan Binali Yıldırım Üniversitesi"] = "Erzincan";
            _universiteSehirMap["Erzincan"] = "Erzincan";
            
            _universiteSehirMap["Tunceli Üniversitesi"] = "Tunceli";
            _universiteSehirMap["Tunceli"] = "Tunceli";
            
            _universiteSehirMap["Bingöl Üniversitesi"] = "Bingöl";
            _universiteSehirMap["Bingöl"] = "Bingöl";
            
            _universiteSehirMap["Burdur Mehmet Akif Ersoy Üniversitesi"] = "Burdur";
            _universiteSehirMap["Burdur"] = "Burdur";
            
            _universiteSehirMap["Isparta Uygulamalı Bilimler Üniversitesi"] = "Isparta";
            
            _universiteSehirMap["Uşak Üniversitesi"] = "Uşak";
            _universiteSehirMap["Uşak"] = "Uşak";
            
            _universiteSehirMap["Manisa Celal Bayar Üniversitesi"] = "Manisa";
            _universiteSehirMap["Manisa"] = "Manisa";
            
            _universiteSehirMap["Aydın Adnan Menderes Üniversitesi"] = "Aydın";
            _universiteSehirMap["Aydın"] = "Aydın";
            
            _universiteSehirMap["Muğla"] = "Muğla";
            
            _universiteSehirMap["Bilecik Şeyh Edebali Üniversitesi"] = "Bilecik";
            _universiteSehirMap["Bilecik"] = "Bilecik";
            
            _universiteSehirMap["Eskişehir Teknik Üniversitesi"] = "Eskişehir";
            
            _universiteSehirMap["Bolu Abant İzzet Baysal Üniversitesi"] = "Bolu";
            _universiteSehirMap["Bolu"] = "Bolu";
            
            _universiteSehirMap["Düzce Üniversitesi"] = "Düzce";
            _universiteSehirMap["Düzce"] = "Düzce";
            
            _universiteSehirMap["Karabük Üniversitesi"] = "Karabük";
            _universiteSehirMap["Karabük"] = "Karabük";
            
            _universiteSehirMap["Çankırı Karatekin Üniversitesi"] = "Çankırı";
            _universiteSehirMap["Çankırı"] = "Çankırı";
            
            _universiteSehirMap["Yalova Üniversitesi"] = "Yalova";
            _universiteSehirMap["Yalova"] = "Yalova";
            
            _universiteSehirMap["Tekirdağ Namık Kemal Üniversitesi"] = "Tekirdağ";
            _universiteSehirMap["Tekirdağ"] = "Tekirdağ";
            
            _universiteSehirMap["Kırklareli Üniversitesi"] = "Kırklareli";
            _universiteSehirMap["Kırklareli"] = "Kırklareli";
            
            _universiteSehirMap["Çorum Hitit Üniversitesi"] = "Çorum";
            _universiteSehirMap["Çorum"] = "Çorum";
            
            _universiteSehirMap["Samsun Ondokuz Mayıs Üniversitesi"] = "Samsun";
            
            _universiteSehirMap["Gümüşhane"] = "Gümüşhane";
            
            _universiteSehirMap["Rize"] = "Rize";
            
            _universiteSehirMap["Ordu"] = "Ordu";
            
            _universiteSehirMap["Giresun"] = "Giresun";
            
            _universiteSehirMap["Trabzon"] = "Trabzon";
            
            _universiteSehirMap["Artvin"] = "Artvin";
            
            _universiteSehirMap["Ardahan"] = "Ardahan";
            
            _universiteSehirMap["Kars Kafkas Üniversitesi"] = "Kars";
            _universiteSehirMap["Kars"] = "Kars";
            
            _universiteSehirMap["Iğdır Üniversitesi"] = "Iğdır";
            _universiteSehirMap["Iğdır"] = "Iğdır";
            
            _universiteSehirMap["Ağrı"] = "Ağrı";
            
            _universiteSehirMap["Erzurum Atatürk Üniversitesi"] = "Erzurum";
            
            _universiteSehirMap["Erzincan"] = "Erzincan";
            
            _universiteSehirMap["Tunceli"] = "Tunceli";
            
            _universiteSehirMap["Bingöl"] = "Bingöl";
            
            _universiteSehirMap["Muş"] = "Muş";
            
            _universiteSehirMap["Bitlis"] = "Bitlis";
            
            _universiteSehirMap["Van"] = "Van";
            
            _universiteSehirMap["Hakkari"] = "Hakkari";
            
            _universiteSehirMap["Şırnak"] = "Şırnak";
            
            _universiteSehirMap["Siirt"] = "Siirt";
            
            _universiteSehirMap["Batman"] = "Batman";
            
            _universiteSehirMap["Mardin"] = "Mardin";
            
            _universiteSehirMap["Şanlıurfa"] = "Şanlıurfa";
            
            _universiteSehirMap["Diyarbakır"] = "Diyarbakır";
            
            _universiteSehirMap["Adıyaman"] = "Adıyaman";
            
            _universiteSehirMap["Osmaniye"] = "Osmaniye";
            
            _universiteSehirMap["Hatay"] = "Hatay";
            
            _universiteSehirMap["Kahramanmaraş"] = "Kahramanmaraş";
            
            _universiteSehirMap["Malatya"] = "Malatya";
            
            _universiteSehirMap["Elazığ"] = "Elazığ";
            
            _universiteSehirMap["Tunceli"] = "Tunceli";
            
            _universiteSehirMap["Bingöl"] = "Bingöl";
            
            _universiteSehirMap["Muş"] = "Muş";
            
            _universiteSehirMap["Bitlis"] = "Bitlis";
            
            _universiteSehirMap["Van"] = "Van";
            
            _universiteSehirMap["Hakkari"] = "Hakkari";
            
            _universiteSehirMap["Şırnak"] = "Şırnak";
            
            _universiteSehirMap["Siirt"] = "Siirt";
            
            _universiteSehirMap["Batman"] = "Batman";
            
            _universiteSehirMap["Mardin"] = "Mardin";
            
            _universiteSehirMap["Şanlıurfa"] = "Şanlıurfa";
            
            _universiteSehirMap["Diyarbakır"] = "Diyarbakır";
            
            _universiteSehirMap["Adıyaman"] = "Adıyaman";
            
            _universiteSehirMap["Osmaniye"] = "Osmaniye";
            
            _universiteSehirMap["Hatay"] = "Hatay";
            
            _universiteSehirMap["Kahramanmaraş"] = "Kahramanmaraş";
            
            _universiteSehirMap["Malatya"] = "Malatya";
            
            _universiteSehirMap["Elazığ"] = "Elazığ";
            
            _universiteSehirMap["Tunceli"] = "Tunceli";
            
            _universiteSehirMap["Bingöl"] = "Bingöl";
            
            _universiteSehirMap["Muş"] = "Muş";
            
            _universiteSehirMap["Bitlis"] = "Bitlis";
            
            _universiteSehirMap["Van"] = "Van";
            
            _universiteSehirMap["Hakkari"] = "Hakkari";
            
            _universiteSehirMap["Şırnak"] = "Şırnak";
            
            _universiteSehirMap["Siirt"] = "Siirt";
            
            _universiteSehirMap["Batman"] = "Batman";
            
            _universiteSehirMap["Mardin"] = "Mardin";
            
            _universiteSehirMap["Şanlıurfa"] = "Şanlıurfa";
            
            _universiteSehirMap["Diyarbakır"] = "Diyarbakır";
            
            _universiteSehirMap["Adıyaman"] = "Adıyaman";
            
            // Şehir -> Yaşam Maliyeti Katsayısı Mapping
            // Yüksek yaşam maliyeti = yüksek katsayı (daha fazla burs gerekir)
            _sehirKatsayiMap["İstanbul"] = 1.30m; // En yüksek
            _sehirKatsayiMap["Ankara"] = 1.15m;
            _sehirKatsayiMap["İzmir"] = 1.20m;
            _sehirKatsayiMap["Antalya"] = 1.10m;
            _sehirKatsayiMap["Bursa"] = 1.08m;
            _sehirKatsayiMap["Kocaeli"] = 1.05m;
            _sehirKatsayiMap["Adana"] = 1.03m;
            _sehirKatsayiMap["Gaziantep"] = 1.02m;
            _sehirKatsayiMap["Konya"] = 1.00m;
            _sehirKatsayiMap["Mersin"] = 1.00m;
            _sehirKatsayiMap["Eskişehir"] = 1.00m;
            _sehirKatsayiMap["Samsun"] = 0.98m;
            _sehirKatsayiMap["Trabzon"] = 0.98m;
            _sehirKatsayiMap["Diyarbakır"] = 0.95m;
            _sehirKatsayiMap["Kayseri"] = 0.95m;
            _sehirKatsayiMap["Denizli"] = 0.95m;
            _sehirKatsayiMap["Elazığ"] = 0.90m; // Düşük yaşam maliyeti
            _sehirKatsayiMap["Erzurum"] = 0.90m;
            _sehirKatsayiMap["Malatya"] = 0.92m;
            _sehirKatsayiMap["Kahramanmaraş"] = 0.92m;
            _sehirKatsayiMap["Hatay"] = 0.93m;
            _sehirKatsayiMap["Osmaniye"] = 0.90m;
            _sehirKatsayiMap["Adıyaman"] = 0.88m;
            _sehirKatsayiMap["Şanlıurfa"] = 0.90m;
            _sehirKatsayiMap["Mardin"] = 0.88m;
            _sehirKatsayiMap["Batman"] = 0.88m;
            _sehirKatsayiMap["Siirt"] = 0.85m;
            _sehirKatsayiMap["Şırnak"] = 0.85m;
            _sehirKatsayiMap["Hakkari"] = 0.85m;
            _sehirKatsayiMap["Van"] = 0.88m;
            _sehirKatsayiMap["Muş"] = 0.85m;
            _sehirKatsayiMap["Bitlis"] = 0.85m;
            _sehirKatsayiMap["Ağrı"] = 0.85m;
            _sehirKatsayiMap["Ardahan"] = 0.80m;
            _sehirKatsayiMap["Artvin"] = 0.85m;
            _sehirKatsayiMap["Rize"] = 0.90m;
            _sehirKatsayiMap["Giresun"] = 0.88m;
            _sehirKatsayiMap["Ordu"] = 0.88m;
            _sehirKatsayiMap["Gümüşhane"] = 0.85m;
            _sehirKatsayiMap["Bayburt"] = 0.80m;
            _sehirKatsayiMap["Erzincan"] = 0.88m;
            _sehirKatsayiMap["Tunceli"] = 0.85m;
            _sehirKatsayiMap["Bingöl"] = 0.85m;
            _sehirKatsayiMap["Burdur"] = 0.90m;
            _sehirKatsayiMap["Isparta"] = 0.90m;
            _sehirKatsayiMap["Uşak"] = 0.90m;
            _sehirKatsayiMap["Manisa"] = 0.95m;
            _sehirKatsayiMap["Aydın"] = 0.95m;
            _sehirKatsayiMap["Muğla"] = 0.95m;
            _sehirKatsayiMap["Bilecik"] = 0.93m;
            _sehirKatsayiMap["Bolu"] = 0.90m;
            _sehirKatsayiMap["Düzce"] = 0.90m;
            _sehirKatsayiMap["Karabük"] = 0.88m;
            _sehirKatsayiMap["Çankırı"] = 0.88m;
            _sehirKatsayiMap["Yalova"] = 1.05m;
            _sehirKatsayiMap["Tekirdağ"] = 1.00m;
            _sehirKatsayiMap["Kırklareli"] = 0.95m;
            _sehirKatsayiMap["Çorum"] = 0.90m;
            _sehirKatsayiMap["Kars"] = 0.85m;
            _sehirKatsayiMap["Iğdır"] = 0.85m;
            _sehirKatsayiMap["Sakarya"] = 1.00m;
            _sehirKatsayiMap["Balıkesir"] = 0.95m;
            _sehirKatsayiMap["Edirne"] = 0.95m;
            _sehirKatsayiMap["Kırıkkale"] = 0.90m;
            _sehirKatsayiMap["Afyon"] = 0.90m;
            _sehirKatsayiMap["Kütahya"] = 0.90m;
            _sehirKatsayiMap["Nevşehir"] = 0.90m;
            _sehirKatsayiMap["Aksaray"] = 0.88m;
            _sehirKatsayiMap["Niğde"] = 0.88m;
            _sehirKatsayiMap["Kastamonu"] = 0.88m;
            _sehirKatsayiMap["Zonguldak"] = 0.90m;
            _sehirKatsayiMap["Bartın"] = 0.88m;
            _sehirKatsayiMap["Sinop"] = 0.85m;
            _sehirKatsayiMap["Amasya"] = 0.88m;
            _sehirKatsayiMap["Tokat"] = 0.88m;
            _sehirKatsayiMap["Sivas"] = 0.88m;
            _sehirKatsayiMap["Yozgat"] = 0.85m;
            _sehirKatsayiMap["Kırşehir"] = 0.88m;
            _sehirKatsayiMap["Çanakkale"] = 0.95m;
        }

        /// <summary>
        /// Üniversite adına göre şehir bilgisini döndürür
        /// </summary>
        public string? GetSehirByUniversite(string? universite)
        {
            if (string.IsNullOrWhiteSpace(universite))
                return null;

            var universiteTrim = universite.Trim();
            var universiteLower = universiteTrim.ToLowerInvariant();

            // 1. Önce tam eşleşme dene
            if (_universiteSehirMap.TryGetValue(universiteTrim, out string? sehir))
            {
                return sehir;
            }

            // 2. Üniversite adında şehir adı geçiyor mu kontrol et
            foreach (var kvp in _sehirKatsayiMap)
            {
                var sehirAdi = kvp.Key.ToLowerInvariant();
                if (universiteLower.Contains(sehirAdi))
                {
                    return kvp.Key;
                }
            }

            // 3. Kısmi eşleşme dene (map'teki üniversite adı içinde geçiyorsa)
            foreach (var kvp in _universiteSehirMap)
            {
                var keyLower = kvp.Key.ToLowerInvariant();
                if (keyLower.Contains(universiteLower) || universiteLower.Contains(keyLower))
                {
                    return kvp.Value;
                }
            }

            // 4. Şehir adı direkt girilmiş olabilir
            if (_sehirKatsayiMap.ContainsKey(universiteTrim))
            {
                return universiteTrim;
            }

            // 5. Büyük şehirlerin farklı yazılışlarını kontrol et
            if (universiteLower.Contains("istanbul") || universiteLower.Contains("ist"))
                return "İstanbul";
            if (universiteLower.Contains("ankara") || universiteLower.Contains("ank"))
                return "Ankara";
            if (universiteLower.Contains("izmir"))
                return "İzmir";
            if (universiteLower.Contains("bursa"))
                return "Bursa";
            if (universiteLower.Contains("antalya"))
                return "Antalya";
            if (universiteLower.Contains("adana"))
                return "Adana";
            if (universiteLower.Contains("konya"))
                return "Konya";
            if (universiteLower.Contains("gaziantep"))
                return "Gaziantep";
            if (universiteLower.Contains("kayseri"))
                return "Kayseri";
            if (universiteLower.Contains("eskisehir") || universiteLower.Contains("eskişehir"))
                return "Eskişehir";
            if (universiteLower.Contains("trabzon"))
                return "Trabzon";
            if (universiteLower.Contains("samsun"))
                return "Samsun";
            if (universiteLower.Contains("erzurum"))
                return "Erzurum";
            if (universiteLower.Contains("elazig") || universiteLower.Contains("elazığ"))
                return "Elazığ";
            if (universiteLower.Contains("diyarbakir") || universiteLower.Contains("diyarbakır"))
                return "Diyarbakır";
            if (universiteLower.Contains("malatya"))
                return "Malatya";
            if (universiteLower.Contains("van"))
                return "Van";

            // Bulunamazsa null döndür (varsayılan 1.00 katsayı kullanılacak)
            return null;
        }

        /// <summary>
        /// Şehir adına göre yaşam maliyeti katsayısını döndürür
        /// </summary>
        public decimal GetKatsayiBySehir(string? sehir)
        {
            if (string.IsNullOrWhiteSpace(sehir))
                return 1.00m; // Varsayılan katsayı

            if (_sehirKatsayiMap.TryGetValue(sehir.Trim(), out decimal katsayi))
            {
                return katsayi;
            }

            return 1.00m; // Varsayılan katsayı (bulunamazsa)
        }

        /// <summary>
        /// Üniversite adına göre direkt yaşam maliyeti katsayısını döndürür
        /// </summary>
        public decimal GetKatsayiByUniversite(string? universite)
        {
            var sehir = GetSehirByUniversite(universite);
            return GetKatsayiBySehir(sehir);
        }

        /// <summary>
        /// Tüm şehir katsayılarını döndürür (admin paneli için)
        /// </summary>
        public Dictionary<string, decimal> GetAllSehirKatsayilari()
        {
            return new Dictionary<string, decimal>(_sehirKatsayiMap);
        }

        /// <summary>
        /// Tüm üniversite-şehir eşleşmelerini döndürür (admin paneli için)
        /// </summary>
        public Dictionary<string, string> GetAllUniversiteSehirMap()
        {
            return new Dictionary<string, string>(_universiteSehirMap);
        }
    }
}

