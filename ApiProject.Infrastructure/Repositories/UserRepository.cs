using ApiProject.Domain.Users;
using ApiProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Infrastructure.Repositories;

internal sealed class UserRepository(
    AppDbContext dbContext
    ) : Repository<User>(dbContext), IUserRepository
{
    public Task<User?> GetByEmailAsync(string email)
        => DbContext.Users
            .SingleOrDefaultAsync(u => u.Email.Equals(email));
}
