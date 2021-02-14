using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Cinema.Entites;
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

        public async Task AddMovies(IEnumerable<Movie> movies)
        {
            await _dbContext.Movies.AddRangeAsync(movies);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRooms(IEnumerable<Room> movies)
        {
            await _dbContext.Rooms.AddRangeAsync(movies);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddShows(List<Show> shows)
        {
            await _dbContext.Shows.AddRangeAsync(shows);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddTickets(List<Ticket> tickets)
        {
            await _dbContext.Tickets.AddRangeAsync(tickets);
            await _dbContext.SaveChangesAsync();
        }
    }
}
