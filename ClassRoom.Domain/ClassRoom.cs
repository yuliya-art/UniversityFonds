namespace ClassRoom.Domain
{
    public class ClassRoom
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ClassRoomTypeId ClassRoomTypeId { get; set; }
        public ClassRoomType ClassRoomType { get; set; }

        public Guid? BuildingId { get; set; }
        public Building? Building { get; set; }
        public int Capacity { get; set; }
        public int Number { get; set; }
        public int Floor { get; set;}
    }
}
