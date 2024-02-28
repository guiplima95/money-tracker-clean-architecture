using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using MoneyTracker.Domain.Transactions.TransactionAggregate;
using MoneyTracker.Domain.Users.Events;

namespace MoneyTracker.Domain.Users.UserAggregate;

public class User : Entity
{
    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    // EFCore Constructor
    private User()
    {

    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public string IdentityId { get; private set; } = string.Empty;

    public ICollection<Transaction>? Transactions { get; set; }

    public ICollection<Category>? Categories { get; set; }


    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        User user = new(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
