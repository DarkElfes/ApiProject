namespace ApiProject.Shared.Users.Commands;

public class RegisterUserCommand : LoginUserCommand
{
    public string Username { get; set; } = null!;
}
