using Models;

namespace DataAccessLayer
{
    public class RoomTypeDAO
    {
        private static List<RoomType> listRoomType = new List<RoomType>();

        static RoomTypeDAO()
        {
            // Initialize with mock data
            RoomType standard = new RoomType(1, "Standard", "Basic room with essential amenities", "Suitable for 1-2 people");
            RoomType deluxe = new RoomType(2, "Deluxe", "Spacious room with premium amenities", "Includes breakfast");
            RoomType suite = new RoomType(3, "Suite", "Luxury suite with separate living area", "Includes all meals and spa access");
            RoomType family = new RoomType(4, "Family", "Large room suitable for families", "Can accommodate up to 5 people");
            RoomType executive = new RoomType(5, "Executive", "Business-oriented room with workspace", "Includes business center access");

            listRoomType = new List<RoomType> { standard, deluxe, suite, family, executive };
        }

        public static List<RoomType> GetAllRoomTypes()
        {
            return listRoomType;
        }

        public static RoomType GetRoomTypeById(int id)
        {
            RoomType? roomType = listRoomType.FirstOrDefault(rt => rt.TypeId == id);
            return roomType ?? throw new InvalidOperationException($"No room type found with the id {id}");
        }

        public static void AddRoomType(RoomType roomType)
        {
            listRoomType.Add(roomType);
        }

        public static void RemoveRoomType(RoomType roomType) { listRoomType.Remove(roomType); }
        public static bool UpdateRoomType(RoomType roomType)
        {
            RoomType? room = listRoomType.FirstOrDefault(rt => rt.TypeId == roomType.TypeId);
            if (room != null)
            {
                room.TypeNote = roomType.TypeNote;
                room.TypeDescription = roomType.TypeDescription;
                room.TypeName = roomType.TypeName;
                return true;
            }
            return false;
        }
    }
}