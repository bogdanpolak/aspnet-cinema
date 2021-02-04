using System;

namespace API.Cinema.DTOs
{
    public class PostTicketsRequest
    {
        public string ShowKey { get; set; }
        public int RowNum { get; set; }
        public int SeatNum { get; set; }
        public decimal Price { get; set; }

        public PostTicketsRequest()
        {
        }

        public override bool Equals(object obj)
            => obj is PostTicketsRequest request &&
                ShowKey == request.ShowKey &&
                RowNum == request.RowNum &&
                SeatNum == request.SeatNum &&
                Price == request.Price;

        public override int GetHashCode()
            => HashCode.Combine(ShowKey, RowNum, SeatNum, Price);
    }
}
