
namespace Ordering.Domain.Abstractions;
//esta clase abstracta implementa la interfaz IAgregate y proporciona una implementación concreta para manejar eventos de dominio
public abstract class Agregate<TId> : Entity<TId>, IAgregate<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IDomainEvent[] ClearDomainEvent()
    {
        IDomainEvent[] dequeuedEvents = _domainEvents.ToArray();
        _domainEvents.Clear();
        return dequeuedEvents;
    }
}
