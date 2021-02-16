using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Cinema.Entites
{
    public partial class Room
    {
        public Room()
        {
            Shows = new HashSet<Show>();
        }

        public int Roomid { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public virtual ICollection<Show> Shows { get; set; }

        public int TotalSeats()
            => Rows * Columns;

    }
}
