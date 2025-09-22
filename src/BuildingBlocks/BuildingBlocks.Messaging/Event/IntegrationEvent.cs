
namespace BuildingBlocks.Messaging.Event
{
    public record IntegrationEvent
    {
        public Guid Id =>Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public string EventType => GetType().AssemblyQualifiedName; // This is the fully qualified name of the event type, including the assembly name.
    }
}
