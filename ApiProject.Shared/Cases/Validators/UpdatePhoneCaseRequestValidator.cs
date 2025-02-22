using ApiProject.Shared.Cases.Request;
using FluentValidation;

namespace ApiProject.Shared.Cases.Validators;
public class UpdatePhoneCaseRequestValidator : AbstractValidator<UpdatePhoneCaseCommand>
{
    public UpdatePhoneCaseRequestValidator()
    {
        Include(new CreatePhoneCaseRequestValidator());

        RuleFor(r => r.Id)
            .NotEmpty();

        RuleFor(r => r.Sold)
            .NotEmpty();
    }
}
