using ApiProject.Domain.PhoneCases;
using ApiProject.Shared.Cases.Request;

namespace ApiProject.Application.Features.PhoneCases;
internal sealed class RemovePhoneCaseHandler(
    IPhoneCaseRepository _phoneCaseRepository
    ) : IRequestHandler<RemovePhoneCaseCommand, Result>
{
    public async Task<Result> Handle(RemovePhoneCaseCommand request, CancellationToken cancellationToken)
    {
        var phoneCase = await _phoneCaseRepository.GetByIdAsync(request.PhoneCaseId);

        if (phoneCase == null)
        {
            return PhoneCaseErrors.NotFound;
        }

        _phoneCaseRepository.Remove(phoneCase);
        await _phoneCaseRepository.SaveChangesAsync();

        return Result.Success();
    }
}
