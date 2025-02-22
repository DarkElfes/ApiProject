using ApiProject.Shared.Users.Response;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace ApiProject.Client.Web.Users;
public class AuthHandler(
    ILocalStorageService _localStorage
    ) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var item = await _localStorage.GetItemAsync<AuthResponse>("Auth", cancellationToken);

        if (item is AuthResponse authResponse &&  !string.IsNullOrWhiteSpace(authResponse.Token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
