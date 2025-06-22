using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Models;

namespace Repository
{
    public class BookingRepository : IRepository<Booking>
    {
        public bool Add(Booking entity)
        {
            return BookingDAO.AddBooking(entity);
        }

        public void Delete(Booking entity)
        {
            BookingDAO.RemoveBooking(entity);
        }

        public IEnumerable<Booking> GetAll()
        {
            return BookingDAO.GetAllBooking();
        }

        public List<RoomInformation> GetAvailableRooms(DateTime checkinDate, DateTime checkoutDate, RoomType selectedRoomType, int numberOfGuests)
        {
            throw new NotImplementedException();
        }

        public Booking GetById(int id)
        {
            return BookingDAO.GetBookingById(id);
        }

        public bool Update(Booking entity)
        {
            return BookingDAO.UpdateBooking(entity);
        }
    }
}
