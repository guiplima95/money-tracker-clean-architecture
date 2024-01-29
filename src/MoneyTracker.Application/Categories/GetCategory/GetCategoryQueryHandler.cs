using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Domain.Abstractions;
using System.Data;

namespace MoneyTracker.Application.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetCategoryQuery, CategoryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                id AS Id,
                title AS Name, 
                icon AS Icon,
                type AS Type
            FROM categories
            WHERE id = @Id
            """;

        CategoryResponse? category = await connection.QueryFirstOrDefaultAsync<CategoryResponse>(
            sql, new
            {
                request.Id
            });


        return category;
    }
}
