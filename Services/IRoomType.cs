using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IRoomType
    {
        IEnumerable<RoomType> GetAllRoomType();
        void AddRoomType(RoomType roomType);
        void RemoveRoomType(RoomType roomType);
        bool UpdateRoomType(RoomType roomType);
        RoomType GetTypeById(int id);
    }
}
