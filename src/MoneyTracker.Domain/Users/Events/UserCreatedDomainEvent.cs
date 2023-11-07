using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Domain.Users.Events;

public record UserCreatedDomainEvent(Guid Id) : IDomainEvent;