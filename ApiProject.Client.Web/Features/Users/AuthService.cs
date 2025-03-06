using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Response;
using Blazored.LocalStorage;
using FluentResults;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace ApiProject.Client.Web.Features.Users;

public class AuthService(
    HttpClient _httpClient,
    ILocalStorageService _localStorage,
    AuthenticationStateProvider _authStateProvider
    )
{
    public async Task SignOutAsync()
    {
        await _localStorage.RemoveItemAsync("Auth");
        await _authStateProvider.GetAuthenticationStateAsync();
    }

    public async Task<Result> LoginAsync(LoginUserCommand request)
        => await HandleResponse(_httpClient.PostAsJsonAsync("login", request));

    public async Task<Result> RegisterAsync(RegisterUserCommand request)
        => await HandleResponse(_httpClient.PostAsJsonAsync("register", request));


    private async Task<Result> HandleResponse(Task<HttpResponseMessage> request)
    {
        var response = await request;

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<Dictionary<string, object?>>();
            return Result.Fail(problemDetails?["detail"]?.ToString() ?? "Unhandled error");
        }

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
        await _localStorage.SetItemAsync("Auth", authResponse);
        await _authStateProvider.GetAuthenticationStateAsync();
        return Result.Ok();

    }
}
