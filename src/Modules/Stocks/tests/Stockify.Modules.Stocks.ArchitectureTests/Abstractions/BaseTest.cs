using System.Reflection;
using Stockify.Modules.Stocks.Domain.Stockholders;
using Stockify.Modules.Stocks.Infrastructure;

namespace Stockify.Modules.Stocks.ArchitectureTests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Assembly ApplicationAssembly = Stocks.Application.AssemblyReference.Assembly;
    protected static readonly Assembly DomainAssembly = typeof(Stockholder).Assembly;
    protected static readonly Assembly InfrastructureAssembly = AssemblyReference.Assembly;
    protected static readonly Assembly PresentationAssembly = Stocks.Presentation.AssemblyReference.Assembly;
}
