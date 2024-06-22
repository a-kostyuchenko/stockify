namespace Stockify.Modules.Users.Domain.Users;

public sealed class Permission
{
    public static readonly Permission AccessEverything = new("administrator:full_access");
    public static readonly Permission AccessUsers = new("users:access");
    
    private Permission(string code) => 
        Code = code;

    public string Code { get; }
}
