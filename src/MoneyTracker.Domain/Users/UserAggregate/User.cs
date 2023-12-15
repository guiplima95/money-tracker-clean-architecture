using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Users.Events;
using MoneyTracker.Domain.Users.UserAggregate;

namespace MoneyTracker.Domain.Users.Aggregators;

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

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        User user = new(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}
