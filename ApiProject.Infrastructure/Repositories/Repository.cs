using ApiProject.Domain.Abstractions;
using ApiProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Infrastructure.Repositories;
internal abstract class Repository<TEntity>(
    AppDbContext dbContext
    ) where TEntity : Entity
{
    protected readonly AppDbContext DbContext = dbContext;

    public async Task<TEntity?> GetByIdAsync(int id)
        => await DbContext.Set<TEntity>().SingleOrDefaultAsync(c => c.Id.Equals(id));

    public async Task AddAsync(TEntity entity)
        => await DbContext.Set<TEntity>().AddAsync(entity);

    public void Update(TEntity entity)
        => DbContext.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity)
        => DbContext.Set<TEntity>().Remove(entity);

    public async Task SaveChangesAsync()
        => await DbContext.SaveChangesAsync();
}
