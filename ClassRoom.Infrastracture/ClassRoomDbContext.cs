using ClassRoom.Application;
using ClassRoom.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClassRoom.Infrastracture
{
    public class ClassRoomDbContext : DbContext, IClassRoomDbContext
    {
        public ClassRoomDbContext(DbContextOptions<ClassRoomDbContext> options) : base(options)
        {
            
        }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Domain.ClassRoom> ClassRooms { get; set; }
        public DbSet<ClassRoomType> ClassRoomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassRoomType>(entity =>
            {
                entity.HasData(Enum.GetValues(typeof(ClassRoomTypeId))
                    .Cast<ClassRoomTypeId>()
                    .Select(x => new ClassRoomType(x, x.ToDescriptionString())));
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(b => b.Id).ValueGeneratedNever();
            });
        }
    }
}
