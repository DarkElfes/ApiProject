using ApiProject.Application.Features.Users;
using ApiProject.Server.Exstensions;
using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Requests;
using ApiProject.Shared.Users.Response;
using Carter;
using MediatR;
using System.Security.Claims;

namespace ApiProject.Server.Endpoints.Users;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Add a new group for auth endpoints
        var userAuthGroup = app.MapGroup("/auth");

        userAuthGroup.MapPost("/login", async (LoginUserCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .WithName("LoginUser")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        userAuthGroup.MapPost("/register", async (RegisterUserCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .WithName("RegisterUser")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status409Conflict) 
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);


        // Add a new group for user endpoints
        var userUpdateGroup = app.MapGroup("/user/update");

        userUpdateGroup.MapPut("/username", async (UpdateUserUsernameRequest request, HttpContext context, ISender sender)
            => (await sender.Send(new UpdateUserUsername.Command(
                UserId: GetUserId(context),
                NewUsername: request.NewUsername))).Match())
            .RequireAuthorization();

        userUpdateGroup.MapPut("/password", async (UpdateUserPasswordRequest request, HttpContext context, ISender sender)
            => (await sender.Send(new UpdateUserPassword.Command(
                UserId: GetUserId(context),
                OldPassword: request.OldPassword,
                NewPassword: request.NewPassword))).Match())
            .RequireAuthorization();
    }

    private static int GetUserId(HttpContext context)
        => int.Parse(context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value!);
}
