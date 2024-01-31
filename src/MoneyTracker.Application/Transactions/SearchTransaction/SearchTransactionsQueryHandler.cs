using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Categories.Dtos;
using MoneyTracker.Application.Transactions.Dtos;
using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using System.Data;

namespace MoneyTracker.Application.Transactions.SearchTransaction;

internal sealed class SearchTransactionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<SearchTransactionsQuery, List<TransactionCategoryNameDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<List<TransactionCategoryNameDto>>> Handle(SearchTransactionsQuery request, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT 
                t.id AS Id,         
                t.note AS Description,                
                t.amount AS Amount, 
                t.date AS Date,
                c.id AS Id,
                c.type AS TypeId,
                c.title AS Title
            FROM transactions t
            INNER JOIN categories c ON c.id = t.category_id
            WHERE t.user_id = @UserId
            AND t.date = @Date
            """;

        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        IEnumerable<TransactionCategoryNameDto> transactions = await connection
            .QueryAsync<TransactionCategoryNameDto, CategoryNameAndTypeDto, TransactionCategoryNameDto>(
              sql,
              (transaction, category) =>
              {
                  return new TransactionCategoryNameDto(
                      Id: transaction.Id,
                      Description: transaction.Description,
                      Amount: transaction.Amount,
                      Date: transaction.Date,
                      Category: new CategoryNameAndTypeDto(
                          Id: category.Id,
                          TypeId: category.TypeId,
                          Title: category.Title,
                          Type: CategoryType.From(category.TypeId).Name));
              },
              param: new { request.UserId, request.Date },
              splitOn: "Id,Id");

        return transactions.ToList();
    }
}