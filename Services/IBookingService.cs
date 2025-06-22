using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAllBooking();
        bool AddBooking(Booking booking);
        void RemoveBooking(Booking booking);
        bool UpdateBooking(Booking booking);
        Booking GetBookingById(int id);
    }
}
