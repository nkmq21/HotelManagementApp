using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Update(T entity);
        void Delete(T entity);
        List<RoomInformation> GetAvailableRooms(DateTime checkinDate, DateTime checkoutDate, RoomType selectedRoomType, int numberOfGuests);
    }
}
