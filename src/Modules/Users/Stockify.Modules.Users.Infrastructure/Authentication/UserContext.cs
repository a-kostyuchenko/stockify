using Microsoft.AspNetCore.Http;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Application.ServiceLifetimes;
using Stockify.Common.Infrastructure.Authentication;
using Stockify.Modules.Users.Application.Abstractions.Authentication;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Infrastructure.Authentication;

internal sealed class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext, IScoped
{
    public UserId UserId =>
        UserId.From(
            httpContextAccessor.HttpContext?.User.GetUserId()
                ?? throw new StockifyException("User identifier is unavailable")
        );
}
