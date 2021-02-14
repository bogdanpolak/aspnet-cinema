using System;

namespace API.Cinema.DTOs
{
    public class ShowRequest
    {
        public int Showid { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime Start { get; set; }
    }
}
