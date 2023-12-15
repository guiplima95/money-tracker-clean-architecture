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

        builder
            .ComplexProperty(transaction => transaction.Note)
            .Property(c => c.Value)
            .HasMaxLength(500);

        builder.OwnsOne(c => c.Amount, amount =>
        {
            amount.Property(money => money.Currency)
                  .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasOne<Category>()
           .WithMany()
           .HasForeignKey(c => c.CategoryId);
    }
}