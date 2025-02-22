using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Users.Response;
using MediatR;

namespace ApiProject.Shared.Users.Commands;

public class LoginUserCommand : IRequest<Result<AuthResponse>>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}