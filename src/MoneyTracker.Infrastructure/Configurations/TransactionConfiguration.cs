using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        builder.Property(tran => tran.Amount)
               .HasConversion(amount => amount.Value, value => new Money(value, Currency.None));

        builder.HasOne(t => t.User)
               .WithMany(u => u.Transactions)
               .HasForeignKey(t => t.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property<uint>("Version").IsRowVersion();
    }
}