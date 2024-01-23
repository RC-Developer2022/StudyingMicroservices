using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsservices.Domain.Entities;

namespace Microsservices.Infrastructure.Core.Settings;
public class ProductsSettings : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.Property(p => p.Name).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(255);
        builder.Property(p => p.Price).HasColumnType("decimal(10,2)").IsRequired();
    }
}
