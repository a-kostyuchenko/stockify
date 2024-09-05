using Microsoft.EntityFrameworkCore;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Infrastructure.Database.Repositories;

internal sealed class UserRepository(UsersDbContext dbContext) : IUserRepository, IScoped
{
    public async Task<User?> GetAsync(UserId id, CancellationToken cancellationToken = default) => 
        await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public void Insert(User user)
    {
        dbContext.Users.Add(user);
        
        dbContext.AttachRange(user.Roles);
    }
}
