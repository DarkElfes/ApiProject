using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.Users;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Response;
using Microsoft.AspNetCore.Identity;

namespace ApiProject.Application.Features.Users;

internal sealed class LoginUserHandler(
    IUserRepository _userRepository,
    ITokenProvider _tokenProvider
    ) : IRequestHandler<LoginUserCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return UserErrors.Login.NotFoundByEmail;
        }
        else if (new PasswordHasher<User>()
            .VerifyHashedPassword(user, user.PasswordHash, request.Password)
            .Equals(PasswordVerificationResult.Failed))
        {
            return UserErrors.Login.IncorrectPassword;
        }

        return new AuthResponse(_tokenProvider.Create(user));
    }
}
