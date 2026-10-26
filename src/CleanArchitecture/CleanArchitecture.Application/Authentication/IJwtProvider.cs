using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Application.Authentication
{
    public interface IJwtProvider
    {
        Task<string> Generate(User user);
    }
}
