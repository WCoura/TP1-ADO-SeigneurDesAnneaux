using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace ReservationsLieux
{
    public static class DatabaseInitializer
    {
        public static void Initialiser()
        {
            string chaine = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string dossierScripts = TrouverDossierScripts();

            ExecuterScript(ChaineMaster(chaine), Path.Combine(dossierScripts, "create_database.sql"));
            ExecuterScript(chaine, Path.Combine(dossierScripts, "create_tables.sql"));
            ExecuterScript(chaine, Path.Combine(dossierScripts, "insert_data.sql"));
        }

        private static string TrouverDossierScripts()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string[] candidats = new string[]
            {
                Path.Combine(baseDir, "DatabaseScripts"),
                Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\DatabaseScripts")),
                Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\DatabaseScripts")),
                Path.GetFullPath(Path.Combine(baseDir, @"..\..\DatabaseScripts"))
            };

            foreach (string dossier in candidats)
            {
                if (File.Exists(Path.Combine(dossier, "create_database.sql")))
                {
                    return dossier;
                }
            }

            throw new FileNotFoundException(
                "Les scripts SQL sont introuvables. Vérifiez le dossier DatabaseScripts.");
        }

        private static string ChaineMaster(string chaine)
        {
            SqlConnectionStringBuilder constructeur = new SqlConnectionStringBuilder(chaine);
            constructeur.InitialCatalog = "master";
            return constructeur.ConnectionString;
        }

        private static void ExecuterScript(string chaineConnexion, string cheminFichier)
        {
            if (!File.Exists(cheminFichier))
            {
                throw new FileNotFoundException("Script SQL introuvable : " + cheminFichier);
            }

            string sql = File.ReadAllText(cheminFichier);
            string[] lots = Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

            using (SqlConnection connexion = new SqlConnection(chaineConnexion))
            {
                connexion.Open();
                foreach (string lot in lots)
                {
                    string texte = lot.Trim();
                    if (texte.Length == 0)
                    {
                        continue;
                    }

                    using (SqlCommand commande = connexion.CreateCommand())
                    {
                        commande.CommandText = texte;
                        commande.CommandTimeout = 60;
                        commande.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
