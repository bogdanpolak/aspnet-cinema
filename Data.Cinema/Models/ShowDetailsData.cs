﻿using System;
namespace Data.Cinema.Models
{
    public class ShowDetailsData
    {
        public string Showid { get; internal set; }
        public int Movieid { get; internal set; }
        public int Roomid { get; internal set; }
        public string Movie { get; internal set; }
        public string Room { get; internal set; }
        public DateTime Start { get; internal set; }
    }
}
