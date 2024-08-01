namespace ClassRoom.Domain
{
    public class ClassRoomType(ClassRoomTypeId id, string description)
    {
        public ClassRoomTypeId Id { get; set; } = id;
        public string Description { get; set; } = description;
        public virtual ICollection<ClassRoom> ClassRooms { get; set; } = null!;
    }
}
