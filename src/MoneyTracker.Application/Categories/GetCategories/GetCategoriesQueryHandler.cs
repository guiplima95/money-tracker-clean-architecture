using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Domain.Abstractions;
using System.Data;

namespace MoneyTracker.Application.Categories.GetCategories;

internal sealed class GetCategoriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetCategoriesQuery, IReadOnlyList<CategoryResponse>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<CategoryResponse>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                id AS Id,
                title AS Name, 
                icon AS Icon,
                type AS Type
            FROM categories
            WHERE user_id = @UserId
            """;

        IEnumerable<CategoryResponse> categories = await connection.QueryAsync<CategoryResponse>(
            sql, new
            {
                request.UserId
            });


        return categories.ToList();
    }
}
