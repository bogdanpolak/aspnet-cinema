using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.Cinema;
using Microsoft.EntityFrameworkCore;
using API.Cinema.DTOs;

namespace API.Cinema.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public TicketsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GetTicketsResultDto>>
            GetTicketsAsync()
        {
            var tickets = new GetTicketsResultDto {
                Tickets = await _context.Tickets.CountAsync(),
                Sum = await _context.Tickets.SumAsync(t => t.Price)
            };
            return tickets;
        }

        [HttpGet("{showName}")]
        public async Task<ActionResult<Object>> GetShowTicketsAsync(
            string showName)
        {
            var show = await _context.Showtimes.FindAsync(showName);
            var showTickets = await _context.Tickets
                .Where(t => t.Showid == showName)
                .ToListAsync();
            var movie = await _context.Movies.FindAsync(show.Movieid);
            var room = await _context.Rooms.FindAsync(show.Roomid);
            var distinctRows = showTickets
                .Select(t=>t.Rownum)
                .Distinct()
                .OrderBy(i => i);
            var tickets = distinctRows.Aggregate<int, List<Object>>(
                new List<Object>(),
                (acc,rowNum) => {
                    var seats = showTickets
                        .Where(t => t.Rownum == rowNum)
                        .Select(t => t.Seatnum)
                        .OrderBy(i => i);
                    acc.Add( new {
                        row = rowNum,
                        seats
                    });
                    return acc;
                });
            return new
            {
                show = movie.Title,
                room = room.Name,
                schedule = show.Start,
                tickets
            };
        }

    }
}
