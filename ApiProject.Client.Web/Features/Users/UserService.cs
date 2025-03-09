using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ApiProject.Client.Web.Features.Users;

public class UserService(
    HttpClient _httpClient,
    AuthService _authService,
    NavigationManager _navigation,
    ISnackbar _snackbar
    )
{
    public async Task DeleteUserAsync()
    {
        var result = await _httpClient.DeleteAsync("delete");

        if(result.IsSuccessStatusCode)
        {
            _snackbar.Add("User deleted", Severity.Success);
            await _authService.SignOutAsync();
            _navigation.NavigateTo("/");
        }
        else
        {
            _snackbar.Add("Failed to delete user", Severity.Error);
        }
    }
}
