using System;

namespace API.Cinema.DTOs
{
    public class GetShowsResponseDto
    {
        public String Id { get; set; }
        public String Movie { get; set; }
        public String Room { get; set; }
        public String Date { get; set; }
        public String Time { get; set; }
        public decimal PercentSold { get; set; }
    }
}
