using Bogus;
using Dapper;
using MoneyTracker.Application.Abstractions.Data;

namespace MoneyTracker.API.Extensions;

public static class SeedDataExtensions
{
    public static void SeedData(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

        using System.Data.IDbConnection connection = sqlConnectionFactory.CreateConnection();

        Faker faker = new();

        List<object> users = [];
        List<object> categories = [];
        List<object> transactions = [];

        for (int i = 0; i < 5; i++)
        {
            Guid userId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();

            string firstName = $"user-{i}";
            string lastName = $"name-{i}";

            users.Add(new
            {
                Id = userId,
                FirstName = firstName,
                LastName = lastName,
                Email = faker.Internet.Email(firstName, lastName, "gmail", null),
                IdentntyId = Guid.NewGuid().ToString(),
            });

            categories.Add(new
            {
                Id = categoryId,
                Title = faker.Commerce.Categories(i),
                Type = faker.Random.Int(1, 2),
                UserId = userId
            });
        }

        for (int i = 0; i < 100; i++)
        {
            Guid userId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();

            int index = faker.Random.Int(0, 2);

            object category = categories[index];
            object user = users[index];

            string? categoryObject = category?.GetType()?.GetProperty("Id")?.GetValue(category, null)?.ToString();
            if (categoryObject is not null)
            {
                categoryId = Guid.Parse(categoryObject.ToString());
            }
            string? userObject = user?.GetType()?.GetProperty("Id")?.GetValue(user, null)?.ToString();
            if (userObject is not null)
            {
                userId = Guid.Parse(userObject.ToString());
            }

            transactions.Add(new
            {
                Id = Guid.NewGuid(),
                Amount = decimal.Parse(faker.Commerce.Price()),
                Note = faker.Commerce.ProductDescription(),
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = categoryId,
                UserId = userId
            });
        }

        const string sqlUsers = """
            INSERT INTO public.users
            (id, first_name, last_name, email, identity_id)
            VALUES(@Id, @FirstName, @LastName, @Email, @IdentityId)
            """;

        connection.Execute(sqlUsers, users);

        const string sqlCategories = """
            INSERT INTO public.categories
            (id, title, type, user_id)
            VALUES(@Id, @Title, @Type, @UserId)
            """;

        connection.Execute(sqlCategories, categories);

        const string sqlTransactions = """
            INSERT INTO public.transactions
            (id, amount, note, date, category_id, user_id)
            VALUES(@Id, @Amount, @Note, @Date, @CategoryId, @UserId)
            """;

        connection.Execute(sqlTransactions, transactions);
    }
}
