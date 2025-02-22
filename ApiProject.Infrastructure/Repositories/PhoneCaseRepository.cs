using ApiProject.Domain.PhoneCases;
using ApiProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Infrastructure.Repositories;
internal sealed class PhoneCaseRepository(
    AppDbContext dbContext
    ) : Repository<PhoneCase>(dbContext), IPhoneCaseRepository
{
    public async Task<IEnumerable<PhoneCase>> GetRangeAsync(short pageNumber, short pageSize = 10)
    {
        return await DbContext.PhoneCases
            .Skip((pageNumber - 1) * 10)
            .Take(pageSize)
            .ToListAsync();
    }
}
