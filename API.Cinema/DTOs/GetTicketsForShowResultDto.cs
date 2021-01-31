using System;
using System.Collections.Generic;

namespace API.Cinema.DTOs
{
    public class GetTicketsForShowResultDto
    {
        public string Show { get; }
        public string Room { get; }
        public DateTime Schedule { get; }
        public List<object> Tickets { get; }

        public GetTicketsForShowResultDto(string show, string room,
            DateTime schedule, List<object> tickets)
        {
            Show = show;
            Room = room;
            Schedule = schedule;
            Tickets = tickets;
        }

        public override bool Equals(object obj)
        {
            return obj is GetTicketsForShowResultDto other &&
                   Show == other.Show &&
                   Room == other.Room &&
                   Schedule == other.Schedule &&
                   EqualityComparer<List<object>>.Default.Equals(Tickets, other.Tickets);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Show, Room, Schedule, Tickets);
        }
    }
}
