using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.Users;
using ApiProject.Shared.Users.Response;
using FluentValidation;

namespace ApiProject.Application.Features.Users;

public class UpdateUserUsername
{
    public record Command(int UserId, string NewUsername) : IRequest<Result<AuthResponse>>;

    public class UpdateUserUserNameValidator : AbstractValidator<Command>
    {
        public UpdateUserUserNameValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.NewUsername).NotEmpty().Length(3, 20);
        }
    }

    internal sealed class UpdateUserHandler(
        IUserRepository _userRepository,
        ITokenProvider _tokenProvider
        ) : IRequestHandler<Command, Result<AuthResponse>>
    {
        public async Task<Result<AuthResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            if(await _userRepository.GetByIdAsync(request.UserId) is not User user)
            {
                return UserErrors.Update.NotFoundById;
            }
            else if(request.NewUsername == user.Username)
            {
                return UserErrors.Update.SameUsername;
            }

            user.Username = request.NewUsername;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new AuthResponse(_tokenProvider.Create(user));
        }
    }
}
