using ApiProject.Domain.Users;

namespace ApiProject.Application.Features.Users;
public static class DeleteUser
{
    public record Command(int UserId) : IRequest<Result>;

    internal sealed class Handler(
        IUserRepository _userRepo
        ) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if(await _userRepo.GetByIdAsync(request.UserId) is not User user)
            {
                return UserErrors.NotFoundById;
            }

            _userRepo.Remove(user);
            await _userRepo.SaveChangesAsync();

            return Result.Success();
        }
    }
}
