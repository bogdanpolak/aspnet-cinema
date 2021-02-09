﻿using System;
namespace Data.Cinema.DataAccess
{
    public class ShowData
    {
        public string Showid { get; internal set; }
        public int Movieid { get; internal set; }
        public int Roomid { get; internal set; }
        public string Movie { get; internal set; }
        public string Room { get; internal set; }
        public DateTime Start { get; internal set; }
        public int Seats { get; internal set; }
        public int Sold { get; internal set; }
        public decimal Total { get; internal set; }
    }
}
