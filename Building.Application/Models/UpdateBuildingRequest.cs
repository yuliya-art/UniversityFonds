using System.ComponentModel.DataAnnotations;

namespace Building.Application.Models
{
    public class CreateBuildingRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Range(0, 100)]
        public int FloorsNumber { get; set; }
    }
}
