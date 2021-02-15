using System;
using System.Collections.Generic;
using System.Linq;
using Data.Cinema.Entites;

namespace API.Cinema.Logic.Populate
{
    public class DataPopulator
    {
        public static List<Movie> GenerateMovies()
        {
            return new List<Movie> {
                new Movie{ Title = "Jumanji: Level One" },
                new Movie{ Title = "Supernova" },
                new Movie{ Title = "Little Fish" },
                new Movie{ Title = "The Mauritanian" },
                new Movie{ Title = "Tom and Jerry" },
                new Movie{ Title = "Godzilla vs.Kong" },
                new Movie{ Title = "Peter Rabbit 2: The Runaway" },
                new Movie{ Title = "A Quiet Place Part II" },
                new Movie{ Title = "Black Widow" },
                new Movie{ Title = "Ghostbusters: Afterlife" },
                new Movie{ Title = "Top Gun: Maverick" },
                new Movie{ Title = "Dune" },
                new Movie{ Title = "Mission: Impossible 7" },
                new Movie{ Title = "The Matrix 4" },
                new Movie{ Title = "Sherlock Holmes 3" }
            };
        }

        public static List<Room> GenerateRooms() => new List<Room>
            {
                new Room { Name = "Turquouse Room", Rows = 8, Columns = 14 },
                new Room { Name = "Garent Room", Rows = 22, Columns = 16 },
            };

        public static List<Show> GenerateShows(IList<Movie> movies, IList<Room> rooms)
        {
            var weekSchedule = new Dictionary<DayOfWeek, string>() {
                { DayOfWeek.Monday, "18:30" },
                { DayOfWeek.Tuesday, "18:30" },
                { DayOfWeek.Wednesday, "18:00,20:30" },
                { DayOfWeek.Thursday, "18:00,20:30" },
                { DayOfWeek.Friday, "18:00,20:30" },
                { DayOfWeek.Saturday, "13:30,16:00,18:30,21:00" },
                { DayOfWeek.Sunday, "12:00,14:30,17:00,19:30" },
            };
            var ago60 = DateTime.Now.AddDays(-30);
            var startDate = new DateTime(ago60.Year, ago60.Month, 1);
            var endDate = DateTime.Now.AddDays(+7);
            var moveid = 0;
            var shows = new List<Show>();
            foreach (var day in EachDay(startDate,endDate))
            {
                var startStr = weekSchedule[day.DayOfWeek];
                var daySchdulesText = new List<string>(startStr.Split(','));
                var starts = daySchdulesText
                    .Select( t => ParseTime(t) )
                    .ToList();
                var start = new DateTime(day.Year, day.Month, day.Day,
                    starts[0].Hours, starts[0].Hours, starts[0].Seconds);
                shows.Add( new Show
                    { Movieid = moveid+1, Roomid = 1, Start =  start} );
                moveid = (moveid + 1) % movies.Count;
            }
            return shows;
        }

        public static List<Ticket> GenerateTickets(List<Show> shows)
        {
            return new List<Ticket> {
                new Ticket { Showid = 1, Rownum = 6, Seatnum = 9, Price = 15.0M },
                new Ticket { Showid = 1, Rownum = 6, Seatnum = 10, Price = 15.0M }
            };
            /* Tickets (row, seat, price)
            1	10	15
            1	11	15
            2	1	15
            2	3	15
            2	11	12.99
            5	11	10
            5	12	17.5
            5	13	17.5
            5	14	12.5
            5	15	12.5
            5	16	12.5
            */

        }

        private static DateTime StartDate(string date) => DateTime.ParseExact(
            date,
            "yyyy-MM-dd HH:mm",
            System.Globalization.CultureInfo.InvariantCulture);

        private static TimeSpan ParseTime(string time) =>
            TimeSpan.Parse(time);

        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
