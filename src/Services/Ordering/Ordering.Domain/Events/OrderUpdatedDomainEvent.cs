
using Ordering.Domain.Models;

namespace Ordering.Domain
{
    public record OrderUpdatedDomainEvent(Order Order) : IDomainEvent;
    
}