using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Application.Features.Users;
using ApiProject.Shared.Abstractions.Result;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Oauth2.v2;
using Microsoft.Extensions.Configuration;

namespace ApiProject.Infrastructure.Authentication;
internal sealed class GoogleAuthService(
    IConfiguration _config
    ) : IGoogleAuthService
{
    private readonly string _redirectUrl = _config["OIDC:Google:RedirectUri"] ??
        throw new ArgumentNullException("Redirect url not found");


    public string GetGoogleAuthUrl()
       => GetFlow()
            .CreateAuthorizationCodeRequest(_redirectUrl)
            .Build()
            .ToString();

    public async Task<Result<GoogleJsonWebSignature.Payload>> ExchangeCodeForToken(string code)
    {
        try
        {
            var token = await GetFlow()
                .ExchangeCodeForTokenAsync("user", code, _redirectUrl, CancellationToken.None);

            return await GoogleJsonWebSignature.ValidateAsync(token.IdToken);
        }
        catch (TokenResponseException ex)
        {
            switch (ex.Error.Error)
            {
                case "invalid_grant":
                    return UserErrors.Login.InvalidGrant;
                case "invalid_request":
                    return UserErrors.Login.InvalidRequest;
                default:
                    throw;
            }
        }
    }


    private GoogleAuthorizationCodeFlow GetFlow()
        => new(new GoogleAuthorizationCodeFlow.Initializer()
        {
            ClientSecrets = new ClientSecrets()
            {
                ClientId = _config["OIDC:Google:ClientId"],
                ClientSecret = _config["OIDC:Google:ClientSecret"]
            },
            Scopes = [
                    Oauth2Service.Scope.Openid,
                    Oauth2Service.Scope.UserinfoEmail,
                    Oauth2Service.Scope.UserinfoProfile,
            ]
        });
}
