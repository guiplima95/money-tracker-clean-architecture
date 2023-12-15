using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTracker.Domain.Transactions.TransactionAggregate;

[ComplexType]
public record Note(string Value);