using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.Users;
using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Response;
using FluentValidation;

namespace ApiProject.Application.Features.Users;
public static class LoginUserByGoogle
{
    public class LoginUserByGoogleValidator : AbstractValidator<LoginUserByGoogleCommand>
    {
        public LoginUserByGoogleValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }

    internal sealed class Handler(
        IGoogleAuthService _googleAuthService,
        IUserRepository _userRepository,
        ITokenProvider _tokenProvider
        ) : IRequestHandler<LoginUserByGoogleCommand, Result<AuthResponse>>
    {

        public async Task<Result<AuthResponse>> Handle(LoginUserByGoogleCommand request, CancellationToken cancellationToken)
        {
            var result = await _googleAuthService.ExchangeCodeForToken(request.Code);

            if (result.IsFailure)
            {
                return result.Error;
            }

            var payload = result.Value;
            var user = await _userRepository.GetByEmailAsync(payload.Email);

            if (user is null)
            {
                user = new User()
                {
                    Username = payload.Name,
                    Email = payload.Email,
                    Role = "User"
                };
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }

            return new AuthResponse(_tokenProvider.Create(user));
        }
    }
}
