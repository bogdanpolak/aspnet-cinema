using System;

namespace API.Cinema.DTOs
{
    public class PostTicketsRequest
    {
        public string ShowId { get; set; }
        public int RowNum { get; set; }
        public int SeatNum { get; set; }
        public decimal Price { get; set; }

        public PostTicketsRequest()
        {
        }

        public override bool Equals(object obj)
            => obj is PostTicketsRequest request &&
                ShowId == request.ShowId &&
                RowNum == request.RowNum &&
                SeatNum == request.SeatNum &&
                Price == request.Price;

        public override int GetHashCode()
            => HashCode.Combine(ShowId, RowNum, SeatNum, Price);
    }
}
