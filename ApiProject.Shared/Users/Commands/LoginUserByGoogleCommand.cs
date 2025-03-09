using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Users.Response;
using MediatR;

namespace ApiProject.Shared.Users.Commands;
public record LoginUserByGoogleCommand(string Code) : IRequest<Result<AuthResponse>>;
