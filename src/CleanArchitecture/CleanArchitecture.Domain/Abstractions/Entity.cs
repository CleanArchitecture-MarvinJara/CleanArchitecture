namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        protected Entity(Guid id) 
        {
            Id = id;
        }
        public Guid Id { get; init; }

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
