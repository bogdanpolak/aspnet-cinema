using System;

namespace API.Cinema.DTOs
{
    public class ShowResult
    {
        public String ShowId { get; set; }
        public String Movie { get; set; }
        public String Room { get; set; }
        public DateTime Start { get; set; }
        public int Seats { get; set; }
        public int Sold { get; set; }
        public decimal Total { get; set; }
    }
}
