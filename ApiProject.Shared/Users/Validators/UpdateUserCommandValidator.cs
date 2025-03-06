using ApiProject.Shared.Users.Commands;
using FluentValidation;

namespace ApiProject.Shared.Users.Validators;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .Length(1, 20);

        RuleFor(r => r.Password)
            .Length(5, 20);
    }
}
