using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Cinema.Errors;
using Data.Cinema.Models;
using Data.Cinema.Repositories;
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

        public async Task<ShowExData[]> GetAll()
        {
            // _logger.LogInformation($"Getting all Shows");

            return await dbContext.Shows
                // Linq projection using Select
                .Select(show => new ShowExData
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

        public async Task<ShowData> FindByShowId(int showId)
        {
            // _logger.LogInformation($"Getting all Shows");

            var show = await dbContext.Shows.FindAsync(showId);
            return new ShowData
            {
                Showid = show.Showid,
                Movieid = show.Movieid,
                Roomid = show.Roomid,
                Start = show.Start,
            };
        }

        public async Task<IList<ShowTicketsData>> GetShowTickets(int showId)
        {
            return await dbContext.Tickets
                .Where(ticket => ticket.Showid == showId)
                .Select(ticket => new ShowTicketsData
                {
                    RowNum = ticket.Rownum,
                    SeatNum = ticket.Seatnum
                })
                .ToListAsync();
        }

        public async Task<ShowDetailsData> FindByShowIdWithDetails(int showId)
        {
            return await dbContext.Shows
                .Where(sh => sh.Showid == showId)
                .Select(show => new ShowDetailsData
                {
                    Showid = show.Showid,
                    Movieid = show.Movieid,
                    Roomid = show.Roomid,
                    Movie = show.Movie.Title,
                    Room = show.Room.Name,
                    Start = show.Start
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateShow(ShowData showData)
        {
            var showEntity = await dbContext.Shows
                .SingleOrDefaultAsync(sh => sh.Showid == showData.Showid);
            if (showEntity == null) throw CreateErrorShowNotExists(showData.Showid);
            if (showEntity.Movieid != showData.Movieid)
            {
                if (!await MovieExists(showData.Movieid))
                    throw CreateErrorMovieNotExists(showData.Movieid);
                showEntity.Movieid = showData.Movieid;

            }
            if (showEntity.Roomid != showData.Roomid)
                showEntity.Roomid = showData.Roomid;
            if (showEntity.Start != showData.Start)
                showEntity.Start = showData.Start;
            _ = await dbContext.SaveChangesAsync();
        }

        private async Task<bool> ShowExists(int showid)
            => await dbContext.Shows.AnyAsync(show => show.Showid == showid);
        private async Task<bool> MovieExists(int movieId)
            => await dbContext.Shows.AnyAsync(show => show.Movieid == movieId);

        private ShowNotExistsException CreateErrorShowNotExists(int showid)
            => new ShowNotExistsException($"Show \"{showid}\" doesn't exist");
        private MovieNotExistsException CreateErrorMovieNotExists(int movieId)
            => new MovieNotExistsException($"Movie ID={movieId} doesn't exist");
    }
}
