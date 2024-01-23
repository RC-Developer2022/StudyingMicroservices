using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsservices.Domain.Entities;
using Microsservices.Domain.Enuns;

namespace Microsservices.Infrastructure.Core.Settings;

public class UserAccessSettings : IEntityTypeConfiguration<UserAccess>
{
    public void Configure(EntityTypeBuilder<UserAccess> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id).HasName("UserId").Metadata.IsPrimaryKey();
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.ConfirmPassword).IsRequired();
        builder.Property(x => x.UserType).HasConversion(new EnumToStringConverter<UserType>()).HasColumnType("varchar(20)").IsRequired();
    }
}
