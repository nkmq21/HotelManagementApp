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
        public bool AddRoom(RoomInformation roomInformation)
        {
            return _roomInformationRepository.Add(roomInformation);
        }

        public List<RoomInformation> GetAvailableRooms(DateTime checkinDate, DateTime checkoutDate, RoomType selectedRoomType, int numberOfGuests)
        {
            return _roomInformationRepository.GetAvailableRooms(checkinDate, checkoutDate, selectedRoomType, numberOfGuests);
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
