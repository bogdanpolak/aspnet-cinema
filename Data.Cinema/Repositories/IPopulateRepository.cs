using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Cinema.Entites;

namespace Data.Cinema.Repositories
{
    public interface IPopulateRepository
    {
        Task AddMovies(IEnumerable<Movie> movies);
        Task ClearDatabase();
    }
}
