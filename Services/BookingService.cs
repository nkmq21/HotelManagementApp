using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService(IRepository<Booking> bookingRepository) : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository = bookingRepository;

        public void AddBooking(Booking booking)
        {
            ValidateBooking(booking);
            if (!IsRoomAvailable(booking.RoomId, booking.CheckinTime, booking.CheckoutTime))
            {
                throw new InvalidOperationException("Room is not available");
            }
            _bookingRepository.Add(booking);
        }

        public void ValidateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            if (booking.CheckinTime >= booking.CheckoutTime)
            {
                throw new InvalidOperationException("Checkout time must be after checkin time");
            }
            if (booking.CheckinTime < booking.CheckoutTime)
            {
                throw new InvalidOperationException("Cannot book a room in the past");
            }
        }

        private bool IsRoomAvailable(int roomId, DateTime checkinTime, DateTime checkoutTime)
        {
            var existingBooking = _bookingRepository.GetAll().Where(b => b.RoomId == roomId && b.BookingStatus).ToList();
            foreach (var booking in existingBooking)
            {
                if (checkinTime < booking.CheckoutTime && checkoutTime > booking.CheckinTime)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Booking> GetAllBooking()
        {
            return _bookingRepository.GetAll();
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public void RemoveBooking(Booking booking)
        {
            _bookingRepository.Delete(booking);
        }

        public bool UpdateBooking(Booking booking)
        {
            return _bookingRepository.Update(booking);
        }
    }
}
