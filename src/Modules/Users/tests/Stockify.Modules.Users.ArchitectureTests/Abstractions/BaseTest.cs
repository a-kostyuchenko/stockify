using System.Reflection;
using Stockify.Modules.Users.Domain.Users;
using AssemblyReference = Stockify.Modules.Users.Infrastructure.AssemblyReference;

namespace Stockify.Modules.Users.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = Users.Application.AssemblyReference.Assembly;
    protected static readonly Assembly DomainAssembly = typeof(User).Assembly;
    protected static readonly Assembly InfrastructureAssembly = AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Users.Presentation.AssemblyReference.Assembly;
}
