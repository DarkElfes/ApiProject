using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.Users;
using ApiProject.Shared.Abstractions.Result;
using ApiProject.Shared.Users.Commands;
using ApiProject.Shared.Users.Response;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace ApiProject.Application.Features.Users;

internal sealed class RegisterUserHandler(
    IUserRepository _userRepository,
    ITokenProvider _tokenProvider
    ) : IRequestHandler<RegisterUserCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if(await _userRepository.GetByEmailAsync(request.Email) is not null)
        {
            return UserErrors.Register.AlreadyExist;
        }

        User user = request.Adapt<User>();
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
        user.Role = "User";

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return new AuthResponse(_tokenProvider.Create(user));
    }
}