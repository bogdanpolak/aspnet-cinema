﻿using System;
using System.Collections.Generic;
using System.Linq;
using Data.Cinema.Entites;

namespace API.Cinema.Logic.Populate
{
    public class DataPopulator
    {
        public static IList<Movie> MovieCollection { get; private set; }
        private static Movie NextMovie { get; set; }

        public static List<Movie> GenerateMovies()
        {
            return new List<Movie> {
                new Movie{ Rate = 1, Title = "Jumanji: Level One" },
                new Movie{ Rate = 2, Title = "Supernova" },
                new Movie{ Rate = 3, Title = "Little Fish" },
                new Movie{ Rate = 2, Title = "The Mauritanian" },
                new Movie{ Rate = 4, Title = "Tom and Jerry" },
                new Movie{ Rate = 3, Title = "Godzilla vs.Kong" },
                new Movie{ Rate = 3, Title = "Peter Rabbit 2: The Runaway" },
                new Movie{ Rate = 1, Title = "A Quiet Place Part II" },
                new Movie{ Rate = 5, Title = "Black Widow" },
                new Movie{ Rate = 2, Title = "Ghostbusters: Afterlife" },
                new Movie{ Rate = 4, Title = "Top Gun: Maverick" },
                new Movie{ Rate = 5, Title = "Dune" },
                new Movie{ Rate = 5, Title = "Mission: Impossible 7" },
                new Movie{ Rate = 5, Title = "The Matrix 4" },
                new Movie{ Rate = 3, Title = "Sherlock Holmes 3" }
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
            var shows = new List<Show>();
            InitMovieId(movies);
            foreach (var day in EachDay(startDate,endDate))
            {
                var startStr = weekSchedule[day.DayOfWeek];
                var daySchdulesText = new List<string>(startStr.Split(','));
                var showsInDay = daySchdulesText
                    .Select(t => ParseTime(t))
                    .Select(start => new Show
                    {
                        Movie = GetMovie(),
                        Room = rooms[0],
                        Start = new DateTime(
                        day.Year, day.Month, day.Day,
                        start.Hours, start.Minutes, 0)
                    })
                    .ToList();
                shows.AddRange( showsInDay );
            }
            return shows;
        }

        public static List<Ticket> GenerateTickets(List<Show> shows)
        {
            var tickets = new List<Ticket>();
            foreach (var show in shows)
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
                tickets.AddRange(ticketsForShow);
            }
            return tickets;
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

        private static void InitMovieId(IList<Movie> movies)
        {
            MovieCollection = movies;
            if (movies.Count == 0) return;
            NextMovie = MovieCollection[0];
        }

        private static Movie GetMovie()
        {
            var movie = NextMovie;
            var idx = MovieCollection.IndexOf(NextMovie);
            var nextIdx = (idx + 1 < MovieCollection.Count) ? idx + 1 : 0;
            NextMovie = MovieCollection[nextIdx]; 
            return movie;
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
