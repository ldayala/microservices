
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new OrderId(value));

            builder.Property(o => o.CustomerId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => new CustomerId(value));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired();


            //EFC introduce nueva propiedas ComplexProperty para soportar tipos de valor complejos como Address y Payment
            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .IsRequired()
                    .HasMaxLength(100);
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                    addressBuilder.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
                    addressBuilder.Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                    .IsRequired()
                    .HasMaxLength(180);

                    addressBuilder.Property(a => a.Country)
                     .IsRequired()
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(5); ;
                });

            builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                    addressBuilder.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
                    addressBuilder.Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.AddressLine)
                    .IsRequired()
                    .HasMaxLength(180);

                    addressBuilder.Property(a => a.Country)
                     .IsRequired()
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(50);

                    addressBuilder.Property(a => a.ZipCode)
                    .IsRequired()
                    .HasMaxLength(5);

                });
            builder.ComplexProperty(
                o => o.Payment, paymentBuilder =>
                {
                    paymentBuilder.Property(p => p.CardName)
                    .IsRequired()
                    .HasMaxLength(50);
                    paymentBuilder.Property(p => p.CardNumber)
                    .IsRequired()
                    .HasMaxLength(25);
                    paymentBuilder.Property(p => p.Expiration)
                    .HasMaxLength(10);
                    paymentBuilder.Property(p => p.CVV)
                    .IsRequired()
                    .HasMaxLength(3);
                    paymentBuilder.Property(p => p.PaymentMethod);


                });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    status => status.ToString(),
                    value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value));
            builder.Property(o => o.TotalPrice);

        }
    }
}
