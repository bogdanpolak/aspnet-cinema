using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Cinema;

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
        public async Task<ActionResult<IEnumerable<Showtime>>> GetShowtimes()
        {
            return await _context.Showtimes.ToListAsync();
        }

        // GET: api/Show/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetShowtime(string id)
        {
            var show = await _context.Showtimes.FindAsync(id);
            if (show == null) return NotFound();
            var response = new
            {
                showid = id,
                movie = show.Movie.Title,
                roomid = show.Room.Name,
                start = show.Start,
                tickets = show.Tickets.Aggregate<Ticket, List<Object>>(
                    new List<Object>(),
                    (acc, t) => {
                        acc.Add(new
                        {
                            row = t.Rownum,
                            seat = t.Seatnum,
                            price = t.Price
                        });
                        return acc;
                    })
            };
            return response;
        }

        // PUT: api/Show/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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
