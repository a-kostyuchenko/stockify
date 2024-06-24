using Stockify.Common.Application.Messaging;
using Stockify.Common.Domain;
using Stockify.Modules.Users.Application.Abstractions;
using Stockify.Modules.Users.Domain.Users;

namespace Stockify.Modules.Users.Application.Users.Commands.Register;

internal sealed class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = User.Create(request.Email, request.FirstName, request.LastName);
        
        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user.Id.Value);
    }
}
