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
        var phoneCaseGroup = app.MapGroup("/phone-case")
            .WithTags("Phone cases");

        phoneCaseGroup.MapGet("/page/{pageNumber?}", async (int? pageNumber, int? pageSize, ISender sender)
            => (await sender.Send(new GetPage.Query { PageNumber = pageNumber ?? 1, PageSize = pageSize })).Match())
            .RequireAuthorization(policy => policy.RequireRole("Admin", "User"))
            .WithSummary("Get phone cases")
            .WithDescription("Get a list of phone cases")
            .Produces<PhoneCaseResponse[]>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        phoneCaseGroup.MapPost("/create", async (CreatePhoneCaseCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .RequireAuthorization(policy => policy.RequireRole("Admin"))
            .WithSummary("Create phone case")
            .WithDescription("Create a new phone case")
            .Produces<PhoneCaseResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);

        phoneCaseGroup.MapPut("/update", async (UpdatePhoneCaseCommand request, ISender sender)
            => (await sender.Send(request)).Match())
            .RequireAuthorization(policy => policy.RequireRole("Admin"))
            .WithSummary("Update phone case")
            .WithDescription("Update a phone case")
            .Produces<PhoneCaseResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden);
    }
}
