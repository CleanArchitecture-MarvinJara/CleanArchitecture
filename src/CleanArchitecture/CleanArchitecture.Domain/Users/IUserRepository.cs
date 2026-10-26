namespace CleanArchitecture.Domain.Users
{
    public interface IUserRepository
    {
        // Task hace al metodo asincrono
        Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
        void Add(User user);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    }
}
