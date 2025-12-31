-- Öğrenci Burs Yönetim Sistemi Veritabanı Şeması
-- SQL Server

USE master;
GO

-- Veritabanı oluştur
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'BursOtomasyonDB')
BEGIN
    CREATE DATABASE BursOtomasyonDB;
END
GO

USE BursOtomasyonDB;
GO

-- Adminler Tablosu
IF OBJECT_ID('Adminler', 'U') IS NOT NULL
    DROP TABLE Adminler;
GO

CREATE TABLE Adminler (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    KullaniciAdi NVARCHAR(50) NOT NULL UNIQUE,
    Sifre NVARCHAR(255) NOT NULL,
    OlusturmaTarihi DATETIME DEFAULT GETDATE()
);
GO

-- Öğrenciler Tablosu
IF OBJECT_ID('Ogrenciler', 'U') IS NOT NULL
    DROP TABLE Ogrenciler;
GO

CREATE TABLE Ogrenciler (
    OgrenciID INT IDENTITY(1,1) PRIMARY KEY,
    Ad NVARCHAR(50) NOT NULL,
    Soyad NVARCHAR(50) NOT NULL,
    TC NVARCHAR(11) NOT NULL UNIQUE,
    DogumTarihi DATE NOT NULL,
    Telefon NVARCHAR(20),
    Email NVARCHAR(100),
    Universite NVARCHAR(100),
    Fakulte NVARCHAR(100),
    Bolum NVARCHAR(100),
    Sinif NVARCHAR(20) CHECK (Sinif IN ('Hazırlık', '1', '2', '3', '4')),
    NotOrtalamasi DECIMAL(3,2) CHECK (NotOrtalamasi >= 0.00 AND NotOrtalamasi <= 4.00),
    KardesSayisi NVARCHAR(10) CHECK (KardesSayisi IN ('0', '1', '2', '3', '4', '4+')),
    AileGeliri DECIMAL(18,2),
    AnneMeslek NVARCHAR(100),
    BabaMeslek NVARCHAR(100),
    BursPuani DECIMAL(5,2) DEFAULT 0.00,
    KayitTarihi DATETIME DEFAULT GETDATE(),
    ProfilFotoYolu NVARCHAR(500),
    KlasikSoru1Cevap NVARCHAR(MAX),
    KlasikSoru2Cevap NVARCHAR(MAX),
    KlasikSoru3Cevap NVARCHAR(MAX)
);
GO

-- Mevcut tabloya kolon ekleme (eğer tablo zaten varsa)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Ogrenciler')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'ProfilFotoYolu')
    BEGIN
        ALTER TABLE Ogrenciler ADD ProfilFotoYolu NVARCHAR(500);
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'KlasikSoru1Cevap')
    BEGIN
        ALTER TABLE Ogrenciler ADD KlasikSoru1Cevap NVARCHAR(MAX);
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'KlasikSoru2Cevap')
    BEGIN
        ALTER TABLE Ogrenciler ADD KlasikSoru2Cevap NVARCHAR(MAX);
    END
    
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Ogrenciler') AND name = 'KlasikSoru3Cevap')
    BEGIN
        ALTER TABLE Ogrenciler ADD KlasikSoru3Cevap NVARCHAR(MAX);
    END
END
GO

-- Başvurular Tablosu
IF OBJECT_ID('Basvurular', 'U') IS NOT NULL
    DROP TABLE Basvurular;
GO

CREATE TABLE Basvurular (
    BasvuruID INT IDENTITY(1,1) PRIMARY KEY,
    OgrenciID INT NOT NULL,
    BasvuruDurumu NVARCHAR(20) DEFAULT 'Beklemede' CHECK (BasvuruDurumu IN ('Beklemede', 'Onaylandı', 'Reddedildi')),
    BasvuruTarihi DATETIME DEFAULT GETDATE(),
    AIYorum NVARCHAR(MAX),
    OnayTarihi DATETIME,
    OnaylayanAdminID INT,
    FOREIGN KEY (OgrenciID) REFERENCES Ogrenciler(OgrenciID) ON DELETE CASCADE,
    FOREIGN KEY (OnaylayanAdminID) REFERENCES Adminler(AdminID)
);
GO

-- İndeksler
CREATE INDEX IX_Ogrenciler_TC ON Ogrenciler(TC);
CREATE INDEX IX_Basvurular_OgrenciID ON Basvurular(OgrenciID);
CREATE INDEX IX_Basvurular_Durum ON Basvurular(BasvuruDurumu);
GO

-- Varsayılan Admin Kullanıcısı
INSERT INTO Adminler (KullaniciAdi, Sifre) 
VALUES ('admin', 'admin123'); -- Gerçek uygulamada hash'lenmiş olmalı
GO

PRINT 'Veritabanı şeması başarıyla oluşturuldu!';
GO

