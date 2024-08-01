namespace Building.Application
{
    public interface IBuildingContext
    {
        DbSet<Domain.Building> Buildings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
    }
}
