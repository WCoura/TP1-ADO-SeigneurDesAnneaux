using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ReservationsLieux.Domain;

namespace ReservationsLieux.Repositories
{
    public class MissionRepository : RepositoryBase<Mission>
    {
        private const string SqlSelectJointure = @"
SELECT m.IdMission,
       m.IdLieu,
       m.IdPersonnage,
       l.NomLieu,
       p.Nom AS NomPersonnage,
       l.Royaume,
       p.Race,
       m.DateDebut,
       m.DateFin
FROM dbo.Mission m
INNER JOIN dbo.Lieu l ON l.IdLieu = m.IdLieu
INNER JOIN dbo.Personnage p ON p.IdPersonnage = m.IdPersonnage";

        public override DataTable ObtenirTous()
        {
            DataTable table = new DataTable("Mission");
            SqlConnection connexion = CreerConnexion();

            try
            {
                Ouvrir(connexion);
                using (SqlDataAdapter adaptateur = new SqlDataAdapter(
                    SqlSelectJointure + " ORDER BY m.DateDebut, l.NomLieu", connexion))
                {
                    adaptateur.Fill(table);
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }

            return table;
        }

        public override Mission ObtenirParId(int id)
        {
            SqlConnection connexion = CreerConnexion();
            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = SqlSelectJointure + " WHERE m.IdMission = @IdMission";
                    commande.Parameters.Add("@IdMission", SqlDbType.Int).Value = id;

                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        if (lecteur.Read())
                        {
                            return LireMission(lecteur);
                        }
                    }
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }

            return null;
        }

        public override void Sauvegarder(DataTable table)
        {
            throw new NotSupportedException(
                "Les missions se créent une à une après vérification de conflit. La liste globale est en lecture seule.");
        }

        public bool ExisteConflit(int idLieu, DateTime dateDebut, DateTime dateFin)
        {
            return ObtenirConflits(idLieu, dateDebut, dateFin).Count > 0;
        }

