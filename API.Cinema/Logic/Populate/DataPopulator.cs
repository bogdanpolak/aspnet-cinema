using System;
using System.Collections.Generic;
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

        public static List<Show> GenerateShows(IEnumerable<Movie> movies, IEnumerable<Room> rooms)
        {
            return new List<Show> {
                new Show { Movieid = 1, Roomid = 1, Start = StartDate("2021-03-03 19:30")}
            };
            /* 
            "2021-03-03 19:30"
            "2021-03-04 19:30"
            "2021-03-05 18:00"
            "2021-03-05 20:30"
            "2021-03-06 12:30"
            "2021-03-06 15:00"
            "2021-03-06 18:00"
            "2021-03-06 20:30"
            */
        }

        public static List<Ticket> GenerateTickets(IEnumerable<Show> shows)
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

    }
}
