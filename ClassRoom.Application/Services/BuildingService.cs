using ClassRoom.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Application.Services
{
    public class BuildingService(IClassRoomDbContext context)
    {
        public async Task<IReadOnlyList<BuildingDto>> GetBuildings()
        {
            return await context.Buildings.Select(x => new BuildingDto()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
        }
    }
}
