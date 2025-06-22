using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRepository<RoomType> _roomTypeRepository;
        public RoomTypeService(IRepository<RoomType> roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public bool AddRoomType(RoomType roomType)
        {
            return _roomTypeRepository.Add(roomType);
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
