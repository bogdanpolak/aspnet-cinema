using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Cinema.Entites
{
    public partial class Movie
    {
        public Movie()
        {
            Shows = new HashSet<Show>();
        }

        public int Movieid { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public DateTime LaunchDate { get; set; }
        public double IMDB { get; set; }
        public int Length { get; set; }

        public virtual ICollection<Show> Shows { get; set; }
    }
}
