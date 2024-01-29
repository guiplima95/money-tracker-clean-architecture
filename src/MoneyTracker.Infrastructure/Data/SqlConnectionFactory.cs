using MoneyTracker.Application.Abstractions.Data;
using Npgsql;
using System.Data;

namespace MoneyTracker.Infrastructure.Data;

internal sealed class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection CreateConnection()
    {
        NpgsqlConnection connection = new(_connectionString);
        connection.Open();

        return connection;
    }
}
