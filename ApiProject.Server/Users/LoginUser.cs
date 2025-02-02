using ApiProject.Server.Data;
using Carter;
using FluentResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ApiProject.Server.Users;

public static class LoginUser
{
    public class Command : IRequest<Result<string>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal sealed class Handler(
        AppDbContext _appDbContext,
        TokenProvider _tokenProvider
        ) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Email.Equals(request.Email));

            if (user is null)
                return Result.Fail("User with this email not found");
            else if (new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, request.Password)
                .Equals(PasswordVerificationResult.Failed))
                return Result.Fail("Incorrect password");

            return Result.Ok(_tokenProvider.Create(user));
        }
    }
}

public class LoginUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async (LoginUserRequest request, ISender sender) =>
        {
            var command = request.Adapt<LoginUser.Command>();

            var result = await sender.Send(command);

            if (result.IsFailed)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });
    }
}
