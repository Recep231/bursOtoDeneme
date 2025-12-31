-- IBAN kolonunu Bagislar tablosuna ekleme
USE BursOtomasyonDB;
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Bagislar')
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Bagislar') AND name = 'IBAN')
    BEGIN
        ALTER TABLE Bagislar ADD IBAN NVARCHAR(34);
        PRINT 'IBAN kolonu Bagislar tablosuna eklendi.';
    END
    ELSE
    BEGIN
        PRINT 'IBAN kolonu zaten mevcut.';
    END
END
ELSE
BEGIN
    PRINT 'Bagislar tablosu bulunamadı. Önce Schema_Bagis_Sistemi.sql dosyasını çalıştırın.';
END
GO

