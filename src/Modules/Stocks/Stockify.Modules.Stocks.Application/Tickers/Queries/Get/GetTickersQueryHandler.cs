using System.Data.Common;
using Dapper;
using Stockify.Common.Application.Data;
using Stockify.Common.Application.Messaging;
using Stockify.Common.Application.Pagination;
using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Application.Tickers.Queries.Get;

internal sealed class GetTickersQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetTickersQuery, PagedResponse<TickerResponse>>
{
    public async Task<Result<PagedResponse<TickerResponse>>> Handle(
        GetTickersQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        var parameters = new GetTickersParameters(
            request.SearchTerm,
            request.PageSize,
            (request.Page - 1) * request.PageSize);
        
        IReadOnlyCollection<TickerResponse> tickers = await GetTickersAsync(connection, parameters);
        
        int totalCount = await CountTickersAsync(connection, parameters);
        
        return new PagedResponse<TickerResponse>(request.Page, request.PageSize, totalCount, tickers);
    }
    
    private static async Task<IReadOnlyCollection<TickerResponse>> GetTickersAsync(
        DbConnection connection,
        GetTickersParameters parameters
    )
    {
        const string sql = $"""
                            SELECT
                                t.symbol AS {nameof(TickerResponse.Symbol)},
                                t.name AS {nameof(TickerResponse.Name)},
                                t.description AS {nameof(TickerResponse.Description)},
                                t.cik AS {nameof(TickerResponse.Cik)},
                                tt.code AS {nameof(TickerResponse.Type)}
                            FROM stocks.tickers t
                            JOIN stocks.ticker_types tt ON t.ticker_type_id = tt.id
                            WHERE to_tsvector('english', t.symbol || ' ' || t.name || ' ' || t.description) @@ phraseto_tsquery('english', @SearchTerm)
                            OFFSET @Skip
                            LIMIT @Take
                            """;

        List<TickerResponse> tickers = (
            await connection.QueryAsync<TickerResponse>(sql, parameters)
        ).AsList();

        return tickers;
    }

    private static async Task<int> CountTickersAsync(
        DbConnection connection,
        GetTickersParameters parameters
    )
    {
        const string sql = """
                           SELECT COUNT(*)
                           FROM stocks.tickers t
                           WHERE to_tsvector('english', t.symbol || ' ' || t.name || ' ' t.description) @@ phraseto_tsquery('english', @SearchTerm)
                           """;

        int totalCount = await connection.ExecuteScalarAsync<int>(sql, parameters);

        return totalCount;
    }
}
