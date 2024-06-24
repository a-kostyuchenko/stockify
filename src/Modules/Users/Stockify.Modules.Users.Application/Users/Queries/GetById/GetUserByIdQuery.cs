using Stockify.Common.Application.Messaging;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Users.Queries.GetById;

public sealed record GetUserByIdQuery(UserId UserId) : IQuery<UserResponse>;
