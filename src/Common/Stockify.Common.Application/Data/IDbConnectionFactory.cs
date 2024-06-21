using System.Data.Common;

namespace Stockify.Common.Application.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
