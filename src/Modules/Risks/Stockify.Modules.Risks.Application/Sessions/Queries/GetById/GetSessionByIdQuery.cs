using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetById;

public sealed record GetSessionByIdQuery(SessionId SessionId) : IQuery<SessionResponse>;
