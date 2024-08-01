using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Application
{
    public interface IClassRoomDbContext
    {
        DbSet<Domain.Building> Buildings { get; set; }
        DbSet<Domain.ClassRoom> ClassRooms { get; set; }
        DbSet<Domain.ClassRoomType> ClassRoomTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}
