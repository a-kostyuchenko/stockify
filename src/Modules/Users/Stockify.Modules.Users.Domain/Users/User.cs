using Stockify.Common.Domain;
using Stockify.Modules.Users.Domain.Roles;
using Stockify.Modules.Users.Domain.Users.Events;

namespace Stockify.Modules.Users.Domain.Users;

public class User : Entity<UserId>
{
    private readonly List<Role> _roles = [];

    private User() : base(UserId.New())
    {
    }
    
    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }
    
    public IReadOnlyCollection<Role> Roles => _roles.ToList();
    
    public static User Create(string email, string firstName, string lastName)
    {
        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
        };

        user._roles.Add(Role.User);

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }
}
