using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ReservationsLieux.Domain;

namespace ReservationsLieux.Repositories
{
    public class PersonnageRepository : RepositoryBase<Personnage>
    {
        private const string SqlSelect =
            "SELECT IdPersonnage, Nom, Race, Faction FROM dbo.Personnage";

        public override DataTable ObtenirTous()
        {
            DataTable table = new DataTable("Personnage");
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

        public override Personnage ObtenirParId(int id)
        {
            SqlConnection connexion = CreerConnexion();
            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = SqlSelect + " WHERE IdPersonnage = @IdPersonnage";
                    commande.Parameters.Add("@IdPersonnage", SqlDbType.Int).Value = id;

                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        if (lecteur.Read())
                        {
                            return LirePersonnage(lecteur);
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

        public List<string> ObtenirRaces()
        {
            List<string> races = new List<string>();
            SqlConnection connexion = CreerConnexion();

            try
            {
                Ouvrir(connexion);
                using (SqlCommand commande = connexion.CreateCommand())
                {
                    commande.CommandText = "SELECT DISTINCT Race FROM dbo.Personnage ORDER BY Race";
                    using (SqlDataReader lecteur = commande.ExecuteReader())
                    {
                        while (lecteur.Read())
                        {
                            races.Add(lecteur.GetString(0));
                        }
                    }
                }
            }
            finally
            {
                Fermer(connexion);
                connexion.Dispose();
            }

            return races;
        }

        private static Personnage LirePersonnage(SqlDataReader lecteur)
        {
            return new Personnage
            {
                IdPersonnage = lecteur.GetInt32(lecteur.GetOrdinal("IdPersonnage")),
                Nom = lecteur.GetString(lecteur.GetOrdinal("Nom")),
                Race = lecteur.GetString(lecteur.GetOrdinal("Race")),
                Faction = lecteur.GetString(lecteur.GetOrdinal("Faction"))
            };
        }
    }
}
