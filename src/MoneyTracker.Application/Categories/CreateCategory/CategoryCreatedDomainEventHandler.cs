using MediatR;
using MoneyTracker.Domain.Categories.Events;

namespace MoneyTracker.Application.Categories.CreateCategory;

internal sealed class CategoryCreatedDomainEventHandler : INotificationHandler<CategoryCreatedDomainEvent>
{
    public async Task Handle(CategoryCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
