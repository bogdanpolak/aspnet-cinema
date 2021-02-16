using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
/* Cinema */
using API.Cinema.DTOs;
using Data.Cinema.Models;
using Data.Cinema.Repositories;

namespace API.Cinema.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IShowRepository _showRepository;

        public ShowsController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        [HttpGet]
        public async Task<ActionResult<ShowResult[]>> GetAsync()
        {
            return new List<ShowExData>(await _showRepository.GetAll())
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
        public async Task<ActionResult<ShowOneResult>> GetShowOne(int showId)
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
        public async Task<ActionResult<ShowTicketsResult>>
            GetTicketsForShowAsync(int showId)
        {
            var show = await _showRepository.FindByShowIdWithDetails(showId);
            var showTickets = await _showRepository.GetShowTickets(showId);
            var tickets = BuildRowSeatsArray(showTickets);
            return new ShowTicketsResult {
                Movie = show.Movie,
                Room = show.Room,
                Start = show.Start,
                Tickets = tickets
            };
        }

        private List<ShowTicketsResult.RowSeats> BuildRowSeatsArray(
            IList<ShowTicketsData> showTickets)
        {
            var distinctRows = showTickets
                .Select(t => t.RowNum)
                .Distinct()
                .OrderBy(i => i)
                .ToList();
            return distinctRows.Aggregate(
                new List<ShowTicketsResult.RowSeats>(),
                (acc, rowNum) =>
                {
                    acc.Add(new ShowTicketsResult.RowSeats {
                        Row = rowNum,
                        Seats = showTickets
                            .Where(t => t.RowNum == rowNum)
                            .Select(t => t.SeatNum)
                            .OrderBy(i => i)
                            .ToList()
                    });
                    return acc;
                });
        }


        [HttpPut("{showId}")]
        public async Task<IActionResult> PutShow(int showId, ShowRequest show)
        {
            if (showId != show.Showid) return BadRequest();
            await _showRepository.UpdateShow(new ShowData {
                Showid = show.Showid,
                Movieid = show.MovieId,
                Roomid = show.RoomId,
                Start = show.Start
            });
            return NoContent();
        }

        // POST: api/Show
        // To protect from overposting attacks, enable the specific
        // properties you want to bind to, for more details, see
        // https://go.microsoft.com/fwlink/?linkid=2123754.
        /*
        [HttpPost]
        public async Task<ActionResult<Show>> PostShow(Show show)
        {
            _context.Show.Add(show);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShowExists(show.Showid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetShow", new { id = show.Showid }, show);
        }
        */

        // To protect from overposting attacks, enable the specific 
        // properties you want to bind to, for more details, see
        //  https://go.microsoft.com/fwlink/?linkid=2123754.

        // DELETE: api/Show/5
        /*
        [HttpDelete("{id}")]
        public async Task<ActionResult<Show>> DeleteShow(string id)
        {
            var show = await _context.Show.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }

            _context.Show.Remove(show);
            await _context.SaveChangesAsync();

            return show;
        }
        */

    }
}
