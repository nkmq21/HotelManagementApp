using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccessLayer
{
    public class RoomDAO
    {
        private static List<RoomInformation> listRoom = new List<RoomInformation>();

        static RoomDAO()
        {
            // Standard Rooms
            RoomInformation room101 = new RoomInformation(101, "Standard 101", "Cozy room with a queen bed", 2, true, 100.00m, 1);
            RoomInformation room102 = new RoomInformation(102, "Standard 102", "Cozy room with twin beds", 2, true, 100.00m, 1);
            RoomInformation room103 = new RoomInformation(103, "Standard 103", "Cozy room with a king bed", 2, true, 120.00m, 1);

            // Deluxe Rooms
            RoomInformation room201 = new RoomInformation(201, "Deluxe 201", "Spacious room with a king bed and city view", 2, true, 180.00m, 2);
            RoomInformation room202 = new RoomInformation(202, "Deluxe 202", "Spacious room with a queen bed and garden view", 2, true, 170.00m, 2);
            RoomInformation room203 = new RoomInformation(203, "Deluxe 203", "Spacious room with twin beds and mountain view", 2, false, 170.00m, 2);

            // Suites
            RoomInformation room301 = new RoomInformation(301, "Suite 301", "Luxury suite with a king bed and separate living area", 2, true, 300.00m, 3);
            RoomInformation room302 = new RoomInformation(302, "Suite 302", "Luxury suite with a panoramic view", 2, true, 350.00m, 3);

            // Family Rooms
            RoomInformation room401 = new RoomInformation(401, "Family 401", "Large room with a king bed and two twin beds", 4, true, 250.00m, 4);
            RoomInformation room402 = new RoomInformation(402, "Family 402", "Large room with two queen beds", 4, true, 230.00m, 4);

            // Executive Rooms
            RoomInformation room501 = new RoomInformation(501, "Executive 501", "Business room with a king bed and work desk", 1, true, 200.00m, 5);
            RoomInformation room502 = new RoomInformation(502, "Executive 502", "Business room with a queen bed and meeting area", 2, true, 220.00m, 5);

            listRoom = new List<RoomInformation> {
                room101, room102, room103,
                room201, room202, room203,
                room301, room302,
                room401, room402,
                room501, room502
            };
        }

        public static List<RoomInformation> getRoomList()
        {
            return listRoom;
        }

        public static void AddRoom(RoomInformation room)
        {
            if (!listRoom.Contains(room))
            {
                listRoom.Add(room);
            }
        }

        public static void RemoveRoom(RoomInformation room)
        {
            if (listRoom.Contains(room))
            {
                listRoom.Remove(room);
            }
        }

        public static RoomInformation GetRoomById(int id)
        {
            return listRoom.FirstOrDefault(c => c.RoomTypeID == id);
        }

        public static bool UpdateRoomInformation(RoomInformation room)
        {
            RoomInformation roomInfo = new RoomInformation();
            roomInfo = listRoom.FirstOrDefault(r => r.RoomId == room.RoomId);
            if (roomInfo != null)
            {
                roomInfo.RoomName = room.RoomName;
                roomInfo.MaxCapacity = room.MaxCapacity;
                roomInfo.RoomStatus = room.RoomStatus;
                roomInfo.RoomType = room.RoomType;
                return true;
            }
            return false;
        }
    }
}
