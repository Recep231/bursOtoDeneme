-- Klasik Sorular ve Profil Fotoğrafı Kolonlarını Ekleme Migration Script'i
-- Bu script'i SQL Server Management Studio'da veya SQL komut satırında çalıştırın

USE BursOtomasyonDB;
GO

-- ProfilFotoYolu kolonunu ekle (eğer yoksa)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'ProfilFotoYolu')
BEGIN
    ALTER TABLE Ogrenciler ADD ProfilFotoYolu NVARCHAR(500);
    PRINT 'ProfilFotoYolu kolonu eklendi.';
END
ELSE
BEGIN
    PRINT 'ProfilFotoYolu kolonu zaten mevcut.';
END
GO

-- KlasikSoru1Cevap kolonunu ekle (eğer yoksa)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'KlasikSoru1Cevap')
BEGIN
    ALTER TABLE Ogrenciler ADD KlasikSoru1Cevap NVARCHAR(MAX);
    PRINT 'KlasikSoru1Cevap kolonu eklendi.';
END
ELSE
BEGIN
    PRINT 'KlasikSoru1Cevap kolonu zaten mevcut.';
END
GO

-- KlasikSoru2Cevap kolonunu ekle (eğer yoksa)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'KlasikSoru2Cevap')
BEGIN
    ALTER TABLE Ogrenciler ADD KlasikSoru2Cevap NVARCHAR(MAX);
    PRINT 'KlasikSoru2Cevap kolonu eklendi.';
END
ELSE
BEGIN
    PRINT 'KlasikSoru2Cevap kolonu zaten mevcut.';
END
GO

-- KlasikSoru3Cevap kolonunu ekle (eğer yoksa)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'KlasikSoru3Cevap')
BEGIN
    ALTER TABLE Ogrenciler ADD KlasikSoru3Cevap NVARCHAR(MAX);
    PRINT 'KlasikSoru3Cevap kolonu eklendi.';
END
ELSE
BEGIN
    PRINT 'KlasikSoru3Cevap kolonu zaten mevcut.';
END
GO

PRINT 'Migration tamamlandı! Tüm kolonlar başarıyla eklendi.';
GO

-- Kolonların eklendiğini doğrula
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Ogrenciler' 
    AND COLUMN_NAME IN ('ProfilFotoYolu', 'KlasikSoru1Cevap', 'KlasikSoru2Cevap', 'KlasikSoru3Cevap')
ORDER BY COLUMN_NAME;
GO
