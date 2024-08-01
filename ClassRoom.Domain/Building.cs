namespace ClassRoom.Domain
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClassRoom> ClassRooms { get; set; }
    }
}
  