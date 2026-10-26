using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Infrastructure.Repositories
{
    // Los metodos definidos en IUserRepository estan siendo implementados por la clase abstracta Repository
    internal sealed class UserRepository : Repository<User,UserId>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
