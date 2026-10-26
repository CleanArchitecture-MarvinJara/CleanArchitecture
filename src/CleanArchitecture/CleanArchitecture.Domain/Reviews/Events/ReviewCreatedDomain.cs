using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomain(ReviewId ReviewId) : IDomainEvent;
}