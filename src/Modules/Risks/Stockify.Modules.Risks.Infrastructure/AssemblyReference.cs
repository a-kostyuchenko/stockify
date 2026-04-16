using System.Reflection;
using Dapper;

[module: DapperAot(false)]
namespace Stockify.Modules.Risks.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
