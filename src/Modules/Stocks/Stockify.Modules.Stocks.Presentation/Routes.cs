namespace Stockify.Modules.Stocks.Presentation;

internal static class Routes
{
    internal static class Stocks
    {
        private const string BaseUri = "stocks";
        
        internal const string GetGlobalQuote = $"{BaseUri}/quote/{{symbol:alpha}}";
        internal const string GetMarketStatus = $"{BaseUri}/market-status";
        internal const string GetStocksData = $"{BaseUri}/{{symbol:alpha}}";
    }
}
