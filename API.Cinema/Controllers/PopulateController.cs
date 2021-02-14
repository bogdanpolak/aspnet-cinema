using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Cinema.Controllers.DTOs;
using API.Cinema.Logic.Populate;
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

        [HttpGet]
        public async Task<ActionResult<PopulateResult>> Get()
        {
            await _populateRepository.ClearDatabase();
            var movies = DataPopulator.GenerateMovies();
            var rooms = DataPopulator.GenerateRooms();
            var shows = DataPopulator.GenerateShows(movies, rooms);
            var tickets = DataPopulator.GenerateTickets(shows);
            await _populateRepository.AddMovies(movies);
            await _populateRepository.AddRooms(rooms);
            await _populateRepository.AddShows(shows);
            await _populateRepository.AddTickets(tickets);
            return new PopulateResult {
                MoviesCreated = movies.Count,
                RoomsCreated = rooms.Count,
                ShowsCreated = shows.Count,
                TicketsSold = tickets.Count
            };
        }

    }
}
