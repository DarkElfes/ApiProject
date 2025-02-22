using ApiProject.Domain.PhoneCases;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Cases.Request;
using ApiProject.Shared.Cases.Response;
using Mapster;

namespace ApiProject.Application.Features.PhoneCases;

internal sealed class CreatePhoneCaseHandler(
    IPhoneCaseRepository _phoneCaseRepository
    ) : IRequestHandler<CreatePhoneCaseCommand, Result<PhoneCaseResponse>>
{
    public async Task<Result<PhoneCaseResponse>> Handle(CreatePhoneCaseCommand request, CancellationToken cancellationToken)
    {
        var phoneCase = request.Adapt<PhoneCase>();

        await _phoneCaseRepository.AddAsync(phoneCase);
        await _phoneCaseRepository.SaveChangesAsync();

        return Result.Success(phoneCase.Adapt<PhoneCaseResponse>());
    }
}
