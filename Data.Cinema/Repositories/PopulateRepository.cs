using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data.Cinema.Repositories
{
    public class PopulateRepository : IPopulateRepository
    {
        private readonly CinemaContext _dbContext;

        public PopulateRepository(CinemaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PopulateRepository()
        {
        }

        public async Task ClearDatabase()
        {
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE from tickets");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE from shows");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE from rooms");
            await _dbContext.Database.ExecuteSqlRawAsync("DELETE from movies");
        }
    }
}
