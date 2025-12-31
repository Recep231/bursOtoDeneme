-- Bağış Sistemi Veritabanı Şeması
-- Bu script'i mevcut veritabanına ekleyin

USE BursOtomasyonDB;
GO

-- Bağışçılar Tablosu
IF OBJECT_ID('Bagiscilar', 'U') IS NOT NULL
    DROP TABLE Bagiscilar;
GO

CREATE TABLE Bagiscilar (
    BagisciID INT IDENTITY(1,1) PRIMARY KEY,
    Ad NVARCHAR(50) NOT NULL,
    Soyad NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Telefon NVARCHAR(20),
    TCKimlikNo NVARCHAR(11),
    Adres NVARCHAR(500),
    Sifre NVARCHAR(255) NOT NULL,
    KayitTarihi DATETIME DEFAULT GETDATE(),
    Aktif BIT DEFAULT 1
);
GO

-- Bağışlar Tablosu
IF OBJECT_ID('Bagislar', 'U') IS NOT NULL
    DROP TABLE Bagislar;
GO

CREATE TABLE Bagislar (
    BagisID INT IDENTITY(1,1) PRIMARY KEY,
    BagisciID INT NOT NULL,
    Tutar DECIMAL(18,2) NOT NULL CHECK (Tutar > 0),
    BagisTarihi DATETIME DEFAULT GETDATE(),
    Aciklama NVARCHAR(500),
    IBAN NVARCHAR(34),
    OdemeYontemi NVARCHAR(50) CHECK (OdemeYontemi IN ('KrediKarti', 'BankaHavalesi', 'Nakit')),
    Durum NVARCHAR(20) DEFAULT 'Beklemede' CHECK (Durum IN ('Beklemede', 'Onaylandi', 'Iptal')),
    FOREIGN KEY (BagisciID) REFERENCES Bagiscilar(BagisciID) ON DELETE CASCADE
);
GO

-- Burs Ödemeleri Tablosu
IF OBJECT_ID('BursOdemeleri', 'U') IS NOT NULL
    DROP TABLE BursOdemeleri;
GO

CREATE TABLE BursOdemeleri (
    OdemeID INT IDENTITY(1,1) PRIMARY KEY,
    OgrenciID INT NOT NULL,
    BasvuruID INT,
    Tutar DECIMAL(18,2) NOT NULL CHECK (Tutar > 0),
    OdemeTarihi DATETIME DEFAULT GETDATE(),
    OdemeDonemi NVARCHAR(20) NOT NULL, -- Örn: "2024-01", "2024-02"
    Aciklama NVARCHAR(500),
    OnaylayanAdminID INT,
    FOREIGN KEY (OgrenciID) REFERENCES Ogrenciler(OgrenciID) ON DELETE CASCADE,
    FOREIGN KEY (BasvuruID) REFERENCES Basvurular(BasvuruID),
    FOREIGN KEY (OnaylayanAdminID) REFERENCES Adminler(AdminID)
);
GO

-- Mevcut tablolara kolon ekleme (eğer tablo zaten varsa)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Bagiscilar')
BEGIN
    PRINT 'Bagiscilar tablosu zaten mevcut.';
END
ELSE
BEGIN
    PRINT 'Bagiscilar tablosu oluşturuldu.';
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Bagislar')
BEGIN
    PRINT 'Bagislar tablosu zaten mevcut.';
END
ELSE
BEGIN
    PRINT 'Bagislar tablosu oluşturuldu.';
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'BursOdemeleri')
BEGIN
    PRINT 'BursOdemeleri tablosu zaten mevcut.';
END
ELSE
BEGIN
    PRINT 'BursOdemeleri tablosu oluşturuldu.';
END
GO

-- İndeksler
CREATE INDEX IX_Bagislar_BagisciID ON Bagislar(BagisciID);
CREATE INDEX IX_Bagislar_Durum ON Bagislar(Durum);
CREATE INDEX IX_BursOdemeleri_OgrenciID ON BursOdemeleri(OgrenciID);
CREATE INDEX IX_BursOdemeleri_OdemeDonemi ON BursOdemeleri(OdemeDonemi);
GO

PRINT 'Bağış sistemi şeması başarıyla oluşturuldu!';
GO

