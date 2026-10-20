namespace CleanArchitecture.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        // Toma los elementos dentro de la persistencia de entity framework y los va a llevar a la BD
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
