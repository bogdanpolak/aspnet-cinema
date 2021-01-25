using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Cinema
{
    public partial class Movie
    {
        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public int Movieid { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
