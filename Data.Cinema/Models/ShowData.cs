using System;
namespace Data.Cinema.DataAccess
{
    public class ShowData
    {
        public string Showid { get; set; }
        public int Movieid { get; set; }
        public int Roomid { get; set; }
        public string Movie { get; set; }
        public string Room { get; set; }
        public DateTime Start { get; set; }
        public int Seats { get; set; }
        public int Sold { get; set; }
        public decimal Total { get; set; }
    }
}
