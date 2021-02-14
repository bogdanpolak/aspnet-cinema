﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Cinema.Entites
{
    public partial class Movie
    {
        public Movie()
        {
            Showtimes = new HashSet<Show>();
        }

        public int Movieid { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Show> Showtimes { get; set; }
    }
}
