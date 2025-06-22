using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomRepository : IRepository<RoomInformation>
    {
        public bool Add(RoomInformation entity)
        {
            return RoomDAO.AddRoom(entity);
        }

        public void Delete(RoomInformation entity)
        {
            RoomDAO.RemoveRoom(entity); 
        }

        public IEnumerable<RoomInformation> GetAll()
        {
            return RoomDAO.getRoomList();
        }

        public RoomInformation GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<RoomInformation> GetAvailableRooms(DateTime checkinDate, DateTime checkoutDate, RoomType selectedRoomType, int numberOfGuests)
        {
            return RoomDAO.GetAvailableRooms(checkinDate, checkoutDate, selectedRoomType, numberOfGuests);
        }


        public bool Update(RoomInformation entity)
        {
            return RoomDAO.UpdateRoomInformation(entity);
        }

        bool IRepository<RoomInformation>.Add(RoomInformation entity)
        {
            throw new NotImplementedException();
        }
    }
}
