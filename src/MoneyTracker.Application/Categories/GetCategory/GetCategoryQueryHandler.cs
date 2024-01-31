using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Categories.Dtos;
using MoneyTracker.Domain.Abstractions;
using System.Data;

namespace MoneyTracker.Application.Categories.GetCategory;

internal sealed class GetCategoryQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetCategoryQuery, CategoryDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<CategoryDto>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
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

        CategoryDto? category = await connection.QueryFirstOrDefaultAsync<CategoryDto>(
            sql, new
            {
                request.Id
            });


        return category;
    }
}
