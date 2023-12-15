using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Users.Aggregators;

namespace MoneyTracker.Infrastructure.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Type.Id);

        builder.ComplexProperty(category => category.Icon);

        builder
            .ComplexProperty(category => category.Title)
            .Property(c => c.Value)
            .HasMaxLength(200);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId);
    }
}