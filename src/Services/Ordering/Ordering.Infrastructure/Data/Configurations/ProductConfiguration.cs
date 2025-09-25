
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain;
using Ordering.Domain.Abstractions;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          builder.HasKey(p => p.Id);
          builder.Property(p => p.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => new ProductId(value));
        builder.Property(p => p.Name).HasMaxLength(200).IsRequired();
        }
    }
}
