namespace Building.Application.Models
{
    public class BuildingDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Address { get; set; }
        public int FloorsNumber { get; set; }
    }
}
  