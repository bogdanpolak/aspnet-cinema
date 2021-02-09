using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Cinema.DataAccess;
using Data.Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Cinema
{
    public class ShowRepository : IShowRepository
    {
        private readonly CinemaContext dbContext;

        public ShowRepository(CinemaContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add<T>(T entity) where T : class
        {
            // logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            dbContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            // _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            dbContext.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // _logger.LogInformation($"Attempitng to save the changes in the context");
            // Only return success if at least one row was changed
            return (await dbContext.SaveChangesAsync()) > 0;
        }

        public async Task<ShowData[]> GetAll()
        {
            // _logger.LogInformation($"Getting all Shows");

            return await dbContext.Showtimes
                // Linq projection using Select
                .Select(show => new ShowData
                {
                    Showid = show.Showid,
                    Movieid = show.Movieid,
                    Roomid = show.Roomid,
                    Movie = show.Movie.Title,
                    Room = show.Room.Name,
                    Start = show.Start,
                    Seats = show.Room.Columns * show.Room.Rows,
                    Sold = show.Tickets.Count,
                    Total = show.Tickets.Sum(t => t.Price)
                })
                .OrderBy(show => show.Start)
                .ToArrayAsync();
        }

        public async Task<ShowData> FindByShowId(string showId)
        {
            // _logger.LogInformation($"Getting all Shows");

            var show = await dbContext.Showtimes.FindAsync(showId);
            return new ShowData
            {
                Showid = show.Showid,
                Movieid = show.Movieid,
                Roomid = show.Roomid,
                Movie = show.Movie.Title,
                Room = show.Room.Name,
                Start = show.Start,
                Seats = show.Room.Columns * show.Room.Rows,
            };
        }

    }
}
