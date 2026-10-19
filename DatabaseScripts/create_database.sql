-- ============================================================
-- TP1 — Réservations de lieux (Seigneur des Anneaux)
-- Script 1/3 : création de la base de données TP1_LSDA
-- Exécuter en premier, connecté à l'instance LocalDB (master)
-- ============================================================

IF DB_ID(N'TP1_LSDA') IS NULL
BEGIN
    CREATE DATABASE [TP1_LSDA];
    PRINT 'Base de données TP1_LSDA créée.';
END
ELSE
BEGIN
    PRINT 'La base de données TP1_LSDA existe déjà.';
END
GO
