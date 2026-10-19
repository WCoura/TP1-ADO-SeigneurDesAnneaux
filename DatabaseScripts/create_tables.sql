-- ============================================================
-- TP1 — Réservations de lieux (Seigneur des Anneaux)
-- Script 2/3 : tables, clés, contraintes et index
-- Exécuter après create_database.sql
-- Ce script est réexécutable : il ne détruit pas les données existantes.
-- ============================================================

USE [TP1_LSDA];
GO

IF OBJECT_ID(N'dbo.Lieu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Lieu
    (
        IdLieu        INT IDENTITY(1, 1) NOT NULL,
        NomLieu       NVARCHAR(100)      NOT NULL,
        Royaume       NVARCHAR(80)       NOT NULL,
        Dangerosite   INT                NOT NULL,
        CONSTRAINT PK_Lieu PRIMARY KEY (IdLieu),
        CONSTRAINT UQ_Lieu_Nom UNIQUE (NomLieu),
        CONSTRAINT CK_Lieu_Nom CHECK (LEN(LTRIM(RTRIM(NomLieu))) > 0),
        CONSTRAINT CK_Lieu_Royaume CHECK (LEN(LTRIM(RTRIM(Royaume))) > 0),
        CONSTRAINT CK_Lieu_Dangerosite CHECK (Dangerosite BETWEEN 1 AND 10)
    );
END
GO

IF OBJECT_ID(N'dbo.Personnage', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Personnage
    (
        IdPersonnage  INT IDENTITY(1, 1) NOT NULL,
        Nom           NVARCHAR(100)      NOT NULL,
        Race          NVARCHAR(50)       NOT NULL,
        Faction       NVARCHAR(80)       NOT NULL,
        CONSTRAINT PK_Personnage PRIMARY KEY (IdPersonnage),
        CONSTRAINT UQ_Personnage_Nom UNIQUE (Nom),
        CONSTRAINT CK_Personnage_Nom CHECK (LEN(LTRIM(RTRIM(Nom))) > 0),
        CONSTRAINT CK_Personnage_Race CHECK (LEN(LTRIM(RTRIM(Race))) > 0),
        CONSTRAINT CK_Personnage_Faction CHECK (LEN(LTRIM(RTRIM(Faction))) > 0)
    );
END
GO

IF OBJECT_ID(N'dbo.Mission', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Mission
    (
        IdMission     INT IDENTITY(1, 1) NOT NULL,
        IdLieu        INT                NOT NULL,
        IdPersonnage  INT                NOT NULL,
        DateDebut     DATETIME2(0)       NOT NULL,
        DateFin       DATETIME2(0)       NOT NULL,
        CONSTRAINT PK_Mission PRIMARY KEY (IdMission),
        CONSTRAINT FK_Mission_Lieu
            FOREIGN KEY (IdLieu) REFERENCES dbo.Lieu (IdLieu),
        CONSTRAINT FK_Mission_Personnage
            FOREIGN KEY (IdPersonnage) REFERENCES dbo.Personnage (IdPersonnage),
        CONSTRAINT CK_Mission_Dates CHECK (DateFin > DateDebut)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Mission_Lieu_Dates' AND object_id = OBJECT_ID(N'dbo.Mission'))
BEGIN
    CREATE INDEX IX_Mission_Lieu_Dates
        ON dbo.Mission (IdLieu, DateDebut, DateFin);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Lieu_Royaume' AND object_id = OBJECT_ID(N'dbo.Lieu'))
BEGIN
    CREATE INDEX IX_Lieu_Royaume
        ON dbo.Lieu (Royaume);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Personnage_Race' AND object_id = OBJECT_ID(N'dbo.Personnage'))
BEGIN
    CREATE INDEX IX_Personnage_Race
        ON dbo.Personnage (Race);
END
GO
