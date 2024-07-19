using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Individuals;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Application.Sessions.Queries.GetResult;

public sealed record GetSessionResultQuery(SessionId SessionId, IndividualId IndividualId) 
    : IQuery<SessionResultResponse>;
