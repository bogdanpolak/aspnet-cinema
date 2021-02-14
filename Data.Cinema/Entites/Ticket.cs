using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Data.Cinema.Entites
{
    public partial class Ticket
    {
        public int Ticketid { get; set; }
        public int Showid { get; set; }
        public int Rownum { get; set; }
        public int Seatnum { get; set; }
        public decimal Price { get; set; }

        public virtual Show Show { get; set; }
    }
}
