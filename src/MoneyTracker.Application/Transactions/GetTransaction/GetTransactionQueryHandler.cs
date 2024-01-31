using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Transactions.Dtos;
using MoneyTracker.Domain.Abstractions;
using System.Data;

namespace MoneyTracker.Application.Transactions.GetTransaction;

internal sealed class GetTransactionQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetTransactionQuery, TransactionDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<TransactionDto>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
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
            WHERE id = @TransactionId
            """;

        TransactionDto? transaction = await connection.QueryFirstOrDefaultAsync<TransactionDto>(
            sql, new
            {
                request.TransactionId
            });


        return transaction;
    }
}
