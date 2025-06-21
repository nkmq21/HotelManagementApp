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
        public void Add(RoomType entity)
        {
            RoomTypeDAO.AddRoomType(entity);
        }

        public void Delete(RoomType entity)
        {
            RoomTypeDAO.RemoveRoomType(entity);
        }

        public IEnumerable<RoomType> GetAll()
        {
            return RoomTypeDAO.GetAllRoomTypes();
        }

        public RoomType GetById(int id)
        {
            return RoomTypeDAO.GetRoomTypeById(id);
        }

        public bool Update(RoomType entity)
        {
            return RoomTypeDAO.UpdateRoomType(entity);
        }
    }
}
