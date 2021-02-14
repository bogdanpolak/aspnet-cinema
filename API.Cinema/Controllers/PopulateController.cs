using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Cinema.Controllers.DTOs;
using Data.Cinema.Entites;
using Data.Cinema.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Cinema.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PopulateController : ControllerBase
    {
        private readonly IPopulateRepository _populateRepository;

        public PopulateController(IPopulateRepository populateRepository)
        {
            _populateRepository = populateRepository;
        }

        private readonly List<string> movieTitles = new List<string> {
            "Jumanji: Level One",
            "Supernova",
            "Little Fish",
            "The Mauritanian",
            "Tom and Jerry",
            "Godzilla vs.Kong",
            "Peter Rabbit 2: The Runaway",
            "A Quiet Place Part II",
            "Black Widow",
            "Ghostbusters: Afterlife",
            "Top Gun: Maverick",
            "Dune",
            "Mission: Impossible 7",
            "The Matrix 4",
            "Sherlock Holmes 3"
        };

        /*
        "Turquouse Room",8,20
        "Garent Room",18,20
        */
        /* 
        "2021-03-03 19:30"
        "2021-03-04 19:30"
        "2021-03-05 18:00"
        "2021-03-05 20:30"
        "2021-03-06 12:30"
        "2021-03-06 15:00"
        "2021-03-06 18:00"
        "2021-03-06 20:30"
        */
        /* Tickets (row, seat, price)
        1	10	15
        1	11	15
        2	1	15
        2	3	15
        2	11	12.99
        5	11	10
        5	12	17.5
        5	13	17.5
        5	14	12.5
        5	15	12.5
        5	16	12.5
        */
        [HttpGet]
        public async Task<ActionResult<PopulateResult>> Get()
        {
            await _populateRepository.ClearDatabase();
            var movies = new List<Movie>();
            foreach (var title in movieTitles)
                movies.Add(new Movie { Title = title });
            await _populateRepository.AddMovies(movies);
            return new PopulateResult { MoviesCreated = movieTitles.Count };
        }

    }
}
