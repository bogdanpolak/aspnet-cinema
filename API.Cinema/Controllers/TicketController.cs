using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Data.Cinema;
using Microsoft.EntityFrameworkCore;

namespace API.Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly CinemaContext _context;

        public TicketController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet("{showName}")]
        public ActionResult<IEnumerable<Ticket>> GetTicket(string showName)
        {
            return _context.Tickets.Where(t => t.Showid == showName).ToList();
        }

    }
}
