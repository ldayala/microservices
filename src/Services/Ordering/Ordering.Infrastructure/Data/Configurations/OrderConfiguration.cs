
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {builder.HasKey(o=>o.Id);
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
            builder.Property(o => o.OrderName)
                .IsRequired()
                .HasMaxLength(100)
                .HasConversion(
                    name => name.Value,
                    value => new OrderName(value));
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
                o=>o.OrderName,nameBuilder=>
                {
                    nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .IsRequired()
                    .HasMaxLength(100);
                });
                

        }
    }
}
