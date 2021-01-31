using System;
using System.Collections.Generic;

namespace API.Cinema.DTOs
{
    public class GetShowsResultDto
    {
        public List<Show> Shows { get; }

        public GetShowsResultDto(List<Show> shows)
        {
            Shows = shows;
        }

        public class Show
        {
            public String Id { get; }
            public String Movie { get; }
            public String Room { get; }
            public String Date { get; }
            public String Time { get; }
            public decimal PercentSold { get; set; }

            public Show(string id, string movie, string room, string date,
                string time, decimal percentSold)
            {
                Id = id;
                Movie = movie;
                Room = room;
                Date = date;
                Time = time;
                PercentSold = percentSold;
            }

            public override bool Equals(object obj)
            {
                return obj is Show show &&
                       Id == show.Id &&
                       Movie == show.Movie &&
                       Room == show.Room &&
                       Date == show.Date &&
                       Time == show.Time &&
                       PercentSold == show.PercentSold;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Movie, Room, Date, Time, PercentSold);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is GetShowsResultDto dto &&
                   EqualityComparer<List<Show>>.Default.Equals(Shows, dto.Shows);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Shows);
        }
    }
}
