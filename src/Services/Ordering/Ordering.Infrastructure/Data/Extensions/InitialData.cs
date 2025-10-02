
using Ordering.Domain;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers => new List<Customer>
        {
           Customer.Create(CustomerId.New(), "John Doe","dl@sk.cu"),
           Customer.Create(CustomerId.New(), "Jane Smith","ri@ri.cu"),
           Customer.Create(CustomerId.New(), "Alice Johnson","pep@s.cu")
        };

        public static IEnumerable<Product> Products => new List<Product>
        {
            Product.Create(ProductId.New(), "Laptop", 999.99m),
            Product.Create(ProductId.New(), "Smartphone", 499.99m),
            Product.Create(ProductId.New(), "Tablet", 299.99m)
        };

        public static IEnumerable<Order> OrdersWithItems {
            get
            {
                var address1 = Address.Of("Luis","Daniel","ldayala@g.com","sk","spain","madrid","28850");
                var address2 = Address.Of("Ana", "Gonzalez", "ldayala@g.com", "sk", "spain", "madrid", "28850");
                var payment1 = Payment.Of("Visa", "1234567812345678", "12/25", "123",1);
                var payment2 = Payment.Of("MasterCard", "8765432187654321", "11/24", "456",2);

                var order = Order.Create(Customers.First().Id, OrderName.Of("Order 1"), address1, address2, payment1);
                order.Add(Products.First().Id, 1, Products.First().Price);
                order.Add(Products.Last().Id, 2, Products.Last().Price);

                return new List<Order>
                {
                    order,
                    Order.Create(Customers.Last().Id, OrderName.Of("Order 2"), address1, address2, payment2)
                };
            }
        
        }
    }
}
