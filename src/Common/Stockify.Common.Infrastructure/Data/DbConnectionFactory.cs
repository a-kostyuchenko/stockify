using System.Data.Common;
using Npgsql;
using Stockify.Common.Application.Data;

namespace Stockify.Common.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource datasource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await datasource.OpenConnectionAsync();
    }
}
