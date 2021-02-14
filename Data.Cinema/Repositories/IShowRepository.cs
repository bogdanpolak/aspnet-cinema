using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Cinema.Models;

namespace Data.Cinema
{
    public interface IShowRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<ShowData> FindByShowId(int showId);
        Task<ShowExData[]> GetAll();
        Task<IList<ShowTicketsData>> GetShowTickets(int showId);
        Task<bool> SaveChangesAsync();
        Task<ShowDetailsData> FindByShowIdWithDetails(int showId);
        Task UpdateShow(ShowData show);
    }
}