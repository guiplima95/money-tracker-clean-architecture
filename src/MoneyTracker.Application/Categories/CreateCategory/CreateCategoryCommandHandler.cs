using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Domain.Abstractions;

namespace MoneyTracker.Application.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
