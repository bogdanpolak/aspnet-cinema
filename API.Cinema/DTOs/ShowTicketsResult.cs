using System;
using System.Collections.Generic;

namespace API.Cinema.DTOs
{
    public class ShowTicketsResult
    {
        public int ShowId { get; set; }
        public String Movie { get; set; }
        public String Room { get; set; }
        public DateTime Start { get; set; }
        public List<RowSeats> Tickets { get; set; }

        public class RowSeats
        {
            public int Row { get; set; }
            public IList<int> Seats { get; set; }
        }

    }
}
