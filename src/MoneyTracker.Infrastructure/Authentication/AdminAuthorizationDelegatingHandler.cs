using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MoneyTracker.Infrastructure.Authentication.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MoneyTracker.Infrastructure.Authentication;

public class AdminAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly KeycloakOptions _keyCloackOptions;

    public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keyCloackOptions)
    {
        _keyCloackOptions = keyCloackOptions.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
      HttpRequestMessage request,
      CancellationToken cancellationToken)
    {
        AuthorizationToken authorizationToken = await GetAuthorizationTokenAsync(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue(
          JwtBearerDefaults.AuthenticationScheme,
          authorizationToken.AccessToken
        );

        HttpResponseMessage httpResponseMessage = await base.SendAsync(request, cancellationToken);
        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage;
    }

    private async Task<AuthorizationToken> GetAuthorizationTokenAsync(CancellationToken cancellationToken)
    {
        KeyValuePair<string, string>[] authorizationTokenRequestParameters =
        [
            new("client_id", _keyCloackOptions.AdminClientId),
            new("client_secret", _keyCloackOptions.AdminClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
        ];

        FormUrlEncodedContent authorizationRequestContent = new(authorizationTokenRequestParameters);

        HttpRequestMessage authorizationRequest = new(
          HttpMethod.Post,
          new Uri("http://moneytracker-idp:8080/auth/realms/moneytracker/protocol/openid-connect/token"))
        {
            Content = authorizationRequestContent
        };

        HttpResponseMessage authorizationResponse = await base
            .SendAsync(authorizationRequest, cancellationToken);

        authorizationResponse.EnsureSuccessStatusCode();

        return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>(
          cancellationToken: cancellationToken) ?? throw new ApplicationException();
    }
}