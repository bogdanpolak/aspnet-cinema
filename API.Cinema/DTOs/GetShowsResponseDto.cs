using System;

namespace API.Cinema.DTOs
{
    public class GetShowsResponseDto
    {
        public String id { get; set; }
        public String movie { get; set; }
        public String room { get; set; }
        public String date { get; set; }
        public String time { get; set; }
        public int sold { get; set; }
        public int seats { get; set; }
    }
}
