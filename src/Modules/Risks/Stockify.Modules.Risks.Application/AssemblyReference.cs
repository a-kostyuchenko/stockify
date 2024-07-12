using System.Reflection;

namespace Stockify.Modules.Risks.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly; 
}
