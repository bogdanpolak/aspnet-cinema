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
using Data.Cinema.Models;

namespace API.Cinema.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly CinemaContext _context;
        private readonly IShowRepository _showRepository;

        public ShowsController(CinemaContext context, IShowRepository showRepository)
        {
            _context = context;
            _showRepository = showRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ShowResult[]>> GetAsync()
        {
            return new List<ShowData>(await _showRepository.GetAll())
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
            var show = await _showRepository.FindByShowId(showId);
            return show == null
                ? NotFound()
                : (ActionResult<ShowOneResult>)new ShowOneResult
                {
                    Showid = show.Showid,
                    MovieId = show.Movieid,
                    RoomId = show.Roomid,
                    Start = show.Start
            };
        }


        [HttpGet("{showId}/tickets")]
        public async Task<ActionResult<GetTicketsForShowResultDto>>
            GetTicketsForShowAsync(string showId)
        {
            var show = await _showRepository.FindByShowId(showId);
            var showTickets = await _showRepository.GetShowTickets(showId);
            var tickets = GetTicketsForShow(showTickets);
            return new GetTicketsForShowResultDto(
                show.Movie,
                show.Room,
                show.Start,
                tickets
            );
        }

        private List<GetTicketsForShowResultDto.RowSeats> GetTicketsForShow(
            IList<ShowTicketsData> showTickets)
        {
            var distinctRows = showTickets
                .Select(t => t.RowNum)
                .Distinct()
                .OrderBy(i => i)
                .ToList();
            return distinctRows.Aggregate(
                new List<GetTicketsForShowResultDto.RowSeats>(),
                (acc, rowNum) =>
                {
                    acc.Add(new GetTicketsForShowResultDto.RowSeats(
                        rowNum,
                        showTickets
                            .Where(t => t.RowNum == rowNum)
                            .Select(t => t.SeatNum)
                            .OrderBy(i => i)
                            .ToList()
                    ));
                    return acc;
                });
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
