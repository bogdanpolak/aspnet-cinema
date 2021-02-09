using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.Cinema;
using Microsoft.EntityFrameworkCore;
using API.Cinema.DTOs;
using Data.Cinema.Entites;

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
            Get()
        {
            var tickets = new GetTicketsResultDto {
                Tickets = await _context.Tickets.CountAsync(),
                Sum = await _context.Tickets.SumAsync(t => t.Price)
            };
            return tickets;
        }

        [HttpGet("{ticketId}")]
        public ActionResult<object>
            GetTicket(int ticketId)
        {
            return new { TicketId = ticketId };
        }

        [HttpPost]
        public async Task<ActionResult<Object>>
            PostTicketAsync([FromBody] PostTicketsRequest request)
        {
            var isBooked = _context.Tickets.Any(t =>
                t.Showid == request.ShowKey &&
                t.Rownum == request.RowNum &&
                t.Seatnum == request.SeatNum);

            if (isBooked)
                return BadRequest("Ticket for the show and for a seat already booked");

            var ticket = new Ticket()
            {
                Showid = request.ShowKey,
                Rownum = request.RowNum,
                Seatnum = request.SeatNum,
                Price = request.Price
            };
            _context.Tickets.Add(ticket);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketExists(request.ShowKey, request.RowNum, request.SeatNum))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostTicket",
                new { id = ticket.Ticketid },
                ticket);
        }

        private bool TicketExists(string id, int row, int seat)
            => _context.Tickets
                .Any(e => e.Showid == id &&
                    e.Rownum == row &&
                    e.Seatnum == seat);

    }

}
