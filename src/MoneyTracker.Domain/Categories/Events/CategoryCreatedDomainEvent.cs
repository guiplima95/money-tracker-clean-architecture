using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Categories.Events;

public record CategoryCreatedDomainEvent(Guid UserId, Guid CategoryId) : IDomainEvent;