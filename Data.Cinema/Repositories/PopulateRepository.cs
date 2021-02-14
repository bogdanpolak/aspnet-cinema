using System;
using System.Collections.Generic;
using System.Linq;
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
            var tables = new List<string> {
                "tickets", "shows", "rooms", "movies"
            };
            foreach (var tabname in tables)
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    $"TRUNCATE TABLE {tabname} RESTART IDENTITY CASCADE");
            }
        }
        }
    }
}
