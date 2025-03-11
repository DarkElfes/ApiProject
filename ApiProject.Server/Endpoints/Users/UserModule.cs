using ApiProject.Application.Abstractions.Authentication;
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
        // Map base group for user endpoints
        var userGroup = app.MapGroup("user/")
            .WithTags("User");

        // Endpoint to delete user
        userGroup.MapDelete("/delete", async (HttpContext context, ISender sender)
            => (await sender.Send(new DeleteUser.Command(GetUserId(context)))).Match())
            .RequireAuthorization(policy => policy.RequireRole("User"))
            .WithSummary("Delete user")
            .WithDescription("Delete user that request")
            .Produces(StatusCodes.Status200OK);


        // Add a new group for user update endpoints
        var userUpdateGroup = userGroup.MapGroup("/update")
            .WithTags("User Update");

        // Endpoint to update username
        userUpdateGroup.MapPut("/username", async (UpdateUserUsernameRequest request, HttpContext context, ISender sender)
            => (await sender.Send(new UpdateUserUsername.Command(
                UserId: GetUserId(context),
                NewUsername: request.NewUsername))).Match())
            .RequireAuthorization()
            .WithSummary("Update username")
            .WithDescription("Update a user name")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        // Endpoint to update password
        userUpdateGroup.MapPut("/password", async (UpdateUserPasswordRequest request, HttpContext context, ISender sender)
            => (await sender.Send(new UpdateUserPassword.Command(
                UserId: GetUserId(context),
                OldPassword: request.OldPassword,
                NewPassword: request.NewPassword))).Match())
            .RequireAuthorization()
            .WithSummary("Update password")
            .WithDescription("Update a user password")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);



        // Add a new group for auth endpoints
        var userAuthGroup = userGroup.MapGroup("/auth")
            .WithTags("User Authentication");

        // Endpoint for user login
        userAuthGroup.MapPost("/login", async (LoginUserCommand request, ISender sender)
            =>
        {
            var result = await sender.Send(request);

            if (result.Error == UserErrors.Login.PasswordNotSet)
            {
                return Results.StatusCode(303);
            }

            return result.Match();
        })
            .WithSummary("Login user")
            .WithDescription("Basic login user with email and password")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status303SeeOther)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        // Endpoint for user registration
        userAuthGroup.MapPost("/register", async (RegisterUserCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .WithSummary("Register user")
            .WithDescription("Register new user using his username, email and password")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status409Conflict)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        // Endpoint for user login via Google
        userAuthGroup.MapPost("/login/by-google", async (LoginUserByGoogleCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .WithSummary("Login user via Google")
            .WithDescription("Login user using code from Google OpenIDConnect")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        // Endpoint to get Google authentication URL
        userAuthGroup.MapGet("/login/by-google/url", (IGoogleAuthService googleAuthService)
            => Results.Ok(googleAuthService.GetGoogleAuthUrl()))
            .WithSummary("Get Google challenge url")
            .WithDescription("Get url to Google OpenIDConnect challenge")
            .Produces<string>(StatusCodes.Status200OK);
    }

    private static int GetUserId(HttpContext context)
        => int.Parse(context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value!);
}
