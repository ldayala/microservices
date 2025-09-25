
namespace Ordering.Domain.Abstractions;
//esta interfaz extiende de IEntity y agrega funcionalidad para manejar eventos de dominio
public interface IAgregate<T>:IEntity<T>,IAgregate
{

}
public interface IAgregate: IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvent(); 
}
