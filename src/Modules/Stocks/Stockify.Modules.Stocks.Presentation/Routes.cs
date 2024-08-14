namespace Stockify.Modules.Stocks.Presentation;

internal static class Routes
{
    internal static class Stocks
    {
        private const string BaseUri = "stocks";
        
        internal const string GetGlobalQuote = $"{BaseUri}/quote/{{symbol:alpha}}";
    }
}
