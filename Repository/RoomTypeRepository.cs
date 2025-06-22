using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Models;

namespace Repository
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        public bool Add(RoomType entity)
        {
            return RoomTypeDAO.AddRoomType(entity);
        }

        public void Delete(RoomType entity)
        {
            RoomTypeDAO.RemoveRoomType(entity);
        }

        public IEnumerable<RoomType> GetAll()
        {
            return RoomTypeDAO.GetAllRoomTypes();
        }

        public List<RoomInformation> GetAvailableRooms(DateTime checkinDate, DateTime checkoutDate, RoomType selectedRoomType, int numberOfGuests)
        {
            throw new NotImplementedException();
        }

        public RoomType GetById(int id)
        {
            return RoomTypeDAO.GetRoomTypeById(id);
        }

        public bool Update(RoomType entity)
        {
            return RoomTypeDAO.UpdateRoomType(entity);
        }

        bool IRepository<RoomType>.Add(RoomType entity)
        {
            throw new NotImplementedException();
        }
    }
}
