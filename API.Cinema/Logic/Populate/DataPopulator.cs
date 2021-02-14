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
            return new List<Show>();
        }

        public static List<Ticket> GenerateTickets(IEnumerable<Show> shows)
        {
            return new List<Ticket>();
        }

    }
}
