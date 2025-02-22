using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.Users;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Response;
using Microsoft.AspNetCore.Identity;

namespace ApiProject.Application.Features.Users;

internal sealed class UpdateUserHandler(
    IUserRepository _userRepository,
    ITokenProvider _tokenProvider
    ) : IRequestHandler<UpdateUserCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            return UserErrors.Update.NotFoundById;
        }

        if (!string.IsNullOrWhiteSpace(request.Username))
        {
            user.Username = request.Username;
        }

        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
        }

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse(_tokenProvider.Create(user));
    }
}