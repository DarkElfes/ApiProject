using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Response;
using Blazored.LocalStorage;
using FluentResults;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Json;

namespace ApiProject.Client.Web.Features.Users;

public class AuthService(
    HttpClient _httpClient,
    ILocalStorageService _localStorage,
    NavigationManager _navigation,
    ISnackbar _snackbar,
    AuthenticationStateProvider _authStateProvider
    )
{
    public async Task SignOutAsync()
    {
        await _localStorage.RemoveItemAsync("Auth");
        await _authStateProvider.GetAuthenticationStateAsync();
        _snackbar.Add("You're sign out");
    }


    // Add basic JWT auth
    public async Task<Result> LoginAsync(LoginUserCommand request)
        => await HandleResponse(_httpClient.PostAsJsonAsync("login", request));
    public async Task<Result> RegisterAsync(RegisterUserCommand request)
        => await HandleResponse(_httpClient.PostAsJsonAsync("register", request));


    // Add OpenID for Google
    public async Task GetChallangeForGoogleAsync()
    {
        var challangeUrl = await _httpClient.GetFromJsonAsync<string>("login/by-google/url");

        if (challangeUrl is null)
        {
            _snackbar.Add("An Server error occurred when try auth by Google. Try later.", Severity.Error);
            return;
        }
        _navigation.NavigateTo(challangeUrl);
    }
    public async Task LoginByGoogleAsync(LoginUserByGoogleCommand request)
        => await HandleResponse(_httpClient.PostAsJsonAsync("login/by-google", request));


    private async Task<Result> HandleResponse(Task<HttpResponseMessage> request)
    {
        var response = await request;

        if (response.StatusCode == System.Net.HttpStatusCode.SeeOther)
        {
            await GetChallangeForGoogleAsync();
            return Result.Fail("You need sign in again");
        }
        else if (!response.IsSuccessStatusCode)
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
