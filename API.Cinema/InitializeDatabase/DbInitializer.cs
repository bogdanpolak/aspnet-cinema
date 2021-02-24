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
                        var dataPopulator = new DataPopulator();
                        dataPopulator.GenerateData();
                        context.Movies.AddRange(dataPopulator.Movies);
                        context.Rooms.AddRange(dataPopulator.Rooms);
                        context.Shows.AddRange(dataPopulator.Shows);
                        context.Tickets.AddRange(dataPopulator.Tickets);
                        context.SaveChanges();
                    }
                }
            }
        }

    }
}
