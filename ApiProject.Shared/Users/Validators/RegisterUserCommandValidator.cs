using ApiProject.Shared.Users.Commands;
using FluentValidation;

namespace ApiProject.Shared.Users.Validators;
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(3, 20);

        Include(new LoginUserCommandValidator());
    }
}
