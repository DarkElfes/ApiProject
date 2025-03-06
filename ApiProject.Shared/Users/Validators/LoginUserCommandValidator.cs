using ApiProject.Shared.Users.Commands;
using FluentValidation;

namespace ApiProject.Shared.Users.Validators;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(5, 20);
    }
}
