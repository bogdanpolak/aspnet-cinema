using System;

namespace API.Cinema.DTOs
{
    public class GetShowResultDto
    {
        public string Showid { get; }
        public int MovieId { get; }
        public int RoomId { get; }
        public DateTime Start { get; }
        public int Seats { get; }
        public int SoldSeats { get; }
        public decimal Total { get; }


        public GetShowResultDto(string showid, int movieId, int roomId,
            DateTime start, int seats, int soldSeats, decimal total)
        {
            Showid = showid;
            MovieId = movieId;
            RoomId = roomId;
            Start = start;
            Seats = seats;
            SoldSeats = soldSeats;
            Total = total;
        }

        public override bool Equals(object obj)
        {
            return obj is GetShowResultDto other &&
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
