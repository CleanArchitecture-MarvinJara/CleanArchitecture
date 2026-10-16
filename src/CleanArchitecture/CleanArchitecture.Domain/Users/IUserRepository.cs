namespace CleanArchitecture.Domain.Users
{
    public interface IUserRepository
    {
        // Task hace al metodo asincrono
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(User user);
    }
}
