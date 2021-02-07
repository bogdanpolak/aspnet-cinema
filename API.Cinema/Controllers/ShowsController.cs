using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Cinema;
using API.Cinema.DTOs;
using Data.Cinema.Entites;
using Data.Cinema.DataAccess;

namespace API.Cinema.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly CinemaContext _context;
        private readonly IShowRepository showRepository;

        public ShowsController(CinemaContext context, IShowRepository showRepository)
        {
            _context = context;
            this.showRepository = showRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ShowResult[]>> GetAsync()
        {
            return new List<ShowData>(await showRepository.GetAll())
                .Select(show => new ShowResult
                {
                    ShowId = show.Showid,
                    Movie = show.Movie,
                    Room = show.Room,
                    Start = show.Start,
                    Seats = show.Seats,
                    Sold = show.Sold,
                    Total = show.Total
                })
                .ToArray();
        }


        [HttpGet("{showId}")]
        public async Task<ActionResult<ShowOneResult>> GetShowOne(string showId)
        {
            var show = await _context.Showtimes.FindAsync(showId);
            if (show == null) return NotFound();
            return new ShowOneResult
            {
                Showid = show.Showid,
                MovieId = show.Movieid,
                RoomId = show.Roomid,
                Start = show.Start
            };
        }


        [HttpGet("{showName}/tickets")]
        public async Task<ActionResult<GetTicketsForShowResultDto>>
            GetTicketsForShowAsync(string showName)
        {
            var show = await _context.Showtimes.FindAsync(showName);
            var result = new GetTicketsForShowResultDto(
                show.Movie.Title,
                show.Room.Name,
                show.Start,
                BuildTicketsForShow(show.Tickets.ToList())
            );
            return result;
        }

        private static List<GetTicketsForShowResultDto.RowSeats>
            BuildTicketsForShow(IList<Ticket> showTickets)
        {
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


        // PUT: api/Show/5
        // To protect from overposting attacks, enable the specific 
        // properties you want to bind to, for more details, see
        //  https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShowtime(string id, Showtime showtime)
        {
            if (id != showtime.Showid)
            {
                return BadRequest();
            }

            _context.Entry(showtime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowtimeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Show
        // To protect from overposting attacks, enable the specific
        // properties you want to bind to, for more details, see
        // https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Showtime>> PostShowtime(Showtime showtime)
        {
            _context.Showtimes.Add(showtime);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShowtimeExists(showtime.Showid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShowtime", new { id = showtime.Showid }, showtime);
        }

        // DELETE: api/Show/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Showtime>> DeleteShowtime(string id)
        {
            var showtime = await _context.Showtimes.FindAsync(id);
            if (showtime == null)
            {
                return NotFound();
            }

            _context.Showtimes.Remove(showtime);
            await _context.SaveChangesAsync();

            return showtime;
        }

        private bool ShowtimeExists(string id)
        {
            return _context.Showtimes.Any(e => e.Showid == id);
        }
    }
}
