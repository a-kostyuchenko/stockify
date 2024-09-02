using System.Reflection;
using Stockify.Modules.Risks.Domain.Questions;
using Stockify.Modules.Risks.Infrastructure;

namespace Stockify.Modules.Risks.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = Risks.Application.AssemblyReference.Assembly;
    protected static readonly Assembly DomainAssembly = typeof(Question).Assembly;
    protected static readonly Assembly InfrastructureAssembly = AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Risks.Presentation.AssemblyReference.Assembly;
}
