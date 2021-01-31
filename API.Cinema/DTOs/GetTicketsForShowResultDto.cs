using System;
using System.Collections.Generic;

namespace API.Cinema.DTOs
{
    public class GetTicketsForShowResultDto
    {
        public string Show { get; }
        public string Room { get; }
        public DateTime Schedule { get; }
        public List<RowSeats> Tickets { get; }

        public GetTicketsForShowResultDto(string show, string room,
            DateTime schedule, List<RowSeats> tickets)
        {
            Show = show;
            Room = room;
            Schedule = schedule;
            Tickets = tickets;
        }

        public class RowSeats
        {
            public int Row { get; }
            public IList<int> Seats { get; }

            public RowSeats(int row, IList<int> seats)
            {
                Row = row;
                Seats = seats;
            }

            public override bool Equals(object obj)
            {
                return obj is RowSeats other &&
                       Row == other.Row &&
                       EqualityComparer<IList<int>>.Default.Equals(Seats, other.Seats);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Row, Seats);
            }
        }


        public override bool Equals(object obj)
        {
            return obj is GetTicketsForShowResultDto other &&
                   Show == other.Show &&
                   Room == other.Room &&
                   Schedule == other.Schedule &&
                   EqualityComparer<List<RowSeats>>.Default.Equals(Tickets, other.Tickets);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Show, Room, Schedule, Tickets);
        }
    }
}
