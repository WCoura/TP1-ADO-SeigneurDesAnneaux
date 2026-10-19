using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ReservationsLieux.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        protected string ChaineConnexion { get; private set; }

        protected RepositoryBase()
        {
            ConnectionStringSettings parametre = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (parametre == null || string.IsNullOrWhiteSpace(parametre.ConnectionString))
            {
                throw new InvalidOperationException(
                    "La chaîne de connexion DefaultConnection est introuvable dans App.config.");
            }

            ChaineConnexion = parametre.ConnectionString;
        }

        protected SqlConnection CreerConnexion()
        {
            return new SqlConnection(ChaineConnexion);
        }

        protected void Ouvrir(SqlConnection connexion)
        {
            if (connexion.State != ConnectionState.Open)
            {
                connexion.Open();
            }
        }

        protected void Fermer(SqlConnection connexion)
        {
            if (connexion != null && connexion.State != ConnectionState.Closed)
            {
                connexion.Close();
            }
        }

        public abstract DataTable ObtenirTous();
        public abstract T ObtenirParId(int id);
        public abstract void Sauvegarder(DataTable table);
    }
}
