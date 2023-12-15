using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Domain.Users.Aggregators;

namespace MoneyTracker.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(user => user.Id);

        builder
            .ComplexProperty(user => user.FirstName)
            .Property(e => e.Value)
            .HasMaxLength(200);

        builder
            .ComplexProperty(user => user.LastName)
            .Property(e => e.Value)
            .HasMaxLength(200);

        builder
            .ComplexProperty(user => user.Email)
            .Property(e => e.Address)
            .HasMaxLength(400);

        builder.HasIndex(user => user.Email).IsUnique();
    }
}