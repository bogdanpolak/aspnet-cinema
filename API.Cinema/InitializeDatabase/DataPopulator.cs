using System;
using System.Collections.Generic;
using System.Linq;
using Data.Cinema.Entites;

namespace API.Cinema.InitializeDatabase
{
    public class DataPopulator
    {
        const int CleaningTime = 35;

        public List<Movie> Movies { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Show> Shows { get; set; }
        public List<Ticket> Tickets { get; set; }

        private int CurrentMovieIdx { get; set; }

        internal void GenerateData()
        {
            GenerateMovies();
            GenerateRooms();
            GenerateShows();
            GenerateTickets();
        }

        private void GenerateMovies()
        {
            Movies = new List<Movie> {
                new Movie{ LaunchDate = Launch(0*7), Length = 100, IMDB = 7.2, Rate = 5, Title = "A Quiet Place Part II" },
                new Movie{ LaunchDate = Launch(0*7), Length = 103, IMDB = 6.3, Rate = 3, Title = "Freaky" },
                new Movie{ LaunchDate = Launch(1*7), Length = 95, IMDB = 7.0, Rate = 4, Title = "The Croods: A New Age" },
                new Movie{ LaunchDate = Launch(1*7), Length = 93, IMDB = 6.2, Rate = 1, Title = "All My Life" },
                new Movie{ LaunchDate = Launch(2*7), Length = 96, IMDB = 5.9, Rate = 1, Title = "Half Brothers" },
                new Movie{ LaunchDate = Launch(2*7), Length = 125, IMDB = 6.2, Rate = 2, Title = "Pinocchio" },
                new Movie{ LaunchDate = Launch(3*7), Length = 103, IMDB = 6.6, Rate = 4, Title = "Jumanji: Level One" },
                new Movie{ LaunchDate = Launch(4*7), Length = 95, IMDB = 6.7, Rate = 3, Title = "Supernova" },
                new Movie{ LaunchDate = Launch(4*7), Length = 107, IMDB = 6.9, Rate = 3, Title = "Little Fish" },
                new Movie{ LaunchDate = Launch(5*7), Length = 129, IMDB = 7.1, Rate = 2, Title = "The Mauritanian" },
                new Movie{ LaunchDate = Launch(6*7), Length = 101, IMDB = 6.6, Rate = 4, Title = "Tom and Jerry" },
                new Movie{ LaunchDate = Launch(6*7), Length = 105, IMDB = 6.9, Rate = 3, Title = "Godzilla vs.Kong" },
                new Movie{ LaunchDate = Launch(7*7), Length = 93, IMDB = 7.1, Rate = 3, Title = "Peter Rabbit 2: The Runaway" },
                new Movie{ LaunchDate = Launch(8*7), Length = 133, IMDB = 7.2, Rate = 5, Title = "Black Widow" },
                new Movie{ LaunchDate = Launch(9*7), Length = 94, IMDB = 6.1, Rate = 2, Title = "Ghostbusters: Afterlife" },
                new Movie{ LaunchDate = Launch(10*7), Length = 89, IMDB = 7.2, Rate = 4, Title = "Top Gun: Maverick" },
                new Movie{ LaunchDate = Launch(11*7), Length = 98, IMDB = 7.9, Rate = 5, Title = "Dune" },
                new Movie{ LaunchDate = Launch(11*7), Length = 102, IMDB = 6.7, Rate = 5, Title = "Mission: Impossible 7" },
                new Movie{ LaunchDate = Launch(12*7), Length = 117, IMDB = 7.8, Rate = 5, Title = "The Matrix 4" },
                new Movie{ LaunchDate = Launch(12*7), Length = 103, IMDB = 6.2, Rate = 3, Title = "Sherlock Holmes 3" }
            };
            for (int idx = 0; idx < Movies.Count; idx++)
                Movies[idx].Movieid = idx + 1;
        }

        private void GenerateRooms()
        {
            Rooms = new List<Room>
            {
                new Room { Roomid = 1, Name = "Turquouse Room", Rows = 8, Columns = 14 },
                new Room { Roomid = 2, Name = "Garent Room", Rows = 22, Columns = 16 },
            };
        }

        private void GenerateShows()
        {
            Shows = new List<Show>();
            var ago60 = DateTime.Now.AddDays(-30);
            var startDate = new DateTime(ago60.Year, ago60.Month, 1);
            var endDate = DateTime.Now.AddDays(+7);
            CurrentMovieIdx = 0;
            foreach (var day in EachDay(startDate,endDate))
            {
                var showsInADay = GenerateOneDayShows(day);
                Shows.AddRange( showsInADay );
            }
            for (int idx = 0; idx < Shows.Count; idx++)
                Shows[idx].Showid = idx + 1;
        }

        private IEnumerable<Show> GenerateOneDayShows(DateTime day)
        {
            var weekSchedule = new Dictionary<DayOfWeek, (TimeSpan, TimeSpan)>() {
                { DayOfWeek.Monday, (ParseTime("18:30"), ParseTime("19:30")) },
                { DayOfWeek.Tuesday, (ParseTime("18:30"), ParseTime("19:30")) },
                { DayOfWeek.Wednesday, (ParseTime("18:30"), ParseTime("20:30")) },
                { DayOfWeek.Thursday, (ParseTime("18:30"), ParseTime("20:30")) },
                { DayOfWeek.Friday, (ParseTime("17:30"), ParseTime("21:00")) },
                { DayOfWeek.Saturday, (ParseTime("12:30"), ParseTime("21:30")) },
                { DayOfWeek.Sunday, (ParseTime("11:30"), ParseTime("20:30")) },
            };
            var (firstShow, lastShow) = weekSchedule[day.DayOfWeek];
            var starts = new List<TimeSpan>();
            Rooms.ForEach(room => starts.Add(firstShow));
            var minTime = starts.Min();
            var showsInADay = new List<Show>();
            while (minTime<lastShow)
            {
                var roomidx = starts.IndexOf(minTime);
                var movie = Movies[CurrentMovieIdx];
                CurrentMovieIdx = (CurrentMovieIdx + 1 < Movies.Count) ? CurrentMovieIdx + 1 : 0;
                showsInADay.Add(new Show
                {
                    Movie = movie,
                    Room = Rooms[roomidx],
                    Start = day + minTime
                });
                var fullLength = (int)Math.Ceiling((double)(movie.Length + CleaningTime) / 10) * 10;
                starts[roomidx] = minTime + new TimeSpan(0, fullLength, 0);
                minTime = starts.Min();
            }
            return showsInADay;
        }

        private void GenerateTickets()
        {
            Tickets = new List<Ticket>();
            foreach (var show in Shows)
            {
                var percentToSold = PercentTicketsToSold(show.Start);
                int toSold = Convert.ToInt32(
                    Math.Round(show.Room.TotalSeats() * percentToSold));
                var seats = show.Room.Columns;
                var ticketsForShow = Enumerable.Range(0, toSold-1)
                    .Select(idx => new Ticket
                    {
                        Show = show,
                        Rownum = idx/seats+1,
                        Seatnum = idx%seats+1,
                        Price = 9.0M
                    })
                    .ToList();
                Tickets.AddRange(ticketsForShow);
            }
        }

        private static double PercentTicketsToSold(DateTime start)
        {
            var days = (start - DateTime.Now).TotalDays;
            if (days < 0)
                return 0.6D;
            if (days <= 1)
                return 0.3D;
            if (days <= 3)
                return 0.2D;
            else
                return 0.1D;
        }


        private static DateTime StartDate(string date) => DateTime.ParseExact(
            date,
            "yyyy-MM-dd HH:mm",
            System.Globalization.CultureInfo.InvariantCulture);

        private static DateTime Launch(int launchOffset)
            => DateTime.Now.Date.AddDays(launchOffset - 16*7);

        private static TimeSpan ParseTime(string time) =>
            TimeSpan.Parse(time);

        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
