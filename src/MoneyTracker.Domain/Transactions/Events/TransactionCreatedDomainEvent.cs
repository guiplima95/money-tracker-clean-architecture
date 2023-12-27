using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Transactions.Events;

public record TransactionCreatedDomainEvent(Guid UserId, Guid TransactionId) : IDomainEvent;
