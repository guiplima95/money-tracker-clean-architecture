using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Domain.Abstractions;
using System.Data;

namespace MoneyTracker.Application.Transactions.SearchTransaction;

internal sealed class SearchTransactionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<SearchTransactionsQuery, List<TransactionResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<List<TransactionResponse>>> Handle(SearchTransactionsQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                id AS Id, 
                user_id AS UserId, 
                category_id AS CategoryId,
                note AS Description, 
                amount AS Amount, 
                date AS Date
            FROM transactions
            WHERE date = @Date
            """;

        List<TransactionResponse>? transactions = (await connection.QueryAsync<TransactionResponse>(
            sql, new
            {
                request.Date
            })).ToList();


        return transactions;
    }
}