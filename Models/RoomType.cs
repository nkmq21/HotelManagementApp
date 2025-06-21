namespace Models
{
    public class RoomType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string TypeDescription { get; set; }
        public string TypeNote { get; set; }

        public RoomType() { }

        public RoomType(int id, string name, string description, string note)
        {
            this.TypeId = id;
            this.TypeName = name;
            this.TypeDescription = description;
            this.TypeNote = note;
        }
    }
}
