using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Building.Application.Models
{
    public class UpdateBuildingRequest
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Range(0, 100)]
        public int FloorsNumber { get; set; }
    }
}
