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
        public void Add(RoomInformation entity)
        {
            RoomDAO.AddRoom(entity);
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

        public bool Update(RoomInformation entity)
        {
            return RoomDAO.UpdateRoomInformation(entity);
        }
    }
}
