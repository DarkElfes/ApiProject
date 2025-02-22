using ApiProject.Shared.Users.Response;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ApiProject.Client.Web.Users;

public class AuthStateProvider(
    ILocalStorageService _localStorage
    ) : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> defaultAuthenticationStateTask =
        Task.FromResult(new AuthenticationState(new()));

    private Task<AuthenticationState> authenticationStateTask = defaultAuthenticationStateTask;

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        authenticationStateTask = defaultAuthenticationStateTask;

        if (await _localStorage.GetItemAsync<AuthResponse>("Auth") is AuthResponse auth)
        {
            var jwt = new JsonWebTokenHandler().ReadJsonWebToken(auth.Token);

            if (jwt.ValidTo > DateTime.UtcNow)
            {
                authenticationStateTask = Task.FromResult(
                    new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
                        claims: jwt.Claims,
                        authenticationType: nameof(AuthenticationStateProvider)
                        ))));
            }
        }

        NotifyAuthenticationStateChanged(authenticationStateTask);

        return await authenticationStateTask;
    }
}
