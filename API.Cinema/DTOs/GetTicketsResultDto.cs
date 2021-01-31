using System;

namespace API.Cinema.DTOs
{
    public class GetTicketsResultDto
    {
        public int Tickets { get; set; }
        public decimal Sum { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GetTicketsResultDto other &&
                   Tickets == other.Tickets &&
                   Sum == other.Sum;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tickets, Sum);
        }
    }
}
