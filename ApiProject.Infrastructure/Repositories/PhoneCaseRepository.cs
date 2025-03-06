using ApiProject.Domain.PhoneCases;
using ApiProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Infrastructure.Repositories;
internal sealed class PhoneCaseRepository(
    AppDbContext dbContext
    ) : Repository<PhoneCase>(dbContext), IPhoneCaseRepository
{
    static readonly byte s_pageSize = 10;

    public async Task<IEnumerable<PhoneCase>> GetRangeAsync(int pageNumber, int? pageSize)
    {
        return await DbContext.PhoneCases
            .Skip((pageNumber - 1) * 10)
            .Take(pageSize ?? s_pageSize)
            .ToListAsync();
    }
}
