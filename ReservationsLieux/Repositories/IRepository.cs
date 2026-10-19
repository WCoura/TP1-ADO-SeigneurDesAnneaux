using System.Data;

namespace ReservationsLieux.Repositories
{
    public interface IRepository<T>
    {
        DataTable ObtenirTous();
        T ObtenirParId(int id);
        void Sauvegarder(DataTable table);
    }
}
