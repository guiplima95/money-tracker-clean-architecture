using MediatR;
using MoneyTracker.Application.Abstractions.Email;
using MoneyTracker.Domain.Transactions.Events;
using MoneyTracker.Domain.Transactions.Repositories;
using MoneyTracker.Domain.Transactions.TransactionAggregate;
using MoneyTracker.Domain.Users.Repositories;
using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Application.Transactions.CreateTransaction;

internal sealed class TransactionCreatedDomainEventHandler(
    ITransactionRepository transactionRepository,
    IUserRepository userRepository,
    IEmailService emailService) : INotificationHandler<TransactionCreatedDomainEvent>
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(TransactionCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        Transaction? transaction = await _transactionRepository
            .GetByIdAsync(notification.TransactionId, cancellationToken);

        if (transaction is null)
        {
            return;
        }

        User? user = await _userRepository
           .GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendAsync(
            user.Email, "Transaction created!",
            $"You have a new transaction on amount: {transaction.Amount}");
    }
}
