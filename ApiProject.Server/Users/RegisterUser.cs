using ApiProject.Server.Data;
using ApiProject.Shared.Users;
using Carter;
using FluentResults;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Server.Users;

public static class RegisterUser
{
    public class Command : IRequest<Result<string>>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    internal sealed class Handler(
        AppDbContext _dbContext,
        TokenProvider _tokenProvider
        ) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken);

            if (user != null)
                return Result.Fail("User with this email already exist");

            user = request.Adapt<User>();
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(_tokenProvider.Create(user));
        }
    }
}

public class RegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async (RegisterUserRequest request, ISender sender) =>
        {
            var command = request.Adapt<RegisterUser.Command>();

            var result = await sender.Send(command);

            if (result.IsFailed)
                return Results.BadRequest(result.Errors);

            return Results.Ok(result.Value);
        });
    }
}
