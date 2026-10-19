-- ============================================================
-- TP1 — Réservations de lieux (Seigneur des Anneaux)
-- Script 3/3 : données d'essai
-- Exécuter après create_tables.sql
-- ============================================================

USE [TP1_LSDA];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Lieu)
BEGIN
    INSERT INTO dbo.Lieu (NomLieu, Royaume, Dangerosite) VALUES
    (N'Minas Tirith',     N'Gondor',      6),
    (N'Fondcombe',        N'Eriador',     2),
    (N'Erebor',           N'Rhovanion',   5),
    (N'Le Mordor',        N'Mordor',     10),
    (N'Edoras',           N'Rohan',       4),
    (N'La Comté',         N'Eriador',     1),
    (N'Isengard',         N'Rohan',       7),
    (N'Gouffre de Helm',  N'Rohan',       8),
    (N'Lothlórien',       N'Rhovanion',   3),
    (N'Cirith Ungol',     N'Mordor',      9);
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Personnage)
BEGIN
    INSERT INTO dbo.Personnage (Nom, Race, Faction) VALUES
    (N'Aragorn',    N'Humain',  N'Gondor'),
    (N'Gandalf',    N'Maia',    N'Conseil Blanc'),
    (N'Éowyn',      N'Humain',  N'Rohan'),
    (N'Legolas',    N'Elfe',    N'Forêt Noire'),
    (N'Gimli',      N'Nain',    N'Erebor'),
    (N'Frodon',     N'Hobbit',  N'La Comté'),
    (N'Arwen',      N'Elfe',    N'Fondcombe'),
    (N'Boromir',    N'Humain',  N'Gondor'),
    (N'Sam',        N'Hobbit',  N'La Comté'),
    (N'Galadriel',  N'Elfe',    N'Lothlórien');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Mission)
BEGIN
    INSERT INTO dbo.Mission (IdLieu, IdPersonnage, DateDebut, DateFin) VALUES
    (1, 1, '2026-09-01 08:00:00', '2026-09-08 18:00:00'), -- Aragorn à Minas Tirith
    (2, 2, '2026-09-03 09:00:00', '2026-09-12 17:00:00'), -- Gandalf à Fondcombe
    (3, 5, '2026-09-05 10:00:00', '2026-09-15 16:00:00'), -- Gimli à Erebor
    (5, 3, '2026-09-10 08:00:00', '2026-09-18 18:00:00'), -- Éowyn à Edoras
    (6, 6, '2026-09-01 08:00:00', '2026-09-20 18:00:00'), -- Frodon à La Comté
    (9, 4, '2026-09-07 08:00:00', '2026-09-14 18:00:00'); -- Legolas à Lothlórien
END
GO
