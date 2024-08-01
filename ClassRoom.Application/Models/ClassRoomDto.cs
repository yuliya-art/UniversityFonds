using ClassRoom.Domain;

namespace ClassRoom.Application.Models
{
    public class ClassRoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ClassRoomTypeId TypeId { get; set; }
        public string Type { get; set; }
        public Guid? BuildingId { get; set; }
        public string Building { get; set; }
        public int Capacity { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
    }
}
