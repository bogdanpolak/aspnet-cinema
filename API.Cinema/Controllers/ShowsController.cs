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

namespace API.Cinema.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public ShowsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: api/Show
        [HttpGet]
        public async Task<ActionResult<GetShowsResultDto>>
            GetShowsAsync()
        {
            var showsInDataBase = await _context.Showtimes.ToListAsync();
            var shows = showsInDataBase
                .Aggregate(
                    new List<GetShowsResultDto.Show>(),
                    (acc, show) =>
                    {
                        var percentSold = Math.Round(
                            show.Tickets.Count() / (decimal)show.Room.Rows
                            / show.Room.Columns * 100, 1);
                        var startDay = show.Start.ToString("yyyy-MM-dd");
                        var startTime = show.Start.ToString("HH:mm");
                        acc.Add(new GetShowsResultDto.Show(
                            show.Showid, show.Movie.Title, show.Room.Name,
                            startDay, startTime, percentSold));
                        return acc;
                    })
                .OrderBy(r => r.Date)
                .ThenBy(r => r.Time)
                .ToList();
            var result = new GetShowsResultDto(shows);
            return result;
        }


        // GET: api/Show/{show-id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetShowResultDto>>
            GetShowAsync(string id)
        {
            var show = await _context.Showtimes.FindAsync(id);
            if (show == null) return NotFound();
            var result = new GetShowResultDto (
                showid: id,
                movieId: show.Movieid,
                roomId: show.Roomid,
                start: show.Start,
                seats: show.Room.Rows * show.Room.Columns,
                soldSeats: show.Tickets.Count(),
                total: show.Tickets.Sum(t => t.Price)
            );
            return result;
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
