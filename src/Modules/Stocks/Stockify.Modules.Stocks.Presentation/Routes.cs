namespace Stockify.Modules.Stocks.Presentation;

internal static class Routes
{
    internal static class Stocks
    {
        private const string BaseUri = "stocks";
        
        internal const string GetGlobalQuote = $"{BaseUri}/{{ticker:alpha}}/quote";
        internal const string GetMarketStatus = $"{BaseUri}/market-status";
        internal const string GetStocksData = $"{BaseUri}/{{ticker:alpha}}";
    }

    internal static class TickerTypes
    {
        private const string BaseUri = "ticker-types";
        
        internal const string Create = BaseUri;
    }

    internal static class Tickers
    {
        private const string BaseUri = "tickers";
        
        internal const string Get = BaseUri;
        internal const string Create = BaseUri;
    } 
}
