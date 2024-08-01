using ClassRoom.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Application.Services
{
    public class ClassRoomTypeService(IClassRoomDbContext context)
    {
        public async Task<IReadOnlyList<ClassRoomTypeDto>> GetClassRoomTypesAsync()
        {
            return await context.ClassRoomTypes
                .Select(x => new ClassRoomTypeDto()
                {
                    Id = x.Id,
                    Description = x.Description
                })
                .ToListAsync();
        }
    }
}
