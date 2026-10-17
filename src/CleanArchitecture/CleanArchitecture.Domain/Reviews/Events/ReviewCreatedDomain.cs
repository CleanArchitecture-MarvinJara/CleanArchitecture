using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomain(Guid ReviewId) : IDomainEvent;
}