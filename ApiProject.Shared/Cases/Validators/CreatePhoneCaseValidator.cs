using ApiProject.Shared.Cases.Request;
using FluentValidation;

namespace ApiProject.Shared.Cases.Validators;
public class CreatePhoneCaseRequestValidator : AbstractValidator<CreatePhoneCaseCommand>
{
    public CreatePhoneCaseRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(r => r.Model)
            .NotNull();

        RuleFor(r => r.ImgUrl)
            .NotEmpty();

        RuleFor(r => r.Color)
            .NotEmpty()
            .Length(1, 50);

        RuleFor(r => r.Price)
            .NotEmpty()
            .InclusiveBetween(1, 1000000);

        RuleFor(r => r.Stock)
            .NotNull();
    }
}
