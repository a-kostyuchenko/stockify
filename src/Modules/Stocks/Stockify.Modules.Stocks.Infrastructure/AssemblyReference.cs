using System.Reflection;

namespace Stockify.Modules.Stocks.Infrastructure;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
