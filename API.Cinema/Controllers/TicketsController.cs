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
    public class TicketsController : ControllerBase
    {
        private readonly CinemaContext _context;

        public TicketsController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Object>> GetTicketsAsync()
        {
            var tickets = _context.Tickets
                .CountAsync();
            var sum = _context.Tickets
                .SumAsync(t => t.Price);
            return new
            {
                tickets = await tickets,
                sum = await sum
            };
        }

    }
}
