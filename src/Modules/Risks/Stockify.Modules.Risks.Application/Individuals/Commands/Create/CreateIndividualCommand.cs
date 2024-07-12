using Stockify.Common.Application.Messaging;
using Stockify.Modules.Risks.Domain.Individuals;

namespace Stockify.Modules.Risks.Application.Individuals.Commands.Create;

public sealed record CreateIndividualCommand(
    IndividualId Id,
    string FirstName,
    string LastName,
    string Email) : ICommand;
