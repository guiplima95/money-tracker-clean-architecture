using Dapper;
using MoneyTracker.Application.Abstractions.Data;
using MoneyTracker.Application.Abstractions.Messaging;
using MoneyTracker.Application.Categories.Dtos;
using MoneyTracker.Domain.Abstractions;
using MoneyTracker.Domain.Categories.CategoryAggragate;
using System.Data;

namespace MoneyTracker.Application.Categories.GetCategories;

internal sealed class GetCategoriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetCategoriesQuery, IReadOnlyList<CategoryNameAndTypeDto>>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public async Task<Result<IReadOnlyList<CategoryNameAndTypeDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT 
                id AS Id,
                type AS TypeId,
                title AS Title             
            FROM categories
            WHERE user_id = @UserId
            """;

        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();

        IEnumerable<CategoryNameAndTypeDto> categories = await connection.QueryAsync<CategoryNameAndTypeDto>(
            sql, new
            {
                request.UserId
            });

        return categories
            .Select(c => new CategoryNameAndTypeDto(
                Id: c.Id, TypeId: c.TypeId,
                Title: c.Title, Type: CategoryType.From(c.TypeId).Name))
            .ToList();
    }
}
