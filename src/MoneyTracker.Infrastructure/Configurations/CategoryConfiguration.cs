using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Domain.Categories.CategoryAggragate;

namespace MoneyTracker.Infrastructure.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Type)
             .HasConversion(type => type.Id, id => CategoryType.From(id));

        builder.Property(c => c.Title)
             .HasConversion(title => title.Value, value => new Title(value));

        builder.Property(c => c.Icon)
            .HasMaxLength(200)
            .HasConversion(icon => icon.Value, value => new Icon(value))
            .IsRequired(false);

        builder.HasOne(c => c.User)
            .WithMany(u => u.Categories)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}