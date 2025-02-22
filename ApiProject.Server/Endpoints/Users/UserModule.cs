using ApiProject.Server.Exstensions;
using ApiProject.Shared.Users.Commands;
using Carter;
using MediatR;
using System.Security.Claims;

namespace ApiProject.Server.Endpoints.Users;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var authGroup = app.MapGroup("/auth");

        authGroup.MapPost("/login", async (LoginUserCommand request, ISender sender)
            => (await sender.Send(request)).Match()
            )
            .WithName("LoginUser")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        authGroup.MapPost("/register", async (RegisterUserCommand request, ISender sender)
            => (await sender.Send(request)).Match()
            )
            .WithName("RegisterUser")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        app.MapPost("/update", async (UpdateUserCommand request, HttpContext context, ISender sender) =>
        {
            request.UserId = int.Parse(context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value!);
            var result = await sender.Send(request);

            return result.Match();
        })
         .RequireAuthorization();
    }
}
