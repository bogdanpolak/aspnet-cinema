﻿using System;

namespace Data.Cinema.Models
{
    public class ShowExData
    {
        public int Showid { get; internal set; }
        public int Movieid { get; internal set; }
        public int Roomid { get; internal set; }
        public string Movie { get; internal set; }
        public int Rate { get; internal set; }
        public DateTime LaunchDate { get; set; }
        public string Room { get; internal set; }
        public DateTime Start { get; internal set; }
        public int Seats { get; internal set; }
        public int Sold { get; internal set; }
        public decimal Total { get; internal set; }
    }
}
