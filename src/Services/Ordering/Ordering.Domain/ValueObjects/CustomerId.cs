namespace Ordering.Domain;
public record CustomerId(Guid Value)
{
    public static CustomerId New() => new(Guid.NewGuid());
}
