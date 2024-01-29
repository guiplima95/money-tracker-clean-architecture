using MoneyTracker.Application.Abstractions.Clock;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Exceptions;
using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Categories.Repositories;
using MoneyTracker.Domain.Shared;
using MoneyTracker.Domain.Transactions;
using MoneyTracker.Domain.Transactions.Repositories;
using MoneyTracker.Domain.Transactions.TransactionAggregate;
using MoneyTracker.Domain.Users;
using MoneyTracker.Domain.Users.Repositories;
using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Application.Transactions.CreateTransaction;

internal sealed class CreateTransactionCommandHandler(
    ITransactionRepository transactionRepository,
    ICategoryRepository categoryRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateTransactionCommand, Guid>
{

    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;

    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        Category? category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure<Guid>(CategoryErrors.NotFound);
        }

        bool isOverlapping = await _transactionRepository
            .IsOverlappingAsync(category.Id, request.Amount, request.Note, cancellationToken);

        if (isOverlapping)
        {
            return Result.Failure<Guid>(TransactionErrors.Overlap);
        }

        try
        {
            Transaction transaction = Transaction.Create(new(request.Amount,
                                                     Currency.None),
                                                     new Note(request.Note),
                                                     DateOnly.FromDateTime(_dateTimeProvider.UtcNow),
                                                     request.CategoryId,
                                                     request.UserId);

            _transactionRepository.Add(transaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(TransactionErrors.Overlap);
        }
    }
}
