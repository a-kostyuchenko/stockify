namespace Stockify.Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task<User?> GetAsync(UserId id, CancellationToken cancellationToken = default);
    
    void Insert(User user);
}
