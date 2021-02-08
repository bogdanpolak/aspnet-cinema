using System.Threading.Tasks;
using Data.Cinema.DataAccess;

namespace Data.Cinema
{
    public interface IShowRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<ShowData> FindByShowId(string showId);
        Task<ShowData[]> GetAll();
        Task<bool> SaveChangesAsync();
    }
}