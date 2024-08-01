using ClassRoom.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Application.Services
{
    public class ClassRoomService(IClassRoomDbContext context)
    {
        public async Task<IReadOnlyList<ClassRoomDto>> GetClassRoomsAsync()
        {
            return await context.ClassRooms
                .Include(x=>x.Building)
                .Include(x => x.ClassRoomType)
                .Select(x => ConvertToDto(x))
                .ToListAsync();
        }

        public async Task<ClassRoomDto?> GetClassRoomByIdAsync(Guid classRoomId)
        {
            var result = await context.ClassRooms
                .Include(x => x.Building)
                .Include(x => x.ClassRoomType)
                .FirstOrDefaultAsync(x => x.Id == classRoomId);

            return result is not null ? ConvertToDto(result) : null;
        }

        public async Task<ClassRoomDto> CreateClassRoomAsync(CreateClassRoomRequest request)
        {
            var entity = new Domain.ClassRoom()
            {
                Name = request.Name,
                Capacity = request.Capacity,
                Number = request.Number,
                ClassRoomTypeId = request.TypeId,
                BuildingId = request.BuildingId,
                Floor = request.Floor
            };
            context.ClassRooms.Add(entity);
            await context.SaveChangesAsync();

            return await GetClassRoomByIdAsync(entity.Id) ?? throw new InvalidOperationException("Cannot create class room");
        }

        public async Task RemoveClassRoomAsync(Guid classRoomId)
        {

            var entity = await context.ClassRooms.FirstAsync(x => x.Id == classRoomId);
            context.ClassRooms.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<ClassRoomDto> UpdateClassRoomAsync(UpdateClassRoomRequest request)
        {
            var entity = await context.ClassRooms
                .Include(x => x.Building)
                .Include(x => x.ClassRoomType)
                .FirstAsync(x => x.Id == request.Id);
            
            entity.Capacity = request.Capacity;
            entity.Number = request.Number;
            entity.Name = request.Name;
            entity.ClassRoomTypeId = request.TypeId;
            entity.BuildingId = request.BuildingId;
            entity.Floor = request.Floor;

            await context.SaveChangesAsync();

            return await GetClassRoomByIdAsync(entity.Id) ?? throw new InvalidOperationException("Cannot update class room");
        }

        private static ClassRoomDto ConvertToDto(Domain.ClassRoom classRoom)
        {
            return new ClassRoomDto()
            {
                Id = classRoom.Id,
                Name = classRoom.Name,
                Capacity = classRoom.Capacity,
                Building = classRoom.Building?.Name ?? "<удалено>",
                BuildingId = classRoom.BuildingId,
                Number = classRoom.Number,
                TypeId = classRoom.ClassRoomTypeId,
                Type = classRoom.ClassRoomType.Description,
                Floor = classRoom.Floor
            };
        }
    }
}
