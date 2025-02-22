using ApiProject.Shared.Users.Commands;
using FluentValidation;

namespace ApiProject.Shared.Users.Validators;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        Include(new LoginUserCommandValidator());

        RuleFor(r => r.Username)
            .NotEmpty()
            .Length(1, 20);
    }
}
