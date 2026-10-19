using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ReservationsLieux.Domain;

namespace ReservationsLieux.Repositories
{
    public class LieuRepository : RepositoryBase<Lieu>
    {
        private const string SqlSelect =
            "SELECT IdLieu, NomLieu, Royaume, Dangerosite FROM dbo.Lieu";

        public override DataTable ObtenirTous()
        {
            DataTable table = new DataTable("Lieu");
            SqlConnection connexion = CreerConnexion();

            try
            {
                Ouvrir(connexion);
                using (SqlDataAdapter adaptateur = new SqlDataAdapter(SqlSelect, connexion))
                {
                    adaptateur.MissingSchemaAction = MissingSchemaAction.AddWithKey;
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

        public override Lieu ObtenirParId(int id)
        {
            SqlConnection connexion = CreerConnexion();
            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = SqlSelect + " WHERE IdLieu = @IdLieu";
                    commande.Parameters.Add("@IdLieu", SqlDbType.Int).Value = id;

                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        if (lecteur.Read())
                        {
                            return LireLieu(lecteur);
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
            SqlConnection connexion = CreerConnexion();
            try
            {
                Ouvrir(connexion);
                using (SqlDataAdapter adaptateur = new SqlDataAdapter(SqlSelect, connexion))
                {
                    adaptateur.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    SqlCommandBuilder constructeur = new SqlCommandBuilder(adaptateur);
                    constructeur.ConflictOption = ConflictOption.OverwriteChanges;
                    adaptateur.InsertCommand = constructeur.GetInsertCommand();
                    adaptateur.UpdateCommand = constructeur.GetUpdateCommand();
                    adaptateur.DeleteCommand = constructeur.GetDeleteCommand();
                    adaptateur.Update(table);
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }
        }

        public List<string> ObtenirRoyaumes()
        {
            List<string> royaumes = new List<string>();
            SqlConnection connexion = CreerConnexion();

            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = "SELECT DISTINCT Royaume FROM dbo.Lieu ORDER BY Royaume";
                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        while (lecteur.Read())
                        {
                            royaumes.Add(lecteur.GetString(0));
                        }
                    }
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }

            return royaumes;
        }

        private static Lieu LireLieu(SqlDataReader lecteur)
        {
            return new Lieu
            {
                IdLieu = lecteur.GetInt32(lecteur.GetOrdinal("IdLieu")),
                NomLieu = lecteur.GetString(lecteur.GetOrdinal("NomLieu")),
                Royaume = lecteur.GetString(lecteur.GetOrdinal("Royaume")),
                Dangerosite = lecteur.GetInt32(lecteur.GetOrdinal("Dangerosite"))
            };
        }
    }
}
