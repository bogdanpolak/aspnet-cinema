using System;
using System.Linq;
using Data.Cinema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.Cinema.InitializeDatabase
{
    public class DbInitializer : IDbInitializer
    {
        public DbInitializer()
        {
        }

        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CinemaContext>())
                {
                    if (!context.Database.IsInMemory())
                        context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CinemaContext>())
                {
                    if (!context.Movies.Any())
                    {
                        var movies = DataPopulator.GenerateMovies();
                        var rooms = DataPopulator.GenerateRooms();
                        var shows = DataPopulator.GenerateShows(movies, rooms);
                        var tickets = DataPopulator.GenerateTickets(shows);
                        context.Movies.AddRange(movies);
                        context.Rooms.AddRange(rooms);
                        context.Shows.AddRange(shows);
                        context.Tickets.AddRange(tickets);
                        context.SaveChanges();
                    }
                }
            }
        }

    }
}
