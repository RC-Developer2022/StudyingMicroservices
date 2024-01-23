using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsservices.Domain.Entities;

namespace Microsservices.Infrastructure.Core.Settings;
public class OrderSettings : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.ClientId).IsRequired();
        builder.Property(o => o.DeliveryAddress).IsRequired();
        builder.HasMany(o => o.Products).WithMany(p => p.Orders).UsingEntity(x => x.ToTable("OrderProducts"));
    }
}
