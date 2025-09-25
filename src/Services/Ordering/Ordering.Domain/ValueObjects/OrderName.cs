namespace Ordering.Domain
{
    public record OrderName(string Value)
    {
        private const int DefaultLength = 5;

        public static OrderName Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
            ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength, nameof(value));
            return new OrderName(value);
        }
    }
   
}