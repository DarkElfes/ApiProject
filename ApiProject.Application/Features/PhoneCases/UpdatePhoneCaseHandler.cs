using ApiProject.Domain.PhoneCases;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Cases.Request;
using ApiProject.Shared.Cases.Response;
using Mapster;

namespace ApiProject.Application.Features.PhoneCases;
internal sealed class UpdatePhoneCaseHandler(
    IPhoneCaseRepository _phoneCaseRepository
    ) : IRequestHandler<UpdatePhoneCaseCommand, Result<PhoneCaseResponse>>
{
    public async Task<Result<PhoneCaseResponse>> Handle(UpdatePhoneCaseCommand command, CancellationToken cancellationToken)
    {
        var phoneCase = await _phoneCaseRepository.GetByIdAsync(command.Id);

        if (phoneCase == null)
        {
            return PhoneCaseErrors.NotFound;
        }
        phoneCase = command.Adapt<PhoneCase>();

        _phoneCaseRepository.Update(phoneCase);
        await _phoneCaseRepository.SaveChangesAsync();

        return Result.Success(phoneCase.Adapt<PhoneCaseResponse>());
    }
}
