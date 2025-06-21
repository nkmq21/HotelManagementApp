using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoomService
    {
        IEnumerable<RoomInformation> GetRoomInformation();
        void AddRoom(RoomInformation roomInformation);
        void RemoveRoom(RoomInformation roomInformation);
        bool UpdateRoom(RoomInformation roomInformation);
        RoomInformation GetRoomById(int roomId);

    }
}
