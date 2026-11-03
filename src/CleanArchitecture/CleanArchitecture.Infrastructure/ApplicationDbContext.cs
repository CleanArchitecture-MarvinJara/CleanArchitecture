using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure
{
    // Nota: El IUnitOfWork es el encargado de enviar todos los cambios guardados en la persistencia
    // de EntityFramework hacia la base de datos
    public sealed class ApplicationDbContext: DbContext, IUnitOfWork
    {
        private readonly IPublisher _publisher;

        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Con esto especificamos que el Assembly contiene el DbContext
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken=default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                await PublishDomainEventsAsync();

                return result;
            } catch(DbUpdateConcurrencyException ex)
            {
                // Cuando insertamos un nuevo record y se detecta una violacion de reglas dentro de la BD
                // La BD automaticamente disparara un DbUpdateConcurrencyException
                throw new ConcurrencyException("La excepcion por concurrencia se disparo", ex);
            }
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<IEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();
                    entity.ClearDomainEvents();
                    return domainEvents;
                }).ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
