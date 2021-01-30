using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Data.Cinema
{
    public partial class Ticket
    {
        public int Ticketid { get; set; }
        public string Showid { get; set; }
        public int Rownum { get; set; }
        public int Seatnum { get; set; }
        public decimal Price { get; set; }

        public virtual Showtime Show { get; set; }
    }
}
