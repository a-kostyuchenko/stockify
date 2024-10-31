using Stockify.Common.Domain;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Domain.Roles;

public sealed class Role : Enumeration<Role>
{
    private readonly HashSet<Permission> _permissions = [];
    private readonly HashSet<User> _users = [];

    public static readonly Role User = new(1, nameof(User));
    public static readonly Role Manager = new(2, nameof(Manager));
    public static readonly Role Administrator = new(3, nameof(Administrator));

    private Role(int id, string name)
        : base(id, name) { }

    public IReadOnlyCollection<Permission> Permissions => [.. _permissions];
    public IReadOnlyCollection<User> Users => [.. _users];
}
