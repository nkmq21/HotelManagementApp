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
        public void Add(Booking entity)
        {
            BookingDAO.AddBooking(entity);
        }

        public void Delete(Booking entity)
        {
            BookingDAO.RemoveBooking(entity);
        }

        public IEnumerable<Booking> GetAll()
        {
            return BookingDAO.GetAllBooking();
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
