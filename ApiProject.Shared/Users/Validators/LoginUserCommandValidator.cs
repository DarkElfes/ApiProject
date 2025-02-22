using ApiProject.Shared.Users.Commands;
using FluentValidation;

namespace ApiProject.Shared.Users.Validators;
public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(r => r.Password)
            .NotEmpty()
            .Length(5, 20);
    }
}
