using ApiProject.Domain.PhoneCases;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Cases.Response;
using Mapster;

namespace ApiProject.Application.Features.PhoneCases;

public class GetCases
{
    public class Query : IRequest<Result<PhoneCaseResponse[]>>
    {
        public short PageNumber { get; set; }
    }

    internal sealed class Handler(
        IPhoneCaseRepository _phoneCaseRespository
        ) : IRequestHandler<Query, Result<PhoneCaseResponse[]>>
    {
        public async Task<Result<PhoneCaseResponse[]>> Handle(Query request, CancellationToken cancellationToken)
        {
            var phoneCaseArr = await _phoneCaseRespository.GetRangeAsync(request.PageNumber);

            return Result.Success(phoneCaseArr.Adapt<PhoneCaseResponse[]>());
        }
    }
}