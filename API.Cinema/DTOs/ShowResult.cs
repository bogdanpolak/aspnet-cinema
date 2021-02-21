using System;

namespace API.Cinema.DTOs
{
    public class ShowResult
    {
        public int ShowId { get; set; }
        public string Movie { get; set; }
        public string Room { get; set; }
        public int Rate { get; set; }
        public DateTime LaunchDate { get; set; }
        public DateTime Start { get; set; }
        public int Seats { get; set; }
        public int Sold { get; set; }
        public decimal Total { get; set; }
    }
}
