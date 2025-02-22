using ApiProject.Application.Features.PhoneCases;
using ApiProject.Server.Exstensions;
using ApiProject.Shared.Cases.Request;
using ApiProject.Shared.Cases.Response;
using Carter;
using MediatR;

namespace ApiProject.Server.Endpoints.PhoneCases;

public class PhoneCaseModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var phoneCaseGroup = app.MapGroup("/phone-case");


        phoneCaseGroup.MapGet("/page/{pageNumber}", async (short pageNumber, ISender sender)
            => (await sender.Send(new GetCases.Query { PageNumber = pageNumber})).Match());

        phoneCaseGroup.MapPost("/create", async (CreatePhoneCaseCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .RequireAuthorization(policy => policy.RequireRole("Admin"))
            .Produces<PhoneCaseResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        phoneCaseGroup.MapPost("/update", async (UpdatePhoneCaseCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
