using ApiProject.Domain.Abstractions;

namespace ApiProject.Domain.Users;

public class User : Entity
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? PasswordHash { get; set; }
    public string? IdToken { get; set; }
    public string? RefreshToken { get; set; }
    public string Role { get; set; } = string.Empty;
}
