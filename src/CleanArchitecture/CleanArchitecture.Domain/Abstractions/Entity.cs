namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class Entity<TEntityId> : IEntity
    {
        protected Entity()
        {

        }

        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(TEntityId id) 
        {
            Id = id;
        }
        public TEntityId? Id { get; init; }

        // Leemos los eventos de la lista
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }

        // Limpiamos la lista de eventos
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        // Agrega un nuevo evento a la lista
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
