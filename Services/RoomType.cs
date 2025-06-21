using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomType : IRoomType
    {
        private readonly IRepository<RoomType> _roomTypeRepository;
        public RoomType(IRepository<RoomType> roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public void AddRoomType(RoomType roomType)
        {
            _roomTypeRepository.Add(roomType);
        }

        public IEnumerable<RoomType> GetAllRoomType()
        {
            return _roomTypeRepository.GetAll();
        }

        public RoomType GetTypeById(int id)
        {
            return _roomTypeRepository.GetById(id);
        }

        public void RemoveRoomType(RoomType roomType)
        {
            _roomTypeRepository.Delete(roomType);
        }

        public bool UpdateRoomType(RoomType roomType)
        {
            return _roomTypeRepository.Update(roomType);
        }
    }
}
