using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Cinema.Entites
{
    public partial class Show
    {
        public Show()
        {
            Tickets = new HashSet<Ticket>();
        }

        public string Showid { get; set; }
        public int Movieid { get; set; }
        public int Roomid { get; set; }
        public DateTime Start { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
