using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RoomInformation
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public string RoomDescription { get; set; }
        public int MaxCapacity { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDate { get; set; }
        public int? RoomTypeID { get; set; }
        public RoomType? RoomType { get; set; }

        public RoomInformation() { }

        public RoomInformation(int roomId, string name, string description, int maxCapacity, bool roomStatus, decimal pricePerDate, int roomTypeId)
        {
            this.RoomId = roomId;
            this.RoomName = name;
            this.RoomDescription = description;
            this.MaxCapacity = maxCapacity;
            this.RoomStatus = roomStatus;
            this.RoomPricePerDate = pricePerDate;
            this.RoomTypeID = roomTypeId;
        }

    }
}
