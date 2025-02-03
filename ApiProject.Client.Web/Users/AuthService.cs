using ApiProject.Shared.Users;
using FluentResults;
using System.Net.Http.Json;

namespace ApiProject.Client.Web.Users;

public class AuthService(
    HttpClient _httpClient
    )
{
    public async Task LoginAsync(LoginUserRequest request)
    {
        
    }

    public async Task<Result<string>> RegisterAsync(RegisterUserRequest register)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/register", register);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return Result.Fail(content);

        return Result.Ok(content);
    }
}
