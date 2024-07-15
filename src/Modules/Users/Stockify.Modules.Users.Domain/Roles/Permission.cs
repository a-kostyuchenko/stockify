namespace Stockify.Modules.Users.Domain.Roles;

public sealed class Permission
{
    public static readonly Permission AccessEverything = new("administrator:full_access");
    public static readonly Permission AccessUsers = new("users:access");
    public static readonly Permission ModifyQuestions = new("questions:modify");
    
    private Permission(string code) => 
        Code = code;

    public string Code { get; }
}
