namespace ApiProject.Shared.Users;

public record RegisterUserRequest(
    string Username,
    string Email,
    string Password
    );
