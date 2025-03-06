using ApiProject.Domain.PhoneCases;
using ApiProject.Shared.Cases.Response;
using FluentValidation;
using Mapster;

namespace ApiProject.Application.Features.PhoneCases;

public class GetPage
{
    public class Query : IRequest<Result<PhoneCaseResponse[]>>
    {
        public int PageNumber { get; set; }
        public int? PageSize { get; set; }
    }

    public class GetPageValidator : AbstractValidator<Query>
    {
        public GetPageValidator()
        {
            RuleFor(x => x.PageNumber).NotEmpty();
            RuleFor(x => x.PageSize).InclusiveBetween(1, 50);
        }
    }

    internal sealed class Handler(
        IPhoneCaseRepository _phoneCaseRespository
        ) : IRequestHandler<Query, Result<PhoneCaseResponse[]>>
    {
        public async Task<Result<PhoneCaseResponse[]>> Handle(Query request, CancellationToken cancellationToken)
        {
            var phoneCaseArr = await _phoneCaseRespository.GetRangeAsync(request.PageNumber, request.PageSize);

            return Result.Success(phoneCaseArr.Adapt<PhoneCaseResponse[]>());
        }
    }
}