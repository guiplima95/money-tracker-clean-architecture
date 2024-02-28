using MoneyTracker.Application.Abstractions.Authentication;
using MoneyTracker.Domain.Users.UserAggregate;
using MoneyTracker.Infrastructure.Authentication.Models;
using System.Net.Http.Json;

namespace MoneyTracker.Infrastructure.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private const string PasswordCredentialType = "password";
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterAsync(User user, string password, CancellationToken cancellationToken = default)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);

        userRepresentationModel.Credentials =
        [
            new()
            {
                Value = password,
                Temporary = false,
                Type = PasswordCredentialType,
            }
        ];

        var response = await _httpClient.PostAsJsonAsync(
            "users",
            userRepresentationModel,
            cancellationToken);

        return ExtractIdentityIdFromLocationHeader(response);
    }

    private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage httpResponseMessage)
    {
        const string usersSegmentName = "/users";
        string? locationHeader = (httpResponseMessage.Headers.Location?.PathAndQuery) ?? throw new InvalidOperationException("Location header can't be null");

        int userSegmentValueIndex = locationHeader.IndexOf(
            usersSegmentName, StringComparison.InvariantCultureIgnoreCase);

        string userIdentityId = locationHeader.Substring(
            userSegmentValueIndex + usersSegmentName.Length);

        return userIdentityId;
    }
}
