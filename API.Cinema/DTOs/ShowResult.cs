using System;

namespace API.Cinema.DTOs
{
    public class ShowResult
    {
        public int ShowId { get; set; }
        public DateTime Start { get; set; }
        public decimal Total { get; set; }
        public MovieDetails Movie { get; set; }
        public RoomDetails Room { get; set; }

        public class MovieDetails
        {
            public string Name { get; set; }
            public int Rate { get; set; }
            public DateTime Launch { get; set; }
        }

        public class RoomDetails
        {
            public string Name { get; set; }
            public int Seats { get; set; }
            public int Sold { get; set; }
        }
    }
}
