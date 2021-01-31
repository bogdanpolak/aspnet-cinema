using System;

namespace API.Cinema.DTOs
{
    public class GetShowResponseDto
    {
        public string Showid { get; set; }
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime Start { get; set; }
        public int Seats { get; set; }
        public int SoldSeats { get; set; }
        public decimal Total { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GetShowResponseDto other &&
                   Showid == other.Showid &&
                   MovieId == other.MovieId &&
                   RoomId == other.RoomId &&
                   Start == other.Start &&
                   Seats == other.Seats &&
                   SoldSeats == other.SoldSeats &&
                   Total == other.Total;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Showid, MovieId, RoomId, Start, Seats, SoldSeats, Total);
        }
    }
}
