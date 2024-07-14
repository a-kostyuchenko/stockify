using Microsoft.AspNetCore.Http;
using Stockify.Common.Application.Exceptions;
using Stockify.Common.Infrastructure.Authentication;
using Stockify.Modules.Risks.Application.Abstractions.Authentication;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Infrastructure.Authentication;

internal sealed class IndividualContext(IHttpContextAccessor httpContextAccessor) : IIndividualContext
{
    public IndividualId IndividualId => IndividualId.FromValue(
        httpContextAccessor.HttpContext?.User.GetUserId() ??
        throw new StockifyException("User identifier is unavailable"));
}
