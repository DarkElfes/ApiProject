using ApiProject.Application.Abstractions.Authentication;
using ApiProject.Domain.Users;
using ApiProject.Shared.Users.Response;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ApiProject.Application.Features.Users;
public class UpdateUserPassword
{
    public record Command(int UserId, string OldPassword, string NewPassword) : IRequest<Result<AuthResponse>>;

    public class UpdateUserPasswordValidator : AbstractValidator<Command>
    {
        public UpdateUserPasswordValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.OldPassword).NotEmpty().Length(6, 20);
            RuleFor(x => x.NewPassword).NotEmpty().Length(6, 20);
        }
    }
        
    internal sealed class Handler(
        IUserRepository _userRepository,
        ITokenProvider _tokenProvider
        ) : IRequestHandler<Command, Result<AuthResponse>>
    {
        public async Task<Result<AuthResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            if(request.OldPassword == request.NewPassword)
            {
                return UserErrors.Update.SamePassword;
            }
            
            if(await _userRepository.GetByIdAsync(request.UserId) is not User user)
            {
                return UserErrors.NotFoundById;
            }
            
            var passwordHasher = new PasswordHasher<User>();

            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.OldPassword)
                .Equals(PasswordVerificationResult.Failed))
            {
                return UserErrors.IncorrectPassword;
            }

            user.PasswordHash = passwordHasher.HashPassword(user, request.NewPassword); ;

                _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new AuthResponse(_tokenProvider.Create(user));
        }
    }
}
