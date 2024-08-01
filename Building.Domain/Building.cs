namespace Building.Domain
{
    public class Building
    {
        public Building(Guid id, string name, string? description, string address, int floorsNumber)
        {
            Id = id;
            Name = name;
            Description = description;
            Address = address;
            FloorsNumber = floorsNumber;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int FloorsNumber { get; set; }
    }
}
  