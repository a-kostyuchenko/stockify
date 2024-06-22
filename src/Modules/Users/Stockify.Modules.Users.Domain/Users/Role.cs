using Stockify.Common.Domain;

namespace Stockify.Modules.Users.Domain.Users;

public abstract class Role : Enumeration<Role>
{
    public static readonly Role User = new UserRole();
    public static readonly Role Manager = new ManagerRole();
    public static readonly Role Administrator = new AdministratorRole();
    private Role(int id, string name)
        : base(id, name)
    {
    }
    
    public abstract IEnumerable<Permission> GetPermissions();
    
    private sealed class UserRole() : Role(1, nameof(User))
    {
        public override IEnumerable<Permission> GetPermissions()
        {
            yield return Permission.AccessUsers;
        }
    }
    
    private sealed class ManagerRole() : Role(2, nameof(Manager))
    {
        public override IEnumerable<Permission> GetPermissions()
        {
            yield return Permission.AccessUsers;
        }
    }
    
    private sealed class AdministratorRole() : Role(int.MaxValue, nameof(Administrator))
    {
        public override IEnumerable<Permission> GetPermissions()
        {
            yield return Permission.AccessEverything;
        }
    }
}
