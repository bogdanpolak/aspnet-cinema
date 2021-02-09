using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Cinema.Models;

namespace Data.Cinema
{
    public interface IShowRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<ShowData> FindByShowId(string showId);
        Task<ShowExData[]> GetAll();
        Task<IList<ShowTicketsData>> GetShowTickets(string showId);
        Task<bool> SaveChangesAsync();
        Task<ShowDetailsData> FindByShowIdWithDetails(string showId);
    }
}