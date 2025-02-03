using ApiProject.Shared.Users;
using FluentResults;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiProject.Client.Web.Users;

public class AuthStateProvider(
    IMemoryCache _memoryCache,
    AuthService _authService
    ) : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> defaultAuthenticationStateTask =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    private Task<AuthenticationState> authenticationStateTask = defaultAuthenticationStateTask;

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        if (_memoryCache.TryGetValue("Token", out string? token))
        {
            JwtSecurityToken jwt = new(token);

            authenticationStateTask = Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
                    claims: jwt.Claims,
                    authenticationType: nameof(AuthenticationStateProvider)
                    ))));
        }
        else
            authenticationStateTask = defaultAuthenticationStateTask;

        NotifyAuthenticationStateChanged(authenticationStateTask);

        return await authenticationStateTask;
    }

    public async Task<Result> RegisterAsync(RegisterUserRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (result.IsFailed)
            return Result.Fail(result.Errors);

        _memoryCache.Set("Token", result.Value);
        await GetAuthenticationStateAsync();
        return Result.Ok();
    }
}
