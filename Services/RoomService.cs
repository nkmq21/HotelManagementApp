using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<RoomInformation> _roomInformationRepository;

        public RoomService(IRepository<RoomInformation> roomInformationRepository)
        {
            _roomInformationRepository = roomInformationRepository;
        }
        public void AddRoom(RoomInformation roomInformation)
        {
            _roomInformationRepository.Add(roomInformation);
        }

        public RoomInformation GetRoomById(int roomId)
        {
            return _roomInformationRepository.GetById(roomId);
        }

        public IEnumerable<RoomInformation> GetRoomInformation()
        {
            return _roomInformationRepository.GetAll();
        }

        public void RemoveRoom(RoomInformation roomInformation)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRoom(RoomInformation roomInformation)
        {
            throw new NotImplementedException();
        }
    }
}
