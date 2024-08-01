using Building.Application.Models;
using MessageBrocker.Messages;

namespace Building.Application.Services
{
    public class BuildingsService(IBuildingContext buildingContext, IMessageSender sender)
    {
        public async Task<IReadOnlyList<BuildingDto>> GetBuildingsAsync()
        {
            return await buildingContext.Buildings
                .Select(x => ConvertToDto(x))
                .ToListAsync();
        }

        public async Task<BuildingDto?> GetBuildingByIdAsync(Guid buildingId)
        {
            var result = await buildingContext.Buildings
                .FirstOrDefaultAsync(x => x.Id == buildingId);

            return result is not null ? ConvertToDto(result) : null;
        }

        public async Task<BuildingDto> CreateBuildingAsync(CreateBuildingRequest request)
        {
            var entity = new Domain.Building(Guid.Empty, request.Name, request.Description,
                request.Address, request.FloorsNumber);
            buildingContext.Buildings.Add(entity);
            await buildingContext.SaveChangesAsync();

            sender.SendMessage(MessageQueues.BuildingCreatedQueue, new BuildingCreated()
            {
                Id = entity.Id,
                Name = entity.Name
            });

            return await GetBuildingByIdAsync(entity.Id) ?? throw new InvalidOperationException("Cannot create building");
        }

        public async Task RemoveBuildingAsync(Guid buildingId)
        {

            var entity = await buildingContext.Buildings.FirstAsync(x => x.Id == buildingId);
            buildingContext.Buildings.Remove(entity);
            await buildingContext.SaveChangesAsync();

            sender.SendMessage(MessageQueues.BuildingRemovedQueue, new BuildingRemoved()
            {
                Id = entity.Id
            });
        }

        public async Task<BuildingDto> UpdateBuildingAsync(UpdateBuildingRequest request)
        {
            var entity = await buildingContext.Buildings.FirstAsync(x => x.Id == request.Id);
            
            entity.Address = request.Address;
            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.FloorsNumber = request.FloorsNumber;

            await buildingContext.SaveChangesAsync();

            sender.SendMessage(MessageQueues.BuildingUpdatedQueue, new BuildingUpdated()
            {
                Id = entity.Id,
                Name = entity.Name
            });

            return ConvertToDto(entity);
        }

        private static BuildingDto ConvertToDto(Domain.Building building)
        {
            return new BuildingDto()
            {
                Id = building.Id,
                Address = building.Address,
                Description = building.Description,
                Name = building.Name,
                FloorsNumber = building.FloorsNumber
            };
        }
    }
}
