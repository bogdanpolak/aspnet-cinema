using System;
using System.Collections.Generic;

namespace API.Cinema.DTOs
{
    public class GetShowsResultDto
    {
        public class Show
        {
            public String Id { get; set; }
            public String Movie { get; set; }
            public String Room { get; set; }
            public String Date { get; set; }
            public String Time { get; set; }
            public decimal PercentSold { get; set; }
        }
        public List<Show> Shows { get; set; }

        public static GetShowsResultDto Create (List<Show> shows) =>
            new GetShowsResultDto { Shows = shows };
    }
}
