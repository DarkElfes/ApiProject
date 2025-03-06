using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Users.Response;
using MediatR;

namespace ApiProject.Shared.Users.Commands;
public class UpdateUserCommand : IRequest<Result<AuthResponse>>
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string? Password { get; set; }
}
