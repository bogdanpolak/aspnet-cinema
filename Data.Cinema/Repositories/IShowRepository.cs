using System.Threading.Tasks;
using Data.Cinema.DTO;

namespace Data.Cinema
{
    public interface IShowRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<ShowDto[]> GetAll();
        Task<bool> SaveChangesAsync();
    }
}