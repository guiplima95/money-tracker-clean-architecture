using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Shared;
using MoneyTracker.Domain.Transactions.TransactionAggregate;

namespace MoneyTracker.Infrastructure.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Date);

        builder.Property(tran => tran.Note)
            .HasMaxLength(500)
            .HasConversion(note => note.Value, value => new Note(value));

        builder.OwnsOne(c => c.Amount, amountBuilder =>
        {
            amountBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne(t => t.Category)
            .WithOne(c => c.Transaction)
            .HasForeignKey<Category>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<uint>("Version").IsRowVersion();
    }
}