        public List<Mission> ObtenirConflits(int idLieu, DateTime dateDebut, DateTime dateFin)
        {
            List<Mission> conflits = new List<Mission>();
            SqlConnection connexion = CreerConnexion();

            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = @"
SELECT m.IdMission, m.IdLieu, m.IdPersonnage, l.NomLieu, p.Nom AS NomPersonnage,
       m.DateDebut, m.DateFin
FROM dbo.Mission m
INNER JOIN dbo.Lieu l ON l.IdLieu = m.IdLieu
INNER JOIN dbo.Personnage p ON p.IdPersonnage = m.IdPersonnage
WHERE m.IdLieu = @IdLieu
  AND m.DateDebut < @DateFin
  AND m.DateFin > @DateDebut
ORDER BY m.DateDebut";

                    commande.Parameters.Add("@IdLieu", SqlDbType.Int).Value = idLieu;
                    commande.Parameters.Add("@DateDebut", SqlDbType.DateTime2).Value = dateDebut;
                    commande.Parameters.Add("@DateFin", SqlDbType.DateTime2).Value = dateFin;

                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        while (lecteur.Read())
                        {
                            conflits.Add(new Mission
                            {
                                IdMission = lecteur.GetInt32(lecteur.GetOrdinal("IdMission")),
                                IdLieu = lecteur.GetInt32(lecteur.GetOrdinal("IdLieu")),
                                IdPersonnage = lecteur.GetInt32(lecteur.GetOrdinal("IdPersonnage")),
                                NomLieu = lecteur.GetString(lecteur.GetOrdinal("NomLieu")),
                                NomPersonnage = lecteur.GetString(lecteur.GetOrdinal("NomPersonnage")),
                                DateDebut = lecteur.GetDateTime(lecteur.GetOrdinal("DateDebut")),
                                DateFin = lecteur.GetDateTime(lecteur.GetOrdinal("DateFin"))
                            });
                        }
                    }
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }

            return conflits;
        }

        public int Creer(Mission mission)
        {
            if (mission == null)
            {
                throw new ArgumentNullException("mission");
            }

            if (ExisteConflit(mission.IdLieu, mission.DateDebut, mission.DateFin))
            {
                throw new InvalidOperationException(
                    "Conflit temporel : ce lieu est déjà réservé sur la plage demandée.");
            }

            SqlConnection connexion = CreerConnexion();
            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = @"
INSERT INTO dbo.Mission (IdLieu, IdPersonnage, DateDebut, DateFin)
VALUES (@IdLieu, @IdPersonnage, @DateDebut, @DateFin);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    commande.Parameters.Add("@IdLieu", SqlDbType.Int).Value = mission.IdLieu;
                    commande.Parameters.Add("@IdPersonnage", SqlDbType.Int).Value = mission.IdPersonnage;
                    commande.Parameters.Add("@DateDebut", SqlDbType.DateTime2).Value = mission.DateDebut;
                    commande.Parameters.Add("@DateFin", SqlDbType.DateTime2).Value = mission.DateFin;

                    return (int)commande.ExecuteScalar();
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }
        }

        public DataTable Rechercher(DateTime? dateMin, DateTime? dateMax, string royaume, string race)
        {
            DataTable table = new DataTable("Resultats");
            table.Columns.Add("IdMission", typeof(int));
            table.Columns.Add("NomLieu", typeof(string));
            table.Columns.Add("Royaume", typeof(string));
            table.Columns.Add("NomPersonnage", typeof(string));
            table.Columns.Add("Race", typeof(string));
            table.Columns.Add("DateDebut", typeof(DateTime));
            table.Columns.Add("DateFin", typeof(DateTime));

            SqlConnection connexion = CreerConnexion();
            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = @"
SELECT m.IdMission, l.NomLieu, l.Royaume, p.Nom AS NomPersonnage, p.Race, m.DateDebut, m.DateFin
FROM dbo.Mission m
INNER JOIN dbo.Lieu l ON l.IdLieu = m.IdLieu
INNER JOIN dbo.Personnage p ON p.IdPersonnage = m.IdPersonnage
WHERE (@DateMin IS NULL OR m.DateFin >= @DateMin)
  AND (@DateMax IS NULL OR m.DateDebut <= @DateMax)
  AND (@Royaume IS NULL OR l.Royaume = @Royaume)
  AND (@Race IS NULL OR p.Race = @Race)
ORDER BY m.DateDebut, l.NomLieu";

                    SqlParameter pDateMin = commande.Parameters.Add("@DateMin", SqlDbType.DateTime2);
                    pDateMin.Value = dateMin.HasValue ? (object)dateMin.Value : DBNull.Value;

                    SqlParameter pDateMax = commande.Parameters.Add("@DateMax", SqlDbType.DateTime2);
                    pDateMax.Value = dateMax.HasValue ? (object)dateMax.Value : DBNull.Value;

                    SqlParameter pRoyaume = commande.Parameters.Add("@Royaume", SqlDbType.NVarChar, 80);
                    pRoyaume.Value = string.IsNullOrWhiteSpace(royaume) ? (object)DBNull.Value : royaume.Trim();

                    SqlParameter pRace = commande.Parameters.Add("@Race", SqlDbType.NVarChar, 50);
                    pRace.Value = string.IsNullOrWhiteSpace(race) ? (object)DBNull.Value : race.Trim();

                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        while (lecteur.Read())
                        {
                            DataRow ligne = table.NewRow();
                            ligne["IdMission"] = lecteur.GetInt32(lecteur.GetOrdinal("IdMission"));
                            ligne["NomLieu"] = lecteur.GetString(lecteur.GetOrdinal("NomLieu"));
                            ligne["Royaume"] = lecteur.GetString(lecteur.GetOrdinal("Royaume"));
                            ligne["NomPersonnage"] = lecteur.GetString(lecteur.GetOrdinal("NomPersonnage"));
                            ligne["Race"] = lecteur.GetString(lecteur.GetOrdinal("Race"));
                            ligne["DateDebut"] = lecteur.GetDateTime(lecteur.GetOrdinal("DateDebut"));
                            ligne["DateFin"] = lecteur.GetDateTime(lecteur.GetOrdinal("DateFin"));
                            table.Rows.Add(ligne);
                        }
                    }
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }

            return table;
        }

        private static Mission LireMission(SqlDataReader lecteur)
        {
            return new Mission
            {
                IdMission = lecteur.GetInt32(lecteur.GetOrdinal("IdMission")),
                IdLieu = lecteur.GetInt32(lecteur.GetOrdinal("IdLieu")),
                IdPersonnage = lecteur.GetInt32(lecteur.GetOrdinal("IdPersonnage")),
                NomLieu = lecteur.GetString(lecteur.GetOrdinal("NomLieu")),
                NomPersonnage = lecteur.GetString(lecteur.GetOrdinal("NomPersonnage")),
                Royaume = lecteur.GetString(lecteur.GetOrdinal("Royaume")),
                Race = lecteur.GetString(lecteur.GetOrdinal("Race")),
                DateDebut = lecteur.GetDateTime(lecteur.GetOrdinal("DateDebut")),
                DateFin = lecteur.GetDateTime(lecteur.GetOrdinal("DateFin"))
            };
        }
    }
}
