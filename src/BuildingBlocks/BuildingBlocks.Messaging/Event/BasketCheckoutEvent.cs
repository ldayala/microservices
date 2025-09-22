
namespace BuildingBlocks.Messaging.Event
{
    public record BasketCheckoutEvent:IntegrationEvent
    {
        public string UserName { get; init; } = default!;
        public Guid CustomerId { get; init; } = default!;
        public decimal TotalPrice { get; init; }
        //Shipping and Billingaddresses
       public string  FirstName { get; init; } = default!;
         public string LastName { get; init; } = default!;
        public string EmailAddress { get; init; } = default!;
        public string AddressLine { get; init; }= default!;
        public string Country { get; init; } = default!;
        public string State { get; init; } = default!;
        public string ZipCode { get; init; } = default!;

        //Payment
        public string CardName { get; init; } = default!;
        public string CardNumber { get; init; } = default!;
        public string Expiration { get; init; } = default!;
        public string CVV { get; init; } = default!;
        public string PaymentMethod { get; init; } = default!;
    }
}
