using Microsoft.EntityFrameworkCore;

namespace Building.Infrastructure
{
    public class BuildingContext: DbContext, IBuildingContext
    {
        public BuildingContext(DbContextOptions<BuildingContext> options): base(options)
        {
        }

        public DbSet<Domain.Building> Buildings { get; set; }
 
    }
}
