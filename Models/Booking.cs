using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public bool BookingStatus { get; set; }
        public Booking() { }

        public Booking(int bookingId, int customerId, int roomId, DateTime checkinTime, DateTime checkoutTime, decimal totalPrice, DateTime bookingDate, bool bookingStatus)
        {
            BookingId = bookingId;
            CustomerId = customerId;
            RoomId = roomId;
            CheckinTime = checkinTime;
            CheckoutTime = checkoutTime;
            this.totalPrice = totalPrice;
            BookingDate = bookingDate;
            BookingStatus = bookingStatus;
        }
    }
        
}
