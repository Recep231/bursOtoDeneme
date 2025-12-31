-- Başvuru Kontrol Script'i
-- Web'den eklenen başvuruların veritabanında olup olmadığını kontrol eder

USE BursOtomasyonDB;
GO

-- Tüm başvuruları listele
SELECT 
    b.BasvuruID,
    b.OgrenciID,
    o.Ad + ' ' + o.Soyad AS OgrenciAdSoyad,
    o.TC AS OgrenciTC,
    b.BasvuruDurumu,
    b.BasvuruTarihi,
    b.AIYorum,
    o.BursPuani
FROM Basvurular b
INNER JOIN Ogrenciler o ON b.OgrenciID = o.OgrenciID
ORDER BY b.BasvuruTarihi DESC;
GO

-- Son eklenen 5 öğrenciyi listele
SELECT TOP 5
    OgrenciID,
    Ad + ' ' + Soyad AS AdSoyad,
    TC,
    KayitTarihi,
    BursPuani
FROM Ogrenciler
ORDER BY KayitTarihi DESC;
GO

-- Başvurusu olmayan öğrencileri bul
SELECT 
    o.OgrenciID,
    o.Ad + ' ' + o.Soyad AS AdSoyad,
    o.TC,
    o.KayitTarihi
FROM Ogrenciler o
LEFT JOIN Basvurular b ON o.OgrenciID = b.OgrenciID
WHERE b.BasvuruID IS NULL
ORDER BY o.KayitTarihi DESC;
GO

-- Başvuru sayısını kontrol et
SELECT 
    COUNT(*) AS ToplamBasvuruSayisi,
    SUM(CASE WHEN BasvuruDurumu = 'Beklemede' THEN 1 ELSE 0 END) AS BeklemedeSayisi,
    SUM(CASE WHEN BasvuruDurumu = 'Onaylandı' THEN 1 ELSE 0 END) AS OnaylandiSayisi,
    SUM(CASE WHEN BasvuruDurumu = 'Reddedildi' THEN 1 ELSE 0 END) AS ReddedildiSayisi
FROM Basvurular;
GO

