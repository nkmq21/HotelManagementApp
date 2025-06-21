using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer
{
    public class BookingDAO
    {
        private static readonly List<Booking> listBooking = new List<Booking>();

        static BookingDAO()
        {
            // Past bookings
            Booking booking1 = new Booking(1, 1, 101,
                new DateTime(2025, 5, 10), new DateTime(2025, 5, 12),
                200.00m, new DateTime(2025, 4, 15), true);

            Booking booking2 = new Booking(2, 3, 201,
                new DateTime(2025, 5, 15), new DateTime(2025, 5, 20),
                900.00m, new DateTime(2025, 4, 20), true);

            // Current bookings
            Booking booking3 = new Booking(3, 2, 301,
                new DateTime(2025, 6, 5), new DateTime(2025, 6, 10),
                1500.00m, new DateTime(2025, 5, 1), true);

            Booking booking4 = new Booking(4, 5, 401,
                new DateTime(2025, 6, 10), new DateTime(2025, 6, 15),
                1250.00m, new DateTime(2025, 5, 10), true);

            Booking booking5 = new Booking(5, 4, 202,
                new DateTime(2025, 6, 7), new DateTime(2025, 6, 9),
                340.00m, new DateTime(2025, 5, 25), true);

            // Future bookings
            Booking booking6 = new Booking(6, 6, 102,
                new DateTime(2025, 7, 1), new DateTime(2025, 7, 5),
                400.00m, new DateTime(2025, 6, 1), true);

            Booking booking7 = new Booking(7, 7, 302,
                new DateTime(2025, 7, 10), new DateTime(2025, 7, 15),
                1750.00m, new DateTime(2025, 6, 5), true);

            Booking booking8 = new Booking(8, 8, 402,
                new DateTime(2025, 7, 20), new DateTime(2025, 7, 25),
                1150.00m, new DateTime(2025, 6, 10), true);

            Booking booking9 = new Booking(9, 9, 501,
                new DateTime(2025, 8, 1), new DateTime(2025, 8, 3),
                400.00m, new DateTime(2025, 6, 15), true);

            Booking booking10 = new Booking(10, 10, 502,
                new DateTime(2025, 8, 5), new DateTime(2025, 8, 10),
                1100.00m, new DateTime(2025, 6, 17), true);

            // Cancelled booking
            Booking booking11 = new Booking(11, 1, 103,
                new DateTime(2025, 8, 15), new DateTime(2025, 8, 20),
                600.00m, new DateTime(2025, 6, 1), false);

            listBooking = new List<Booking> {
                booking1, booking2, booking3, booking4, booking5,
                booking6, booking7, booking8, booking9, booking10, booking11
            };
        }

        public static void AddBooking(Booking booking)
        {
            if (!listBooking.Any(b => b.BookingId == booking.BookingId))
            {
                listBooking.Add(booking);
            }
        }

        public static void RemoveBooking(Booking booking)
        {
            if (listBooking.Any(b => b.BookingId == booking.BookingId))
            {
                listBooking.Remove(booking);
            }
        }

        public static List<Booking> GetAllBooking()
        {
            if (listBooking != null)
            {
                return listBooking;
            }
            else
            {
                return null;
            }
        }

        public static bool UpdateBooking(Booking booking)
        {
            Booking? booked = listBooking.FirstOrDefault(c => c.BookingId == booking.BookingId);
            if (booked != null)
            {
                booked.BookingDate = booking.BookingDate;
                booked.BookingStatus = booking.BookingStatus;
                booked.CheckinTime = booking.CheckinTime;
                booked.CheckoutTime = booking.CheckoutTime;
                return true;
            }
            return false;
        }

        public static Booking GetBookingById(int bookingId)
        {
            Booking? booked = listBooking.FirstOrDefault(c => c.BookingId == bookingId);
            return booked ?? throw new InvalidOperationException($"No booking found with the ID {bookingId}");
        }
    }
}
