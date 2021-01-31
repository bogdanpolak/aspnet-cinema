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
        public async Task<ActionResult<GetTicketsForShowResultDto>>
            GetTicketsForShowAsync(
            string showName)
        {
            var show = await _context.Showtimes.FindAsync(showName);
            var movie = await _context.Movies.FindAsync(show.Movieid);
            var room = await _context.Rooms.FindAsync(show.Roomid);
            var tickets = await BuildTicketsForShow(showName);
            var result = new GetTicketsForShowResultDto(movie.Title,
                room.Name, show.Start, tickets);
            return result;
        }

        private async Task<List<GetTicketsForShowResultDto.RowSeats>>
            BuildTicketsForShow(string showName)
        {
            var showTickets = await _context.Tickets
                .Where(t => t.Showid == showName)
                .ToListAsync();
            var distinctRows = showTickets
                .Select(t => t.Rownum)
                .Distinct()
                .OrderBy(i => i);
            var tickets = distinctRows.Aggregate(
                new List<GetTicketsForShowResultDto.RowSeats>(),
                (acc, rowNum) =>
                {
                    acc.Add(new GetTicketsForShowResultDto.RowSeats(
                        rowNum,
                        showTickets
                        .Where(t => t.Rownum == rowNum)
                        .Select(t => t.Seatnum)
                        .OrderBy(i => i)
                        .ToList()
                    ));
                    return acc;
                });
            return tickets;
        }
    }

}
