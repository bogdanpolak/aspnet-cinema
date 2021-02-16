using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Cinema.Entites;

namespace Data.Cinema.Repositories
{
    public interface IPopulateRepository
    {
        Task AddMovies(IEnumerable<Movie> movies);
        Task AddRooms(IEnumerable<Room> movies);
        Task ClearDatabase();
        Task AddShows(List<Show> shows);
        Task AddTickets(List<Ticket> tickets);
    }
}
