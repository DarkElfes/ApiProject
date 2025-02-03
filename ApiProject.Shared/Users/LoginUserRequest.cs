namespace ApiProject.Shared.Users;

public record LoginUserRequest(
    string Email,
    string Password
    );